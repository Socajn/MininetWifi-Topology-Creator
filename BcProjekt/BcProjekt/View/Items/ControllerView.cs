using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BcProjekt.Model;

namespace BcProjekt.View.Items
{
    public class ControllerView : Form
    {
        private ControllerVM Controller;
        private MainWindowView _parent;
        private bool _isNew;

        private TextBox port = new TextBox();
        private ComboBox type = new ComboBox();
        private ComboBox protocol = new ComboBox();
        private TextBox IpAddress = new TextBox();

        List<string> typeItems = new List<string>(new string[] { "OpenFlow reference", "remote controller", "In-band controller", "OVS Controller" });
        List<string> modeItems = new List<string>(new string[] { "TCP", "SSL" });

        public ControllerView(ControllerVM controller, MainWindowView parent, bool isNew)
        {
            Controller = controller;
            _parent = parent;
            _isNew = isNew;

            typeItems.ForEach(x => type.Items.Add(x));
            modeItems.ForEach(x => protocol.Items.Add(x));

            Text = Controller.Name + " settings";
            Size = new Size(500, 250);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromKnownColor(KnownColor.LightGray);

            Render();

            LoadVM();

            CenterToScreen();
        }

        private void Render()
        {
            MenuStrip ms = new MenuStrip
            {
                Parent = this
            };

            ToolStripMenuItem File = new ToolStripMenuItem("&Edit");
            ToolStripMenuItem Save = new ToolStripMenuItem("&Save", null, OnSave_Click);
            ToolStripMenuItem Back = new ToolStripMenuItem("&Back", null, OnExit_Click);
            ToolStripMenuItem Delete = new ToolStripMenuItem("&Delete", null, OnDelete_Click);

            Save.ShortcutKeys = Keys.Control | Keys.M;

            File.DropDownItems.Add(Save);
            File.DropDownItems.Add(Delete);
            File.DropDownItems.Add(Back);

            ms.Items.Add(File);
            MainMenuStrip = ms;

            Label portLabel = new Label
            {
                Location = new Point(5, 30),
                Text = "Port:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label typeLabel = new Label
            {
                Location = new Point(5, 60),
                Text = "Type:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label protocolLabel = new Label
            {
                Location = new Point(5, 90),
                Text = "Protocol:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };


            Label remoteInbandLabel = new Label
            {
                Location = new Point(5, 120),
                Text = "Remote/In-Band:",
                Font = new Font("Serif", 14, FontStyle.Bold),
                Size = new Size(300, 30)
            };

            Label ipAddressLabel = new Label
            {
                Location = new Point(5, 160),
                Text = "IP Address:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            port.Location = new Point(135, 30);
            port.Size = new Size(200, 20);

            type.Location = new Point(135, 60);
            type.Size = new Size(180, 20);

            protocol.Location = new Point(135, 90);
            protocol.Size = new Size(100, 20);

            IpAddress.Location = new Point(135, 160);
            IpAddress.Size = new Size(200, 20);

            Controls.Add(portLabel);
            Controls.Add(port);

            Controls.Add(typeLabel);
            Controls.Add(type);

            Controls.Add(protocolLabel);
            Controls.Add(protocol);

            Controls.Add(remoteInbandLabel);
            Controls.Add(ipAddressLabel);
            Controls.Add(IpAddress);


        }

        private void LoadVM()
        {
            port.Text = Controller.ControllerPort;

            var typ = typeItems.FindIndex(x => x == Controller.Type);
            type.SelectedIndex = typ;

            var mod = modeItems.FindIndex(x => x == Controller.Protocol);
            protocol.SelectedIndex = mod;

            IpAddress.Text = Controller.IPAddress;

        }

        private void OnSave_Click(object sender, EventArgs e)
        {
            Controller.ControllerPort = port.Text;
            Controller.Type = type.SelectedItem as string;
            Controller.Protocol = protocol.SelectedItem as string;
            Controller.IPAddress = IpAddress.Text;

            _parent.AddControllerButton(Controller);
            this.Visible = false;
        }


        private void OnExit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void OnDelete_Click(object sender, EventArgs e)
        {
            if (_isNew != true)
                _parent.DeleteControllerButton(Controller);
            this.Visible = false;
        }
    }
}
