using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static WinFormsCarStarter.MainForm;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WinFormsCarStarter
{
    public partial class MainForm : Form
    {
        // **** Global variables (private to MainForm) **** //
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

        // Main Functionality Tab Setup
        private Button button_profile = new Button();
        private Button button_trips = new Button();
        private Button button_home = new Button();
        private Button button_status = new Button();
        private Button button_activity = new Button();
        private Panel panel_activity = new Panel();
        private Panel panel_status = new Panel();
        private Panel panel_home = new Panel();
        private Panel panel_trips = new Panel();
        private Panel panel_profile = new Panel();
        private Panel panel_startUp = new Panel();

        // Startup login variables
        private TextBox textBox_firstName = new TextBox();
        private TextBox textBox_lastName = new TextBox();
        private TextBox textBox_email = new TextBox();
        private TextBox textBox_password = new TextBox();
        private TextBox textBox_vin = new TextBox();
        private ComboBox comboBox_vehicleType = new ComboBox();

        // Home Tab Variables
        private ImageList imageList = new ImageList();
        private ImageList active_imageList = new ImageList();
        private ProgressBar progressBar_temp = new ProgressBar();
        private ProgressBar progressBar_oil = new ProgressBar();
        private ProgressBar progressBar_fuel = new ProgressBar();
        private Panel slidePanel = new Panel();

        // Activity Tab Variables
        private ComboBox comboBox_activityDate = new ComboBox();
        private FlowLayoutPanel flowlayoutPanel_activities = new FlowLayoutPanel();

        // Status Tab Variables
        private Panel panel_editVehicle = new Panel();
        private TextBox textBox_tirePressure = new TextBox();
        private TextBox textBox_oilLevel = new TextBox();
        private TextBox textBox_batteryLife = new TextBox();
        private TextBox textBox_miles = new TextBox();
        private TextBox textBox_engineTmp = new TextBox();
        private TextBox textBox_internalTmp = new TextBox();
        private Button button_saveChanges = new Button();
        private FlowLayoutPanel flowLayoutPanel_vehicleStatus = new FlowLayoutPanel();

        // Trips Tab Variables
        private FlowLayoutPanel flowLayoutPanel_trips = new FlowLayoutPanel();
        private ComboBox comboBox_tripDate = new ComboBox();

        // Profile Tab Variables
        private FlowLayoutPanel flowLayoutPanel_profile = new FlowLayoutPanel();
        private FlowLayoutPanel flowLayoutPanel_removeVehicles = new FlowLayoutPanel();
        private Panel divider = new Panel();
        private Panel panel_editProfile;
        private Panel panel_addVehicle;
        private Panel panel_removeVehicle;
        private Label label_profile = new Label();
        private Label label_fullName = new Label();
        private Label label_profileEmail = new Label();
        private Label label_vehicleType = new Label();
        private Label label_profileVin = new Label();
        private PictureBox profilePic = new PictureBox();
        private TextBox textBox_make;
        private TextBox textBox_model;
        private TextBox textBox_year;
        private TextBox textBox_profileVin;
        private Button button_saveVehicle;
        private Button button_editProfile = new Button();
        private Button button_logout = new Button();
        private Button button_addVehicle;
        private Button button_removeVehicle;
        private Button button_accessibility;
        


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

                var enforceForeignKey = connection.CreateCommand();
                enforceForeignKey.CommandText = "PRAGMA foreign_keys = ON;";
                enforceForeignKey.ExecuteNonQuery();

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

                var dropActivities = new SqliteCommand("DROP TABLE IF EXISTS ActivityLog;", connection);
                dropActivities.ExecuteNonQuery();


                string activityTableCmd = @"
                    CREATE TABLE IF NOT EXISTS ActivityLog (
                        ActivityId INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        LogDate DATETIME NOT NULL, 
                        ActivityMessage TEXT NOT NULL,
                        IsStartEvent INTEGER NOT NULL,
                        FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE
                    );";

                var createActivityLog = new SqliteCommand(activityTableCmd, connection);
                createActivityLog.ExecuteNonQuery();

                var dropVehicle = new SqliteCommand("DROP TABLE IF EXISTS VehicleLog;", connection);
                dropVehicle.ExecuteNonQuery();

                string VehicleTableCmd = @"
                    CREATE TABLE IF NOT EXISTS VehicleLog (
                        Make TEXT NOT NULL,
                        Model TEXT NOT NULL,
                        Year INTEGER NOT NULL,
                        VehicleId INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        TirePressure TEXT NOT NULL, 
                        OilLevel TEXT NOT NULL,
                        BatteryLife TEXT NOT NULL,
                        Miles FLOAT NOT NULL,
                        EngineTmp INTEGER NOT NULL,
                        InternalTmp INTEGER NOT NULL,
                        FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE
                    );";

                var createVehicleLog = new SqliteCommand(VehicleTableCmd, connection);
                createVehicleLog.ExecuteNonQuery();

                var dropTrips = new SqliteCommand("DROP TABLE IF EXISTS TripsLog;", connection);
                dropTrips.ExecuteNonQuery();

                string TripsTableCmd = @"
                    CREATE TABLE IF NOT EXISTS TripsLog (
                        TripId INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        TripDate DATETIME NOT NULL, 
                        LocatA TEXT NOT NULL,
                        LocatB TEXT NOT NULL,
                        FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE
                    );";

                var createTripsLog = new SqliteCommand(TripsTableCmd, connection);
                createTripsLog.ExecuteNonQuery();
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

            // ***************************************************** Diagnostics ****************************************************** //
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

            /****************************************************** HOME TAB ***************************************************************/
            Label label_home = new Label()
            {
                Text = "Home",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(102, 20),
            };
            panel_home.Controls.Add(label_home);

            // Picture box for temperature image
            PictureBox pictureBox_temp = new PictureBox()
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
            PictureBox pictureBox_oil = new PictureBox()
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
            PictureBox pictureBox_fuel = new PictureBox()
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

            // *************************************************** Activity Tab ********************************************************************* //
            Label label_activity = new Label()
            {
                Text = "Activities",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(95, 20),
            };
            panel_activity.Controls.Add(label_activity);

            Label label_timePeriod = new Label()
            {
                Text = "Time Period:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(10, 50),
            };
            panel_activity.Controls.Add(label_timePeriod);

            comboBox_activityDate = new ComboBox()
            {
                Location = new Point(10, 73),
                Size = new Size(150, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                DropDownStyle = ComboBoxStyle.DropDownList,
            };

            comboBox_activityDate.Items.Add("Today");
            comboBox_activityDate.Items.Add("Previous Month");
            comboBox_activityDate.Items.Add("Year to Date");
            comboBox_activityDate.Items.Add("All");

            comboBox_activityDate.SelectedIndex = 0;
            comboBox_activityDate.SelectedIndexChanged += ComboBox_activityDate_SelectedIndexChanged;
            panel_activity.Controls.Add(comboBox_activityDate);

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

            // ^^^^^^^^^^^^^^^^^^^ END ACTIVITY ^^^^^^^^^^^^^^^^^^^ //

            // ******************************************************* STATUS TAB ******************************************************* //
            panel_status.BackColor = Color.White;
            BuildEditVehiclePanel();

            Label label_status = new Label()
            {
                Location = new Point(100, 20),
                AutoSize = true,
                Text = "Status",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
            };
            panel_status.Controls.Add(label_status);

            PictureBox pictureBox_vehicleStatus = new PictureBox()
            {
                Image = Image.FromFile("icons\\image_vehicleStatus.jpg"),
                Location = new Point(10, 45),
                Size = new Size(235, 170),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            panel_status.Controls.Add(pictureBox_vehicleStatus);

            flowLayoutPanel_vehicleStatus = new FlowLayoutPanel()
            {
                Size = new Size(panel_status.Width, 175),
                Location = new Point(Left, 220),
                BackColor = Color.White,
                FlowDirection = FlowDirection.TopDown,
            };
            panel_status.Controls.Add(flowLayoutPanel_vehicleStatus);

            Button button_updateVehicle = new Button
            {
                Text = "Update Vehicle Info?",
                Location = new Point(10, 400),
                Width = 200,
                Height = 30,
                Margin = new Padding(10),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.MediumPurple,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
            };
            button_updateVehicle.FlatAppearance.BorderSize = 2;
            button_updateVehicle.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_updateVehicle, 10);
            button_updateVehicle.Click += Button_updateVehicle_Click;
            panel_status.Controls.Add(button_updateVehicle);

            // ^^^^^^^^^^^^^^^^^^ END ^^^^^^^^^^^^^^^^^^^^ //

            // ********************************************* Trips Tab ************************************************** //
            panel_trips.BackColor = Color.White;

            Label label_trips = new Label()
            {
                Location = new Point(100, 20),
                AutoSize = true,
                Text = "Trips",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
            };
            panel_trips.Controls.Add(label_trips);

            PictureBox pictureBox_gps = new PictureBox()
            {
                Image = Image.FromFile("icons\\gpsimg.png"),
                Location = new Point(10, 45),
                Size = new Size(235, 170),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            panel_trips.Controls.Add(pictureBox_gps);

            flowLayoutPanel_trips = new FlowLayoutPanel()
            {
                Size = new Size(panel_status.Width, 200),
                Location = new Point(Left, 240),
                BackColor = Color.White,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                WrapContents = false,
            };
            panel_trips.Controls.Add(flowLayoutPanel_trips);

            comboBox_tripDate = new ComboBox()
            {
                Location = new Point(10, 215),
                Size = new Size(150, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                DropDownStyle = ComboBoxStyle.DropDownList,
            };

            comboBox_tripDate.Items.Add("Today");
            comboBox_tripDate.Items.Add("Previous Month");
            comboBox_tripDate.Items.Add("Year to Date");
            comboBox_tripDate.Items.Add("All");


            comboBox_tripDate.SelectedIndex = 0;
            comboBox_tripDate.SelectedIndexChanged += ComboBox_tripDate_SelectedIndexChanged;
            panel_trips.Controls.Add(comboBox_tripDate);

            // *********************************** PROFILE TAB ******************************** //
            panel_profile.BackColor = Color.White;
            BuildProfileTab();
            

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
            panel_startUp.Visible = false;

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

        // TruncateTables -- deletes table information on close
        private void TruncateTables()
        {
            try
            {
                using (var connection = new SqliteConnection("Data Source=carstarter.db"))
                {
                    connection.Open();
                    var enforceForeignKey = connection.CreateCommand();
                    enforceForeignKey.CommandText = "PRAGMA foreign_keys = ON;";
                    enforceForeignKey.ExecuteNonQuery();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                                              DELETE FROM Users;
                                              DELETE FROM ActivityLog;
                                              DELETE FROM VehicleLog;
                                              ";
                        /*DELETE FROM TripsLog;
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


        // ************************* ACTIVITY TAB METHODS ************************** //
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
                return; 
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

                var enforceForeignKey = connection.CreateCommand();
                enforceForeignKey.CommandText = "PRAGMA foreign_keys = ON;";
                enforceForeignKey.ExecuteNonQuery();

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
                            InsertFakeActivities();
                            InsertRandomVehicleData(Session.CurrentUserID);
                            InsertFakeTrips();
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

                var enforceForeignKey = connection.CreateCommand();
                enforceForeignKey.CommandText = "PRAGMA foreign_keys = ON;";
                enforceForeignKey.ExecuteNonQuery();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO ActivityLog (UserId, LogDate, ActivityMessage, IsStartEvent)
                    VALUES ($UserId, $LogDate, $ActivityMessage, $IsStartEvent)";

                command.Parameters.AddWithValue("$UserId", Session.CurrentUserID);
                command.Parameters.AddWithValue("$LogDate", DateTime.Now);
                command.Parameters.AddWithValue("$ActivityMessage", message); 
                command.Parameters.AddWithValue("$IsStartEvent", isStartEvent ? 1 : 0);

                command.ExecuteNonQuery();

                DisplayActivity($"{DateTime.Now:MMM-dd-yyyy hh:mm:ss tt} - {message}", isStartEvent);
            }
        }

        // DisplayActivity -- displays the activities in the activity tab
        private void DisplayActivity(string message, bool isStartEvent)
        {
            Panel panel = new Panel
            {
                Size = new Size(246, 50),
                Margin = new Padding(7),
                BackColor = isStartEvent ? Color.MediumPurple : Color.IndianRed,
                BorderStyle = BorderStyle.FixedSingle,
            };
            

            Label label = new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 7, FontStyle.Regular),
                ForeColor = Color.White,
                Padding = new Padding(8, 12, 10, 8)
            };

            panel.Controls.Add(label);
            CornerRadius(panel, 10);
            flowlayoutPanel_activities.Controls.Add(panel);
            flowlayoutPanel_activities.Controls.SetChildIndex(panel, 0); 


        }

        // LoadActivityLog -- loads each activity from database and uses displayActivity to display them to the activity tab
        private void LoadActivityLogs()
        {
            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var enforceForeignKey = connection.CreateCommand();
                enforceForeignKey.CommandText = "PRAGMA foreign_keys = ON;";
                enforceForeignKey.ExecuteNonQuery();

                var command = connection.CreateCommand();

                string filter = comboBox_activityDate.SelectedItem.ToString();
                string query = @"
                    SELECT LogDate, ActivityMessage, IsStartEvent
                    FROM ActivityLog
                    WHERE UserId = $UserId ";

                // Apply date filter
                if (filter == "Today")
                {
                    query += "AND date(LogDate) = date('now') ";
                }
                else if (filter == "Previous Month")
                {
                    DateTime now = DateTime.Now;
                    DateTime firstDayLastMonth = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                    DateTime lastDayLastMonth = new DateTime(now.Year, now.Month, 1).AddDays(-1);

                    string firstDay = firstDayLastMonth.ToString("yyyy-MM-dd");
                    string lastDay = lastDayLastMonth.ToString("yyyy-MM-dd");

                    query += "AND date(LogDate) BETWEEN date($FirstDay) AND date($LastDay) ";
                    command.Parameters.AddWithValue("$FirstDay", firstDay);
                    command.Parameters.AddWithValue("$LastDay", lastDay);
                }
                else if (filter == "Last Year")
                {
                    query += "AND strftime('%Y', LogDate) = strftime('%Y', 'now') ORDER BY LogDate DESC ";
                }

                query += "ORDER BY LogDate DESC;";
                command.CommandText = query;
                command.Parameters.AddWithValue("$UserId", Session.CurrentUserID);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string logDateTime = reader.GetString(0); // "2025-04-27 15:39:00"
                        string message = reader.GetString(1);     // "Vehicle Started"
                        bool isStartEvent = reader.GetInt32(2) == 1;

                        DateTime parsedDate = DateTime.Parse(logDateTime);

                        // Correctly combine on display:
                        DisplayActivity($"{parsedDate:MMM-dd-yyyy hh:mm:ss tt} - {message}", isStartEvent);
                    }
                }
            }
        }

        private void InsertFakeActivities()
        {
            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();
                var enforceForeignKey = connection.CreateCommand();
                enforceForeignKey.CommandText = "PRAGMA foreign_keys = ON;";
                enforceForeignKey.ExecuteNonQuery();

                var command = connection.CreateCommand();
                DateTime now = DateTime.Now;
                DateTime firstDayLastMonth = new DateTime(now.Year, now.Month, 1).AddMonths(-1);

                command.CommandText = @"
                    INSERT INTO ActivityLog (UserId, LogDate, ActivityMessage, IsStartEvent) VALUES
                    ($UserId1, $LogDate1, $Message1, $IsStartEvent1),
                    ($UserId2, $LogDate2, $Message2, $IsStartEvent2),
                    ($UserId3, $LogDate3, $Message3, $IsStartEvent3);";

                command.Parameters.AddWithValue("$UserId1", Session.CurrentUserID);
                command.Parameters.AddWithValue("$LogDate1", firstDayLastMonth.AddDays(5));
                command.Parameters.AddWithValue("$Message1", "Vehicle Started (Test)");
                command.Parameters.AddWithValue("$IsStartEvent1", 1);

                command.Parameters.AddWithValue("$UserId2", Session.CurrentUserID);
                command.Parameters.AddWithValue("$LogDate2", firstDayLastMonth.AddDays(10));
                command.Parameters.AddWithValue("$Message2", "Horn Activated (Test)");
                command.Parameters.AddWithValue("$IsStartEvent2", 1);

                command.Parameters.AddWithValue("$UserId3", Session.CurrentUserID);
                command.Parameters.AddWithValue("$LogDate3", firstDayLastMonth.AddDays(15));
                command.Parameters.AddWithValue("$Message3", "Lights Toggled (Test)");
                command.Parameters.AddWithValue("$IsStartEvent3", 0);

                command.ExecuteNonQuery();
            }
        }

        // ************************************** STATUS TAB METHODS ********************************************** //
        private void LoadVehicleStatus()
        {
            //flowLayoutPanel_vehicleStatus.Controls.Clear(); 

            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT Make, Model, Year, TirePressure, OilLevel, BatteryLife, Miles, EngineTmp, InternalTmp
                    FROM VehicleLog
                    WHERE UserId = $UserId
                    LIMIT 1;";

                command.Parameters.AddWithValue("$UserId", Session.CurrentUserID);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        AddStatusLabel($"Make: {reader.GetString(0)}");
                        AddStatusLabel($"Model: {reader.GetString(1)}");
                        AddStatusLabel($"Year: {reader.GetInt32(2)}");
                        AddStatusLabel($"Tire Pressure: {reader.GetString(3)}");
                        AddStatusLabel($"Oil Level: {reader.GetString(4)}");
                        AddStatusLabel($"Battery Life: {reader.GetString(5)}");
                        AddStatusLabel($"Miles: {reader.GetFloat(6)}");
                        AddStatusLabel($"Engine Temperature: {reader.GetInt32(7)} °C");
                        AddStatusLabel($"Internal Temperature: {reader.GetInt32(8)} °C");
                    }
                    else
                    {
                        AddStatusLabel("No vehicle information found.");
                    }
                }
            }
        }

        private void AddStatusLabel(string text)
        {
            Label label = new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Black,
                Margin = new Padding(10, 5, 10, 5),
            };

            flowLayoutPanel_vehicleStatus.Controls.Add(label);
        }

        private void InsertRandomVehicleData(int userId)
        {
            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO VehicleLog (Make, Model, Year, UserId, TirePressure, OilLevel, BatteryLife, Miles, EngineTmp, InternalTmp)
                    VALUES ($Make, $Model, $Year, $UserId, $TirePressure, $OilLevel, $BatteryLife, $Miles, $EngineTmp, $InternalTmp);";

                Random rand = new Random();

                command.Parameters.AddWithValue("$Make", "Toyota");
                command.Parameters.AddWithValue("$Model", "Camry");
                command.Parameters.AddWithValue("$Year", 2020);
                command.Parameters.AddWithValue("$UserId", userId);
                command.Parameters.AddWithValue("$TirePressure", $"{rand.Next(30, 36)} PSI"); 
                command.Parameters.AddWithValue("$OilLevel", rand.NextDouble() < 0.8 ? "Good" : "Low"); 
                command.Parameters.AddWithValue("$BatteryLife", $"{rand.Next(70, 100)}%"); 
                command.Parameters.AddWithValue("$Miles", Math.Round(rand.NextDouble() * 20000 + 10000, 2)); 
                command.Parameters.AddWithValue("$EngineTmp", rand.Next(70, 110)); 
                command.Parameters.AddWithValue("$InternalTmp", rand.Next(18, 30)); 

                command.ExecuteNonQuery();
            }
        }

        private void LoadVehicleDataIntoEditFields()
        {
            flowLayoutPanel_vehicleStatus.Controls.Clear(); 

            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT TirePressure, OilLevel, BatteryLife, Miles, EngineTmp, InternalTmp
                    FROM VehicleLog
                    WHERE UserId = $UserId
                    LIMIT 1;";

                command.Parameters.AddWithValue("$UserId", Session.CurrentUserID);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox_tirePressure.Text = reader.GetString(0);
                        textBox_oilLevel.Text = reader.GetString(1);
                        textBox_batteryLife.Text = reader.GetString(2);
                        textBox_miles.Text = reader.GetFloat(3).ToString();
                        textBox_engineTmp.Text = reader.GetInt32(4).ToString();
                        textBox_internalTmp.Text = reader.GetInt32(5).ToString();
                    }
                }
            }
        }

        private void Button_saveChanges_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(textBox_miles.Text, out float miles))
            {
                MessageBox.Show("Please enter a valid number for Miles.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBox_engineTmp.Text, out int engineTemp))
            {
                MessageBox.Show("Please enter a valid integer for Engine Temperature.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBox_internalTmp.Text, out int internalTemp))
            {
                MessageBox.Show("Please enter a valid integer for Internal Temperature.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                UPDATE VehicleLog
                SET TirePressure = $TirePressure,
                OilLevel = $OilLevel,
                BatteryLife = $BatteryLife,
                Miles = $Miles,
                EngineTmp = $EngineTmp,
                InternalTmp = $InternalTmp
                WHERE UserId = $UserId;";

                command.Parameters.AddWithValue("$TirePressure", textBox_tirePressure.Text);
                command.Parameters.AddWithValue("$OilLevel", textBox_oilLevel.Text);
                command.Parameters.AddWithValue("$BatteryLife", textBox_batteryLife.Text);
                command.Parameters.AddWithValue("$Miles", miles);
                command.Parameters.AddWithValue("$EngineTmp", engineTemp);
                command.Parameters.AddWithValue("$InternalTmp", internalTemp);
                command.Parameters.AddWithValue("$UserId", Session.CurrentUserID);

                command.ExecuteNonQuery();
            }

            LogActivity("Updated vehicle information.", true);

            ShowNotification("Vehicle information updated successfully!", "Success");

            panel_editVehicle.Visible = false;
            flowLayoutPanel_vehicleStatus.Visible = true;
            LoadVehicleStatus(); // Reload updated vehicle info
        }


        private void BuildEditVehiclePanel()
        {
            panel_editVehicle = new Panel
            {
                Size = new Size(panel_status.Width, panel_status.Height),
                Location = new Point(0, 0),
                BackColor = Color.White,
                Visible = false
            };
            panel_status.Controls.Add(panel_editVehicle);

            // Create FlowLayoutPanel inside panel_editVehicle
            FlowLayoutPanel flowLayout_editFields = new FlowLayoutPanel()
            {
                Size = new Size(panel_editVehicle.Width, 350),
                Location = new Point(Left, 30),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White,
            };
            panel_editVehicle.Controls.Add(flowLayout_editFields);

            Label label_editVehicle = new Label()
            {
                Location = new Point(100, 20),
                AutoSize = true,
                Text = "Edit Vehicle",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
            };
            panel_editVehicle.Controls.Add(label_editVehicle);

            // Helper function to add label + textbox vertically
            void AddField(string labelText, ref TextBox textBox)
            {
                Label label = new Label
                {
                    Text = labelText,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    AutoSize = true,
                };

                textBox = new TextBox
                {
                    Width = 100,
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    Margin = new Padding(10,0,0, 5)
                };

                flowLayout_editFields.Controls.Add(label);
                flowLayout_editFields.Controls.Add(textBox);
            }

            AddField("Tire Pressure (PSI):", ref textBox_tirePressure);
            AddField("Oil Level:", ref textBox_oilLevel);
            AddField("Battery Life (%):", ref textBox_batteryLife);
            AddField("Miles:", ref textBox_miles);
            AddField("Engine Temp (°C):", ref textBox_engineTmp);
            AddField("Internal Temp (°C):", ref textBox_internalTmp);

            // Save Changes Button
            button_saveChanges = new Button
            {
                Text = "Save Changes?",
                Location = new Point(20, 380),
                Width = 200,
                Height = 45,
                BackColor = Color.MediumPurple,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
            };
            button_saveChanges.FlatAppearance.BorderSize = 2;
            button_saveChanges.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_saveChanges, 10);
            button_saveChanges.Click += Button_saveChanges_Click;

            panel_editVehicle.Controls.Add(button_saveChanges);
        }


        private void Button_updateVehicle_Click(object sender, EventArgs e)
        {
            LoadVehicleDataIntoEditFields(); // load current values
            flowLayoutPanel_vehicleStatus.Visible = false;
            panel_editVehicle.Visible = true;
        }

        // *************************************** TRIPS METHODS ****************************** //
        private void DisplayTrip(string message)
        {
            Panel panel = new Panel
            {
                Size = new Size(230, 70),
                Margin = new Padding(7),
                BackColor = Color.MediumPurple,
                BorderStyle = BorderStyle.FixedSingle,
            };

            PictureBox pictureBox = new PictureBox()
            {
                Image = Image.FromFile("icons\\trip1_image.jpg"),
                Size = new Size(50, 50),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };

            Label label = new Label
            {
                Text = message,
                Location = new Point(70, 20),  // Adjust text to right of image,
                Font = new Font("Segoe UI", 7, FontStyle.Regular),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            panel.Controls.Add(pictureBox); // add image inside panel
            panel.Controls.Add(label);      // add label inside panel
            CornerRadius(panel, 10);
            flowLayoutPanel_trips.Controls.Add(panel);
            flowLayoutPanel_trips.Controls.SetChildIndex(panel, 0); // show newest on top
        }

        private void LoadTrips()
        {
            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var enforceForeignKey = connection.CreateCommand();
                enforceForeignKey.CommandText = "PRAGMA foreign_keys = ON;";
                enforceForeignKey.ExecuteNonQuery();

                var command = connection.CreateCommand();

                string filter = comboBox_tripDate.SelectedItem.ToString();
                string query = @"
            SELECT TripDate, LocatA, LocatB
            FROM TripsLog
            WHERE UserId = $UserId ";

                if (filter == "Today")
                {
                    query += "AND date(TripDate) = date('now') ";
                }
                else if (filter == "Previous Month")
                {
                    DateTime now = DateTime.Now;
                    DateTime firstDayLastMonth = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                    DateTime lastDayLastMonth = new DateTime(now.Year, now.Month, 1).AddDays(-1);

                    string firstDay = firstDayLastMonth.ToString("yyyy-MM-dd");
                    string lastDay = lastDayLastMonth.ToString("yyyy-MM-dd");

                    query += "AND date(TripDate) BETWEEN date($FirstDay) AND date($LastDay) ";
                    command.Parameters.AddWithValue("$FirstDay", firstDay);
                    command.Parameters.AddWithValue("$LastDay", lastDay);
                }
                else if (filter == "Year to Date")
                {
                    query += "AND strftime('%Y', TripDate) = strftime('%Y', 'now') ";
                }

                query += "ORDER BY TripDate DESC;";
                command.CommandText = query;
                command.Parameters.AddWithValue("$UserId", Session.CurrentUserID);

                flowLayoutPanel_trips.Controls.Clear();  // Clear trips before loading

                using (var reader = command.ExecuteReader())
                {
                    Random rand = new Random();

                    while (reader.Read())
                    {
                        string logDateTime = reader.GetString(0);
                        string locationA = reader.GetString(1);
                        string locationB = reader.GetString(2);

                        DateTime parsedDate = DateTime.Parse(logDateTime);
                        DisplayTrip($"{parsedDate:MMM-dd-yyyy hh:mm:ss tt} - {locationA} to {locationB}. Avg Speed: {rand.Next(20, 80)} mph");
                    }
                }
            }
        }

        private void InsertFakeTrips()
        {
            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();
                var enforceForeignKey = connection.CreateCommand();
                enforceForeignKey.CommandText = "PRAGMA foreign_keys = ON;";
                enforceForeignKey.ExecuteNonQuery();

                var command = connection.CreateCommand();
                DateTime now = DateTime.Now;
                DateTime firstDayLastMonth = new DateTime(now.Year, now.Month, 1).AddMonths(-1);

                command.CommandText = @"
                    INSERT INTO TripsLog (UserId, TripDate, LocatA, LocatB) VALUES
                    ($UserId1, $TripDate1, $LocatA1, $LocatB1),
                    ($UserId2, $TripDate2, $LocatA2, $LocatB2),
                    ($UserId3, $TripDate3, $LocatA3, $LocatB3);";

                command.Parameters.AddWithValue("$UserId1", Session.CurrentUserID);
                command.Parameters.AddWithValue("$TripDate1", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("$LocatA1", "123 ABC ST SE");
                command.Parameters.AddWithValue("$LocatB1", "456 DEB AVE NW");

                command.Parameters.AddWithValue("$UserId2", Session.CurrentUserID);
                command.Parameters.AddWithValue("$TripDate2", firstDayLastMonth.AddDays(10));
                command.Parameters.AddWithValue("$LocatA2", "307 Ronda ST SE");
                command.Parameters.AddWithValue("$LocatB2", "912 Jerico AVE NW");

                command.Parameters.AddWithValue("$UserId3", Session.CurrentUserID);
                command.Parameters.AddWithValue("$TripDate3", DateTime.Now.AddYears(-1));
                command.Parameters.AddWithValue("$LocatA3", "611 Main BLVD");
                command.Parameters.AddWithValue("$LocatB3", "201 Townsquare Way");

                command.ExecuteNonQuery();
            }
        }

        private void ComboBox_tripDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTrips();
        }

        // ***************************************** PROFILE TAB METHODS ************************************** //
        private void BuildProfileTab()
        {
            label_profile = new Label()
            {
                Location = new Point(100, 20),
                AutoSize = true,
                Text = "Profile",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
            };
            panel_profile.Controls.Add(label_profile);

            flowLayoutPanel_profile = new FlowLayoutPanel()
            {
                Size = new Size(panel_status.Width, 400),
                Location = new Point(Left, 45),
                BackColor = Color.White,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                WrapContents = false,
            };
            panel_profile.Controls.Add(flowLayoutPanel_profile);

            // 1. Profile picture
            profilePic = new PictureBox
            {
                Size = new Size(100, 100),
                Image = Image.FromFile("icons\\6422378-200.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray,
                Margin = new Padding(80, 0, 30, 0)
            };
            flowLayoutPanel_profile.Controls.Add(profilePic);

            // 2. Full Name label
            label_fullName = new Label
            {
                Text = "FirstName LastName",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.Black,
                Location = new Point(70, 130),
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(40, 0, 0, 0)
            };
            label_fullName.Left = (panel_profile.Width - label_fullName.Width) / 2;
            flowLayoutPanel_profile.Controls.Add(label_fullName);

            // 3. Email label
            label_profileEmail = new Label
            {
                Text = "you@example.com",
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point((panel_profile.Width - 200) / 2, 165),
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(80, 0, 0, 0)
            };
            label_profileEmail.Left = (panel_profile.Width - label_profileEmail.Width) / 2;
            flowLayoutPanel_profile.Controls.Add(label_profileEmail);

            // 4. Divider Line
            divider = new Panel
            {
                BackColor = Color.LightGray,
                Height = 1,
                Width = 200,
                Location = new Point((panel_profile.Width - 200) / 2, 200),
                Margin = new Padding(40, 0, 0, 0)
            };
            flowLayoutPanel_profile.Controls.Add(divider);

            // 5. Car Info labels
            label_vehicleType = new Label
            {
                Text = "Vehicle Type: (ex: Hybrid)",
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(50, 230),
                Margin = new Padding(40, 0, 0, 0)
            };
            flowLayoutPanel_profile.Controls.Add(label_vehicleType);

            label_profileVin = new Label
            {
                Text = "VIN: 1HGBH41JXMN109186",
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(50, 260),
                Margin = new Padding(40, 0, 0, 0)
            };
            flowLayoutPanel_profile.Controls.Add(label_profileVin);

            // 6. Edit Profile Button
            button_editProfile = new Button
            {
                Text = "Edit Profile",
                Size = new Size(200, 40),
                Location = new Point((panel_profile.Width - 200) / 2, 310),
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Margin = new Padding(40, 0, 10, 0)
            };
            button_editProfile.FlatAppearance.BorderSize = 2;
            button_editProfile.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_editProfile, 10);
            flowLayoutPanel_profile.Controls.Add(button_editProfile);

            // 7. Log Out Button
            button_logout = new Button
            {
                Text = "Log Out",
                Size = new Size(200, 40),
                Location = new Point((panel_profile.Width - 200) / 2, 370),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Margin = new Padding(40, 0, 10, 0)
            };
            button_logout.FlatAppearance.BorderSize = 2;
            button_logout.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_logout, 10);
            flowLayoutPanel_profile.Controls.Add(button_logout);

            // Add Vehicle Button
            button_addVehicle = new Button()
            {
                Text = "Add Vehicle",
                Size = new Size(200, 40),
                Location = new Point(80, 450),
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
            };
            //button_addVehicle.Click += Button_addVehicle_Click;
            panel_profile.Controls.Add(button_addVehicle);

            // Remove Vehicle Button
            button_removeVehicle = new Button()
            {
                Text = "Remove Vehicle",
                Size = new Size(200, 40),
                Location = new Point(320, 450),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
            };
            //button_removeVehicle.Click += Button_removeVehicle_Click;
            panel_profile.Controls.Add(button_removeVehicle);

            // Accessibility Button (Admin Only)
            button_accessibility = new Button()
            {
                Text = "Accessibility",
                Size = new Size(200, 40),
                Location = new Point((panel_profile.Width - 200) / 2, 520),
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Visible = false // Only show if admin
            };
            //button_accessibility.Click += Button_accessibility_Click;
            panel_profile.Controls.Add(button_accessibility);

            // Panels for Edit Profile and Add Vehicle (hidden until needed)
            //BuildEditProfilePanel();
            BuildAddVehiclePanel();
        }

        private void LoadProfileInfo()
        {
            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT FirstName, LastName, Email, CarType
                    FROM Users
                    WHERE UserId = $UserId
                    LIMIT 1;";

                command.Parameters.AddWithValue("$UserId", Session.CurrentUserID);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string fullName = $"{reader.GetString(0)} {reader.GetString(1)}";
                        string email = reader.GetString(2);
                        string carType = reader.GetString(3);

                        label_fullName.Text = fullName;
                        label_profileEmail.Text = email;
                        label_vehicleType.Text = $"Vehicle Type: {carType}";

                        // Load VIN separately
                        LoadUserVin();
                    }
                }
            }
        }

        private void LoadUserVin()
        {
            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT Vin
                    FROM Users
                    WHERE UserId = $UserId
                    LIMIT 1;";

                command.Parameters.AddWithValue("$UserId", Session.CurrentUserID);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string vin = reader.GetString(0);
                        label_profileVin.Text = $"VIN: {vin}";
                    }
                }
            }
        }

        private void BuildAddVehiclePanel()
        {
            panel_addVehicle = new Panel
            {
                Size = new Size(panel_profile.Width, panel_profile.Height),
                Location = new Point(0, 0),
                BackColor = Color.White,
                Visible = false
            };
            panel_profile.Controls.Add(panel_addVehicle);

            int startY = 50;
            int spacing = 60;

            Label labelTitle = new Label
            {
                Text = "Add New Vehicle",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(80, 20),
                AutoSize = true
            };
            panel_addVehicle.Controls.Add(labelTitle);

            textBox_make = CreateInputField("Make:", startY);
            textBox_model = CreateInputField("Model:", startY += spacing);
            textBox_year = CreateInputField("Year:", startY += spacing);
            textBox_vin = CreateInputField("VIN:", startY += spacing);

            // Save Vehicle Button
            button_saveVehicle = new Button
            {
                Text = "Save Vehicle",
                Size = new Size(200, 40),
                Location = new Point(100, startY + 60),
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            //button_saveVehicle.Click += Button_saveVehicle_Click;

            panel_addVehicle.Controls.Add(button_saveVehicle);
        }

        // Helper function
        private TextBox CreateInputField(string labelText, int y)
        {
            Label label = new Label
            {
                Text = labelText,
                Location = new Point(50, y),
                Size = new Size(100, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            panel_addVehicle.Controls.Add(label);

            TextBox textBox = new TextBox
            {
                Location = new Point(150, y),
                Width = 200
            };
            panel_addVehicle.Controls.Add(textBox);

            return textBox;
        }

        private void BuildRemoveVehiclePanel()
        {
            panel_removeVehicle = new Panel
            {
                Size = new Size(panel_profile.Width, panel_profile.Height),
                Location = new Point(0, 0),
                BackColor = Color.White,
                Visible = false
            };
            panel_profile.Controls.Add(panel_removeVehicle);

            Label labelTitle = new Label
            {
                Text = "Remove Vehicle",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(100, 10),
                AutoSize = true
            };
            panel_removeVehicle.Controls.Add(labelTitle);

            flowLayoutPanel_removeVehicles = new FlowLayoutPanel
            {
                Location = new Point(50, 50),
                Size = new Size(panel_profile.Width - 100, panel_profile.Height - 100),
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true
            };
            panel_removeVehicle.Controls.Add(flowLayoutPanel_removeVehicles);
        }

        private void Button_addVehicle_Click(object sender, EventArgs e)
        {
            panel_addVehicle.Visible = true;
            flowLayoutPanel_profile.Visible = false;
            panel_removeVehicle.Visible = false;
        }

        private void Button_removeVehicle_Click(object sender, EventArgs e)
        {
            panel_removeVehicle.Visible = true;
            flowLayoutPanel_profile.Visible = false;
            panel_addVehicle.Visible = false;
            LoadUserVehiclesForRemoval();
        }

        private void Button_saveVehicle_Click(object sender, EventArgs e)
        {
            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
            INSERT INTO VehicleLog (Make, Model, Year, Vin, UserId, TirePressure, OilLevel, BatteryLife, Miles, EngineTmp, InternalTmp)
            VALUES ($Make, $Model, $Year, $Vin, $UserId, 'Good', 'Good', '100%', 0, 90, 22);";

                command.Parameters.AddWithValue("$Make", textBox_make.Text);
                command.Parameters.AddWithValue("$Model", textBox_model.Text);
                command.Parameters.AddWithValue("$Year", int.Parse(textBox_year.Text));
                command.Parameters.AddWithValue("$Vin", textBox_vin.Text);
                command.Parameters.AddWithValue("$UserId", Session.CurrentUserID);

                command.ExecuteNonQuery();
            }

            MessageBox.Show("Vehicle added successfully!");

            // Return to profile view
            panel_addVehicle.Visible = false;
            flowLayoutPanel_profile.Visible = true;
            LoadProfileInfo();
        }

        private void LoadUserVehiclesForRemoval()
        {
            flowLayoutPanel_removeVehicles.Controls.Clear();

            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT VehicleId, Make, Model, Year
            FROM VehicleLog
            WHERE UserId = $UserId;";

                command.Parameters.AddWithValue("$UserId", Session.CurrentUserID);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int vehicleId = reader.GetInt32(0);
                        string make = reader.GetString(1);
                        string model = reader.GetString(2);
                        int year = reader.GetInt32(3);

                        Panel panel_vehicle = new Panel
                        {
                            Width = flowLayoutPanel_removeVehicles.Width - 25,
                            Height = 60,
                            BackColor = Color.LightGray,
                            Margin = new Padding(5)
                        };

                        Label label = new Label
                        {
                            Text = $"{year} {make} {model}",
                            Location = new Point(10, 10),
                            AutoSize = true,
                            Font = new Font("Segoe UI", 10, FontStyle.Regular)
                        };
                        panel_vehicle.Controls.Add(label);

                        Button button_remove = new Button
                        {
                            Text = "Remove",
                            Size = new Size(80, 30),
                            Location = new Point(panel_vehicle.Width - 100, 10),
                            BackColor = Color.Red,
                            ForeColor = Color.White,
                            Font = new Font("Segoe UI", 8, FontStyle.Bold),
                            Tag = vehicleId
                        };
                        button_remove.Click += Button_removeVehicleConfirm_Click;
                        panel_vehicle.Controls.Add(button_remove);

                        flowLayoutPanel_removeVehicles.Controls.Add(panel_vehicle);
                    }
                }
            }
        }

        private void Button_removeVehicleConfirm_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int vehicleId = (int)btn.Tag;

            using (var connection = new SqliteConnection("Data Source=carstarter.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM VehicleLog WHERE VehicleId = $VehicleId;";
                command.Parameters.AddWithValue("$VehicleId", vehicleId);

                command.ExecuteNonQuery();
            }

            MessageBox.Show("Vehicle removed successfully!");
            LoadUserVehiclesForRemoval();
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

            if (textBox_vin.Text.Length < 17 || textBox_vin.Text.Length > 17)
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
            LoadVehicleStatus();
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
            LoadTrips();
        }

        private void button_profile_Click(object sender, EventArgs e)
        {
            ShowTab(panel_profile);
            ActiveTab(button_profile);
            LoadProfileInfo();
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

        // ^^^^^^^^^ END ^^^^^^^^^^^ //

        // ************* ACTIVITY PAGE HNADLERS *************** //
        private void ComboBox_activityDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowlayoutPanel_activities.Controls.Clear();
            LoadActivityLogs();
        }

        // ^^^^^^^^^^ END ^^^^^^^^^^^ //
        
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
