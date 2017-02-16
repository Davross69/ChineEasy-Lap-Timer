using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChineEasy
{
    public partial class RacerUI : UserControl
    {
        private RacerInfo racer = new RacerInfo();

        public RacerUI()
        {
            InitializeComponent();
        }

        public void AttachRacerInfo(RacerInfo racer)
        {
            this.racer = racer;
            racerName.Text = racer.Name;
            BackColor = Color.FromArgb(63, racer.UIColour);
            label1.BackColor = Color.FromArgb(0, racer.UIColour);
            label2.BackColor = Color.FromArgb(0, racer.UIColour);
            label3.BackColor = Color.FromArgb(0, racer.UIColour);
            label4.BackColor = Color.FromArgb(0, racer.UIColour);
            label5.BackColor = Color.FromArgb(0, racer.UIColour);
            label6.BackColor = Color.FromArgb(0, racer.UIColour);
            listViewLaps.Columns[1].TextAlign = HorizontalAlignment.Right;
            listViewLaps.Columns[2].TextAlign = HorizontalAlignment.Right;
            UpdateUI(-1);
        }

        private void racerName_TextChanged(object sender, EventArgs e)
        {
            racer.Name = racerName.Text;
        }

        public void UpdateUI(int focusIndex)
        {
            if (focusIndex != -1)
            {
                if (listViewLaps.Items.Count > 0)
                {
                    listViewLaps.Items[focusIndex % listViewLaps.Items.Count].EnsureVisible();
                }
            }
            else
            {
                int lapCount = racer.LapCount;
                if (lapCount == 0)
                {
                    // Reset listview
                    if (listViewLaps.Items.Count > 0) listViewLaps.Items.Clear();
                }
                else
                {
                    // Append new items
                    for (int i = listViewLaps.Items.Count; i < lapCount; i++)
                    {
                        String[] arr = new String[3];
                        ListViewItem itm;
                        arr[0] = (i + 1).ToString();
                        arr[1] = racer.LapTime(i);
                        arr[2] = racer.LapDelta(i);
                        itm = new ListViewItem(arr);
                        listViewLaps.Items.Add(itm);
                    }
                    listViewLaps.Items[listViewLaps.Items.Count - 1].EnsureVisible();
                }
                textBoxCurrentDelta.Text = racer.CurrentLapDelta;
                textBoxCurentLapNumber.Text = racer.CurrentLap;
                textBoxBestLapNumber.Text = racer.BestLap;
                textBoxBestDelta.Text = racer.BestLapDelta;
            }
        }
    }
}
