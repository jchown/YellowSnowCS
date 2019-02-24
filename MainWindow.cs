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
        private Image mapImage;

        public MainWindow()
        {
            InitializeComponent();

            var annotater = new AnnotaterGit();
            SetAnnotations(annotater.GetAnnotations("C:\\Users\\Jez\\eclipse-workspace\\mvr.api-merge\\src\\starship\\mvr\\model\\db\\FriendsDB.java"));
        }

        void SetAnnotations(Annotations annotations)
        {
            this.annotations = annotations;

            if (annotations == null)
            {
                mapImage = null;
                mapView.Image = null;
                return;
            }

            textView.DocumentText = annotations.GetHTML();

            mapImage = annotations.CreateImage(mapView.Width, mapView.Height);
            UpdateMap();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void OnMapViewResized(object sender, EventArgs e)
        {
            if (annotations == null)
                return;

            mapImage = annotations.CreateImage(mapView.Width, mapView.Height);
            UpdateMap();
        }

        private void OnMapViewClicked(object sender, EventArgs e)
        {
            if (annotations == null)
                return;

            var mouse = e as MouseEventArgs;
            if (mouse == null)
                return;

            ShowMapViewLine(mouse.X, mouse.Y);
        }

        private void OnMapViewMouseMove(object sender, MouseEventArgs e)
        {
            if (annotations == null)
                return;

            var mouse = e as MouseEventArgs;
            if (mouse == null)
                return;

            if (mouse.Button.HasFlag(MouseButtons.Left))
                ShowMapViewLine(mouse.X, mouse.Y);
        }

        private void ShowMapViewLine(int x, int y)
        {
            int line = (y * annotations.GetNumLines()) / mapView.Height;
            textView.Document.GetElementById("line_" + line).ScrollIntoView(false);
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
            if (annotations == null)
                return;

            if (textView.Document.Body == null)
                return;

            var windowHeight = textView.Height;
            var documentHeight = textView.Document.Body.ScrollRectangle.Height;
            var top = textView.Document.Body.ScrollTop;

            int from = (top * annotations.GetNumLines()) / documentHeight;
            int to = ((top + windowHeight) * annotations.GetNumLines()) / documentHeight;

            mapView.Image = MapView.RenderBox(mapImage, annotations, from, to);
        }
        
        private void OnTextViewDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            textView.Document.Body.MouseOver += OnTextViewMouseHover;
            textView.Document.Window.AttachEventHandler("onscroll", OnTextViewScroll);

            UpdateMap();
        }

        private void OnTextViewMouseHover(object sender, EventArgs e)
        {
            if (annotations == null)
            {
                status.Text = "";
                return;
            }

            var htmlEvent = (e as HtmlElementEventArgs);
            if (htmlEvent == null)
            {
                status.Text = "";
                return;
            }

            var element = htmlEvent.ToElement;
            string href = element.GetAttribute("id");
            if (href == null || !href.StartsWith("line_"))
            {
                status.Text = "";
                return;
            }

            status.Text = annotations.GetSummary(int.Parse(href.Substring(5)));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
