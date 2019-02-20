using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YellowSnow
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            var annotater = new AnnotaterGit();
            var annotations = annotater.GetAnnotations("C:\\Users\\Jez\\eclipse-workspace\\mvr.api-merge\\src\\starship\\mvr\\model\\db\\FriendsDB.java");

            textView.DocumentText = annotations.GetHTML();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
