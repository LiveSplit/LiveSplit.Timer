namespace LiveSplit.UI.Components
{
    partial class TimerSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimerSettings));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbTimingMethod = new System.Windows.Forms.ComboBox();
            this.trkSize = new System.Windows.Forms.TrackBar();
            this.lblSize = new System.Windows.Forms.Label();
            this.chkGradient = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTimerColor = new System.Windows.Forms.Button();
            this.chkOverrideTimerColors = new System.Windows.Forms.CheckBox();
            this.cmbGradientType = new System.Windows.Forms.ComboBox();
            this.btnColor1 = new System.Windows.Forms.Button();
            this.btnColor2 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.chkCenterTimer = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTimerFormat = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.trkDecimalsSize = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkSize)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkDecimalsSize)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.cmbTimingMethod, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.trkSize, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblSize, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkGradient, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbGradientType, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnColor1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnColor2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkCenterTimer, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cmbTimerFormat, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.trkDecimalsSize, 1, 7);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // cmbTimingMethod
            // 
            resources.ApplyResources(this.cmbTimingMethod, "cmbTimingMethod");
            this.tableLayoutPanel1.SetColumnSpan(this.cmbTimingMethod, 3);
            this.cmbTimingMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimingMethod.FormattingEnabled = true;
            this.cmbTimingMethod.Items.AddRange(new object[] {
            resources.GetString("cmbTimingMethod.Items"),
            resources.GetString("cmbTimingMethod.Items1"),
            resources.GetString("cmbTimingMethod.Items2")});
            this.cmbTimingMethod.Name = "cmbTimingMethod";
            this.cmbTimingMethod.SelectedIndexChanged += new System.EventHandler(this.cmbTimingMethod_SelectedIndexChanged);
            // 
            // trkSize
            // 
            resources.ApplyResources(this.trkSize, "trkSize");
            this.tableLayoutPanel1.SetColumnSpan(this.trkSize, 3);
            this.trkSize.Name = "trkSize";
            this.trkSize.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // lblSize
            // 
            resources.ApplyResources(this.lblSize, "lblSize");
            this.lblSize.Name = "lblSize";
            // 
            // chkGradient
            // 
            resources.ApplyResources(this.chkGradient, "chkGradient");
            this.tableLayoutPanel1.SetColumnSpan(this.chkGradient, 2);
            this.chkGradient.Name = "chkGradient";
            this.chkGradient.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 4);
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnTimerColor, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.chkOverrideTimerColors, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnTimerColor
            // 
            resources.ApplyResources(this.btnTimerColor, "btnTimerColor");
            this.btnTimerColor.Name = "btnTimerColor";
            this.btnTimerColor.UseVisualStyleBackColor = false;
            this.btnTimerColor.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // chkOverrideTimerColors
            // 
            resources.ApplyResources(this.chkOverrideTimerColors, "chkOverrideTimerColors");
            this.tableLayoutPanel3.SetColumnSpan(this.chkOverrideTimerColors, 2);
            this.chkOverrideTimerColors.Name = "chkOverrideTimerColors";
            this.chkOverrideTimerColors.UseVisualStyleBackColor = true;
            this.chkOverrideTimerColors.CheckedChanged += new System.EventHandler(this.chkOverrideTimerColors_CheckedChanged);
            // 
            // cmbGradientType
            // 
            resources.ApplyResources(this.cmbGradientType, "cmbGradientType");
            this.cmbGradientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGradientType.FormattingEnabled = true;
            this.cmbGradientType.Items.AddRange(new object[] {
            resources.GetString("cmbGradientType.Items"),
            resources.GetString("cmbGradientType.Items1"),
            resources.GetString("cmbGradientType.Items2")});
            this.cmbGradientType.Name = "cmbGradientType";
            this.cmbGradientType.SelectedIndexChanged += new System.EventHandler(this.cmbGradientType_SelectedIndexChanged);
            // 
            // btnColor1
            // 
            resources.ApplyResources(this.btnColor1, "btnColor1");
            this.btnColor1.Name = "btnColor1";
            this.btnColor1.UseVisualStyleBackColor = false;
            this.btnColor1.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // btnColor2
            // 
            resources.ApplyResources(this.btnColor2, "btnColor2");
            this.btnColor2.Name = "btnColor2";
            this.btnColor2.UseVisualStyleBackColor = false;
            this.btnColor2.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // chkCenterTimer
            // 
            resources.ApplyResources(this.chkCenterTimer, "chkCenterTimer");
            this.chkCenterTimer.Name = "chkCenterTimer";
            this.chkCenterTimer.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cmbTimerFormat
            // 
            resources.ApplyResources(this.cmbTimerFormat, "cmbTimerFormat");
            this.tableLayoutPanel1.SetColumnSpan(this.cmbTimerFormat, 3);
            this.cmbTimerFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimerFormat.FormattingEnabled = true;
            this.cmbTimerFormat.Items.AddRange(new object[] {
            resources.GetString("cmbTimerFormat.Items"),
            resources.GetString("cmbTimerFormat.Items1"),
            resources.GetString("cmbTimerFormat.Items2"),
            resources.GetString("cmbTimerFormat.Items3"),
            resources.GetString("cmbTimerFormat.Items4"),
            resources.GetString("cmbTimerFormat.Items5"),
            resources.GetString("cmbTimerFormat.Items6"),
            resources.GetString("cmbTimerFormat.Items7"),
            resources.GetString("cmbTimerFormat.Items8")});
            this.cmbTimerFormat.Name = "cmbTimerFormat";
            this.cmbTimerFormat.SelectedIndexChanged += new System.EventHandler(this.cmbTimerFormat_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // trkDecimalsSize
            // 
            resources.ApplyResources(this.trkDecimalsSize, "trkDecimalsSize");
            this.tableLayoutPanel1.SetColumnSpan(this.trkDecimalsSize, 3);
            this.trkDecimalsSize.Maximum = 50;
            this.trkDecimalsSize.Minimum = 10;
            this.trkDecimalsSize.Name = "trkDecimalsSize";
            this.trkDecimalsSize.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkDecimalsSize.Value = 10;
            // 
            // TimerSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TimerSettings";
            this.Load += new System.EventHandler(this.TimerSettings_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkSize)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkDecimalsSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar trkSize;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnTimerColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkOverrideTimerColors;
        private System.Windows.Forms.CheckBox chkGradient;
        private System.Windows.Forms.ComboBox cmbGradientType;
        private System.Windows.Forms.Button btnColor1;
        private System.Windows.Forms.Button btnColor2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkCenterTimer;
        private System.Windows.Forms.ComboBox cmbTimingMethod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTimerFormat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trkDecimalsSize;
    }
}