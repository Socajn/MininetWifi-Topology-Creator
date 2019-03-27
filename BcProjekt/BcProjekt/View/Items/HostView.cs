using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BcProjekt.Model;

namespace BcProjekt.View.Items
{
    public class HostView : Form
    {
        private HostVM Host;
        private MainWindowView _paretn;
        private bool _isNew;

        private TextBox ipAddress = new TextBox();
        private TextBox defaultRoute = new TextBox();
        private TextBox amountCpu = new TextBox();
        private TextBox cores = new TextBox();
        private TextBox startCommand = new TextBox();
        private TextBox stopCommand = new TextBox();

        private ComboBox type = new ComboBox();

        private DataGridView vlanDatagrid = new DataGridView();
        private DataGridView externalDatagrid = new DataGridView();
        private DataGridView privateDatagrid = new DataGridView();

        List<string> comboboxItems = new List<string>(new string[] { "host", "cfs", "rt" });

        public HostView(HostVM host, MainWindowView parent, bool isNew )
        {
            Host = host;
            _paretn = parent;
            _isNew = isNew;
            comboboxItems.ForEach(x => type.Items.Add(x));

            Text = Host.Hostname + " settings";
            Size = new Size(500, 900);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromKnownColor(KnownColor.LightGray);

            RenderMain();
            RenderVlan();
            RenderExternal();
            RenderPrivate();

            LoadVm();

            CenterToScreen();
        }

        private void RenderPrivate()
        {
            Label privateLabel = new Label()
            {
                Location = new Point(5, 610),
                Text = "Private directories:",
                Font = new Font("Serif", 14, FontStyle.Bold),
                Size = new Size(245, 25)
            };

            Button addPrivate = new Button()
            {
                Location = new Point(250, 610),
                Size = new Size(100, 20),
                Text = "Add",
                BackColor = Color.WhiteSmoke
            };

            Button deletePrivate = new Button()
            {
                Location = new Point(350, 610),
                Size = new Size(100, 20),
                Text = "Delete",
                BackColor = Color.WhiteSmoke
            };

            addPrivate.Click += OnAddPrivateClick;
            deletePrivate.Click += OnDeletePrivateClick;

            privateDatagrid.ColumnCount = 2;
            privateDatagrid.Name = "vlansDataGridView";
            privateDatagrid.AllowUserToAddRows = false;
            privateDatagrid.Location = new Point(5, 650);
            privateDatagrid.Size = new Size(300, 150);
            privateDatagrid.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            privateDatagrid.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            privateDatagrid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            privateDatagrid.GridColor = Color.Black;
            privateDatagrid.RowHeadersVisible = true;

            privateDatagrid.Columns[0].Name = "Mount";
            privateDatagrid.Columns[1].Name = "Persistent directory";




            this.Controls.Add(privateLabel);
            this.Controls.Add(addPrivate);
            this.Controls.Add(deletePrivate);
            this.Controls.Add(privateDatagrid);
        }

