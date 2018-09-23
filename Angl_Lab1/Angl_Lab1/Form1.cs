using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Angl_Lab1
{
    public partial class Form1 : Form
    {
        GetFriends getFriends;

        public Form1()
        {
            InitializeComponent();
            getFriends = new GetFriends(this);
        }

        private void buttonAuth_Click(object sender, EventArgs e)
        {
            try
            {
                getFriends.Authorization();
                textBoxMain.Text = "Авторизация прошла успешно";
                buttonGetFriends.Enabled = true;
            }
            catch
            {
                textBoxMain.Text = "Авторизация не пройдена";
            }
        }

        private void buttonGetFriends_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = null;
            getFriends.GetFriendInformation();
        }
    }
}
