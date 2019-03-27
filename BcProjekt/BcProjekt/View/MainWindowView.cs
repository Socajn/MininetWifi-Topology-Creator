using System;
using System.Windows.Forms;
using System.Drawing;
using BcProjekt.View.Items;
using BcProjekt.Model;
using System.Collections.Generic;

namespace BcProjekt.View
{
    public class MainWindowView : Form
    {
        public MainVM topology;

        private bool isDragging = false;
        private int oldX, oldY;
        ToolStripMenuItem Link;


        public MainWindowView()
        {
            topology = new MainVM();
            Text = "Main Window";
            Size = new Size(1000, 600);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            BackColor = Color.FromKnownColor(KnownColor.White);

            Render();

            CenterToScreen();

        }


        private void Render()
        {
            MenuStrip ms = new MenuStrip
            {
                Parent = this
            };

            ToolStripMenuItem File = new ToolStripMenuItem("&File");
            ToolStripMenuItem New = new ToolStripMenuItem("&New", null, OnNew_Click);
            ToolStripMenuItem Open = new ToolStripMenuItem("&Open", null); //ToDo: open json file and deserialize it to my VM
            ToolStripMenuItem Save = new ToolStripMenuItem("&Save", null); //ToDo: Save json file and serialize my VM
            ToolStripMenuItem Export = new ToolStripMenuItem("&Export to .py script", null); //ToDo: Save python file with topology from VMs
            ToolStripMenuItem Exit = new ToolStripMenuItem("&Exit", null, OnExit_Click);


            New.ShortcutKeys = Keys.Control | Keys.N;
            Open.ShortcutKeys = Keys.Control | Keys.O;
            Save.ShortcutKeys = Keys.Control | Keys.S;
            Export.ShortcutKeys = Keys.Control | Keys.E;
            Exit.ShortcutKeys = Keys.Control | Keys.X;

            File.DropDownItems.Add(New);
            File.DropDownItems.Add(Open);
            File.DropDownItems.Add(Save);
            File.DropDownItems.Add(Export);
            File.DropDownItems.Add(Exit);


            ToolStripMenuItem Edit = new ToolStripMenuItem("&Edit");
            //ToolStripMenuItem Add = new ToolStripMenuItem("&Add...");

            //ToolStripMenuItem Properties = new ToolStripMenuItem("&Properties", null, Onproperties_Click);
            ToolStripMenuItem Host = new ToolStripMenuItem("&Host", null, OnHostAdd_Click);
            ToolStripMenuItem Station = new ToolStripMenuItem("&Station", null, OnStationAdd_Click);
            ToolStripMenuItem Switch = new ToolStripMenuItem("&Switch", null, OnSwitchAdd_Click);
            ToolStripMenuItem Ap = new ToolStripMenuItem("&Access Point", null, OnApAdd_Click);
            ToolStripMenuItem LegacySwitch = new ToolStripMenuItem("&Legacy Switch", null, OnLsAdd_Click);
            ToolStripMenuItem LegacyRouter = new ToolStripMenuItem("&Legacy Router", null, OnLrAdd_Click);
            ToolStripMenuItem NetLink = new ToolStripMenuItem("&Net Link", null, OnNetLinkAdd_Click);
            ToolStripMenuItem Controler = new ToolStripMenuItem("&Controler", null, OnControllerAdd_Click);

            //Properties.ShortcutKeys = Keys.Alt | Keys.P;
            Host.ShortcutKeys = Keys.Alt | Keys.H;
            Station.ShortcutKeys = Keys.Alt | Keys.S;
            Switch.ShortcutKeys = Keys.Alt | Keys.O;
            Ap.ShortcutKeys = Keys.Alt | Keys.A;
            LegacySwitch.ShortcutKeys = Keys.Alt | Keys.X;
            LegacyRouter.ShortcutKeys = Keys.Alt | Keys.Z;
            NetLink.ShortcutKeys = Keys.Alt | Keys.N;
            Controler.ShortcutKeys = Keys.Alt | Keys.C;


            //Edit.DropDownItems.Add(Properties);
            //Edit.DropDownItems.Add(Add);
            Edit.DropDownItems.Add(Host);
            Edit.DropDownItems.Add(Station);
            Edit.DropDownItems.Add(Switch);
            Edit.DropDownItems.Add(Ap);
            Edit.DropDownItems.Add(LegacySwitch);
            Edit.DropDownItems.Add(LegacyRouter);
            Edit.DropDownItems.Add(NetLink);
            Edit.DropDownItems.Add(Controler);

            ToolStripMenuItem Run = new ToolStripMenuItem("&Run");
            ToolStripMenuItem RunFile = new ToolStripMenuItem("&Run selected item...", null, StartMode_Click)
            {
                ShortcutKeys = Keys.Control | Keys.R
            };

            Run.DropDownItems.Add(RunFile);

            Link = new ToolStripMenuItem("&Links");


            ms.Items.Add(File);
            ms.Items.Add(Edit);
            ms.Items.Add(Run);
            ms.Items.Add(Link);
            MainMenuStrip = ms;


        }

