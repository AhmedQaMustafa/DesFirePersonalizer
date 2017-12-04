using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesFirePersonalizer
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            LoginFrm login = new LoginFrm();
            login.ShowDialog();

        }

        private void applicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MainForm userpage = new MainForm();

            userpage.MdiParent = this;
            userpage.Left = 0;
            userpage.Top = 0;
            userpage.Size = this.ClientRectangle.Size;
            userpage.Dock = DockStyle.Fill;
            userpage.Show();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Savebtn userpage = new Savebtn();
            userpage.MdiParent = this;
            userpage.Left = 0;
            userpage.Top = 0;
            userpage.Size = this.ClientRectangle.Size;
            userpage.Dock = DockStyle.Fill;
            userpage.Show();
        }
    }
}
