using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.UI.Components
{
    public class Timer : IComponent
    {
        public SimpleLabel BigTextLabel { get; set; }
        public SimpleLabel SmallTextLabel { get; set; }
        protected SimpleLabel BigMeasureLabel { get; set; }
        protected ITimeFormatter Formatter { get; set; }

        protected TimeAccuracy CurrentAccuracy { get; set; }

        public GraphicsCache Cache { get; set; }

        public TimerSettings Settings { get; set; }
        public float ActualWidth { get; set; }

        public string ComponentName
        {
            get { return "Timer"; }
        }

        public float VerticalHeight
        {
            get { return Settings.TimerHeight; }
        }

        public float MinimumWidth
        {
            get { return 20; }
        }

        public float HorizontalWidth
        {
            get { return Settings.TimerWidth; }
        }

        public float MinimumHeight
        {
            get { return 20; }
        }

        public float PaddingTop { get { return 0f; } }
        public float PaddingLeft { get { return 7f; } }
        public float PaddingBottom { get { return 0f; } }
        public float PaddingRight { get { return 7f; } }

        public IDictionary<string, Action> ContextMenuControls
        {
            get { return null; }
        }

        public Timer()
        {
            BigTextLabel = new SimpleLabel()
            {
                HorizontalAlignment = StringAlignment.Far,
                VerticalAlignment = StringAlignment.Near,
                Width = 493,
                Text = "0",
            };

            SmallTextLabel = new SimpleLabel()
            {
                HorizontalAlignment = StringAlignment.Near,
                VerticalAlignment = StringAlignment.Near,
                Width = 257,
                Text = "0",
            };


            BigMeasureLabel = new SimpleLabel()
            {
                Text = "88:88:88",
                IsMonospaced = true
            };

            Formatter = new ShortTimeFormatter();
            Settings = new TimerSettings();
            CurrentAccuracy = Settings.TimerAccuracy;
            Cache = new GraphicsCache();
        }

        private void DrawGeneral(Graphics g, LiveSplitState state, float width, float height)
        {
            if (Settings.BackgroundColor.ToArgb() != Color.Transparent.ToArgb()
            || Settings.BackgroundGradient != GradientType.Plain
            && Settings.BackgroundColor2.ToArgb() != Color.Transparent.ToArgb())
            {
                var gradientBrush = new LinearGradientBrush(
                            new PointF(0, 0),
                            Settings.BackgroundGradient == GradientType.Horizontal
                            ? new PointF(width, 0)
                            : new PointF(0, height),
                            Settings.BackgroundColor,
                            Settings.BackgroundGradient == GradientType.Plain
                            ? Settings.BackgroundColor
                            : Settings.BackgroundColor2);
                g.FillRectangle(gradientBrush, 0, 0, width, height);
            }

            BigTextLabel.Font = BigMeasureLabel.Font = state.LayoutSettings.TimerFont;
            SmallTextLabel.Font = state.LayoutSettings.TimerDecimalPlacesFont;

            BigMeasureLabel.SetActualWidth(g);
            SmallTextLabel.SetActualWidth(g);

            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            var oldMatrix = g.Transform;
            var unscaledWidth = (float)(Math.Max(10, BigMeasureLabel.ActualWidth + SmallTextLabel.ActualWidth + 11));
            var unscaledHeight = 45f;
            var widthFactor = (width - 14) / (unscaledWidth - 14);
            var heightFactor = height / unscaledHeight;
            var adjustValue = !Settings.CenterTimer ? 7f : 0f;
            var scale = Math.Min(widthFactor, heightFactor);
            g.TranslateTransform(width - adjustValue, height / 2);
            g.ScaleTransform(scale, scale);
            g.TranslateTransform(-unscaledWidth + adjustValue, -0.5f * unscaledHeight);
            if (Settings.CenterTimer)
                g.TranslateTransform((-(width - unscaledWidth * scale) / 2f) / scale, 0);
            DrawUnscaled(g, state, unscaledWidth, unscaledHeight);
            ActualWidth = scale * (SmallTextLabel.ActualWidth + BigTextLabel.ActualWidth);
            g.Transform = oldMatrix;
            if (!state.LayoutSettings.AntiAliasing)
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        }

        public void DrawUnscaled(Graphics g, LiveSplitState state, float width, float height)
        {
            BigTextLabel.ShadowColor = state.LayoutSettings.ShadowsColor;
            SmallTextLabel.ShadowColor = state.LayoutSettings.ShadowsColor;
            if (CurrentAccuracy != Settings.TimerAccuracy)
                CurrentAccuracy = Settings.TimerAccuracy;

            BigTextLabel.HasShadow
                = SmallTextLabel.HasShadow
                = state.LayoutSettings.DropShadows;
            var smallFont = state.LayoutSettings.TimerDecimalPlacesFont;
            var bigFont = state.LayoutSettings.TimerFont;
            var sizeMultiplier = bigFont.Size / ((16f / 2048) * bigFont.FontFamily.GetEmHeight(bigFont.Style));
            var smallSizeMultiplier = smallFont.Size / ((16f / 2048) * bigFont.FontFamily.GetEmHeight(bigFont.Style));
            var ascent = sizeMultiplier * (16f / 2048) * bigFont.FontFamily.GetCellAscent(bigFont.Style);
            var descent = sizeMultiplier * (16f / 2048) * bigFont.FontFamily.GetCellDescent(bigFont.Style);
            var smallAscent = smallSizeMultiplier * (16f / 2048) * smallFont.FontFamily.GetCellAscent(smallFont.Style);
            var shift = (height - ascent - descent)/2f;
            BigTextLabel.X = width - 499 - SmallTextLabel.ActualWidth;
            SmallTextLabel.X = width - SmallTextLabel.ActualWidth - 6;
            BigTextLabel.Y = shift;
            SmallTextLabel.Y = shift + ascent - smallAscent;
            BigTextLabel.Height = 150f;
            SmallTextLabel.Height = 150f;

            BigTextLabel.IsMonospaced = true;
            SmallTextLabel.IsMonospaced = true;

            //TODO: Do it right from the beginning on
            if (Settings.ShowGradient && BigTextLabel.Brush is SolidBrush)
            {
                var originalColor = (BigTextLabel.Brush as SolidBrush).Color;
                double h, s, v;
                originalColor.ToHSV(out h, out s, out v);

                var bottomColor = ColorExtensions.FromHSV(h, s, 0.8 * v);
                var topColor = ColorExtensions.FromHSV(h, 0.5 * s, Math.Min(1, 1.5 * v + 0.1));

                var bigTimerGradiantBrush = new LinearGradientBrush(
                    new PointF(BigTextLabel.X, BigTextLabel.Y),
                    new PointF(BigTextLabel.X, BigTextLabel.Y + ascent + descent),
                    topColor,
                    bottomColor);
                var smallTimerGradiantBrush = new LinearGradientBrush(
                    new PointF(SmallTextLabel.X, SmallTextLabel.Y),
                    new PointF(SmallTextLabel.X, SmallTextLabel.Y + ascent + descent + smallFont.Size - bigFont.Size),
                    topColor,
                    bottomColor);

                BigTextLabel.Brush = bigTimerGradiantBrush;
                SmallTextLabel.Brush = smallTimerGradiantBrush;
            }

            BigTextLabel.Draw(g);
            SmallTextLabel.Draw(g);
        }

        public virtual TimeSpan? GetTime(LiveSplitState state)
        {
            if (state.CurrentPhase == TimerPhase.NotRunning)
                return state.Run.Offset;
            else
                return state.CurrentTime[state.CurrentTimingMethod];
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            DrawGeneral(g, state, width, VerticalHeight);
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            DrawGeneral(g, state, HorizontalWidth, height);
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            Settings.Mode = mode;
            return Settings;
        }

        public void SetSettings(System.Xml.XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        public System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            return Settings.GetSettings(document);
        }


        public void RenameComparison(string oldName, string newName)
        {
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            Cache.Restart();

            var timeValue = GetTime(state);
            if (timeValue != null)
            {
                var timeString = Formatter.Format(timeValue);
                int dotIndex = timeString.IndexOf(".");
                BigTextLabel.Text = timeString.Substring(0, dotIndex);
                if (CurrentAccuracy == TimeAccuracy.Hundredths)
                    SmallTextLabel.Text = timeString.Substring(dotIndex);
                else if (CurrentAccuracy == TimeAccuracy.Tenths)
                    SmallTextLabel.Text = timeString.Substring(dotIndex, 2);
                else
                    SmallTextLabel.Text = "";
            }
            else
            {
                SmallTextLabel.Text = "-";
                BigTextLabel.Text = "";
            }

            if (Settings.OverrideSplitColors)
            {
                BigTextLabel.ForeColor = Settings.TimerColor;
                SmallTextLabel.ForeColor = Settings.TimerColor;
            }
            else if (state.CurrentPhase == TimerPhase.NotRunning || state.CurrentTime[state.CurrentTimingMethod] < TimeSpan.Zero)
            {
                BigTextLabel.ForeColor = state.LayoutSettings.NotRunningColor;
                SmallTextLabel.ForeColor = state.LayoutSettings.NotRunningColor;
            }
            else if (state.CurrentPhase == TimerPhase.Paused)
            {
                BigTextLabel.ForeColor = SmallTextLabel.ForeColor = state.LayoutSettings.PausedColor;
            }
            else if (state.CurrentPhase == TimerPhase.Ended)
            {
                if (state.Run.Last().Comparisons[state.CurrentComparison][state.CurrentTimingMethod] == null || state.CurrentTime[state.CurrentTimingMethod] < state.Run.Last().Comparisons[state.CurrentComparison][state.CurrentTimingMethod])
                {
                    BigTextLabel.ForeColor = state.LayoutSettings.PersonalBestColor;
                    SmallTextLabel.ForeColor = state.LayoutSettings.PersonalBestColor;
                }
                else
                {
                    BigTextLabel.ForeColor = state.LayoutSettings.BehindLosingTimeColor;
                    SmallTextLabel.ForeColor = state.LayoutSettings.BehindLosingTimeColor;
                }
            }
            else if (state.CurrentPhase == TimerPhase.Running)
            {
                Color timerColor;
                if (state.CurrentSplit.Comparisons[state.CurrentComparison][state.CurrentTimingMethod] != null)
                {
                    timerColor = LiveSplitStateHelper.GetSplitColor(state, state.CurrentTime[state.CurrentTimingMethod] - state.CurrentSplit.Comparisons[state.CurrentComparison][state.CurrentTimingMethod], -1, state.CurrentSplitIndex, state.CurrentComparison, state.CurrentTimingMethod).Value;
                }
                else
                    timerColor = state.LayoutSettings.AheadGainingTimeColor;
                BigTextLabel.ForeColor = timerColor;
                SmallTextLabel.ForeColor = timerColor;
            }

            var smallFont = state.LayoutSettings.TimerDecimalPlacesFont;
            var bigFont = state.LayoutSettings.TimerFont;
            var sizeMultiplier = bigFont.Size / ((16f / 2048) * bigFont.FontFamily.GetEmHeight(bigFont.Style));
            var ascent = sizeMultiplier * (16f / 2048) * bigFont.FontFamily.GetCellAscent(bigFont.Style);
            var descent = sizeMultiplier * (16f / 2048) * bigFont.FontFamily.GetCellDescent(bigFont.Style);
            var shift = (height - ascent - descent) / 2f;

            Cache["TimerText"] = BigTextLabel.Text + SmallTextLabel.Text;
            if (BigTextLabel.Brush != null && invalidator != null)
            {
                /*if (BigTextLabel.Brush is LinearGradientBrush)
                    Cache["TimerColor"] = ((LinearGradientBrush)BigTextLabel.Brush).LinearColors.First().ToArgb();
                else*/
                    Cache["TimerColor"] = BigTextLabel.ForeColor.ToArgb();
            }

            if (invalidator != null && Cache.HasChanged)
            {
                invalidator.Invalidate(0, 0, width, height);
            }
        }

        public void Dispose()
        {
        }
    }
}