        private void OnNew_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Application.Run(new MainWindowView());
        }

        private void OnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnHostAdd_Click(Object sender, EventArgs e)
        {
            var host = new HostVM(topology.Hosts.Count + 1 );
            Application.Run(new HostView(host, this, true));
        }

        private void OnStationAdd_Click(Object sender, EventArgs e)
        {
            var station = new StationVM(topology.Stations.Count + 1);
            Application.Run(new StationView(station,this,true));
        }

        private void OnSwitchAdd_Click(Object sender, EventArgs e)
        {
            var switchvm = new SwitchVM(topology.Switches.Count + 1);
            Application.Run(new SwitchView(switchvm, this, true));
        }

        private void OnApAdd_Click(Object sender, EventArgs e)
        {
            var ap = new ApVM(topology.Aps.Count + 1);
            Application.Run(new ApView(ap,this,true));
        }

        private void OnLsAdd_Click(Object sender, EventArgs e)
        {
            var ls = new LegacySwitchVM(topology.LegacySwitches.Count + 1);
            AddLegacySwitchButton(ls);

        }

        private void OnLrAdd_Click(Object sender, EventArgs e)
        {
            var lr = new LegacyRouterVM(topology.LegacyRouters.Count + 1);
            AddLegacyRouter(lr);
        }

        private void OnNetLinkAdd_Click(Object sender, EventArgs e)
        {
            var nl = new NetLinkVM(topology.NetLinks.Count + 1);
            Application.Run(new NetLinkView(nl, this, true, GetAllVMs()));
        }

        private void OnControllerAdd_Click(Object sender, EventArgs e)
        {
            var contro = new ControllerVM(topology.Controllers.Count + 1);
            Application.Run(new ControllerView(contro,this,true));
        }

        private void StartMode_Click(object sender, EventArgs e)
        {
            Application.Run(new StartModeView(this));
        }



        private void OnLinkEdit(Object sender, EventArgs e)
        {
            //get button name
            var a = sender as ToolStripDropDownItem;
            var n = a.Name;
            var nl = topology.NetLinks.Find(x => x.Name == n);
            //todo: draw line for old start and end

            Application.Run(new NetLinkView(nl, this, false, GetAllVMs()));
        }








        //------------------------------------------Add buttons methods-----------------------------------------------------

        public void AddHostButton(HostVM host)
        {

            var ho = topology.Hosts.FindIndex(x => x.Hostname == host.Hostname);

            if(ho == -1)
            {

                topology.Hosts.Add(host);
                Button button = new Button()
                {
                    Name = host.Hostname,
                    Text = host.Hostname,
                    BackColor = Color.LightGreen,
                    Location = new Point(500, 300),
                    Size = new Size(30, 30)
                };
                button.MouseDown += OnMouseDown;
                button.MouseUp += OnMouseUp;
                button.MouseMove += OnMouseMove;

                this.Controls.Add(button);

            }
        }

