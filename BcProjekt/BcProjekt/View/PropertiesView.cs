using System;
using System.Drawing;
using System.Windows.Forms;

namespace BcProjekt.View
{
    public class PropertiesView : Form
    {
        private Form _parent;

        public PropertiesView(Form parent)
        {
            _parent = parent;
            Text = "Properties";
            Size = new Size(500, 300);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            ControlBox = false;
            BackColor = Color.FromKnownColor(KnownColor.LightGray);

            Render();

            CenterToScreen();

        }


        private void Render()
        {
            MenuStrip ms = new MenuStrip
            {
                Parent = this
            };

            ToolStripMenuItem File = new ToolStripMenuItem("&Edit");
            ToolStripMenuItem Save = new ToolStripMenuItem("&Save", null);
            ToolStripMenuItem Back = new ToolStripMenuItem("&Back", null, OnExit_Click);

            File.DropDownItems.Add(Save);
            File.DropDownItems.Add(Back);

            ms.Items.Add(File);
            MainMenuStrip = ms;


            Label ipBaseLabel = new Label
            {
                Location = new Point(5, 32),
                Text = "IP Base:",
                Font = new Font("Serif", 10),
                Size = new Size(75, 20)
            };


            TextBox ipBase = new TextBox
            {
                Location = new Point(80, 30),
                Size = new Size(100, 20)
            };


            Label authLabel = new Label
            {
                Location = new Point(5, 53),
                Text = "Authentication:",
                Font = new Font("Serif", 10),
                Size = new Size(100, 20)
            };

            ComboBox authCombo = new ComboBox
            {
                Location = new Point(106, 53),
                Name = "authCombobox",
                Size = new Size(100, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            authCombo.Items.Add("none");
            authCombo.Items.Add("WEP");
            authCombo.Items.Add("WPA");
            authCombo.Items.Add("WPA2");
            authCombo.Items.Add("8021x");
            authCombo.SelectedIndex = 0;

            Label oVSwitchLabel = new Label
            {
                Location = new Point(5, 74),
                Text = "Open vSwitch:",
                Font = new Font("Serif", 10),
                Size = new Size(110, 20)
            };

            CheckBox version1 = new CheckBox()
            {
                Font = new Font("Arial", 10),
                Text = "OpenFlow 1.0",
                Size = new Size(110, 20),
                Location = new Point(5, 95)
            };

            CheckBox version2 = new CheckBox()
            {
                Font = new Font("Arial", 10),
                Text = "OpenFlow 1.1",
                Size = new Size(110, 20),
                Location = new Point(5, 115)
            };

            CheckBox version3 = new CheckBox()
            {
                Font = new Font("Arial", 10),
                Text = "OpenFlow 1.2",
                Size = new Size(110, 20),
                Location = new Point(116, 95)
            };

            CheckBox version4 = new CheckBox()
            {
                Font = new Font("Arial", 10),
                Text = "OpenFlow 1.3",
                Size = new Size(110, 20),
                Location = new Point(116, 115)

            };


            Label dpctlLabel = new Label
            {
                Location = new Point(5, 135),
                Text = "dpctl port:",
                Font = new Font("Serif", 10),
                Size = new Size(100, 20)
            };


            TextBox dpctl = new TextBox
            {
                Location = new Point(105, 135),
                Size = new Size(100, 20)
            };


            Controls.Add(ipBaseLabel);
            Controls.Add(ipBase);
            Controls.Add(authLabel);
            Controls.Add(authCombo);
            Controls.Add(oVSwitchLabel);
            Controls.Add(version1);
            Controls.Add(version2);
            Controls.Add(version3);
            Controls.Add(version4);
            Controls.Add(dpctlLabel);
            Controls.Add(dpctl);
        }


        private void OnExit_Click(object sender, EventArgs e)
        {
            _parent.Enabled = true;
            this.Visible = false;
        }


    }
}
