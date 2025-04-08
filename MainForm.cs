using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static WinFormsCarStarter.MainForm;
using System.Runtime.CompilerServices;

namespace WinFormsCarStarter
{
    public partial class MainForm : Form
    {
        // Global variables (private to MainForm)
        private ImageList imageList = new ImageList();
        private ImageList active_imageList = new ImageList();
        private bool isToggled = false;
        private Panel slidePanel;
        private bool isDragging = false;
        private int dragStart;
        private int originalPanelTop;
        private int collapsedTop;
        private int expandedTop;

        public MainForm()
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

            /************************ TAB DESIGN *************************/
            /******* Diagnostics ********/
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

            /*************** Home Tab ******************/
            Label label_home = new Label()
            {
                Name = "Home Tab",
                Text = "Home",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(102, 20),
            };
            panel_home.Controls.Add(label_home);


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
            panel_home.Controls.Add(roundButton_unlock);

            // ******** Slide Up Panel Home Tab ************/
            collapsedTop = panel_home.Height - 125;
            expandedTop = panel_home.Height - 200;
            
            slidePanel = new Panel
            {
                Height = 200,
                Width = this.ClientSize.Width,
                Top = collapsedTop,
                Left = 0,
                BackColor = Color.FromArgb(125,125,125),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            panel_home.Controls.Add(slidePanel);

            slidePanel.MouseDown += SlidePanel_MouseDown;
            slidePanel.MouseMove += SlidePanel_MouseMove;
            slidePanel.MouseUp += SlidePanel_MouseUp;
            CornerRadius(slidePanel, 20);

            Panel handleBar = new Panel
            {
                Size = new Size(100, 5),
                BackColor = Color.FromArgb(100,100,100),
                Location = new Point((slidePanel.Width - 100) / 2, 5),
                Cursor = Cursors.Hand
            };
            slidePanel.Controls.Add(handleBar);

            handleBar.MouseDown += SlidePanel_MouseDown;
            handleBar.MouseMove += SlidePanel_MouseMove;
            handleBar.MouseUp += SlidePanel_MouseUp;
            CornerRadius(handleBar, 20);

            /********* Slide Panel Buttons **********/
            Button button_lights = new Button
            {
                Size = new Size(75, 50),
                Location = new Point(10, 15),
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Text = "Lights",
                BackColor = Color.MediumPurple,
                FlatStyle = FlatStyle.Flat
            };
            button_lights.FlatAppearance.BorderSize = 2;
            button_lights.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_lights, 10);
            slidePanel.Controls.Add(button_lights);

            Button button_hazards = new Button 
            {
                Size = new Size(75, 50),
                Location = new Point(95, 15),
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Text = "Hazards",
                BackColor = Color.MediumPurple,
                FlatStyle = FlatStyle.Flat
            };
            button_hazards.FlatAppearance.BorderSize = 2;
            button_hazards.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_hazards, 10);
            slidePanel.Controls.Add(button_hazards);

            Button button_horn = new Button
            {
                Size = new Size(75, 50),
                Location = new Point(180, 15),
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Text = "Horn",
                BackColor = Color.MediumPurple,
                FlatStyle = FlatStyle.Flat
            };
            button_horn.FlatAppearance.BorderSize = 2;
            button_horn.FlatAppearance.BorderColor = Color.Black;
            CornerRadius(button_horn, 10);
            slidePanel.Controls.Add(button_horn);
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

        /******************* TAB BUTTON METHODS ************************/
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
        
        /************************ Bottom Tab Event Handlers ********************************/
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

            isToggled = !isToggled;

            if (isToggled)
            {
                senderButton.BackColor = Color.Red;  
                senderButton.Text = "STOP";
                senderButton.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                ShowNotification("Vehicle Started Successfully", "success");
            }
            else
            {
                senderButton.BackColor = Color.Green;  
                senderButton.Text = "Start";
                senderButton.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                ShowNotification("Vehicle Stopped Succesfuly", "stop");
            }

            senderButton.Invalidate(); 
        }

        private void roundButton_lock_Click(object sender, EventArgs e)
        {
            ShowNotification("Vehicle Locked Successfully", "success");
        }

        private void roundButton_unlock_Click(object sender, EventArgs e)
        {
            ShowNotification("Vehicle Unlocked Successfully", "success");
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

            // Clamp between expanded and collapsed positions
            newTop = Math.Max(expandedTop, Math.Min(collapsedTop, newTop));

            slidePanel.Top = newTop;
            slidePanel.Height = this.ClientSize.Height - newTop;
        }

        private void SlidePanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
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

        // Rounded corners for buttons and panels
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
    }
}
  