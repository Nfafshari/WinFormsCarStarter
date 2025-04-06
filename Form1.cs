using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static WinFormsCarStarter.Form1;

namespace WinFormsCarStarter
{
    public partial class Form1 : Form
    {
        private ImageList imageList = new ImageList();
        private ImageList active_imageList = new ImageList();

        public Form1()
        {
            InitializeComponent();

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

            // Show home tab on startup
            ShowTab(panel_home);
            ActiveTab(button_home);

            /************************ Panel Setup *************************/
            /******* Diagnostics Panel ********/
            // Clock
            Label label_time = new Label();
            label_time.Name = "Clock";
            label_time.Font = new Font("Segoe UI", 8);
            label_time.ForeColor = Color.Black;
            label_time.Padding = new Padding(0, 3, 0, 0);
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

            /******* Home Tab ********/
            Label label_home = new Label();
            label_home.Name = "Home Tab";
            label_home.Text = "Home";
            label_home.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label_home.Location = new Point(102, 20);
            panel_home.Controls.Add(label_home);

            // Start/Stop button
            CircularButton circularButton_startStop = new CircularButton();
            circularButton_startStop.Size = new Size(80, 80);
            circularButton_startStop.Invalidate();
            circularButton_startStop.Text = "Start";
            circularButton_startStop.Font = new Font("Segoe UI", 10);
            circularButton_startStop.Location = new Point(95, 200);
            circularButton_startStop.BorderColor = Color.Black;
            circularButton_startStop.BorderThickness = 1;
            circularButton_startStop.Click += circularBotton_startStop_Click;
            panel_home.Controls.Add(circularButton_startStop);
        }

        /************** GLOBAL METHODS *************/
        // ShowTab -- makes the tab (panel) visible depending on which button is clicked
        private void ShowTab (Panel visibleTab)
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

        /******************* TAB BUTTON METHODS ************************/
        // ActiveTab -- shows the user what tab they are currently on 
        private void ActiveTab (Button activeTab)
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

        // button_activity_Click -- controller for when activity button is clicked
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

        /********************** HOME PAGE CONTROLS *************************/
        // CircularButton -- circular button class for changing buttons to a circle instead of rectangular
        public class CircularButton : Button
        {
            // Border 
            public Color BorderColor { get; set; } = Color.Black;
            public int BorderThickness { get; set; } = 2;

            // Mouse over
            public Color HoverBackColor { get; set; } = Color.LightGray;
            public Color DefaultBackColor { get; set; } = Color.White;

            public Color HoverBorderColor { get; set; } = Color.MediumPurple;
            public Color DefaultBorderColor { get; set; } = Color.Black;

            private bool isHovering = false;

            public CircularButton()
            {
                this.FlatStyle = FlatStyle.Flat;
                this.BackColor = DefaultBackColor;
                this.BorderColor = DefaultBorderColor;

                this.MouseEnter += (s, e) => {
                    isHovering = true;
                    this.BackColor = HoverBackColor;
                    this.BorderColor = HoverBorderColor;
                    this.Invalidate(); // Force redraw
                };

                this.MouseLeave += (s, e) => {
                    isHovering = false;
                    this.BackColor = DefaultBackColor;
                    this.BorderColor = DefaultBorderColor;
                    this.Invalidate();
                };
            }

            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, this.Width, this.Height);
                this.Region = new Region(path);
            }
            protected override void OnPaint(PaintEventArgs pevent)
            {
                base.OnPaint(pevent);

                // Smooth edges
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Draw circular border
                using (Pen pen = new Pen(BorderColor, BorderThickness))
                {
                    pen.Alignment = PenAlignment.Inset;
                    pevent.Graphics.DrawEllipse(pen, BorderThickness / 2, BorderThickness / 2,
                                                this.Width - BorderThickness, this.Height - BorderThickness);
                }
            }
        }
        // 
        private void circularBotton_startStop_Click(object sender, EventArgs e)
        {
            
        }
    }
}
