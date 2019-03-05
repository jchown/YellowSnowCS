﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Navigation;
using mshtml;

namespace YellowSnow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Annotater> annotaters;
        private Annotations annotations;
        private SoftImage mapImage;

        public MainWindow()
        {
            InitializeComponent();

            annotaters = new List<Annotater>();
            annotaters.Add(new AnnotaterGit());
            annotaters.Add(new AnnotaterSVN());

            Open("C:\\Users\\Jez\\eclipse-workspace\\mvr.api-merge\\src\\starship\\mvr\\model\\db\\FriendsDB.java");
            // Open("C:\\Work\\vTime\\vTime_Now_iOS\\bin\\prebuild.xml");#
            
            text.DocumentCompleted += OnTextLoadCompleted;
            map.MouseMove += OnMapMouseMoved;
        }

        private void OnTextLoadCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            UpdateMap();

            text.Document.Body.MouseOver += OnTextMouseHover;
            text.Document.Window.AttachEventHandler("onscroll", OnTextScroll);
        }

        private void OnTextScroll(object sender, EventArgs e)
        {
            UpdateMap();
        }

        private void OnMapMouseMoved(object sender, EventArgs e)
        {
            if (annotations == null)
                return;

            var mouse = e as MouseEventArgs;
            if (mouse == null)
                return;

            if (mouse.Button.HasFlag(MouseButtons.Left))
                ShowMapViewLine(mouse.X, mouse.Y);
        }

        private void OnTextMouseHover(object sender, EventArgs e)
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

        private void ShowMapViewLine(int x, int y)
        {
            int line = (int) ((y * annotations.GetNumLines()) / map.ActualHeight);
            dynamic document = text.Document;
            document.GetElementById("line_" + line).ScrollIntoView(false);
        }

        void Open(string filename)
        {
            status.Text = "Loading " + filename;

            foreach (Annotater annotater in annotaters)
            {
                if (!annotater.IsInWorkspace(filename))
                    continue;

                status.Text = "Analysing " + filename;
                var annotations = annotater.GetAnnotations(filename);

                status.Text = "Rendering " + filename;
                SetAnnotations(annotations);
                return;
            }

            //MessageBox.Show("A version control system workspace was not found.\n\nIs this file in Git or Subversion?", "No VCS Workspace");
        }

        void SetAnnotations(Annotations annotations)
        {
            this.annotations = annotations;

            if (annotations == null)
            {
                mapImage = null;
                map.Source = null;
                return;
            }

            text.DocumentText = annotations.GetHTML();

            mapImage = annotations.CreateImage(128, Math.Max(512, annotations.GetNumLines()));
            UpdateMap();
        }

        private void UpdateMap()
        {
            if (annotations == null)
                return;

            if (text.Document == null)
                return;

            if (text.Document.Body == null)
                return;

            var windowHeight = text.Height;
            var documentHeight = text.Document.Body.ScrollRectangle.Height;
            var top = text.Document.Body.ScrollTop;

            int from = (top * annotations.GetNumLines()) / documentHeight;
            int to = (int) ((top + windowHeight) * annotations.GetNumLines()) / documentHeight;

            map.Source = MapView.RenderBox(mapImage, annotations, from, to).ToBitmap();
        }
    }
}
