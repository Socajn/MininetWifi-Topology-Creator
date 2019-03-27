using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;


namespace BcProjekt.View
{
    class StartModeView : Form
    {
        private Button Open;
        private TextBox textbox;
        private ToolTip openTooltip;

        private int WIDTH = 300;
        private int HEIGHT = 300;
        private int BUTTONS_SPACE = 15;
        private int PANEL_SPACE = 8;
        private int CLOSE_SPACE = 10;
        private string path = "";

        private Form _parent;

        public StartModeView(Form parent)
        {
            _parent = parent;
            Text = "StartMode";
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            ControlBox = false;
            Size = new Size(WIDTH, HEIGHT);
            BackColor = Color.FromKnownColor(KnownColor.LemonChiffon);

            RenderOpenDialog();
            RenderBackAndStart();
        }

        private void RenderOpenDialog()
        {
            Open = new Button();
            openTooltip = new ToolTip();
            Open.BackColor = Color.FromKnownColor(KnownColor.LightSalmon);
            Open.Text = "Open";
            openTooltip.SetToolTip(this.Open, "Open python file with topology to run");



            textbox = new TextBox();
            textbox.Parent = this;
            textbox.WordWrap = true;
            textbox.ScrollBars = ScrollBars.Both;
            textbox.Width = 300;
            textbox.Height = 200;
            textbox.Multiline = true;
            textbox.Enabled = true;
            textbox.Location = new Point(0, 30);

            Open.Click += OnClicked;

            Controls.Add(Open);
            Controls.Add(textbox);
        }

        private void RenderBackAndStart()
        {
            Button ok = new Button();
            ok.BackColor = Color.FromKnownColor(KnownColor.LightSalmon);

            int PANEL_HEIGHT = ok.Height + PANEL_SPACE;

            Panel panel = new Panel();
            panel.Height = PANEL_HEIGHT;
            panel.Dock = DockStyle.Bottom;
            panel.Parent = this;

            int x = ok.Width * 2 + BUTTONS_SPACE;
            int y = (PANEL_HEIGHT - ok.Height) / 2;

            ok.Text = "Ok";
            ok.Parent = panel;
            ok.Location = new Point(WIDTH - x, y);
            ok.Anchor = AnchorStyles.Right;
            ok.Click += Ok_Click;

            Button close = new Button();

            x = close.Width;

            close.Text = "Close";
            close.Parent = panel;
            close.Location = new Point(WIDTH - x - CLOSE_SPACE, y);
            close.Anchor = AnchorStyles.Right;
            close.BackColor = Color.FromKnownColor(KnownColor.LightSalmon);
            close.Click += Close_Click;

            CenterToScreen();
        }

        void Close_Click(object sender, EventArgs e)
        {
            var b = sender as Button;
            var t = b.Text;
            this.Visible = false;
        }

        void Ok_Click(object sender, EventArgs e)
        {
            if (path != "")
            {
                var command = "gnome-terminal -x bash -ic 'sudo python " + path + "; bash'";

                Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "/bin/bash";
                proc.StartInfo.Arguments = "-c \" " + command + " \"";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();

                while (!proc.StandardOutput.EndOfStream)
                {
                    Console.WriteLine(proc.StandardOutput.ReadLine());
                }
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                string Message = "You have to chose file before starting the topology through Mininet Wifi!";
                string caption = "Warning!";
                MessageBoxIcon icon = MessageBoxIcon.Warning;

                DialogResult result;

                result = MessageBox.Show(Message, caption, buttons, icon);
            }

        }


        void OnClicked(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Python files (*.py)|*.py";

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo info = new FileInfo(dialog.FileName);
                textbox.Text = info.DirectoryName +"/"+ info.Name;
                path = info.DirectoryName + "/" + info.Name;
            }
        }



    }
}
