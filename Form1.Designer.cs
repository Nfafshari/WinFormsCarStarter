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
            panel1.SuspendLayout();
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
            button_profile.Text = "button5";
            button_profile.UseVisualStyleBackColor = true;
            // 
            // button_trips
            // 
            button_trips.FlatStyle = FlatStyle.Flat;
            button_trips.Location = new Point(157, 0);
            button_trips.Name = "button_trips";
            button_trips.Size = new Size(56, 46);
            button_trips.TabIndex = 3;
            button_trips.Text = "button4";
            button_trips.UseVisualStyleBackColor = true;
            // 
            // button_home
            // 
            button_home.FlatStyle = FlatStyle.Flat;
            button_home.Location = new Point(106, 0);
            button_home.Name = "button_home";
            button_home.Size = new Size(53, 46);
            button_home.TabIndex = 2;
            button_home.Text = "button3";
            button_home.UseVisualStyleBackColor = true;
            // 
            // button_status
            // 
            button_status.FlatStyle = FlatStyle.Flat;
            button_status.Location = new Point(54, 0);
            button_status.Name = "button_status";
            button_status.Size = new Size(54, 46);
            button_status.TabIndex = 1;
            button_status.Text = "button2";
            button_status.UseVisualStyleBackColor = true;
            // 
            // button_activity
            // 
            button_activity.FlatStyle = FlatStyle.Flat;
            button_activity.Location = new Point(0, 0);
            button_activity.Name = "button_activity";
            button_activity.Size = new Size(55, 46);
            button_activity.TabIndex = 0;
            button_activity.Text = "button1";
            button_activity.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(265, 484);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button_profile;
        private Button button_trips;
        private Button button_home;
        private Button button_status;
        private Button button_activity;
    }
}
