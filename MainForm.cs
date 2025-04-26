using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static WinFormsCarStarter.MainForm;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;
using System.IO;

namespace WinFormsCarStarter
{
    public partial class MainForm : Form
    {
        // **** Global variables (private to MainForm) **** //
        // Home Tab Variables
        private ImageList imageList = new ImageList();
        private ImageList active_imageList = new ImageList();
        private ProgressBar progressBar_temp = new ProgressBar();
        private ProgressBar progressBar_oil = new ProgressBar();
        private ProgressBar progressBar_fuel = new ProgressBar();
        private TextBox textBox_firstName = new TextBox();
        private TextBox textBox_lastName = new TextBox();
        private TextBox textBox_email = new TextBox();
        private TextBox textBox_password = new TextBox();
        private TextBox textBox_vin = new TextBox();
        private ComboBox comboBox_vehicleType = new ComboBox();
        private Panel slidePanel;
        private FlowLayoutPanel flowlayoutPanel_activities = new FlowLayoutPanel();
        private bool isStartStopToggled = false;
        private bool isLightsToggled = false;
        private bool isHazardsToggled = false;
        private bool isWindowsToggled = false;
        private bool isDragging = false;
        private int dragStart;
        private int originalPanelTop;
        private int collapsedTop;
        private int expandedTop;
        private int UserId;


        public MainForm()
        {
            InitializeComponent();
            // MainForm closing handlers
            Application.ApplicationExit += (s, e) => TruncateTables();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => TruncateTables();

            /******** Tabbed Interface Setup ********/
            // Tab setup using panels
            panel_diagnostics.Dock = DockStyle.Top;
            panel_diagnostics.Height = 20;

            panel_tabContainer.Dock = DockStyle.Fill;
            this.Controls.Add(panel_tabContainer);

            panel_activity.Dock = DockStyle.Fill;
            panel_status.Dock = DockStyle.Fill;
            panel_home.Dock = DockStyle.Fill;
            panel_trips.Dock = DockStyle.Fill;
            panel_profile.Dock = DockStyle.Fill;
            panel_diagnostics.Dock = DockStyle.Top;

            panel_tabContainer.Controls.Add(panel_activity);
            panel_tabContainer.Controls.Add(panel_status);
            panel_tabContainer.Controls.Add(panel_home);
            panel_tabContainer.Controls.Add(panel_trips);
            panel_tabContainer.Controls.Add(panel_profile);


            // Button appearance
            button_activity.FlatAppearance.BorderSize = 0;
            button_status.FlatAppearance.BorderSize = 0;
            button_home.FlatAppearance.BorderSize = 0;
            button_trips.FlatAppearance.BorderSize = 0;
            button_profile.FlatAppearance.BorderSize = 0;

            button_activity.FlatAppearance.MouseOverBackColor = Color.LightGray;
            button_status.FlatAppearance.MouseOverBackColor = Color.LightGray;
            button_home.FlatAppearance.MouseOverBackColor = Color.LightGray;
            button_trips.FlatAppearance.MouseOverBackColor = Color.LightGray;
            button_profile.FlatAppearance.MouseOverBackColor = Color.LightGray;

            button_activity.FlatAppearance.MouseDownBackColor = Color.Gray;
            button_status.FlatAppearance.MouseDownBackColor = Color.Gray;
            button_home.FlatAppearance.MouseDownBackColor = Color.Gray;
            button_trips.FlatAppearance.MouseDownBackColor = Color.Gray;
            button_profile.FlatAppearance.MouseDownBackColor = Color.Gray;

            // Button text alignment
            button_activity.TextAlign = ContentAlignment.BottomCenter;
            button_status.TextAlign = ContentAlignment.BottomCenter;
            button_home.TextAlign = ContentAlignment.BottomCenter;
            button_trips.TextAlign = ContentAlignment.BottomCenter;
            button_profile.TextAlign = ContentAlignment.BottomCenter;

            // Button Icons
            ImageList imageList = new ImageList();
            imageList.Images.Add("activity", Image.FromFile("icons\\activity.png"));
            imageList.Images.Add("status", Image.FromFile("icons\\status.png"));
            imageList.Images.Add("home", Image.FromFile("icons\\home.png"));
            imageList.Images.Add("trips", Image.FromFile("icons\\nav.png"));
            imageList.Images.Add("profile", Image.FromFile("icons\\profile.png"));
            imageList.Images.Add("activated activity", Image.FromFile("icons\\active_activity.png"));
            imageList.Images.Add("activated status", Image.FromFile("icons\\active_status.png"));
            imageList.Images.Add("activated home", Image.FromFile("icons\\active_house.png"));
            imageList.Images.Add("activated trips", Image.FromFile("icons\\active_nav.png"));
            imageList.Images.Add("activated profile", Image.FromFile("icons\\active_profile.png"));

            button_activity.ImageList = imageList;
            button_status.ImageList = imageList;
            button_home.ImageList = imageList;
            button_trips.ImageList = imageList;
            button_profile.ImageList = imageList;

            button_activity.ImageIndex = 0;
            button_status.ImageIndex = 1;
            button_home.ImageIndex = 2;
            button_trips.ImageIndex = 3;
            button_profile.ImageIndex = 4;

            button_activity.ImageAlign = ContentAlignment.TopCenter;
            button_status.ImageAlign = ContentAlignment.TopCenter;
            button_home.ImageAlign = ContentAlignment.TopCenter;
            button_trips.ImageAlign = ContentAlignment.TopCenter;
            button_profile.ImageAlign = ContentAlignment.TopCenter;

            // ******************** DATABASE SETUP ******************** //  
            // Database file
            string dbPath = "carstarter.db";
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var dropUsers = new SqliteCommand("DROP TABLE IF EXISTS Users;", connection);
                dropUsers.ExecuteNonQuery();

                string userTableCmd = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                        FirstName TEXT NOT NULL,
                        LastName TEXT NOT NULL,
                        Email TEXT NOT NULL UNIQUE,
                        Password TEXT NOT NULL,
                        Vin TEXT NOT NULL,
                        CarType TEXT
                    );";

