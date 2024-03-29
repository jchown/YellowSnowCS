﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls.Primitives;

namespace YellowSnow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string          filename;
        private List<Annotater> annotaters;
        private AnnotaterNull   annotaterNull;
        private Annotations     annotations;
        private Image           mapImage;

        public MainWindow()
        {
            InitializeComponent();

            annotaters = new List<Annotater>();
            annotaters.Add(new AnnotaterGit());
            annotaters.Add(new AnnotaterSVN());

            annotaterNull = new AnnotaterNull();

            Open("C:\\Work\\vTime\\mvr.api\\src\\starship\\mvr\\model\\db\\FriendsDB.java");
            //Open("C:\\Work\\vTime\\vTime_Now_Android\\bin\\prebuild.xml");

            text.DocumentCompleted += OnTextLoadCompleted;
            map.MouseMove += OnMapMouseEvent;
            map.MouseDown += OnMapMouseEvent;

            UpdateFontPointSize();
            FontPS8.Checked += (s, e) => SetFontPointSize(8);
            FontPS10.Checked += (s, e) => SetFontPointSize(10);
            FontPS12.Checked += (s, e) => SetFontPointSize(12);

            UpdateCheckedTheme();
            ThemeYS.Checked += (s, e) => SetTheme(Themes.YELLOW_SNOW);
            ThemeDB.Checked += (s, e) => SetTheme(Themes.DARK_BRUISES);

            ApplyTheme();
        }

        public void OnFileOpen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Open File in VCS";

            if (filename != null)
                openFileDialog.InitialDirectory = filename.GetFilePath();

            if (openFileDialog.ShowDialog() == true)
                Open(openFileDialog.FileName);
        }

        public void OnFileExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnTextLoadCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            UpdateMap();

            text.Document.Body.MouseOver += OnTextMouseHover;
            text.Document.Window.AttachEventHandler("onscroll", OnTextScroll);
        }

        private void OnTextScroll(object sender, EventArgs e)
        {
            UpdateMap();
        }

        private void OnMapMouseEvent(object sender, EventArgs e)
        {
            if (annotations == null)
                return;

            var mouse = e as MouseEventArgs;
            if (mouse == null)
                return;

            if (!mouse.LeftButton.HasFlag(MouseButtonState.Pressed))
                return;

            var pos = mouse.GetPosition(map);
            ShowMapViewLine((int) pos.X, (int) pos.Y);
        }

        private void OnTextMouseHover(object sender, EventArgs e)
        {
            if (annotations == null)
            {
                status.Text = "";
                return;
            }

            var htmlEvent = (e as System.Windows.Forms.HtmlElementEventArgs);
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
            if (!File.Exists(filename) && !Directory.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            status.Text = "Loading " + filename;

            foreach (Annotater annotater in annotaters)
            {
                if (!annotater.IsInWorkspace(filename))
                    continue;

                Title = " Yellow Snow - " + filename;
                status.Text = "Analysing " + filename;
                this.filename = filename;
                var annotations = annotater.GetAnnotations(filename);

                status.Text = "Rendering " + filename;
                SetAnnotations(annotations);

                status.Text = "";
                return;
            }

            MessageBox.Show("A version control system workspace was not found.\n\nIs this file in Git or Subversion?", "No VCS Workspace");

            SetAnnotations(annotaterNull.GetAnnotations(filename));
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

            mapImage = annotations.CreateImage(256, Math.Max(512, annotations.GetNumLines()));
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

            map.Source = ToWPF(MapView.RenderBox(mapImage, annotations, from, to));
        }

        private void SetTheme(Theme theme)
        {
            Themes.Selected = theme;
            ApplyTheme();
            UpdateCheckedTheme();
            SetAnnotations(annotations);
        }

        private void ApplyTheme()
        {
            var bg = new SolidColorBrush(ToWPF(Themes.Selected.bgOld));
            var fg = new SolidColorBrush(ToWPF(Themes.Selected.fgOld));

            text.ScrollBarsEnabled = false;
//            ApplyTheme(bg, fg, menubar);
//            ApplyTheme(bg, fg, menuFile);
//            ApplyTheme(bg, fg, menuView);
            ApplyTheme(bg, fg, statusbar);
            ApplyTheme(bg, fg, status);
            ApplyTheme(bg, fg, scrollx);
            ApplyTheme(bg, fg, scrolly);
        }

        private void ApplyTheme(SolidColorBrush bg, SolidColorBrush fg, ScrollBar scrollBar)
        {
            var track = scrollBar.Track;
            if (track != null)
            {
                ApplyTheme(bg, fg, (System.Windows.Controls.Control)scrollBar.Track.Thumb);
                ApplyTheme(bg, fg, (System.Windows.Controls.Control)scrollBar.Track.DecreaseRepeatButton);
                ApplyTheme(bg, fg, (System.Windows.Controls.Control)scrollBar.Track.IncreaseRepeatButton);
            }

            ApplyTheme(bg, fg, (System.Windows.Controls.Control) scrollBar);
        }

        private void ApplyTheme(SolidColorBrush bg, SolidColorBrush fg, System.Windows.Controls.Control control)
        {
            control.Background = bg;
            control.Foreground = fg;
        }

        private void ApplyTheme(SolidColorBrush bg, SolidColorBrush fg, System.Windows.Controls.TextBlock text)
        {
            text.Background = bg;
            text.Foreground = fg;
        }

        private void UpdateCheckedTheme()
        {
            ThemeYS.IsChecked = Themes.Selected == Themes.YELLOW_SNOW;
            ThemeDB.IsChecked = Themes.Selected == Themes.DARK_BRUISES;
        }

        private void SetFontPointSize(int pointSize)
        {
            Font.PointSize = pointSize;
            UpdateFontPointSize();
            SetAnnotations(annotations);
        }

        private void UpdateFontPointSize()
        {
            FontPS8.IsChecked = Font.PointSize == 8;
            FontPS10.IsChecked = Font.PointSize == 10;
            FontPS12.IsChecked = Font.PointSize == 12;
        }

        public static System.Windows.Media.Color ToWPF(System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromRgb(color.R, color.G, color.B);
        }

        public static BitmapSource ToWPF(Image myImage)
        {
            var bitmap = new Bitmap(myImage);
            IntPtr bmpPt = bitmap.GetHbitmap();
            BitmapSource bitmapSource =
             System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                   bmpPt,
                   IntPtr.Zero,
                   Int32Rect.Empty,
                   BitmapSizeOptions.FromEmptyOptions());

            //freeze bitmapSource and clear memory to avoid memory leaks
            bitmapSource.Freeze();
            DeleteObject(bmpPt);

            return bitmapSource;
        }

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr value);
    }
}