        public void AddStationButton(StationVM station)
        {
            var ho = topology.Stations.FindIndex(x => x.Name == station.Name);

            if(ho == -1)
            {
                topology.Stations.Add(station);
                Button button = new Button()
                {
                    Name = station.Name,
                    Text = station.Name,
                    BackColor = Color.LightBlue,
                    Location = new Point(400, 300),
                    Size = new Size(30, 30)
                };

                button.MouseDown += OnMouseDown;
                button.MouseUp += OnMouseUp;
                button.MouseMove += OnMouseMove;

                this.Controls.Add(button);
            }
            var but = this.Controls.Find(station.Name, true);

            var top = but[0].Top + 15;
            var left = but[0].Left + 15;

            Graphics l = CreateGraphics();
            Pen p = new Pen(Color.White, 3);
            Pen p2 = new Pen(Color.Green, 3);
            l.DrawEllipse(p, left - station.PreviousSignalRange, top - station.PreviousSignalRange, 2 * station.PreviousSignalRange, 2 * station.PreviousSignalRange);
            l.DrawEllipse(p2, left - station.SignalRange, top - station.SignalRange, 2 * station.SignalRange, 2 * station.SignalRange);

        }

        public void AddApButton(ApVM ap)
        {
            var ho = topology.Aps.FindIndex(x => x.Name == ap.Name);


            if (ho == -1)
            {

                topology.Aps.Add(ap);
                Button button = new Button()
                {
                    Name = ap.Name,
                    Text = ap.Name,
                    BackColor = Color.LightYellow,
                    Location = new Point(400, 300),
                    Size = new Size(30, 30)
                };

                button.MouseDown += OnMouseDown;
                button.MouseUp += OnMouseUp;
                button.MouseMove += OnMouseMove;

                this.Controls.Add(button);

            }
            var but = this.Controls.Find(ap.Name, true);

            var top = but[0].Top + 15;
            var left = but[0].Left + 15;

            Graphics l = CreateGraphics();
            Pen p = new Pen(Color.White, 3);
            Pen p2 = new Pen(Color.Yellow, 3);
            l.DrawEllipse(p, left - ap.PreviousSignalRange, top - ap.PreviousSignalRange, 2 * ap.PreviousSignalRange, 2 * ap.PreviousSignalRange);
            l.DrawEllipse(p2, left - ap.SignalRange, top - ap.SignalRange, 2 * ap.SignalRange, 2 * ap.SignalRange);
        }

        public void AddSwithcButton(SwitchVM switchVM)
        {
            var ho = topology.Switches.FindIndex(x => x.Name == switchVM.Name);

            if (ho == -1)
            {

                topology.Switches.Add(switchVM);
                Button button = new Button()
                {
                    Name = switchVM.Name,
                    Text = switchVM.Name,
                    BackColor = Color.LightCoral,
                    Location = new Point(300, 500),
                    Size = new Size(30, 30)
                };
                button.MouseDown += OnMouseDown;
                button.MouseUp += OnMouseUp;
                button.MouseMove += OnMouseMove;

                this.Controls.Add(button);

            }
        }

        public void AddControllerButton(ControllerVM contro)
        {
            var ho = topology.Controllers.FindIndex(x => x.Name == contro.Name);

            if (ho == -1)
            {

                topology.Controllers.Add(contro);
                Button button = new Button()
                {
                    Name = contro.Name,
                    Text = contro.Name,
                    BackColor = Color.LightSeaGreen,
                    Location = new Point(300, 500),
                    Size = new Size(30, 30)
                };
                button.MouseDown += OnMouseDown;
                button.MouseUp += OnMouseUp;
                button.MouseMove += OnMouseMove;

                this.Controls.Add(button);

            }
        }

        public void AddLegacySwitchButton(LegacySwitchVM ls)
        {
            var ho = topology.LegacySwitches.FindIndex(x => x.Name == ls.Name);

            if (ho == -1)
            {

                topology.LegacySwitches.Add(ls);
                Button button = new Button()
                {
                    Name = ls.Name,
                    Text = ls.Name,
                    BackColor = Color.LightPink,
                    Location = new Point(300, 500),
                    Size = new Size(30, 30)
                };
                button.MouseDown += OnMouseDown;
                button.MouseUp += OnMouseUp;
                button.MouseMove += OnMouseMove;

                this.Controls.Add(button);
            }
        }

        public void AddLegacyRouter(LegacyRouterVM lr)
        {
            var ho = topology.LegacyRouters.FindIndex(x => x.Name == lr.Name);

            if (ho == -1)
            {

                topology.LegacyRouters.Add(lr);
                Button button = new Button()
                {
                    Name = lr.Name,
                    Text = lr.Name,
                    BackColor = Color.LightCyan,
                    Location = new Point(300, 500),
                    Size = new Size(30, 30)
                };
                button.MouseDown += OnMouseDown;
                button.MouseUp += OnMouseUp;
                button.MouseMove += OnMouseMove;

                this.Controls.Add(button);
            }
        }

