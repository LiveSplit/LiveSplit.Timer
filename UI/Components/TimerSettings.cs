using Fetze.WinFormsColor;
using LiveSplit.TimeFormatters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public partial class TimerSettings : UserControl
    {
        public float TimerHeight { get; set; }
        public float TimerWidth { get; set; }

        public float DecimalsSize { get; set; }

        public String TimerFormat { get; set; }

        public LayoutMode Mode { get; set; }

        public Color TimerColor { get; set; }
        public bool OverrideSplitColors { get; set; }

        public bool CenterTimer { get; set; }

        public bool ShowGradient { get; set; }

        public String TimingMethod { get; set; }

        public Color BackgroundColor { get; set; }
        public Color BackgroundColor2 { get; set; }
        public GradientType BackgroundGradient { get; set; }
        public String GradientString
        {
            get { return BackgroundGradient.ToString(); }
            set { BackgroundGradient = (GradientType)Enum.Parse(typeof(GradientType), value); }
        }

        public TimerSettings()
        {
            InitializeComponent();

            TimerWidth = 225;
            TimerHeight = 50;
            TimerFormat = "1.23";
            TimerColor = Color.FromArgb(170, 170, 170);
            OverrideSplitColors = false;
            ShowGradient = true;
            BackgroundColor = Color.Transparent;
            BackgroundColor2 = Color.Transparent;
            BackgroundGradient = GradientType.Plain;
            CenterTimer = false;
            TimingMethod = "Current Timing Method";
            DecimalsSize = 35f;

            this.Load += TimerSettings_Load;

            btnTimerColor.DataBindings.Add("BackColor", this, "TimerColor", false, DataSourceUpdateMode.OnPropertyChanged);
            chkOverrideTimerColors.DataBindings.Add("Checked", this, "OverrideSplitColors", false, DataSourceUpdateMode.OnPropertyChanged);
            chkGradient.DataBindings.Add("Checked", this, "ShowGradient", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbGradientType.SelectedIndexChanged += cmbGradientType_SelectedIndexChanged;
            cmbGradientType.DataBindings.Add("SelectedItem", this, "GradientString", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor1.DataBindings.Add("BackColor", this, "BackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor2.DataBindings.Add("BackColor", this, "BackgroundColor2", false, DataSourceUpdateMode.OnPropertyChanged);
            chkCenterTimer.DataBindings.Add("Checked", this, "CenterTimer", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbTimingMethod.DataBindings.Add("SelectedItem", this, "TimingMethod", false, DataSourceUpdateMode.OnPropertyChanged);

            trkDecimalsSize.DataBindings.Add("Value", this, "DecimalsSize", false, DataSourceUpdateMode.OnPropertyChanged);

            cmbTimingMethod.SelectedIndexChanged += cmbTimingMethod_SelectedIndexChanged;

            cmbTimerFormat.SelectedIndexChanged += cmbTimerFormat_SelectedIndexChanged;
            cmbTimerFormat.DataBindings.Add("SelectedItem", this, "TimerFormat", false, DataSourceUpdateMode.OnPropertyChanged);

            chkOverrideTimerColors.CheckedChanged += chkOverrideTimerColors_CheckedChanged;
        }

        void cmbTimerFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimerFormat = cmbTimerFormat.SelectedItem.ToString();
        }

        void cmbTimingMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimingMethod = cmbTimingMethod.SelectedItem.ToString();
        }

        void chkOverrideTimerColors_CheckedChanged(object sender, EventArgs e)
        {
            label1.Enabled = btnTimerColor.Enabled = chkOverrideTimerColors.Checked;
        }

        void cmbGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnColor1.Visible = cmbGradientType.SelectedItem.ToString() != "Plain";
            btnColor2.DataBindings.Clear();
            btnColor2.DataBindings.Add("BackColor", this, btnColor1.Visible ? "BackgroundColor2" : "BackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            GradientString = cmbGradientType.SelectedItem.ToString();
        }

        void TimerSettings_Load(object sender, EventArgs e)
        {
            chkOverrideTimerColors_CheckedChanged(null, null);

            if (Mode == LayoutMode.Horizontal)
            {
                trkSize.DataBindings.Clear();
                trkSize.Minimum = 50;
                trkSize.Maximum = 500;
                trkSize.DataBindings.Add("Value", this, "TimerWidth", false, DataSourceUpdateMode.OnPropertyChanged);
                lblSize.Text = "Width:";
            }
            else
            {
                trkSize.DataBindings.Clear();
                trkSize.Minimum = 20;
                trkSize.Maximum = 150;
                trkSize.DataBindings.Add("Value", this, "TimerHeight", false, DataSourceUpdateMode.OnPropertyChanged);
                lblSize.Text = "Height:";
            }
        }

        private Color ParseColor(XmlElement colorElement)
        {
            return Color.FromArgb(Int32.Parse(colorElement.InnerText, NumberStyles.HexNumber));
        }

        private XmlElement ToElement(XmlDocument document, Color color, string name)
        {
            var element = document.CreateElement(name);
            element.InnerText = color.ToArgb().ToString("X8");
            return element;
        }

        private T ParseEnum<T>(XmlElement element)
        {
            return (T)Enum.Parse(typeof(T), element.InnerText);
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            Version version;
            if (element["Version"] != null)
                version = Version.Parse(element["Version"].InnerText);
            else
                version = new Version(1, 0, 0, 0);
            TimerHeight = Single.Parse(element["TimerHeight"].InnerText.Replace(',', '.'), CultureInfo.InvariantCulture);
            TimerWidth = Single.Parse(element["TimerWidth"].InnerText.Replace(',', '.'), CultureInfo.InvariantCulture);
            if (version >= new Version(1, 2))
            {
                if (version >= new Version(1, 5))
                {
                    TimerFormat = element["TimerFormat"].InnerText;
                    DecimalsSize = Single.Parse(element["DecimalsSize"].InnerText, CultureInfo.InvariantCulture);
                }
                else
                {
                    var accuracy = ParseEnum<TimeAccuracy>(element["TimerAccuracy"]);
                    if (accuracy == TimeAccuracy.Hundredths)
                        TimerFormat = "1.23";
                    else if (accuracy == TimeAccuracy.Tenths)
                        TimerFormat = "1.2";
                    else
                        TimerFormat = "1";
                    DecimalsSize = 35f;
                }
                TimerColor = ParseColor(element["TimerColor"]);
                if (version >= new Version(1, 3))
                    OverrideSplitColors = Boolean.Parse(element["OverrideSplitColors"].InnerText);
                else
                    OverrideSplitColors = !Boolean.Parse(element["UseSplitColors"].InnerText);
                ShowGradient = Boolean.Parse(element["ShowGradient"].InnerText);
            }
            else
            {
                TimerFormat = "1.23";
                TimerColor = Color.FromArgb(170, 170, 170);
                OverrideSplitColors = false;
                ShowGradient = true;
            }
            if (version >= new Version(1, 3))
            {
                BackgroundColor = ParseColor(element["BackgroundColor"]);
                BackgroundColor2 = ParseColor(element["BackgroundColor2"]);
                GradientString = element["BackgroundGradient"].InnerText;
                CenterTimer = Boolean.Parse(element["CenterTimer"].InnerText);
            }
            else
            {
                BackgroundColor = Color.Transparent;
                BackgroundColor2 = Color.Transparent;
                BackgroundGradient = GradientType.Plain;
            }
            if (version >= new Version(1, 4))
            {
                TimingMethod = element["TimingMethod"].InnerText;
            }
            else
            {
                TimingMethod = "Current Timing Method";
            }
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            parent.AppendChild(ToElement(document, "Version", "1.5"));
            parent.AppendChild(ToElement(document, "TimerHeight", TimerHeight));
            parent.AppendChild(ToElement(document, "TimerWidth", TimerWidth));
            parent.AppendChild(ToElement(document, "TimerFormat", TimerFormat));
            parent.AppendChild(ToElement(document, "OverrideSplitColors", OverrideSplitColors));
            parent.AppendChild(ToElement(document, "ShowGradient", ShowGradient));
            parent.AppendChild(ToElement(document, TimerColor, "TimerColor"));
            parent.AppendChild(ToElement(document, BackgroundColor, "BackgroundColor"));
            parent.AppendChild(ToElement(document, BackgroundColor2, "BackgroundColor2"));
            parent.AppendChild(ToElement(document, "BackgroundGradient", BackgroundGradient));
            parent.AppendChild(ToElement(document, "CenterTimer", CenterTimer));
            parent.AppendChild(ToElement(document, "TimingMethod", TimingMethod));
            parent.AppendChild(ToElement(document, "DecimalsSize", DecimalsSize));
            return parent;
        }

        private void ColorButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var picker = new ColorPickerDialog();
            picker.SelectedColor = picker.OldColor = button.BackColor;
            picker.SelectedColorChanged += (s, x) => button.BackColor = picker.SelectedColor;
            picker.ShowDialog(this);
            button.BackColor = picker.SelectedColor;
        }

        private XmlElement ToElement<T>(XmlDocument document, String name, T value)
        {
            var element = document.CreateElement(name);
            element.InnerText = value.ToString();
            return element;
        }

        private XmlElement ToElement(XmlDocument document, String name, float value)
        {
            var element = document.CreateElement(name);
            element.InnerText = value.ToString(CultureInfo.InvariantCulture);
            return element;
        }
    }
}
