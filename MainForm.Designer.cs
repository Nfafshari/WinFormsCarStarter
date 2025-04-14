namespace WinFormsCarStarter
{
    partial class MainForm
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
            pictureBox_fuel = new PictureBox();
            pictureBox_oil = new PictureBox();
            pictureBox_temp = new PictureBox();
            panel_profile = new Panel();
            panel_home = new Panel();
            panel_trips = new Panel();
            panel_diagnostics = new Panel();
            pictureBox_battery = new PictureBox();
            pictureBox_connection = new PictureBox();
            pictureBox_wifi = new PictureBox();
            panel2 = new Panel();
            panel_tabContainer = new Panel();
            panel1.SuspendLayout();
            panel_activity.SuspendLayout();
            panel_status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_fuel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_oil).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_temp).BeginInit();
            panel_trips.SuspendLayout();
            panel_diagnostics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_battery).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_connection).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_wifi).BeginInit();
            panel_tabContainer.SuspendLayout();
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
            panel_activity.Controls.Add(panel_profile);
            panel_activity.Dock = DockStyle.Fill;
            panel_activity.Location = new Point(0, 0);
            panel_activity.Name = "panel_activity";
            panel_activity.Size = new Size(265, 415);
            panel_activity.TabIndex = 1;
            // 
            // panel_status
            // 
            panel_status.Controls.Add(pictureBox_fuel);
            panel_status.Controls.Add(pictureBox_oil);
            panel_status.Controls.Add(pictureBox_temp);
            panel_status.Dock = DockStyle.Fill;
            panel_status.Location = new Point(0, 0);
            panel_status.Name = "panel_status";
            panel_status.Size = new Size(265, 415);
            panel_status.TabIndex = 0;
            // 
            // pictureBox_fuel
            // 
            pictureBox_fuel.Location = new Point(201, 7);
            pictureBox_fuel.Name = "pictureBox_fuel";
            pictureBox_fuel.Size = new Size(51, 39);
            pictureBox_fuel.TabIndex = 5;
            pictureBox_fuel.TabStop = false;
            // 
            // pictureBox_oil
            // 
            pictureBox_oil.Location = new Point(106, 10);
            pictureBox_oil.Name = "pictureBox_oil";
            pictureBox_oil.Size = new Size(59, 36);
            pictureBox_oil.TabIndex = 4;
            pictureBox_oil.TabStop = false;
            // 
            // pictureBox_temp
            // 
            pictureBox_temp.Location = new Point(21, 21);
            pictureBox_temp.Name = "pictureBox_temp";
            pictureBox_temp.Size = new Size(34, 25);
            pictureBox_temp.TabIndex = 3;
            pictureBox_temp.TabStop = false;
            // 
            // panel_profile
            // 
            panel_profile.Location = new Point(6, 174);
            panel_profile.Name = "panel_profile";
            panel_profile.Size = new Size(265, 170);
            panel_profile.TabIndex = 4;
            // 
            // panel_home
            // 
            panel_home.Location = new Point(3, 161);
            panel_home.Name = "panel_home";
            panel_home.Size = new Size(265, 193);
            panel_home.TabIndex = 2;
            // 
            // panel_trips
            // 
            panel_trips.Controls.Add(panel_home);
            panel_trips.Location = new Point(6, 30);
            panel_trips.Name = "panel_trips";
            panel_trips.Size = new Size(262, 180);
            panel_trips.TabIndex = 3;
            // 
            // panel_diagnostics
            // 
            panel_diagnostics.Controls.Add(pictureBox_battery);
            panel_diagnostics.Controls.Add(pictureBox_connection);
            panel_diagnostics.Controls.Add(pictureBox_wifi);
            panel_diagnostics.Controls.Add(panel2);
            panel_diagnostics.Location = new Point(0, -1);
            panel_diagnostics.Name = "panel_diagnostics";
            panel_diagnostics.Size = new Size(265, 24);
            panel_diagnostics.TabIndex = 0;
            // 
            // pictureBox_battery
            // 
            pictureBox_battery.Anchor = AnchorStyles.None;
            pictureBox_battery.Location = new Point(240, 5);
            pictureBox_battery.Margin = new Padding(0);
            pictureBox_battery.Name = "pictureBox_battery";
            pictureBox_battery.Size = new Size(22, 16);
            pictureBox_battery.TabIndex = 3;
            pictureBox_battery.TabStop = false;
            // 
            // pictureBox_connection
            // 
            pictureBox_connection.Anchor = AnchorStyles.None;
            pictureBox_connection.Location = new Point(220, 10);
            pictureBox_connection.Margin = new Padding(0);
            pictureBox_connection.Name = "pictureBox_connection";
            pictureBox_connection.Size = new Size(16, 12);
            pictureBox_connection.TabIndex = 2;
            pictureBox_connection.TabStop = false;
            // 
            // pictureBox_wifi
            // 
            pictureBox_wifi.Location = new Point(205, 5);
            pictureBox_wifi.Name = "pictureBox_wifi";
            pictureBox_wifi.Size = new Size(15, 15);
            pictureBox_wifi.TabIndex = 1;
            pictureBox_wifi.TabStop = false;
            // 
            // panel2
            // 
            panel2.Location = new Point(0, 30);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 100);
            panel2.TabIndex = 0;
            // 
            // panel_tabContainer
            // 
            panel_tabContainer.Controls.Add(panel_activity);
            panel_tabContainer.Controls.Add(panel_trips);
            panel_tabContainer.Location = new Point(0, 25);
            panel_tabContainer.Name = "panel_tabContainer";
            panel_tabContainer.Size = new Size(265, 415);
            panel_tabContainer.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(265, 484);
            Controls.Add(panel_tabContainer);
            Controls.Add(panel_diagnostics);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Piper Auto Start";
            panel1.ResumeLayout(false);
            panel_activity.ResumeLayout(false);
            panel_status.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox_fuel).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_oil).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_temp).EndInit();
            panel_trips.ResumeLayout(false);
            panel_diagnostics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox_battery).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_connection).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_wifi).EndInit();
            panel_tabContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button_profile;
        private Button button_trips;
        private Button button_home;
        private Button button_status;
        private Button button_activity;
        private Panel panel_activity;
        private Panel panel_status;
        private Panel panel_home;
        private Panel panel_trips;
        private Panel panel_profile;
        private Panel panel_diagnostics;
        private Panel panel2;
        private Panel panel_tabContainer;
        private PictureBox pictureBox_wifi;
        private PictureBox pictureBox_connection;
        private PictureBox pictureBox_battery;
        private PictureBox pictureBox_fuel;
        private PictureBox pictureBox_oil;
        private PictureBox pictureBox_temp;
    }
}
