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
        private Annotations annotations;

        public MainWindow()
        {
            InitializeComponent();

            var annotater = new AnnotaterGit();
            SetAnnotations(annotater.GetAnnotations("C:\\Users\\Jez\\eclipse-workspace\\mvr.api-merge\\src\\starship\\mvr\\model\\db\\FriendsDB.java"));
        }

        void SetAnnotations(Annotations annotations)
        {
            this.annotations = annotations;

            textView.DocumentText = annotations.GetHTML();

            mapView.Image = annotations.CreateImage(mapView.Width, mapView.Height);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void OnMapViewResized(object sender, EventArgs e)
        {
            if (annotations != null)
                mapView.Image = annotations.CreateImage(mapView.Width, mapView.Height);
        }

        private void OnMapViewClicked(object sender, EventArgs e)
        {

        }

        private void OnTextViewRegionChanged(object sender, EventArgs e)
        {
        }

        private void OnTextViewRegionChanged(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            textView.Document.Window.AttachEventHandler("onscroll", OnTextViewScroll);
            UpdateMap();
        }

        private void OnTextViewScroll(object sender, EventArgs e)
        {
            UpdateMap();
        }

        private void UpdateMap()
        {
            var top = textView.Document.GetElementsByTagName("HTML")[0].ScrollTop;
//            var top = textView.Document.GetElementsByTagName("HTML")[0].ScrollTop;
            int from = GetLine(1, 1);
            int to = GetLine(1, textView.Height - 2);
        }

        private int GetLine(int x, int y)
        {
            var element = textView.Document.GetElementFromPoint(new Point(x, y));
            if (element == null)
                return 0;

            var attrib = element.GetAttribute("name");
            if (element.InnerHtml.StartsWith("<FONT"))
                return 0;

            return int.Parse(element.Name.Substring(5));
        }
    }
}
