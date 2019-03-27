using System;
using System.Drawing;
using System.Windows.Forms;

namespace BcProjekt.View.Items
{
    public class LegacyRouterView : Form
    {
        public LegacyRouterView()
        {
            Text = "Legacy router settings";
            Size = new Size(500, 300);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
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
                Location = new Point(5, 30),
                Text = "IP Base:",
                Font = new Font("Serif", 12),
                Size = new Size(75, 20)
            };


            TextBox ipBase = new TextBox
            {
                Location = new Point(80, 30)
            };



            Controls.Add(ipBaseLabel);
            Controls.Add(ipBase);
        }


        private void OnExit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
