using Eto.Drawing;
using ScintillaNET;
using System.Drawing;
using System.Windows.Media;
using SystemColors = System.Drawing.SystemColors;
using Color = System.Drawing.Color;
using System.Windows;

namespace Eto.Forms.Controls.Scintilla.WPF
{

    public class ScintillaControlHandler : Eto.Wpf.Forms.WpfFrameworkElement<FrameworkElement, Shared.ScintillaControl, Shared.ScintillaControl.ICallback>, Shared.ScintillaControl.IScintillaControl
    {

        private ScintillaControl_WPF nativecontrol;

        public ScintillaControlHandler()
        {
            nativecontrol = new ScintillaControl_WPF();
            this.Control = nativecontrol;
        }

        public string ScriptText
        {
            get
            {
                return nativecontrol.WinFormsControl.Text;
            }
            set
            {
                nativecontrol.WinFormsControl.Text = value;
            }
        }

        public override Drawing.Color BackgroundColor { get; set; }

        public void SetKeywords(int level, string keywords)
        {
            nativecontrol.WinFormsControl.SetKeywords(level, keywords);
        }

        public void SetStyle(int styleID, int item, object value)
        {
            //
        }

        public void SetFont(string fontname)
        {
            nativecontrol.WinFormsControl.Styles[ScintillaNET.Style.Default].Font = fontname;
        }

        public void SetFontSize(int fontsize)
        {
            nativecontrol.WinFormsControl.Styles[ScintillaNET.Style.Default].Size = fontsize;
        }

        public void ResetDefaultStyle()
        {
            nativecontrol.WinFormsControl.StyleResetDefault();
        }

        public void ClearAllStyles()
        {
            nativecontrol.WinFormsControl.StyleClearAll();
        }
        public void Cut()
        {
            nativecontrol.WinFormsControl.Cut();
        }

        public void Copy()
        {
            nativecontrol.WinFormsControl.Copy();
        }

        public void Paste()
        {
            nativecontrol.WinFormsControl.Paste();
        }

        public void Undo()
        {
            nativecontrol.WinFormsControl.Undo();
        }

        public void Redo()
        {
            nativecontrol.WinFormsControl.Redo();
        }

        public void ToggleCommenting()
        {
            var lines = nativecontrol.WinFormsControl.SelectedText.Split(System.Environment.NewLine.ToCharArray());
            string newlines = "";
            foreach (string l in lines)
            {
                if (l != "" && l.StartsWith("#")) newlines += l.TrimStart('#') + System.Environment.NewLine;
                else if (l != "" && !l.StartsWith("#")) newlines += l.Insert(0, "#") + System.Environment.NewLine;
            }
            nativecontrol.WinFormsControl.ReplaceSelection(newlines.TrimEnd(System.Environment.NewLine.ToCharArray()));
        }

        public void Indent()
        {
            var lines = nativecontrol.WinFormsControl.SelectedText.Split(System.Environment.NewLine.ToCharArray());
            string newlines = "";
            foreach (string l in lines)
            {
                if (l != "") newlines += l.Insert(0, '\t'.ToString()) + System.Environment.NewLine;
            }
            nativecontrol.WinFormsControl.ReplaceSelection(newlines.TrimEnd(System.Environment.NewLine.ToCharArray()));
        }

        public void Unindent()
        {
            var lines = nativecontrol.WinFormsControl.SelectedText.Split(System.Environment.NewLine.ToCharArray());
            string newlines = "";
            foreach (string l in lines)
            {
                if (l != "") newlines += l.TrimStart('\t') + System.Environment.NewLine;
            }
            nativecontrol.WinFormsControl.ReplaceSelection(newlines.TrimEnd(System.Environment.NewLine.ToCharArray()));
        }

        public void InsertSnippet(string snippet)
        {
            nativecontrol.WinFormsControl.InsertText(nativecontrol.WinFormsControl.SelectionStart, snippet);
            nativecontrol.WinFormsControl.CurrentPosition += snippet.Length + 1;
            nativecontrol.WinFormsControl.SelectionStart = nativecontrol.WinFormsControl.CurrentPosition;
            nativecontrol.WinFormsControl.SelectionEnd = nativecontrol.WinFormsControl.CurrentPosition;
        }

        public void Print()
        {

            ScintillaPrinting.Printing printer = new ScintillaPrinting.Printing(nativecontrol.WinFormsControl);

            printer.PageSettings = new ScintillaPrinting.PageSettings() { ColorMode = ScintillaPrinting.PageSettings.PrintColorMode.BlackOnWhite };

            printer.PrintPreview();

        }

        public void IncreaseFontSize()
        {
            nativecontrol.WinFormsControl.Styles[ScintillaNET.Style.Python.Default].Size += 1;
        }

        public void DecreaseFontSize()
        {
            nativecontrol.WinFormsControl.Styles[ScintillaNET.Style.Python.Default].Size -= 1;
        }

    }

    public class ScintillaControl_WPF : System.Windows.Controls.Grid
    {

        public ScintillaNET.Scintilla WinFormsControl;

