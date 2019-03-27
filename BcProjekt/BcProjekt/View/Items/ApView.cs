using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BcProjekt.Model;

namespace BcProjekt.View.Items
{
    public class ApView : Form
    {
        private ApVM Ap;
        private MainWindowView _parent;
        private bool _isNew;

        private TextBox ssid = new TextBox();
        private TextBox channel = new TextBox();
        private TextBox signalRange = new TextBox();

        private TextBox ipAddress = new TextBox();
        private TextBox dpid = new TextBox();
        private TextBox dpctl = new TextBox();
        private TextBox startCommand = new TextBox();
        private TextBox stopCommand = new TextBox();

        private ComboBox type = new ComboBox();
        private ComboBox mode = new ComboBox();
        private ComboBox auth = new ComboBox();

        CheckBox netFlow = new CheckBox();
        CheckBox sFlow = new CheckBox();


        private DataGridView externalDatagrid = new DataGridView();


        List<string> typeItems = new List<string>(new string[] { "Default", "Open vSwitch kernel mode", "Indigo virtual AP", "Userspace AP", "Userspace AP in Namespace" });
        List<string> modeItems = new List<string>(new string[] { "g", "a", "b", "n" });
        List<string> authItems = new List<string>(new string[] { "none", "WEP", "WPA", "WPA2", "8021x" });


        public ApView(ApVM station, MainWindowView parent, bool isNew)
        {
            Ap = station;
            _parent = parent;
            _isNew = isNew;

            typeItems.ForEach(x => type.Items.Add(x));
            modeItems.ForEach(x => mode.Items.Add(x));
            authItems.ForEach(x => auth.Items.Add(x));

            Text = Ap.Name + " settings";
            Size = new Size(500, 650);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromKnownColor(KnownColor.LightGray);

            RenderMain();
            RenderExternal();

            LoadVm();

            CenterToScreen();
        }

        private void RenderExternal()
        {
            Label externalLabel = new Label()
            {
                Location = new Point(5, 430),
                Text = "External Interfaces:",
                Font = new Font("Serif", 14, FontStyle.Bold),
                Size = new Size(250, 25)
            };

            Button addExternal = new Button()
            {
                Location = new Point(260, 430),
                Size = new Size(100, 20),
                Text = "Add",
                BackColor = Color.WhiteSmoke
            };

            Button deleteExternal = new Button()
            {
                Location = new Point(360, 430),
                Size = new Size(100, 20),
                Text = "Delete",
                BackColor = Color.WhiteSmoke
            };

            addExternal.Click += OnAddExternalClick;
            deleteExternal.Click += OnDeleteExternalClick;

            externalDatagrid.ColumnCount = 1;
            externalDatagrid.Name = "externalDataGridView";
            externalDatagrid.AllowUserToAddRows = false;
            externalDatagrid.Location = new Point(5, 470);
            externalDatagrid.Size = new Size(200, 150);
            externalDatagrid.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            externalDatagrid.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            externalDatagrid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            externalDatagrid.GridColor = Color.Black;
            externalDatagrid.RowHeadersVisible = true;

            externalDatagrid.Columns[0].Name = "Interface name";

            this.Controls.Add(externalLabel);
            this.Controls.Add(addExternal);
            this.Controls.Add(deleteExternal);
            this.Controls.Add(externalDatagrid);
        }

        private void RenderMain()
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


