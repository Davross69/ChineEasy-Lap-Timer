namespace ChineEasy
{
    partial class ConfigureUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureUI));
            this.textBoxMinLap = new System.Windows.Forms.TextBox();
            this.checkBeep = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMinPeak = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxMaxPeak = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.checkDebug = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxEdgeRSSI = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxPeakRSSI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxMinLap
            // 
            resources.ApplyResources(this.textBoxMinLap, "textBoxMinLap");
            this.textBoxMinLap.Name = "textBoxMinLap";
            // 
            // checkBeep
            // 
            resources.ApplyResources(this.checkBeep, "checkBeep");
            this.checkBeep.Name = "checkBeep";
            this.checkBeep.UseVisualStyleBackColor = true;
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
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxMinPeak
            // 
            resources.ApplyResources(this.textBoxMinPeak, "textBoxMinPeak");
            this.textBoxMinPeak.Name = "textBoxMinPeak";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textBoxMaxPeak
            // 
            resources.ApplyResources(this.textBoxMaxPeak, "textBoxMaxPeak");
            this.textBoxMaxPeak.Name = "textBoxMaxPeak";
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkDebug
            // 
            resources.ApplyResources(this.checkDebug, "checkDebug");
            this.checkDebug.Name = "checkDebug";
            this.checkDebug.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // textBoxEdgeRSSI
            // 
            resources.ApplyResources(this.textBoxEdgeRSSI, "textBoxEdgeRSSI");
            this.textBoxEdgeRSSI.Name = "textBoxEdgeRSSI";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // textBoxPeakRSSI
            // 
            resources.ApplyResources(this.textBoxPeakRSSI, "textBoxPeakRSSI");
            this.textBoxPeakRSSI.Name = "textBoxPeakRSSI";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ConfigureUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxPeakRSSI);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxEdgeRSSI);
            this.Controls.Add(this.checkDebug);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxMaxPeak);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxMinPeak);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBeep);
            this.Controls.Add(this.textBoxMinLap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigureUI";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBoxMinLap;
        public System.Windows.Forms.CheckBox checkBeep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBoxMinPeak;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBoxMaxPeak;
        private System.Windows.Forms.Button btnApply;
        public System.Windows.Forms.CheckBox checkDebug;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox textBoxEdgeRSSI;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox textBoxPeakRSSI;
        private System.Windows.Forms.Label label1;
    }
}