        private void RenderExternal()
        {
            Label externalLabel = new Label()
            {
                Location = new Point(5, 410),
                Text = "External Interfaces:",
                Font = new Font("Serif", 14, FontStyle.Bold),
                Size = new Size(250, 25)
            };

            Button addExternal = new Button()
            {
                Location = new Point(260, 410),
                Size = new Size(100, 20),
                Text = "Add",
                BackColor = Color.WhiteSmoke
            };

            Button deleteExternal = new Button()
            {
                Location = new Point(360, 410),
                Size = new Size(100, 20),
                Text = "Delete",
                BackColor = Color.WhiteSmoke
            };

            addExternal.Click += OnAddExternalClick;
            deleteExternal.Click += OnDeleteExternalClick;

            externalDatagrid.ColumnCount = 1;
            externalDatagrid.Name = "externalDataGridView";
            externalDatagrid.AllowUserToAddRows = false;
            externalDatagrid.Location = new Point(5, 450);
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


        private void RenderVlan()
        {
            Label vlanLabel = new Label()
            {
                Location = new Point(5, 210),
                Text = "VLAN Interfaces:",
                Font = new Font("Serif", 14, FontStyle.Bold),
                Size = new Size(200, 25)
            };

            Button addVlan = new Button()
            {
                Location = new Point(210, 210),
                Size = new Size(100, 20),
                Text = "Add",
                BackColor = Color.WhiteSmoke
            };

            Button deleteVlan = new Button()
            {
                Location = new Point(310, 210),
                Size = new Size(100, 20),
                Text = "Delete",
                BackColor = Color.WhiteSmoke
            };

            addVlan.Click += OnAddVlanClick;
            deleteVlan.Click += OnDeleteVlanClick;

            vlanDatagrid.ColumnCount = 2;
            vlanDatagrid.Name = "vlansDataGridView";
            vlanDatagrid.AllowUserToAddRows = false;
            vlanDatagrid.Location = new Point(5, 250);
            vlanDatagrid.Size = new Size(300, 150);
            vlanDatagrid.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            vlanDatagrid.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            vlanDatagrid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            vlanDatagrid.GridColor = Color.Black;
            vlanDatagrid.RowHeadersVisible = true;

            vlanDatagrid.Columns[0].Name = "IP Address";
            vlanDatagrid.Columns[1].Name = "VLAN ID";



            this.Controls.Add(vlanLabel);
            this.Controls.Add(addVlan);
            this.Controls.Add(deleteVlan);
            this.Controls.Add(vlanDatagrid);
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


            Label ipBaseLabel = new Label
            {
                Location = new Point(5, 30),
                Text = "IP Address:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label defaultRouteLabel = new Label
            {
                Location = new Point(5, 60),
                Text = "Default Route:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };


            Label amountCpuLabel = new Label
            {
                Location = new Point(5, 90),
                Text = "Amount CPU:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label coresLabel = new Label
            {
                Location = new Point(5, 120),
                Text = "Cores:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label startLabel = new Label
            {
                Location = new Point(5, 150),
                Text = "Start command:",
                Font = new Font("Serif", 12),
                Size = new Size(130, 20)
            };

            Label stopLabel = new Label
            {
                Location = new Point(5, 180),
                Text = "Stop command:",
                Font = new Font("Serif", 12),
                Size = new Size(130, 20)
            };

            type.Location = new Point(340, 90);
            type.Size = new Size(100, 20);


            ipAddress.Location = new Point(135, 30);
            ipAddress.Size = new Size(200, 20);
            
            defaultRoute.Location = new Point(135, 60);
            defaultRoute.Size = new Size(200, 20);

            amountCpu.Location = new Point(135, 90);
            amountCpu.Size = new Size(200, 20);

            cores.Location = new Point(135, 120);
            cores.Size = new Size(200, 20);

            startCommand.Location = new Point(135, 150);
            startCommand.Size = new Size(300, 20);

            stopCommand.Location = new Point(135, 180);
            stopCommand.Size = new Size(300, 20);

            Controls.Add(ipBaseLabel);
            Controls.Add(ipAddress);

            Controls.Add(defaultRouteLabel);
            Controls.Add(defaultRoute);

            Controls.Add(amountCpuLabel);
            Controls.Add(amountCpu);

            Controls.Add(coresLabel);
            Controls.Add(cores);

            Controls.Add(startLabel);
            Controls.Add(startCommand);

            Controls.Add(stopLabel);
            Controls.Add(stopCommand);

            Controls.Add(type);

        }

       

        private void LoadVm()
        {
            //Todo :: Mapp VM to textblocks
            ipAddress.Text = Host.IpAddress;
            defaultRoute.Text = Host.DefaultRoute;
            amountCpu.Text = Host.AmountCPU;
            cores.Text = Host.Cores;
            startCommand.Text = Host.StartCommand;
            stopCommand.Text = Host.StopCommad;
            var item = comboboxItems.FindIndex(x => x == Host.Type);
            type.SelectedIndex = item;

            foreach (var b in Host.VlanInterfaces)
            {
                string[] vlanRow = { b.IpAddress,b.VlanId };
                vlanDatagrid.Rows.Add(vlanRow);
            }

            foreach (var b in Host.ExternalInterfaces)
            {
                string[] externalRow = { b.Name };
                externalDatagrid.Rows.Add(externalRow);
            }

            foreach (var b in Host.PrivateDiroctories)
            {
                string[] privateRow = { b.Mount, b.Directory };
                privateDatagrid.Rows.Add(privateRow);
            }
        }


        private void OnSave_Click(object sender, EventArgs e)
        {

            //todo: Map textblocks to hostVM
            Host.IpAddress = ipAddress.Text;
            Host.DefaultRoute = defaultRoute.Text;
            Host.AmountCPU = amountCpu.Text;
            Host.Cores = cores.Text;
            Host.StartCommand = startCommand.Text;
            Host.StopCommad = stopCommand.Text;
            Host.Type = type.SelectedItem as string;

            var vlans = new List<VlanInterface>();
            foreach (DataGridViewRow row in vlanDatagrid.Rows)
            {
                var first = row.Cells[0].Value;
                var second = row.Cells[1].Value;
                if (first != null && second != null)
                {
                    vlans.Add(new VlanInterface() { IpAddress = first.ToString(), VlanId = second.ToString() });
                }
            }
            Host.VlanInterfaces = vlans;

            var externals = new List<ExternalInterface>();
            foreach (DataGridViewRow row in externalDatagrid.Rows)
            {
                var first = row.Cells[0].Value;
                if (first != null)
                {
                    externals.Add(new ExternalInterface() { Name = first.ToString() });
                }
            }
            Host.ExternalInterfaces = externals;


            var privates = new List<PrivateDiroctory>();
            foreach (DataGridViewRow row in privateDatagrid.Rows)
            {
                var first = row.Cells[0].Value;
                var second = row.Cells[1].Value;
                if (first != null && second != null)
                {
                    privates.Add(new PrivateDiroctory() { Mount = first.ToString(), Directory = second.ToString() });
                }
            }
            Host.PrivateDiroctories = privates;

            _paretn.AddHostButton(Host);
            this.Visible = false;
        }

        private void OnDelete_Click(object sender, EventArgs e)
        {
            if(_isNew != true)
                _paretn.DeleteHostButton(Host);
            this.Visible = false;
        }

        private void OnExit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }


        private void OnAddVlanClick(object sender, EventArgs e)
        {
            vlanDatagrid.Rows.Add();
        }

        private void OnDeleteVlanClick(object sender, EventArgs e)
        {
            if (this.vlanDatagrid.SelectedRows.Count > 0 )
            {
                this.vlanDatagrid.Rows.RemoveAt(
                    this.vlanDatagrid.SelectedRows[0].Index);
            }
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


        private void OnAddPrivateClick(object sender, EventArgs e)
        {
            privateDatagrid.Rows.Add();
        }

        private void OnDeletePrivateClick(object sender, EventArgs e)
        {
            if (this.privateDatagrid.SelectedRows.Count > 0)
            {
                this.privateDatagrid.Rows.RemoveAt(
                    this.privateDatagrid.SelectedRows[0].Index);
            }
        }
    }
}

