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
        // Global variables (specific to Notification)
        private System.Windows.Forms.Timer timer;
        private int notifTime = 2000;
        private int fade = 0;
        private Form parentForm;

        // Notification class for implementing notifications to main form (MainForm) 
        // can set message, color, and which form to pop up notification in
        public Notification(string message, Color backColor, Form parent)
        {
            InitializeComponent();

            // Notification appearance
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

            // Label for specified message
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

        // Shows notification on form
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            fade = 1;
            timer.Start();
        }

        // Timer for animating the notification (fades in then slowly fades out)
        private void timer_Tick(object sender, EventArgs e)
        {
            if (fade == 1) // Fade in
            {
                this.Opacity += 0.25;
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