            Label ssidLabel = new Label
            {
                Location = new Point(5, 30),
                Text = "SSID:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label channelLabel = new Label
            {
                Location = new Point(5, 60),
                Text = "Channel:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label modeLabel = new Label
            {
                Location = new Point(5, 90),
                Text = "Mode:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };


            Label signalRangeLabel = new Label
            {
                Location = new Point(5, 120),
                Text = "Signal Range:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label authLabel = new Label
            {
                Location = new Point(5, 150),
                Text = "Authentication:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label typeLabel = new Label
            {
                Location = new Point(5, 180),
                Text = "Type:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label dpidLabel = new Label
            {
                Location = new Point(5, 210),
                Text = "DPID:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label netflowLabel = new Label
            {
                Location = new Point(5, 240),
                Text = "NetFlow:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label sflowLabel = new Label
            {
                Location = new Point(5, 270),
                Text = "sFlow:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label ipLabel = new Label
            {
                Location = new Point(5, 300),
                Text = "Ip Address:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label dpctlLabel = new Label
            {
                Location = new Point(5, 330),
                Text = "DPCTL port:",
                Font = new Font("Serif", 12),
                Size = new Size(130, 20)
            };

            Label startLabel = new Label
            {
                Location = new Point(5, 360),
                Text = "Start command:",
                Font = new Font("Serif", 12),
                Size = new Size(130, 20)
            };

            Label stopLabel = new Label
            {
                Location = new Point(5, 390),
                Text = "Stop command:",
                Font = new Font("Serif", 12),
                Size = new Size(130, 20)
            };

            ssid.Location = new Point(135, 30);
            ssid.Size = new Size(200, 20);

            channel.Location = new Point(135, 60);
            channel.Size = new Size(200, 20);

            mode.Location = new Point(135, 90);
            mode.Size = new Size(50, 20);

            signalRange.Location = new Point(135, 120);
            signalRange.Size = new Size(200, 20);

            auth.Location = new Point(135, 150);
            auth.Size = new Size(100, 20);

            type.Location = new Point(135, 180);
            type.Size = new Size(180, 20);

            dpid.Location = new Point(135, 210);
            dpid.Size = new Size(200, 20);

            netFlow.Location = new Point(135, 240);

            sFlow.Location = new Point(135, 270);

            ipAddress.Location = new Point(135, 300);
            ipAddress.Size = new Size(200, 20);

            dpctl.Location = new Point(135, 330);
            dpctl.Size = new Size(200, 20);

            startCommand.Location = new Point(135, 360);
            startCommand.Size = new Size(300, 20);

            stopCommand.Location = new Point(135, 390);
            stopCommand.Size = new Size(300, 20);


            Controls.Add(ssidLabel);
            Controls.Add(ssid);

            Controls.Add(channelLabel);
            Controls.Add(channel);

            Controls.Add(modeLabel);
            Controls.Add(mode);

            Controls.Add(signalRangeLabel);
            Controls.Add(signalRange);

            Controls.Add(authLabel);
            Controls.Add(auth);

            Controls.Add(typeLabel);
            Controls.Add(type);

            Controls.Add(dpidLabel);
            Controls.Add(dpid);

            Controls.Add(netflowLabel);
            Controls.Add(netFlow);

            Controls.Add(sflowLabel);
            Controls.Add(sFlow);

            Controls.Add(ipLabel);
            Controls.Add(ipAddress);

            Controls.Add(dpctlLabel);
            Controls.Add(dpctl);

            Controls.Add(startLabel);
            Controls.Add(startCommand);

            Controls.Add(stopLabel);
            Controls.Add(stopCommand);
        }


        private void LoadVm()
        {
            ssid.Text = Ap.SSID.ToString();
            channel.Text = Ap.Channel.ToString();

            var mod = modeItems.FindIndex(x => x == Ap.Mode);
            mode.SelectedIndex = mod;

            signalRange.Text = Ap.SignalRange.ToString();

            var aut = authItems.FindIndex(x => x == Ap.Authentication);
            auth.SelectedIndex = aut;

            var typ = typeItems.FindIndex(x => x == Ap.Type);
            type.SelectedIndex = typ;

            dpid.Text = Ap.DPID;

            netFlow.Checked = Ap.NetFlow;
            sFlow.Checked = Ap.sFlow;

            ipAddress.Text = Ap.IpAddress;
            dpctl.Text = Ap.DPCTLport;

            startCommand.Text = Ap.StartCommand;
            stopCommand.Text = Ap.StopCommad;

            foreach (var b in Ap.ExternalInterfaces)
            {
                string[] externalRow = { b.Name };
                externalDatagrid.Rows.Add(externalRow);
            }

        }


            private void OnSave_Click(object sender, EventArgs e)
        {

            Ap.SSID = ssid.Text;
            Ap.Channel = Int32.Parse(channel.Text);
            Ap.Mode = mode.SelectedItem as string;
            Ap.PreviousSignalRange = Ap.SignalRange;
            Ap.SignalRange = Int32.Parse(signalRange.Text);
            Ap.Authentication = auth.SelectedItem as string;
            Ap.Type = type.SelectedItem as string;
            Ap.DPID = dpid.Text;
            Ap.NetFlow = netFlow.Checked;
            Ap.sFlow = sFlow.Checked;
            Ap.IpAddress = ipAddress.Text;
            Ap.DPCTLport = dpctl.Text;
            Ap.StartCommand = startCommand.Text;
            Ap.StopCommad = stopCommand.Text;

            var externals = new List<ExternalInterface>();
            foreach (DataGridViewRow row in externalDatagrid.Rows)
            {
                var first = row.Cells[0].Value;
                if (first != null)
                {
                    externals.Add(new ExternalInterface() { Name = first.ToString() });
                }
            }
            Ap.ExternalInterfaces = externals;

            _parent.AddApButton(Ap);
            this.Visible = false;
        }

        private void OnExit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void OnDelete_Click(object sender, EventArgs e)
        {
            if (_isNew != true)
                _parent.DeleteApButton(Ap);
            this.Visible = false;
        }

        private void OnAddExternalClick(object sender, EventArgs e)
        {
            externalDatagrid.Rows.Add();
        }

        private void OnDeleteExternalClick(object sender, EventArgs e)
        {
            if (this.externalDatagrid.SelectedRows.Count > 0)
            {
                this.externalDatagrid.Rows.RemoveAt(
                    this.externalDatagrid.SelectedRows[0].Index);
            }
        }
    }
}