                var createUserTable = new SqliteCommand(userTableCmd, connection);
                createUserTable.ExecuteNonQuery();

                var dropTable = new SqliteCommand("DROP TABLE IF EXISTS ActivityLog;", connection);
                dropTable.ExecuteNonQuery();

                string activityTableCmd = @"
                    CREATE TABLE IF NOT EXISTS ActivityLog (
                        ActivityId INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        LogDate TEXT NOT NULL, 
                        LogTime TEXT NOT NULL,
                        ActivityMessage TEXT NOT NULL,
                        IsStartEvent INTEGER NOT NULL
                    );";

                var createActivityLog = new SqliteCommand(activityTableCmd, connection);
                createActivityLog.ExecuteNonQuery();
            }

            // ******************** INITIAL STARTUP SCREEN ********************* //
            // Hide these panels until user logs in
            panel1.Visible = false;
            panel2.Visible = false;

            // Startup Login Panel
            panel_startUp = new Panel()
            {
                Dock = DockStyle.Fill,
            };

            this.Controls.Add(panel_startUp);
            panel_startUp.BringToFront();

            Label label_hello = new Label()
            {
                Location = new Point(35, 5),
                Size = new Size(200, 80),
                Text = "Hello Driver, let's create an account!",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };
            panel_startUp.Controls.Add(label_hello);

