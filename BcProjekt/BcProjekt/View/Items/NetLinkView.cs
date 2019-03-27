using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BcProjekt.Model;

namespace BcProjekt.View.Items
{
    public class NetLinkView : Form
    {

        private NetLinkVM NetLink;
        private MainWindowView _parent;
        private bool _isNew;
        private List<string> _pointItems;

        private ComboBox start = new ComboBox();
        private ComboBox end = new ComboBox();

        private ComboBox conType = new ComboBox();

        private TextBox ssid = new TextBox();
        private TextBox channe = new TextBox();

        private ComboBox mode = new ComboBox();

        private TextBox bandwith = new TextBox();
        private TextBox delay = new TextBox();
        private TextBox queSize = new TextBox();
        private TextBox jitter = new TextBox();
        private TextBox speedup = new TextBox();
        private TextBox loss = new TextBox();


        List<string> typeItems = new List<string>(new[] { "wired", "adhoc", "mesh", "wifi", "lowpan" });
        List<string> modeitems = new List<string>(new[] { "a", "b", "g", "n"});

        public NetLinkView(NetLinkVM net, MainWindowView parent, bool isNew, List<string> pointItems)
        {
            NetLink = net;
            _parent = parent;
            _isNew = isNew;
            _pointItems = pointItems;

            typeItems.ForEach(x => conType.Items.Add(x));
            modeitems.ForEach(x => mode.Items.Add(x));
            pointItems.ForEach(x => start.Items.Add(x));
            pointItems.ForEach(x => end.Items.Add(x));
            //fill start and end with names of everything in topology

            Text = NetLink.Name + " settings";
            Size = new Size(500, 600);
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

            File.DropDownItems.Add(Save);
            File.DropDownItems.Add(Delete);
            File.DropDownItems.Add(Back);

            ms.Items.Add(File);
            MainMenuStrip = ms;


            Label startLabel = new Label
            {
                Location = new Point(5, 30),
                Text = "Start:",
                Font = new Font("Serif", 12),
                Size = new Size(75, 20)
            };

            Label endLabel = new Label
            {
                Location = new Point(230, 30),
                Text = "End:",
                Font = new Font("Serif", 12),
                Size = new Size(75, 20)
            };

            Label typeLabel = new Label
            {
                Location = new Point(5, 60),
                Text = "Type:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label ssidLabel = new Label
            {
                Location = new Point(5, 90),
                Text = "SSID:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };


            Label channelLabel = new Label
            {
                Location = new Point(5, 120),
                Text = "Channel:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label modeLabel = new Label
            {
                Location = new Point(5, 150),
                Text = "Mode:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label bandLabel = new Label
            {
                Location = new Point(5, 180),
                Text = "Bandwith:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label delayLabel = new Label
            {
                Location = new Point(5, 210),
                Text = "Delay:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label lossLabel = new Label
            {
                Location = new Point(5, 240),
                Text = "Loss:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label queSizelabel = new Label
            {
                Location = new Point(5, 270),
                Text = "Queue size:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label speedLabel = new Label
            {
                Location = new Point(5, 300),
                Text = "Speedup:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };

            Label jiterLabel = new Label
            {
                Location = new Point(5, 330),
                Text = "Jitter:",
                Font = new Font("Serif", 12),
                Size = new Size(120, 20)
            };


            start.Location = new Point(100, 30);

            end.Location = new Point(310, 30);

            conType.Location = new Point(135, 60);
            conType.Size = new Size(180, 20);

            ssid.Location = new Point(135, 90);
            ssid.Size = new Size(180, 20);

            channe.Location = new Point(135, 120);
            channe.Size = new Size(180, 20);

            mode.Location = new Point(135, 150);
            mode.Size = new Size(180, 20);

            bandwith.Location = new Point(135, 180);
            bandwith.Size = new Size(180, 20);

            delay.Location = new Point(135, 210);
            delay.Size = new Size(180, 20);

            loss.Location = new Point(135, 240);
            loss.Size = new Size(180, 20);

            queSize.Location = new Point(135, 270);
            queSize.Size = new Size(180, 20);

            speedup.Location = new Point(135, 300);
            speedup.Size = new Size(180, 20);

            jitter.Location = new Point(135, 330);
            jitter.Size = new Size(180, 20);


            Controls.Add(startLabel);
            Controls.Add(start);

            Controls.Add(endLabel);
            Controls.Add(end);

            Controls.Add(typeLabel);
            Controls.Add(ssidLabel);
            Controls.Add(channelLabel);
            Controls.Add(modeLabel);
            Controls.Add(bandLabel);
            Controls.Add(delayLabel);
            Controls.Add(lossLabel);
            Controls.Add(queSizelabel);
            Controls.Add(speedLabel);
            Controls.Add(jiterLabel);

            Controls.Add(conType);
            Controls.Add(ssid);
            Controls.Add(channe);
            Controls.Add(mode);
            Controls.Add(bandwith);
            Controls.Add(delay);
            Controls.Add(loss);
            Controls.Add(queSize);
            Controls.Add(speedup);
            Controls.Add(jitter);

        }

        private void LoadVM()
        {
            //start and end
            var str = _pointItems.FindIndex(x => x == NetLink.Start);
            start.SelectedIndex = str;

            var en = _pointItems.FindIndex(x => x == NetLink.End);
            end.SelectedIndex = en;

            var typ = typeItems.FindIndex(x => x == NetLink.Connection);
            conType.SelectedIndex = typ;

            ssid.Text = NetLink.SSID;
            channe.Text = NetLink.Cahnnel.ToString();

            var mod = modeitems.FindIndex(x => x == NetLink.Mode);
            mode.SelectedIndex = mod;

            bandwith.Text = NetLink.Bandwith;
            delay.Text = NetLink.Delay;
            loss.Text = NetLink.Loss;
            queSize.Text = NetLink.MaxQueueSize;
            jitter.Text = NetLink.Jitter;
            speedup.Text = NetLink.Speedup;

        }


        private void OnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {

                NetLink.Start = start.SelectedItem as string;
                NetLink.End = end.SelectedItem as string;
                NetLink.Connection = conType.SelectedItem as string;
                NetLink.SSID = ssid.Text;
                NetLink.Cahnnel = Int32.Parse(channe.Text);
                NetLink.Mode = mode.SelectedItem as string ;
                NetLink.Bandwith = bandwith.Text;
                NetLink.Delay = delay.Text;
                NetLink.Loss = loss.Text;
                NetLink.MaxQueueSize = queSize.Text;
                NetLink.Jitter = jitter.Text;
                NetLink.Speedup = speedup.Text;


                _parent.AddLinkButton(NetLink);
                this.Visible = false;
            }
        }

        private void OnExit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void OnDelete_Click(object sender, EventArgs e)
        {
            if (_isNew != true)
                _parent.DeleteLinkButton(NetLink);
            this.Visible = false;
        }


        private bool ValidateData()
        {

            if (start.SelectedItem == null)
            {
                MessageBox.Show("Start point of link isnt chosen", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (end.SelectedItem == null)
            {
                MessageBox.Show("End point of link isnt chosen", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (start.SelectedItem as string == end.SelectedItem as string)
            {
                MessageBox.Show("Start and End cant be the same", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if(!_parent.IsConected(start.SelectedItem as string, end.SelectedItem as string))
            {
                MessageBox.Show("This connection between start and end already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