        public void AddLinkButton(NetLinkVM link)
        {
            var ho = topology.NetLinks.FindIndex(x => x.Name == link.Name);

            if(ho == -1)
            {
                topology.NetLinks.Add(link);
                ToolStripMenuItem netLink = new ToolStripMenuItem(link.Name + "(" + link.Start + "-" + link.End + ")", null, OnLinkEdit)
                {
                    Name = link.Name
                };
                Link.DropDownItems.Add(netLink);
                //todo : DRaw the line for link

            }
            else
            {
                var b = Link.DropDownItems.Find(link.Name, true);
                b[0].Text = link.Name + "(" + link.Start + "-" + link.End + ")";

                //todo : Draw the line for link
            }
        }






        //--------------------------------------Delete items-------------------------------------------

        public void DeleteHostButton(HostVM host)
        {
            var but = this.Controls.Find(host.Hostname, true);

            if(but[0] != null)
            {
                this.Controls.Remove(but[0]);

                host.IsDeleted = true;
            }



        }

        public void DeleteStationButton(StationVM station)
        {
            var but = this.Controls.Find(station.Name, true);

            if(but[0] != null)
            {

                var top = but[0].Top + 15;
                var left = but[0].Left + 15;

                Graphics l = CreateGraphics();
                Pen p = new Pen(Color.White, 3);
                l.DrawEllipse(p, left - station.SignalRange, top - station.SignalRange, 2 * station.SignalRange, 2 * station.SignalRange);

                this.Controls.Remove(but[0]);



                station.IsDeleted = true;
            }
        }

        public void DeleteApButton(ApVM ap)
        {
            var but = this.Controls.Find(ap.Name, true);

            if (but[0] != null)
            {

                var top = but[0].Top + 15;
                var left = but[0].Left + 15;

                Graphics l = CreateGraphics();
                Pen p = new Pen(Color.White, 3);
                l.DrawEllipse(p, left - ap.SignalRange, top - ap.SignalRange, 2 * ap.SignalRange, 2 * ap.SignalRange);


                this.Controls.Remove(but[0]);

                ap.IsDeleted = true;
            }
        }

        public void DeleteSwitchButton(SwitchVM switchVM)
        {
            var but = this.Controls.Find(switchVM.Name, true);

            if (but[0] != null)
            {
                this.Controls.Remove(but[0]);

                switchVM.IsDeleted = true;
            }
        }


        public void DeleteControllerButton(ControllerVM contro)
        {
            var but = this.Controls.Find(contro.Name, true);

            if (but[0] != null)
            {
                this.Controls.Remove(but[0]);

                contro.IsDeleted = true;
            }
        }

        public void DeleteLegacySwitchButton(LegacySwitchVM ls)
        {
            var but = this.Controls.Find(ls.Name, true);

            if (but[0] != null)
            {
                this.Controls.Remove(but[0]);

                ls.IsDeleted = true;
            }
        }


        public void DeleteLegacyRouterButton(LegacyRouterVM lr)
        {
            var but = this.Controls.Find(lr.Name, true);

            if (but[0] != null)
            {
                this.Controls.Remove(but[0]);

                lr.IsDeleted = true;
            }
        }

        public void DeleteLinkButton(NetLinkVM link)
        {
            var l = Link.DropDownItems.Find(link.Name, true);

            if(l[0] != null)
            {
                Link.DropDownItems.Remove(l[0]);

                link.IsDeleted = false;

                //todo : Draw white line for link
            }
        }