            // Name
            Label label_name = new Label()
            {
                Location = new Point(15, 100),
                AutoSize = true,
                Text = "Name: ",
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            panel_startUp.Controls.Add(label_name);

            textBox_firstName = new TextBox()
            {
                Location = new Point(18, 120),
                Size = new Size(110, 20),
                PlaceholderText = "First"
            };
            panel_startUp.Controls.Add(textBox_firstName);

            textBox_lastName = new TextBox()
            {
                Location = new Point(135, 120),
                Size = new Size(110, 20),
                PlaceholderText = "Last"
            };
            panel_startUp.Controls.Add(textBox_lastName);

            // Email
            Label label_email = new Label()
            {
                Location = new Point(15, 150),
                AutoSize = true,
                Text = "Email: ",
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            panel_startUp.Controls.Add(label_email);

            textBox_email = new TextBox()
            {
                Location = new Point(18, 170),
                Size = new Size(228, 20),
                PlaceholderText = "yourname@example.com"
            };
            panel_startUp.Controls.Add(textBox_email);

            // Password
            Label label_password = new Label()
            {
                Location = new Point(15, 200),
                AutoSize = true,
                Text = "Password: ",
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            panel_startUp.Controls.Add(label_password);

            Label label_passwordLength = new Label()
            {
                Location = new Point(15, 245),
                AutoSize = true,
                Text = "(Password must be atleast 6 characters) ",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = Color.Gray,
            };
            panel_startUp.Controls.Add(label_passwordLength);

            textBox_password = new TextBox()
            {
                Location = new Point(18, 220),
                Size = new Size(228, 20),
                UseSystemPasswordChar = true,
            };
            panel_startUp.Controls.Add(textBox_password);

            // Vehicle Vin 
            Label label_vin = new Label()
            {
                Location = new Point(15, 260),
                AutoSize = true,
                Text = "VIN: ",
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            panel_startUp.Controls.Add(label_vin);

            textBox_vin = new TextBox()
            {
                Location = new Point(18, 280),
                Size = new Size(228, 20),
                PlaceholderText = "ex: 1HGBH41JXMN109186"
            };
            panel_startUp.Controls.Add(textBox_vin);

            // Car type
            comboBox_vehicleType = new ComboBox()
            {
                Location = new Point(18, 325),
                Size = new Size(150, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                DropDownStyle = ComboBoxStyle.DropDownList,
            };

            comboBox_vehicleType.Items.Add("Select Vehicle Type");
            comboBox_vehicleType.Items.Add("Electric Vehicle");
            comboBox_vehicleType.Items.Add("Hybrid Vehicle");
            comboBox_vehicleType.Items.Add("Gas/Fuel Vehicle");

            comboBox_vehicleType.SelectedIndex = 0;
            panel_startUp.Controls.Add(comboBox_vehicleType);

            // Create account
            Button button_createAccount = new Button()
            {
                Location = new Point(33, 390),
                Size = new Size(200, 50),
                Text = "Create Account!",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.MediumPurple,
                FlatStyle = FlatStyle.Flat
            };
            button_createAccount.FlatAppearance.BorderSize = 2;
            button_createAccount.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_createAccount, 10);
            button_createAccount.Click += button_createAccount_Click;
            panel_startUp.Controls.Add(button_createAccount);

            // Show home tab on startup
            ShowTab(panel_home);
            ActiveTab(button_home);

            // ******* Diagnostics ******** //
            // Clock
            Label label_time = new Label()
            {
                Name = "Clock",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Black,
                Padding = new Padding(0, 3, 0, 0)
            };
            panel_diagnostics.Controls.Add(label_time);



            // Timer to update the time every second
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += (s, e) =>
            {
                label_time.Text = DateTime.Now.ToString("hh:mm tt");
            };
            timer.Start();

            panel_diagnostics.BackColor = Color.LightGray;

            // Wifi icons
            pictureBox_wifi.Image = Image.FromFile("icons\\wifi.png");
            pictureBox_wifi.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_connection.Image = Image.FromFile("icons\\connection.png");
            pictureBox_connection.SizeMode = PictureBoxSizeMode.StretchImage;

            // Battery icon
            pictureBox_battery.Image = Image.FromFile("icons\\battery.png");
            pictureBox_battery.SizeMode = PictureBoxSizeMode.StretchImage;

            // ^^^ END of diagnostics section ^^^ //

            /************************ TAB DESIGN *************************/
            /*
             * Section for designing each tab, tabs are seperated by name
             * Includes all tab customization
             */

            /*************** HOME TAB ******************/
            Label label_home = new Label()
            {
                Text = "Home",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(102, 20),
            };
            panel_home.Controls.Add(label_home);

            // Picture box for temperature image
            pictureBox_temp = new PictureBox()
            {
                Image = Image.FromFile("icons\\temperature.png"),
                Size = new Size(30, 30),
                Location = new Point(30, 50),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            panel_home.Controls.Add(pictureBox_temp);

            // Progress bar for temperature (this will be temp of the vehicle engine ** how to show that it is engine temp??)
            progressBar_temp = new ColoredProgressBar()
            {
                Location = new Point(8, 90),
                Size = new Size(75, 7),
                BarColor = Color.Purple,
                Minimum = 0,
                Maximum = 100,
                Value = 40
            };
            panel_home.Controls.Add(progressBar_temp);

            Label label_temperature = new Label()
            {
                Location = new Point(8, 100),
                AutoSize = true,
                Text = "Engine Temp",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
            };
            panel_home.Controls.Add(label_temperature);

            // Picture box for oil image
            pictureBox_oil = new PictureBox()
            {
                Image = Image.FromFile("icons\\oil.png"),
                Size = new Size(40, 40),
                Location = new Point(110, 50),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            panel_home.Controls.Add(pictureBox_oil);

            // Progress bar for oil level
            progressBar_oil = new ColoredProgressBar()
            {
                Location = new Point(97, 90),
                Size = new Size(75, 7),
                BarColor = Color.Purple,
                Minimum = 0,
                Maximum = 100,
                Value = 80
            };
            panel_home.Controls.Add(progressBar_oil);

            Label label_oil = new Label()
            {
                Location = new Point(105, 100),
                AutoSize = true,
                Text = "Oil Level",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
            };
            panel_home.Controls.Add(label_oil);

            // Picture box for fuel image
            pictureBox_fuel = new PictureBox()
            {
                Image = Image.FromFile("icons\\fuel.png"),
                Size = new Size(30, 30),
                Location = new Point(205, 51),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            panel_home.Controls.Add(pictureBox_fuel);

            // Progress bar for fuel level
            progressBar_fuel = new ColoredProgressBar()
            {
                Location = new Point(185, 90),
                Size = new Size(75, 7),
                BarColor = Color.Purple,
                Minimum = 0,
                Maximum = 100,
                Value = 60
            };
            panel_home.Controls.Add(progressBar_fuel);

            Label label_fuel = new Label()
            {
                Location = new Point(192, 100),
                AutoSize = true,
                Text = "Fuel Level",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
            };
            panel_home.Controls.Add(label_fuel);

            // Start/Stop Button
            RoundButton roundButton_startStop = new RoundButton
            {
                Size = new Size(100, 100),
                Location = new Point(80, 175),
                Text = "Start",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                BackColor = Color.Green,
                ForeColor = Color.Black,
            };
            roundButton_startStop.Click += roundButton_startStop_Click;
            panel_home.Controls.Add(roundButton_startStop);

            // Lock Doors
            RoundButton roundButton_lock = new RoundButton
            {
                Size = new Size(50, 50),
                Location = new Point(30, 225),
                Text = "Lock",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                BackColor = Color.MediumPurple,
                ForeColor = Color.Black,
            };
            roundButton_lock.Click += roundButton_lock_Click;
            roundButton_lock.MouseEnter += RoundButton_lockUnlock_MouseEnter;
            roundButton_lock.MouseLeave += RoundButton_lockUnlock_MouseLeave;
            panel_home.Controls.Add(roundButton_lock);

            // Unlock Doors
            RoundButton roundButton_unlock = new RoundButton
            {
                Size = new Size(50, 50),
                Location = new Point(180, 225),
                Text = "Unlock",
                Font = new Font("Segoe UI", 7, FontStyle.Regular),
                BackColor = Color.MediumPurple,
                ForeColor = Color.Black,
            };
            roundButton_unlock.Click += roundButton_unlock_Click;
            roundButton_unlock.MouseEnter += RoundButton_lockUnlock_MouseEnter;
            roundButton_unlock.MouseLeave += RoundButton_lockUnlock_MouseLeave;
            panel_home.Controls.Add(roundButton_unlock);

            // ******** Slide Up Panel Home Tab ************/
            // slide constraints
            collapsedTop = panel_home.Height - 125;
            expandedTop = panel_home.Height - 200;

            // Panel that will slide upwards
            slidePanel = new Panel
            {
                Height = 200,
                Width = this.ClientSize.Width,
                Top = collapsedTop,
                Left = 0,
                BackColor = Color.FromArgb(175, 175, 175),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            panel_home.Controls.Add(slidePanel);
            CornerRadius(slidePanel, 20);

            // Slide panel functionality
            slidePanel.MouseDown += SlidePanel_MouseDown;
            slidePanel.MouseMove += SlidePanel_MouseMove;
            slidePanel.MouseUp += SlidePanel_MouseUp;

            // Handle for slide panel
            Panel handleBar = new Panel
            {
                Size = new Size(100, 5),
                BackColor = Color.FromArgb(125, 125, 125),
                Location = new Point((slidePanel.Width - 100) / 2, 5),
                Cursor = Cursors.Hand
            };
            slidePanel.Controls.Add(handleBar);
            CornerRadius(handleBar, 20);

            // Handle bar functionality
            handleBar.MouseDown += SlidePanel_MouseDown;
            handleBar.MouseMove += SlidePanel_MouseMove;
            handleBar.MouseUp += SlidePanel_MouseUp;

            /********* Slide Panel Buttons **********/
            // Button for turning on and off vehicle headlights
            Button button_lights = new Button
            {
                Size = new Size(75, 50),
                Location = new Point(10, 20),
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                Text = "Turn Lights On",
                BackColor = Color.MediumPurple,
                FlatStyle = FlatStyle.Flat
            };
            button_lights.FlatAppearance.BorderSize = 2;
            button_lights.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_lights, 10);
            button_lights.Click += button_lights_Click;
            slidePanel.Controls.Add(button_lights);

            // Button for turning on and off vehicle hazards
            Button button_hazards = new Button
            {
                Size = new Size(75, 50),
                Location = new Point(95, 20),
                Font = new Font("Segoe UI", 7, FontStyle.Regular),
                Text = "Turn Hazards On",
                BackColor = Color.MediumPurple,
                FlatStyle = FlatStyle.Flat
            };
            button_hazards.FlatAppearance.BorderSize = 2;
            button_hazards.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_hazards, 10);
            button_hazards.Click += button_hazards_Click;
            slidePanel.Controls.Add(button_hazards);

            // Button for honking the vehicles horn 
            Button button_horn = new Button
            {
                Size = new Size(75, 50),
                Location = new Point(180, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Text = "Horn",
                BackColor = Color.MediumPurple,
                FlatStyle = FlatStyle.Flat
            };
            button_horn.FlatAppearance.BorderSize = 2;
            button_horn.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_horn, 10);
            button_horn.Click += button_horn_Click;
            slidePanel.Controls.Add(button_horn);

            // Button for opening vehicle windows
            Button button_windows = new Button
            {
                Size = new Size(75, 50),
                Location = new Point(50, 80),
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                Text = "Open Windows",
                BackColor = Color.MediumPurple,
                FlatStyle = FlatStyle.Flat
            };
            button_windows.FlatAppearance.BorderSize = 2;
            button_windows.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_windows, 10);
            button_windows.Click += button_windows_Click;
            slidePanel.Controls.Add(button_windows);

            // Button for turning on and off vehicle hazards
            Button button_trunk = new Button
            {
                Size = new Size(75, 50),
                Location = new Point(140, 80),
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Text = "Open Trunk",
                BackColor = Color.MediumPurple,
                FlatStyle = FlatStyle.Flat
            };
            button_trunk.FlatAppearance.BorderSize = 2;
            button_trunk.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_trunk, 10);
            button_trunk.Click += button_trunk_Click;
            slidePanel.Controls.Add(button_trunk);

            // ^^^ END of Home Tab section ^^^ //

            // *********************** Activity Tab *********************** //
            Label label_activity = new Label()
            {
                Text = "Activities",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(102, 20),
            };
            panel_home.Controls.Add(label_activity);

            // flow panel for list of activities
            flowlayoutPanel_activities = new FlowLayoutPanel() 
            {
                Size = new Size(285, 450),
                Location = new Point(Left, 100),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
            };
            panel_activity.Controls.Add(flowlayoutPanel_activities);

            // Load the activities into the activity tab
            LoadActivityLogs();


        }

        /************** GLOBAL METHODS *************/
        // ShowTab -- makes the tab (panel) visible depending on which button is clicked
        private void ShowTab(Panel visibleTab)
        {
            // Hide all tabs
            panel_activity.Visible = false;
            panel_status.Visible = false;
            panel_home.Visible = false;
            panel_trips.Visible = false;
            panel_profile.Visible = false;

            // Show parameter tab
            visibleTab.Visible = true;
        }

        // ShowNotification -- changes the notification color based on what type of notification it should be
        private void ShowNotification(string message, string type)
        {
            Color backColor;

            switch (type.ToLower())
            {
                case "success":
                    backColor = Color.Green;
                    break;
                case "stop":
                    backColor = Color.Red;
                    break;
                default:
                    backColor = Color.Gray;
                    break;
            }

            Notification notification = new Notification(message, backColor, this);
            notification.Show();
        }

        // CornerRadius -- Rounded corners for buttons and panels
        private void CornerRadius(Control control, int radius)
        {
            Rectangle bounds = control.ClientRectangle;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }

        // ActiveTab -- shows the user what tab they are currently on 
        private void ActiveTab(Button activeTab)
        {
            // Set all tabs text one color
            button_activity.ForeColor = Color.Black;
            button_status.ForeColor = Color.Black;
            button_home.ForeColor = Color.Black;
            button_trips.ForeColor = Color.Black;
            button_profile.ForeColor = Color.Black;

            // Set all icons to one color
            button_activity.ImageIndex = 0;
            button_status.ImageIndex = 1;
            button_home.ImageIndex = 2;
            button_trips.ImageIndex = 3;
            button_profile.ImageIndex = 4;


            // Set active text color
            activeTab.ForeColor = Color.MediumPurple;

            // Change the icon of the active button
            if (activeTab == button_activity)
                button_activity.ImageIndex = 5;
            else if (activeTab == button_status)
                button_status.ImageIndex = 6;
            else if (activeTab == button_home)
                button_home.ImageIndex = 7;
            else if (activeTab == button_trips)
                button_trips.ImageIndex = 8;
            else if (activeTab == button_profile)
                button_profile.ImageIndex = 9;
        }

        // InsertUser -- inserts user information into database
        private void InsertUser()
        {
            // EXTRA local validation here too just in case (defensive coding)
            if (string.IsNullOrEmpty(textBox_firstName.Text) ||
                string.IsNullOrEmpty(textBox_lastName.Text) ||
                string.IsNullOrEmpty(textBox_email.Text) ||
                string.IsNullOrEmpty(textBox_password.Text) ||
                string.IsNullOrEmpty(textBox_vin.Text) ||
                comboBox_vehicleType.SelectedIndex == 0)
            {
                ShowNotification("Please fill in all fields correctly.", "stop");
                return; // Don't insert anything
            }

            string dbPath = "carstarter.db";
            string connectionString = $"Data Source={dbPath};";

            string insertQuery = @"
                    INSERT INTO Users (FirstName, LastName, Email, Password, Vin, CarType)
                    VALUES (@FirstName, @LastName, @Email, @Password, @Vin, @CarType);

                    SELECT last_insert_rowid();";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var cmd = new SqliteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@FirstName", textBox_firstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", textBox_lastName.Text);
                    cmd.Parameters.AddWithValue("@Email", textBox_email.Text);
                    cmd.Parameters.AddWithValue("@Password", textBox_password.Text);
                    cmd.Parameters.AddWithValue("@Vin", textBox_vin.Text);
                    cmd.Parameters.AddWithValue("@CarType", comboBox_vehicleType.SelectedItem.ToString());

                    try
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            Session.CurrentUserID = Convert.ToInt32(result);
                        }

                        ShowTab(panel_home);
                        panel_startUp.Visible = false;
                        panel1.Visible = true;
                        panel2.Visible = true;
                        ActiveTab(button_home);
                        ShowNotification($"Welcome {textBox_firstName.Text}, to Piper Autostart!", "success");
                        panel_startUp.Dispose();
                    }
                    catch (SqliteException ex)
                    {
                        if (ex.Message.Contains("UNIQUE constraint failed"))
                        {
                            ShowNotification("An account with this email already exists!", "stop");
                        }
                        else
                        {
                            MessageBox.Show("Error creating account: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        // LogActivity -- logs a performed activity to the database
        private void LogActivity(string message, bool isStartEvent)
        {
            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                        INSERT INTO ActivityLog (UserId, LogDate, LogTime, ActivityMessage, IsStartEvent)
                        VALUES ($UserId, $LogDate, $LogTime, $ActivityMessage, $IsStartEvent)";

                command.Parameters.AddWithValue("$UserId", Session.CurrentUserID);
                command.Parameters.AddWithValue("$LogDate", DateTime.Now.ToString("MMM-dd-yyyy"));
                command.Parameters.AddWithValue("$LogTime", DateTime.Now.ToString("hh:mm:ss tt"));
                command.Parameters.AddWithValue("$ActivityMessage", message);
                command.Parameters.AddWithValue("$IsStartEvent", isStartEvent ? 1 : 0);

                command.ExecuteNonQuery();

                DisplayActivity($"{DateTime.Now:MMM-dd-yyyy hh:mm:ss tt} - {message}", isStartEvent);
            }
        }

        // DisplayActivity -- displays the activities in the activity tab
        private void DisplayActivity(string message, bool isStartEvent)
        {
            Panel panel_activity = new Panel
            {
                Width = flowlayoutPanel_activities.Width - 25,
                Height = 50,
                Margin = new Padding(10),
                BackColor = isStartEvent ? Color.MediumPurple : Color.IndianRed,
                BorderStyle = BorderStyle.FixedSingle,
            };
            
            

            Label label = new Label
            {
                Text = $"{DateTime.Now:hh:mm:ss tt} - {message}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.White,
                Padding = new Padding(10, 15, 10, 10)
            };

            panel_activity.Controls.Add(label);
            flowlayoutPanel_activities.Controls.Add(panel_activity);
            flowlayoutPanel_activities.Controls.SetChildIndex(panel_activity, 0); // Move to top
        }

        // LoadActivityLog -- loads each activity from database and uses displayActivity to display them to the activity tab
        private void LoadActivityLogs()
        {
            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT LogDate, LogTime, ActivityMessage, IsStartEvent
                    FROM ActivityLog
                    WHERE UserId = $UserId
                    ORDER BY ActivityId DESC
                    LIMIT 50";

                command.Parameters.AddWithValue("$UserId", Session.CurrentUserID); 

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string date = reader.GetString(0);
                        string time = reader.GetString(1);
                        string message = reader.GetString(2);
                        bool isStartEvent = reader.GetInt32(3) == 1;

                        DisplayActivity($"{date} {time} - {message}", isStartEvent);
                    }
                }
            }
        }



        // TruncateTables -- deletes table information on close
        private void TruncateTables()
        {
            try
            {
                using (var connection = new SqliteConnection("Data Source=carstarter.db"))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                                              DELETE FROM Users;
                                              DELETE FROM ActivityLog;
                                              ";
                                              /*DELETE FROM Logs;
                                              DELETE FROM sqlite_sequence;
                                                "; */
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        

        // ********************** START-UP PANEL EVENT HANDLERS *************************** //
        private void button_createAccount_Click(object sender, EventArgs e)
        {
            // Check if any of the text boxes are empty
            if (string.IsNullOrEmpty(textBox_firstName.Text) ||
               (string.IsNullOrEmpty(textBox_lastName.Text) ||
               (string.IsNullOrEmpty(textBox_email.Text) ||
               (string.IsNullOrEmpty(textBox_password.Text) ||
               (string.IsNullOrEmpty(textBox_vin.Text))))))
            {
                ShowNotification("Please fill in all fields", "stop");
                return;
            }

            // Check if user selected a car type
            if (comboBox_vehicleType.SelectedIndex <= 0)
            {
                ShowNotification("Please select a vehicle type (via the drop down)", "stop");
                return;
            }

            // Check if email textbox somewhat resembles an email format
            if (!textBox_email.Text.Contains("@") || !textBox_email.Text.Contains("."))
            {
                ShowNotification("Please enter a valid email address.", "stop");
                return;
            }

            // Check if password entered is atleast 6 characters
            if (textBox_password.Text.Length < 6)
            {
                ShowNotification("Password must be at least 6 characters long.", "stop");
                return;
            }

            if (textBox_vin.Text.Length < 17)
            {
                ShowNotification("VIN must be exactly 17 characters long.", "stop");
                return;
            }

            InsertUser();

        }

        // ^^^^^^ END ^^^^^^ //

        /************************ BOTTOM TAB EVENT HANDLERS ********************************/
        private void button_activity_Click(object sender, EventArgs e)
        {
            ShowTab(panel_activity);
            ActiveTab(button_activity);
        }

        private void button_status_Click(object sender, EventArgs e)
        {
            ShowTab(panel_status);
            ActiveTab(button_status);
        }

        private void button_home_Click(object sender, EventArgs e)
        {
            ShowTab(panel_home);
            ActiveTab(button_home);
        }

        private void button_trips_Click(object sender, EventArgs e)
        {
            ShowTab(panel_trips);
            ActiveTab(button_trips);
        }

        private void button_profile_Click(object sender, EventArgs e)
        {
            ShowTab(panel_profile);
            ActiveTab(button_profile);
        }

        /*************************** HOME PAGE EVENT HANDLERS *****************************/
        private void roundButton_startStop_Click(object sender, EventArgs e)
        {
            var senderButton = (RoundButton)sender;

            isStartStopToggled = !isStartStopToggled;

            if (isStartStopToggled)
            {
                senderButton.BackColor = Color.Red;
                senderButton.Text = "STOP";
                senderButton.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                ShowNotification("Vehicle Started Successfully", "success");
                LogActivity("Vehicle Started", true);
            }
            else
            {
                senderButton.BackColor = Color.Green;
                senderButton.Text = "Start";
                senderButton.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                ShowNotification("Vehicle Stopped Succesfuly", "stop");
                LogActivity("Vehicle Stopped", false);
            }

            senderButton.Invalidate();
        }

        private void RoundButton_lockUnlock_MouseEnter(object sender, EventArgs e)
        {
            var senderButton = (RoundButton)sender;
            senderButton.BackColor = Color.FromArgb(147, 85, 219);
        }

        private void RoundButton_lockUnlock_MouseLeave(object sender, EventArgs e)
        {
            var senderButton = (RoundButton)sender;
            senderButton.BackColor = Color.MediumPurple;
        }

        private void roundButton_lock_Click(object sender, EventArgs e)
        {
            ShowNotification("Vehicle Locked Successfully", "success");
            LogActivity("Vehicle Locked", false);
        }

        private void roundButton_unlock_Click(object sender, EventArgs e)
        {
            ShowNotification("Vehicle Unlocked Successfully", "success");
            LogActivity("Vehicle Unlocked", true);
        }

        private void SlidePanel_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragStart = Cursor.Position.Y;
            originalPanelTop = slidePanel.Top;
        }

        private void SlidePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging) return;

            int delta = Cursor.Position.Y - dragStart;
            int newTop = originalPanelTop + delta;

            newTop = Math.Max(expandedTop, Math.Min(collapsedTop, newTop));

            slidePanel.Top = newTop;
            slidePanel.Height = this.ClientSize.Height - newTop;
        }

