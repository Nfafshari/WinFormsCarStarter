using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsCarStarter
{
    public partial class Notification : Form
    {
        private System.Windows.Forms.Timer timer;
        private int notifTime = 2000;
        private int fade = 0;
        private Form parentForm;

        public Notification(string message, Color backColor, Form parent)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            BackColor = backColor;
            Opacity = 0;
            ShowInTaskbar = false;
            TopMost = true;
            parentForm = parent;

            Width = parent.Width - 20;
            Height = 40;
            Location = new Point(parent.Left + 10, parent.Top + 35);

            Label lbl = new Label()
            {
                Text = message,
                ForeColor = Color.White,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10)
            };
            Controls.Add(lbl);

            // Timer to close the notification
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            timer.Tick += timer_Tick;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            fade = 1;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (fade == 1) // Fade in
            {
                this.Opacity += 0.1;
                if (this.Opacity >= 1)
                {
                    fade = 2;
                    Task.Delay(notifTime).ContinueWith(_ => fade = 3); // Wait, then fade out
                }
            }
            else if (fade == 3) // Fade out
            {
                this.Opacity -= 0.05;
                if (this.Opacity <= 0)
                {
                    timer.Stop();
                    this.Close();
                }
            }
        }
    }
}
