namespace WinFormsCarStarter
{
    public partial class Form1 : Form
    {
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
    }
}
