using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BcProjekt.Model;

namespace BcProjekt.View.Items
{
    public class SwitchView : Form
    {
        private SwitchVM Switch;
        private MainWindowView _parent;
        private bool _isNew;

        private TextBox dpid = new TextBox();

        private CheckBox netFlow = new CheckBox();
        private CheckBox sFlow = new CheckBox();

        private ComboBox type = new ComboBox();

        private TextBox ipAddress = new TextBox();
        private TextBox dpctlPort = new TextBox();
        private TextBox startCommand = new TextBox();
        private TextBox stopCommand = new TextBox();

        private DataGridView externalDatagrid = new DataGridView();

        List<string> comboboxItems = new List<string>(new[] { "default", "OpenV Switch kernel mode", "Indigo virtual switch", "Usersapce switch", "Userspace swith in namespace" });

        public SwitchView(SwitchVM switchvm, MainWindowView parent, bool isNew)
        {

            Switch = switchvm;
            _parent = parent;
            _isNew = isNew;

            comboboxItems.ForEach(x => type.Items.Add(x));

            Text = Switch.Name + " settigns";
            Size = new Size(500, 650);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromKnownColor(KnownColor.LightGray);

            RenderMain();
            RenderExternal();

            LoadVM();

            CenterToScreen();
        }

        private void RenderExternal()
        {
            Label externalLabel = new Label()
            {
                Location = new Point(5, 270),
                Text = "External Interfaces:",
                Font = new Font("Serif", 14, FontStyle.Bold),
                Size = new Size(250, 25)
            };

            Button addExternal = new Button()
            {
                Location = new Point(260, 270),
                Size = new Size(100, 20),
                Text = "Add",
                BackColor = Color.WhiteSmoke
            };

            Button deleteExternal = new Button()
            {
                Location = new Point(360, 270),
                Size = new Size(100, 20),
                Text = "Delete",
                BackColor = Color.WhiteSmoke
            };

            addExternal.Click += OnAddExternalClick;
            deleteExternal.Click += OnDeleteExternalClick;

            externalDatagrid.ColumnCount = 1;
            externalDatagrid.Name = "externalDataGridView";
            externalDatagrid.AllowUserToAddRows = false;
            externalDatagrid.Location = new Point(5, 310);
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

            Label dpidLabel = new Label
            {
                Location = new Point(5, 30),
                Text = "DPID:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label netFlowLabel = new Label
            {
                Location = new Point(5, 60),
                Text = "NetFlow:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label sFlowLabel = new Label
            {
                Location = new Point(5, 90),
                Text = "sFlow:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };


            Label typeLabel = new Label
            {
                Location = new Point(5, 120),
                Text = "Swithc Type:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label ipAddressLabel = new Label
            {
                Location = new Point(5, 150),
                Text = "Ip Address:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label dpctlLabel = new Label
            {
                Location = new Point(5, 180),
                Text = "DPCTL port:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label startLabel = new Label
            {
                Location = new Point(5, 210),
                Text = "Start command:",
                Font = new Font("Serif", 12),
                Size = new Size(130, 20)
            };

            Label stopLabel = new Label
            {
                Location = new Point(5, 240),
                Text = "Stop command:",
                Font = new Font("Serif", 12),
                Size = new Size(130, 20)
            };

            dpid.Location = new Point(135, 30);
            dpid.Size = new Size(200, 20);

            netFlow.Location = new Point(135, 60);

            sFlow.Location = new Point(135, 90);

            type.Location = new Point(135, 120);
            type.Size = new Size(180, 20);

            ipAddress.Location = new Point(135, 150);
            ipAddress.Size = new Size(100, 20);

            dpctlPort.Location = new Point(135, 180);
            dpctlPort.Size = new Size(180, 20);

            startCommand.Location = new Point(135, 210);
            startCommand.Size = new Size(300, 20);

            stopCommand.Location = new Point(135, 240);
            stopCommand.Size = new Size(300, 20);

            Controls.Add(dpidLabel);
            Controls.Add(dpid);

            Controls.Add(netFlowLabel);
            Controls.Add(netFlow);

            Controls.Add(sFlow);
            Controls.Add(sFlowLabel);

            Controls.Add(typeLabel);
            Controls.Add(type);

            Controls.Add(ipAddressLabel);
            Controls.Add(ipAddress);

            Controls.Add(dpctlLabel);
            Controls.Add(dpctlPort);

            Controls.Add(startLabel);
            Controls.Add(startCommand);

            Controls.Add(stopLabel);
            Controls.Add(stopCommand);

        }

        private void LoadVM()
        {
            dpid.Text = Switch.DPID;

            netFlow.Checked = Switch.NetFlow;
            sFlow.Checked = Switch.sFlow;

            var typ = comboboxItems.FindIndex(x => x == Switch.Type);
            type.SelectedIndex = typ;

            ipAddress.Text = Switch.IpAddress;
            dpctlPort.Text = Switch.DPCTLport;

            startCommand.Text = Switch.StartCommand;
            stopCommand.Text = Switch.StopCommad;

            foreach (var b in Switch.ExternalInterfaces)
            {
                string[] externalRow = { b.Name };
                externalDatagrid.Rows.Add(externalRow);
            }

        }

        private void OnSave_Click(object sender, EventArgs e)
        {
            Switch.DPID = dpid.Text;
            Switch.NetFlow = netFlow.Checked;
            Switch.sFlow = sFlow.Checked;
            Switch.Type = type.SelectedItem as string;
            Switch.IpAddress = ipAddress.Text;
            Switch.DPCTLport = dpctlPort.Text;
            Switch.StartCommand = startCommand.Text;
            Switch.StopCommad = stopCommand.Text;

            var externals = new List<ExternalInterface>();
            foreach (DataGridViewRow row in externalDatagrid.Rows)
            {
                var first = row.Cells[0].Value;
                if (first != null)
                {
                    externals.Add(new ExternalInterface() { Name = first.ToString() });
                }
            }
            Switch.ExternalInterfaces = externals;

            _parent.AddSwithcButton(Switch);
            this.Visible = false;
        }


        private void OnExit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void OnDelete_Click(object sender, EventArgs e)
        {
            if (_isNew != true)
                _parent.DeleteSwitchButton(Switch);
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
