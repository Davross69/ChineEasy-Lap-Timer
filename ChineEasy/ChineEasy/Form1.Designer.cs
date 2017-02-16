namespace ChineEasy
{
    partial class MainForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            for (int racer = 0; racer < numberOfRacers; racer++)
            {
                this.racerUI[racer] = new ChineEasy.RacerUI();
            }
    
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // Add panels
            // 
            for (int racer = numberOfRacers - 1; racer >= 0; racer--)
            {
                if (racer % 2 == 1)
                {
                    this.splitContainer1.Panel2.Controls.Add(this.racerUI[racer]);
                } else {
                    this.splitContainer1.Panel1.Controls.Add(this.racerUI[racer]);
                }
            }
                
            // 
            // racerUI[2]
            // 
            for (int racer = 0; racer < numberOfRacers; racer++)
            {
                int index = racer / 2;
                this.racerUI[racer].AutoSize = true;
                this.racerUI[racer].AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                this.racerUI[racer].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.racerUI[racer].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.racerUI[racer].Location = new System.Drawing.Point(2, 2 + index * 388);
                this.racerUI[racer].Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
                this.racerUI[racer].Name = "Racer" + racer.ToString();                
                this.racerUI[racer].Size = new System.Drawing.Size(293, 384);
                this.racerUI[racer].TabIndex = index;
            }

            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Size = new System.Drawing.Size(601, 803);
            this.splitContainer1.SplitterDistance = 297;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
   
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(596, (numberOfRacers > 2)? 778 : 389);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "ChineEasy Lap Timer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private RacerUI[] racerUI = new RacerUI[RacerInfo._maxRacers];
    }
}