        //Event Handlers for moving and enditing buttons
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            switch (MouseButtons)
            {
                case MouseButtons.Left:
                    isDragging = true;
                    oldX = e.X;
                    oldY = e.Y;
                    var a = sender as Button;
                    var n = a.Name;
                    if(n[0] == 'a')
                    {
                        var left = a.Left + 15;
                        var top = a.Top + 15;
                        var ap = topology.Aps.Find(x => x.Name == n);
                        Graphics l = CreateGraphics();
                        Pen p = new Pen(Color.White, 3);
                        l.DrawEllipse(p, left - ap.SignalRange, top - ap.SignalRange, 2 * ap.SignalRange, 2 * ap.SignalRange);
                    }

                    if (n.Contains("sta"))
                    {
                        var left = a.Left + 15;
                        var top = a.Top + 15;
                        var sta = topology.Stations.Find(x => x.Name == n);
                        Graphics l = CreateGraphics();
                        Pen p = new Pen(Color.White, 3);
                        l.DrawEllipse(p, left - sta.SignalRange, top - sta.SignalRange, 2 * sta.SignalRange, 2 * sta.SignalRange);
                    }

                    //todo: subor

                    break;

                case MouseButtons.Right:
                    var b = sender as Button;
                    var name = b.Name;
                    switch (name.Substring(0, 1))
                    {
                        case "h":
                            var host = topology.Hosts.Find(x => x.Hostname == name);
                            Application.Run(new HostView(host, this, false));
                            break;

                        case "s":
                            switch (name.Substring(1, 1))
                            {
                                case "t":
                                    var station = topology.Stations.Find(x => x.Name == name);
                                    Application.Run(new StationView(station, this, false));
                                    break;
                                default:
                                    var switchVm = topology.Switches.Find(x => x.Name == name);
                                    Application.Run(new SwitchView(switchVm, this, false));
                                    break;
                            }
                            break;

                        case "a":
                            var ap = topology.Aps.Find(x => x.Name == name);
                            Application.Run(new ApView(ap, this, false));
                            break;

                        case "l":
                            switch (name.Substring(1, 1))
                            {
                                case "s":
                                    var ls = topology.LegacySwitches.Find(x => x.Name == name);
                                    DeleteLegacySwitchButton(ls);
                                    break;
                                case "r":
                                    var lr = topology.LegacyRouters.Find(x => x.Name == name);
                                    DeleteLegacyRouterButton(lr);
                                    break;
                            }

                            break;

                        case "c":
                            var contro = topology.Controllers.Find(x => x.Name == name);
                            Application.Run(new ControllerView(contro, this, false));
                            break;


                    }



                    break;
            }
                

        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {

            var button = sender as Button;
            if (isDragging)
            {
                button.Top = button.Top + (e.Y - oldY);
                button.Left = button.Left + (e.X - oldX);
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;

            var b = sender as Button;
            var n = b.Name;
            if (n[0] == 'a')
            {
                var left = b.Left + 15;
                var top = b.Top + 15;
                var ap = topology.Aps.Find(x => x.Name == n);
                Graphics l = CreateGraphics();
                Pen p = new Pen(Color.Yellow, 3);
                l.DrawEllipse(p, left - ap.SignalRange, top - ap.SignalRange, 2 * ap.SignalRange, 2 * ap.SignalRange);
            }

            if (n.Contains("sta"))
            {
                var left = b.Left + 15;
                var top = b.Top + 15;
                var sta = topology.Stations.Find(x => x.Name == n);
                Graphics l = CreateGraphics();
                Pen p = new Pen(Color.Green, 3);
                l.DrawEllipse(p, left - sta.SignalRange, top - sta.SignalRange, 2 * sta.SignalRange, 2 * sta.SignalRange);
            }

            //todo : Subor

        }





        private List<string> GetAllVMs()
        {

            var list = new List<string>();

            topology.Aps.FindAll(x => x.IsDeleted == false).ForEach(a => list.Add(a.Name));
            topology.Controllers.FindAll(x => x.IsDeleted == false).ForEach(c => list.Add(c.Name));
            topology.Hosts.FindAll(x => x.IsDeleted == false).ForEach(h => list.Add(h.Hostname));
            topology.LegacyRouters.FindAll(x => x.IsDeleted == false).ForEach(lr => list.Add(lr.Name));
            topology.LegacySwitches.FindAll(x => x.IsDeleted == false).ForEach(ls => list.Add(ls.Name));
            topology.Stations.FindAll(x => x.IsDeleted == false).ForEach(s => list.Add(s.Name));
            topology.Switches.FindAll(x => x.IsDeleted == false).ForEach(s => list.Add(s.Name));

            return list;

        }

        public bool IsConected(string start, string end)
        {
            var line = topology.NetLinks.Find(x => (x.Start == start && x.End == end) || (x.Start == end && x.End == start));
            if (line == null)
                return true;
            else
                return false;
        }

    }
}
