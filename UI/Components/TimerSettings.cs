using LiveSplit.TimeFormatters;
using System;
using System.Drawing;
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
        public DeltasGradientType BackgroundGradient { get; set; }
        public String GradientString
        {
            get { return GetBackgroundTypeString(BackgroundGradient); }
            set { BackgroundGradient = (DeltasGradientType)Enum.Parse(typeof(DeltasGradientType), value.Replace(" ", "")); }
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
            BackgroundGradient = DeltasGradientType.Plain;
            CenterTimer = false;
            TimingMethod = "Current Timing Method";
            DecimalsSize = 35f;

            btnTimerColor.DataBindings.Add("BackColor", this, "TimerColor", false, DataSourceUpdateMode.OnPropertyChanged);
            chkOverrideTimerColors.DataBindings.Add("Checked", this, "OverrideSplitColors", false, DataSourceUpdateMode.OnPropertyChanged);
            chkGradient.DataBindings.Add("Checked", this, "ShowGradient", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbGradientType.DataBindings.Add("SelectedItem", this, "GradientString", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor1.DataBindings.Add("BackColor", this, "BackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnColor2.DataBindings.Add("BackColor", this, "BackgroundColor2", false, DataSourceUpdateMode.OnPropertyChanged);
            chkCenterTimer.DataBindings.Add("Checked", this, "CenterTimer", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbTimingMethod.DataBindings.Add("SelectedItem", this, "TimingMethod", false, DataSourceUpdateMode.OnPropertyChanged);
            trkDecimalsSize.DataBindings.Add("Value", this, "DecimalsSize", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbTimerFormat.DataBindings.Add("SelectedItem", this, "TimerFormat", false, DataSourceUpdateMode.OnPropertyChanged);
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
            var selectedText = cmbGradientType.SelectedItem.ToString();
            btnColor1.Visible = selectedText != "Plain" && !selectedText.Contains("Delta");
            btnColor2.Visible = !selectedText.Contains("Delta");
            btnColor2.DataBindings.Clear();
            btnColor2.DataBindings.Add("BackColor", this, btnColor1.Visible ? "BackgroundColor2" : "BackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            GradientString = cmbGradientType.SelectedItem.ToString();
        }

        public static string GetBackgroundTypeString(DeltasGradientType type)
        {
            switch (type)
            {
                case DeltasGradientType.Horizontal:
                    return "Horizontal Gradient";
                case DeltasGradientType.HorizontalWithDeltaColor:
                    return "Horizontal With Delta Color";
                case DeltasGradientType.PlainWithDeltaColor:
                    return "Plain With Delta Color";
                case DeltasGradientType.Vertical:
                    return "Vertical";
                case DeltasGradientType.VerticalWithDeltaColor:
                    return "Vertical With Delta Color";
                case DeltasGradientType.Plain:
                default:
                    return "Plain";
            }
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

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            Version version = SettingsHelper.ParseVersion(element["Version"]);

            TimerHeight = SettingsHelper.ParseFloat(element["TimerHeight"]);
            TimerWidth = SettingsHelper.ParseFloat(element["TimerWidth"]);
            ShowGradient = SettingsHelper.ParseBool(element["ShowGradient"], true);
            TimerColor = SettingsHelper.ParseColor(element["TimerColor"], Color.FromArgb(170, 170, 170));
            DecimalsSize = SettingsHelper.ParseFloat(element["DecimalsSize"], 35f);
            BackgroundColor = SettingsHelper.ParseColor(element["BackgroundColor"], Color.Transparent);
            BackgroundColor2 = SettingsHelper.ParseColor(element["BackgroundColor2"], Color.Transparent);
            GradientString = SettingsHelper.ParseString(element["BackgroundGradient"], DeltasGradientType.Plain.ToString());
            CenterTimer = SettingsHelper.ParseBool(element["CenterTimer"], false);
            TimingMethod = SettingsHelper.ParseString(element["TimingMethod"], "Current Timing Method");

            if (version >= new Version(1, 3))
                OverrideSplitColors = SettingsHelper.ParseBool(element["OverrideSplitColors"]);
            else
                OverrideSplitColors = !SettingsHelper.ParseBool(element["UseSplitColors"], true);

            if (version >= new Version(1, 2))
            {
                if (version >= new Version(1, 5))
                {
                    TimerFormat = SettingsHelper.ParseString(element["TimerFormat"]);
                }
                else
                {
                    var accuracy = SettingsHelper.ParseEnum<TimeAccuracy>(element["TimerAccuracy"]);
                    if (accuracy == TimeAccuracy.Hundredths)
                        TimerFormat = "1.23";
                    else if (accuracy == TimeAccuracy.Tenths)
                        TimerFormat = "1.2";
                    else
                        TimerFormat = "1";
                }              
            }
            else
                TimerFormat = "1.23";              
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.5") ^
            SettingsHelper.CreateSetting(document, parent, "TimerHeight", TimerHeight) ^
            SettingsHelper.CreateSetting(document, parent, "TimerWidth", TimerWidth) ^
            SettingsHelper.CreateSetting(document, parent, "TimerFormat", TimerFormat) ^
            SettingsHelper.CreateSetting(document, parent, "OverrideSplitColors", OverrideSplitColors) ^
            SettingsHelper.CreateSetting(document, parent, "ShowGradient", ShowGradient) ^
            SettingsHelper.CreateSetting(document, parent, "TimerColor", TimerColor) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundColor", BackgroundColor) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundColor2", BackgroundColor2) ^
            SettingsHelper.CreateSetting(document, parent, "BackgroundGradient", BackgroundGradient) ^
            SettingsHelper.CreateSetting(document, parent, "CenterTimer", CenterTimer) ^
            SettingsHelper.CreateSetting(document, parent, "TimingMethod", TimingMethod) ^
            SettingsHelper.CreateSetting(document, parent, "DecimalsSize", DecimalsSize);
        }

        private void ColorButtonClick(object sender, EventArgs e)
        {
            SettingsHelper.ColorButtonClick((Button)sender, this);
        }
    }
}