        public ScintillaControl_WPF() : base()
        {
            WinFormsControl = new ScintillaNET.Scintilla();
            SetStyle();
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            // Assign the winforms control as the host control's child.
            host.Child = WinFormsControl;

            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.Children.Add(host);

        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            WinFormsControl.Invalidate();
        }

        public void SetStyle()
        {

            WinFormsControl.StyleResetDefault();
            WinFormsControl.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            WinFormsControl.Styles[ScintillaNET.Style.Default].Size = 8;
            WinFormsControl.StyleClearAll();

            //  Set the lexer
            WinFormsControl.Lexer = Lexer.Python;

            WinFormsControl.AutomaticFold = AutomaticFold.Click;

            //  Some properties we like
            WinFormsControl.SetProperty("tab.timmy.whinge.level", "1");
            WinFormsControl.SetProperty("fold", "1");
            WinFormsControl.Margins[0].Width = 20;

            //  Use margin 2 for fold markers
            WinFormsControl.Margins[1].Type = MarginType.Symbol;
            WinFormsControl.Margins[1].Mask = Marker.MaskFolders;
            WinFormsControl.Margins[1].Sensitive = true;
            WinFormsControl.Margins[1].Width = 20;

            //  Reset folder markers
            for (int i = Marker.FolderEnd; (i <= Marker.FolderOpen); i++)
            {
                WinFormsControl.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                WinFormsControl.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            //  Style the folder markers
            WinFormsControl.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            WinFormsControl.Markers[Marker.Folder].SetBackColor(SystemColors.ControlText);
            WinFormsControl.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            WinFormsControl.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            WinFormsControl.Markers[Marker.FolderEnd].SetBackColor(SystemColors.ControlText);
            WinFormsControl.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            WinFormsControl.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            WinFormsControl.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            WinFormsControl.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;
            WinFormsControl.Styles[ScintillaNET.Style.Python.Default].ForeColor = Color.FromArgb(128, 128, 128);
            WinFormsControl.Styles[ScintillaNET.Style.Python.CommentLine].ForeColor = Color.FromArgb(0, 127, 0);
            WinFormsControl.Styles[ScintillaNET.Style.Python.CommentLine].Italic = true;
            WinFormsControl.Styles[ScintillaNET.Style.Python.Number].ForeColor = Color.FromArgb(0, 127, 127);
            WinFormsControl.Styles[ScintillaNET.Style.Python.String].ForeColor = Color.FromArgb(127, 0, 127);
            WinFormsControl.Styles[ScintillaNET.Style.Python.Character].ForeColor = Color.FromArgb(127, 0, 127);
            WinFormsControl.Styles[ScintillaNET.Style.Python.Word].ForeColor = Color.FromArgb(0, 0, 127);
            WinFormsControl.Styles[ScintillaNET.Style.Python.Word].Bold = true;
            WinFormsControl.Styles[ScintillaNET.Style.Python.Triple].ForeColor = Color.FromArgb(127, 0, 0);
            WinFormsControl.Styles[ScintillaNET.Style.Python.TripleDouble].ForeColor = Color.FromArgb(127, 0, 0);
            WinFormsControl.Styles[ScintillaNET.Style.Python.ClassName].ForeColor = Color.FromArgb(0, 0, 255);
            WinFormsControl.Styles[ScintillaNET.Style.Python.ClassName].Bold = true;
            WinFormsControl.Styles[ScintillaNET.Style.Python.DefName].ForeColor = Color.FromArgb(0, 127, 127);
            WinFormsControl.Styles[ScintillaNET.Style.Python.DefName].Bold = true;
            WinFormsControl.Styles[ScintillaNET.Style.Python.Operator].Bold = true;
            WinFormsControl.Styles[ScintillaNET.Style.Python.CommentBlock].ForeColor = Color.FromArgb(127, 127, 127);
            WinFormsControl.Styles[ScintillaNET.Style.Python.CommentBlock].Italic = true;
            WinFormsControl.Styles[ScintillaNET.Style.Python.StringEol].ForeColor = Color.FromArgb(0, 0, 0);
            WinFormsControl.Styles[ScintillaNET.Style.Python.StringEol].BackColor = Color.FromArgb(224, 192, 224);
            WinFormsControl.Styles[ScintillaNET.Style.Python.StringEol].FillLine = true;
            WinFormsControl.Styles[ScintillaNET.Style.Python.DefName].ForeColor = Color.Brown;
            WinFormsControl.Styles[ScintillaNET.Style.Python.DefName].Bold = true;
            WinFormsControl.Styles[ScintillaNET.Style.Python.Word2].ForeColor = Color.DarkRed;
            WinFormsControl.Styles[ScintillaNET.Style.Python.Word2].Bold = true;

            //  Keyword lists:
            //  0 "Keywords",
            //  1 "Highlighted identifiers"
            object python2 = "and as assert break class continue def del elif else except exec finally for from global if import in" +
            " is lambda not or pass print raise return try while with yield";
            object python3 = "False None True and as assert break class continue def del elif else except finally for from global i" +
            "f import in is lambda nonlocal not or pass raise return try while with yield";

            WinFormsControl.SetKeywords(0, (python2 + (" " + python3)));

        }

    }
}
