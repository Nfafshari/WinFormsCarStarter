namespace WinFormsCarStarter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            button_profile = new Button();
            button_trips = new Button();
            button_home = new Button();
            button_status = new Button();
            button_activity = new Button();
            panel_activity = new Panel();
            panel_status = new Panel();
            panel_home = new Panel();
            panel_trips = new Panel();
            panel_profile = new Panel();
            panel1.SuspendLayout();
            panel_activity.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(button_profile);
            panel1.Controls.Add(button_trips);
            panel1.Controls.Add(button_home);
            panel1.Controls.Add(button_status);
            panel1.Controls.Add(button_activity);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 438);
            panel1.Name = "panel1";
            panel1.Size = new Size(265, 46);
            panel1.TabIndex = 0;
            // 
            // button_profile
            // 
            button_profile.FlatStyle = FlatStyle.Flat;
            button_profile.Location = new Point(211, 0);
            button_profile.Name = "button_profile";
            button_profile.Size = new Size(54, 46);
            button_profile.TabIndex = 4;
            button_profile.Text = "Profile";
            button_profile.UseVisualStyleBackColor = true;
            button_profile.Click += button_profile_Click;
            // 
            // button_trips
            // 
            button_trips.FlatStyle = FlatStyle.Flat;
            button_trips.Location = new Point(157, 0);
            button_trips.Name = "button_trips";
            button_trips.Size = new Size(56, 46);
            button_trips.TabIndex = 3;
            button_trips.Text = "Trips";
            button_trips.UseVisualStyleBackColor = true;
            button_trips.Click += button_trips_Click;
            // 
            // button_home
            // 
            button_home.FlatStyle = FlatStyle.Flat;
            button_home.Location = new Point(106, 0);
            button_home.Name = "button_home";
            button_home.Size = new Size(53, 46);
            button_home.TabIndex = 2;
            button_home.Text = "Home";
            button_home.UseVisualStyleBackColor = true;
            button_home.Click += button_home_Click;
            // 
            // button_status
            // 
            button_status.FlatStyle = FlatStyle.Flat;
            button_status.Location = new Point(54, 0);
            button_status.Name = "button_status";
            button_status.Size = new Size(54, 46);
            button_status.TabIndex = 1;
            button_status.Text = "Status";
            button_status.UseVisualStyleBackColor = true;
            button_status.Click += button_status_Click;
            // 
            // button_activity
            // 
            button_activity.FlatStyle = FlatStyle.Flat;
            button_activity.Location = new Point(0, 0);
            button_activity.Name = "button_activity";
            button_activity.Size = new Size(55, 46);
            button_activity.TabIndex = 0;
            button_activity.Text = "Activity";
            button_activity.UseVisualStyleBackColor = true;
            button_activity.Click += button_activity_Click;
            // 
            // panel_activity
            // 
            panel_activity.Controls.Add(panel_status);
            panel_activity.Dock = DockStyle.Fill;
            panel_activity.Location = new Point(0, 0);
            panel_activity.Name = "panel_activity";
            panel_activity.Size = new Size(265, 438);
            panel_activity.TabIndex = 1;
            // 
            // panel_status
            // 
            panel_status.Dock = DockStyle.Fill;
            panel_status.Location = new Point(0, 0);
            panel_status.Name = "panel_status";
            panel_status.Size = new Size(265, 438);
            panel_status.TabIndex = 0;
            // 
            // panel_home
            // 
            panel_home.Dock = DockStyle.Fill;
            panel_home.Location = new Point(0, 0);
            panel_home.Name = "panel_home";
            panel_home.Size = new Size(265, 438);
            panel_home.TabIndex = 2;
            // 
            // panel_trips
            // 
            panel_trips.Dock = DockStyle.Fill;
            panel_trips.Location = new Point(0, 0);
            panel_trips.Name = "panel_trips";
            panel_trips.Size = new Size(265, 438);
            panel_trips.TabIndex = 3;
            // 
            // panel_profile
            // 
            panel_profile.Dock = DockStyle.Fill;
            panel_profile.Location = new Point(0, 0);
            panel_profile.Name = "panel_profile";
            panel_profile.Size = new Size(265, 438);
            panel_profile.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(265, 484);
            Controls.Add(panel_profile);
            Controls.Add(panel_trips);
            Controls.Add(panel_home);
            Controls.Add(panel_activity);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form1";
            Text = "Piper Auto Start";
            panel1.ResumeLayout(false);
            panel_activity.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}