        private void SlidePanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void button_lights_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;

            isLightsToggled = !isLightsToggled;

            if (isLightsToggled)
            {
                senderButton.BackColor = Color.Red;
                senderButton.Text = "Turn Lights OFF";
                senderButton.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                ShowNotification("Vehicle Lights Turned On Successfully", "success");
                LogActivity("Vehicle Lights Turned On", true);
            }
            else
            {
                senderButton.BackColor = Color.MediumPurple;
                senderButton.Text = "Turn Lights On";
                senderButton.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                ShowNotification("Vehicle Lights Turned OFF Successfully", "stop");
                LogActivity("Vehicle Lights Turned Off", false);
            }
        }

        private void button_hazards_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;

            isHazardsToggled = !isHazardsToggled;

            if (isHazardsToggled)
            {
                senderButton.BackColor = Color.Red;
                senderButton.Text = "Turn Hazards OFF";
                senderButton.Font = new Font("Segoe UI", 7, FontStyle.Regular);
                ShowNotification("Vehicle Hazards Turned On Successfully", "success");
                LogActivity("Vehicle Hazards Turned On", true);
            }
            else
            {
                senderButton.BackColor = Color.MediumPurple;
                senderButton.Text = "Turn Hazards On";
                senderButton.Font = new Font("Segoe UI", 7, FontStyle.Regular);
                ShowNotification("Vehicle Hazards Turned OFF Successfully", "stop");
                LogActivity("Vehicle Hazards Turned Off", false);
            }
        }

        private void button_horn_Click(object sender, EventArgs e)
        {
            ShowNotification("Vehicle Horn Honked Successfully", "success");
            LogActivity("Vehicle Horn Honked", true);
        }

        private void button_windows_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;

            isWindowsToggled = !isWindowsToggled;

            if (isWindowsToggled)
            {
                senderButton.BackColor = Color.Red;
                senderButton.Text = "CLOSE windows";
                senderButton.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                ShowNotification("Vehicle Windows Opened Successfully", "success");
                LogActivity("Vehicle Windows Opened", true);
            }
            else
            {
                senderButton.BackColor = Color.MediumPurple;
                senderButton.Text = "Open Windows";
                senderButton.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                ShowNotification("Vehicle Windows CLOSED Successfully", "stop");
                LogActivity("Vehicle Windows Closed", false);
            }
        }

        private void button_trunk_Click(object sender, EventArgs e)
        {
            ShowNotification("Vehicle Trunk Opened Successfully", "success");
            LogActivity("Vehicle Trunk Opened", true);
        }

        /************************** ROUND BUTTON CLASS *********************************/
        // Class for making normal buttons into round buttons for home tab
        public class RoundButton : Button
        {
            public RoundButton()
            {
                this.FlatStyle = FlatStyle.Flat;
                this.FlatAppearance.BorderSize = 0;
                this.BackColor = Color.Green;
                this.ForeColor = Color.White;


                this.Text = "Click Me";

                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                              ControlStyles.UserPaint |
                              ControlStyles.ResizeRedraw |
                              ControlStyles.OptimizedDoubleBuffer |
                              ControlStyles.SupportsTransparentBackColor, true);
            }


            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle buttonRect = this.ClientRectangle;
                int edgeWidth = GetEdgeWidth(buttonRect);

                FillBackground(g, buttonRect);

                ShrinkShape(ref buttonRect, edgeWidth);

                DrawButton(g, buttonRect);

                DrawText(g, buttonRect);

                SetClickableRegion();
            }

            private int GetEdgeWidth(Rectangle rect)
            {
                return Math.Min(rect.Width, rect.Height) / 10;
            }

            private void FillBackground(Graphics g, Rectangle rect)
            {
                using (Brush brush = new SolidBrush(this.Parent.BackColor))
                {
                    g.FillRectangle(brush, rect);
                }
            }

            private void ShrinkShape(ref Rectangle rect, int amount)
            {
                rect.Inflate(-amount, -amount);
            }

            private void DrawButton(Graphics g, Rectangle rect)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(rect);

                    using (PathGradientBrush brush = new PathGradientBrush(path))
                    {

                        brush.CenterColor = this.BackColor;
                        brush.SurroundColors = new Color[] { ControlPaint.Light(this.BackColor) };
                        g.FillPath(brush, path);
                    }

                    using (Pen border = new Pen(Color.Black, 2))
                    {
                        g.DrawEllipse(border, rect);
                    }
                }
            }

            private void DrawText(Graphics g, Rectangle rect)
            {
                TextRenderer.DrawText(g, this.Text, this.Font, rect, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            private void SetClickableRegion()
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(this.ClientRectangle);
                    this.Region = new Region(path);
                }
            }
        }

        // ColoredProgressBar class for making a colored progress bar instead of green
        public class ColoredProgressBar : ProgressBar
        {
            public Color BarColor { get; set; } = Color.MediumPurple;

            public ColoredProgressBar()
            {
                this.SetStyle(ControlStyles.UserPaint, true);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Rectangle rec = e.ClipRectangle;
                if (ProgressBarRenderer.IsSupported)
                    ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rec);

                rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
                rec.Height -= 4;

                using (SolidBrush brush = new SolidBrush(BarColor))
                {
                    e.Graphics.FillRectangle(brush, 2, 2, rec.Width, rec.Height);
                }
            }
        }
    }
}
