using System.Drawing.Imaging;
using System.Windows.Forms;

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
            panel_activity.Dock = DockStyle.Fill;
            panel_status.Dock = DockStyle.Fill;
            panel_home.Dock = DockStyle.Fill;
            panel_trips.Dock = DockStyle.Fill;
            panel_profile.Dock = DockStyle.Fill;

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

            // Button text
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


        /******************* BUTTON METHODS ************************/
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
    }
}
