namespace ChineEasy
{
    partial class RacerUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            this.groupBoxCurrent = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCurentLapNumber = new System.Windows.Forms.TextBox();
            this.textBoxCurrentDelta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxBest = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBestLapNumber = new System.Windows.Forms.TextBox();
            this.textBoxBestDelta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.racerName = new System.Windows.Forms.TextBox();
            this.listViewLaps = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxCurrent.SuspendLayout();
            this.groupBoxBest.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCurrent
            // 
            this.groupBoxCurrent.Controls.Add(this.label5);
            this.groupBoxCurrent.Controls.Add(this.label2);
            this.groupBoxCurrent.Controls.Add(this.textBoxCurentLapNumber);
            this.groupBoxCurrent.Controls.Add(this.textBoxCurrentDelta);
            this.groupBoxCurrent.Controls.Add(this.label1);
            this.groupBoxCurrent.Location = new System.Drawing.Point(3, 41);
            this.groupBoxCurrent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxCurrent.Name = "groupBoxCurrent";
            this.groupBoxCurrent.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxCurrent.Size = new System.Drawing.Size(286, 64);
            this.groupBoxCurrent.TabIndex = 0;
            this.groupBoxCurrent.TabStop = false;
            this.groupBoxCurrent.Text = "Current Lap";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "#:";
            // 
            // textBoxCurentLapNumber
            // 
            this.textBoxCurentLapNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCurentLapNumber.Location = new System.Drawing.Point(36, 26);
            this.textBoxCurentLapNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxCurentLapNumber.Name = "textBoxCurentLapNumber";
            this.textBoxCurentLapNumber.ReadOnly = true;
            this.textBoxCurentLapNumber.Size = new System.Drawing.Size(41, 32);
            this.textBoxCurentLapNumber.TabIndex = 2;
            this.textBoxCurentLapNumber.Text = "00";
            // 
            // textBoxCurrentDelta
            // 
            this.textBoxCurrentDelta.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCurrentDelta.Location = new System.Drawing.Point(157, 26);
            this.textBoxCurrentDelta.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxCurrentDelta.Name = "textBoxCurrentDelta";
            this.textBoxCurrentDelta.ReadOnly = true;
            this.textBoxCurrentDelta.Size = new System.Drawing.Size(62, 32);
            this.textBoxCurrentDelta.TabIndex = 1;
            this.textBoxCurrentDelta.Text = "00:00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Delta:";
            // 
            // groupBoxBest
            // 
            this.groupBoxBest.Controls.Add(this.label6);
            this.groupBoxBest.Controls.Add(this.label3);
            this.groupBoxBest.Controls.Add(this.textBoxBestLapNumber);
            this.groupBoxBest.Controls.Add(this.textBoxBestDelta);
            this.groupBoxBest.Controls.Add(this.label4);
            this.groupBoxBest.Location = new System.Drawing.Point(4, 112);
            this.groupBoxBest.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxBest.Name = "groupBoxBest";
            this.groupBoxBest.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxBest.Size = new System.Drawing.Size(285, 60);
            this.groupBoxBest.TabIndex = 4;
            this.groupBoxBest.TabStop = false;
            this.groupBoxBest.Text = "Best Lap";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "#:";
            // 
            // textBoxBestLapNumber
            // 
            this.textBoxBestLapNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBestLapNumber.Location = new System.Drawing.Point(36, 26);
            this.textBoxBestLapNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxBestLapNumber.Name = "textBoxBestLapNumber";
            this.textBoxBestLapNumber.ReadOnly = true;
            this.textBoxBestLapNumber.Size = new System.Drawing.Size(40, 32);
            this.textBoxBestLapNumber.TabIndex = 2;
            this.textBoxBestLapNumber.Text = "00";
            // 
            // textBoxBestDelta
            // 
            this.textBoxBestDelta.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBestDelta.Location = new System.Drawing.Point(156, 26);
            this.textBoxBestDelta.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxBestDelta.Name = "textBoxBestDelta";
            this.textBoxBestDelta.ReadOnly = true;
            this.textBoxBestDelta.Size = new System.Drawing.Size(62, 32);
            this.textBoxBestDelta.TabIndex = 1;
            this.textBoxBestDelta.Text = "00:00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(86, 29);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Delta:";
            // 
            // racerName
            // 
            this.racerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.racerName.Location = new System.Drawing.Point(3, 3);
            this.racerName.Name = "racerName";
            this.racerName.Size = new System.Drawing.Size(286, 32);
            this.racerName.TabIndex = 5;
            this.racerName.Text = "Racer Name";
            this.racerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.racerName.TextChanged += new System.EventHandler(this.racerName_TextChanged);
            // 
            // listViewLaps
            // 
            this.listViewLaps.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewLaps.AutoArrange = false;
            this.listViewLaps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewLaps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewLaps.FullRowSelect = true;
            this.listViewLaps.GridLines = true;
            this.listViewLaps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewLaps.HoverSelection = true;
            this.listViewLaps.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewLaps.Location = new System.Drawing.Point(4, 178);
            this.listViewLaps.MultiSelect = false;
            this.listViewLaps.Name = "listViewLaps";
            this.listViewLaps.Size = new System.Drawing.Size(285, 201);
            this.listViewLaps.TabIndex = 7;
            this.listViewLaps.UseCompatibleStateImageBehavior = false;
            this.listViewLaps.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Lap";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Time";
            this.columnHeader2.Width = 110;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Delta";
            this.columnHeader3.Width = 110;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(226, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "ss:hh";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(225, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 25);
            this.label6.TabIndex = 5;
            this.label6.Text = "ss:hh";
            // 
            // RacerUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.listViewLaps);
            this.Controls.Add(this.racerName);
            this.Controls.Add(this.groupBoxBest);
            this.Controls.Add(this.groupBoxCurrent);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "RacerUI";
            this.Size = new System.Drawing.Size(293, 382);
            this.groupBoxCurrent.ResumeLayout(false);
            this.groupBoxCurrent.PerformLayout();
            this.groupBoxBest.ResumeLayout(false);
            this.groupBoxBest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCurrent;
        private System.Windows.Forms.TextBox textBoxCurrentDelta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCurentLapNumber;
        private System.Windows.Forms.GroupBox groupBoxBest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxBestLapNumber;
        private System.Windows.Forms.TextBox textBoxBestDelta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox racerName;
        private System.Windows.Forms.ListView listViewLaps;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
