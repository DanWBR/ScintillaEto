using ScintillaNET;
using System.Drawing;

namespace Eto.Forms.Controls.Scintilla.WinForms
{
    
    public class ScintillaControlHandler : Eto.WinForms.Forms.WindowsControl<System.Windows.Forms.Control, Shared.ScintillaControl, Shared.ScintillaControl.ICallback>, Shared.ScintillaControl.IScintillaControl
    {

        private ScintillaControl_WinForms nativecontrol;

        public ScintillaControlHandler()
        {
            nativecontrol = new ScintillaControl_WinForms();
            this.Control = nativecontrol;
        }

        public string ScriptText
        {
            get
            {
                return nativecontrol.Text;
            }
            set
            {
                nativecontrol.Text = value;
            }
        }

        public void SetKeywords(int level, string keywords)
        {
            nativecontrol.SetKeywords(level, keywords);
        }

        public void SetStyle(int styleID, int item, object value)
        {
            //
        }

        public void SetFont(string fontname)
        {
            nativecontrol.Styles[ScintillaNET.Style.Default].Font = fontname;
        }

        public void SetFontSize(int fontsize)
        {
            nativecontrol.Styles[ScintillaNET.Style.Default].Size = fontsize;
        }

        public void ResetDefaultStyle()
        {
            nativecontrol.StyleResetDefault();
        }

        public void ClearAllStyles()
        {
            nativecontrol.StyleClearAll();
        }

        public void Cut()
        {
            nativecontrol.Cut();
        }

        public void Copy()
        {
            nativecontrol.Copy();
        }

        public void Paste()
        {
            nativecontrol.Paste();
        }

        public void Undo()
        {
            nativecontrol.Undo();
        }

        public void Redo()
        {
            nativecontrol.Redo();
        }

        public void ToggleCommenting()
        {
            var lines = nativecontrol.SelectedText.Split(System.Environment.NewLine.ToCharArray());
            string newlines = "";
            foreach (string l in lines)
            {
                if (l != "" && l.StartsWith("#")) newlines += l.TrimStart('#') + System.Environment.NewLine;
                else if (l != "" && !l.StartsWith("#")) newlines += l.Insert(0, "#") + System.Environment.NewLine;
            }
            nativecontrol.ReplaceSelection(newlines.TrimEnd(System.Environment.NewLine.ToCharArray()));
        }

        public void Indent()
        {
            var lines = nativecontrol.SelectedText.Split(System.Environment.NewLine.ToCharArray());
            string newlines = "";
            foreach (string l in lines)
            {
                if (l != "") newlines += l.Insert(0, '\t'.ToString()) + System.Environment.NewLine;
            }
            nativecontrol.ReplaceSelection(newlines.TrimEnd(System.Environment.NewLine.ToCharArray()));
        }

        public void Unindent()
        {
            var lines = nativecontrol.SelectedText.Split(System.Environment.NewLine.ToCharArray());
            string newlines = "";
            foreach (string l in lines)
            {
                if (l != "") newlines += l.TrimStart('\t') + System.Environment.NewLine;
            }
            nativecontrol.ReplaceSelection(newlines.TrimEnd(System.Environment.NewLine.ToCharArray()));
        }

        public void InsertSnippet(string snippet)
        {
            nativecontrol.InsertText(nativecontrol.SelectionStart, snippet);
        }

        public void Print()
        { }


    }

    public class ScintillaControl_WinForms: ScintillaNET.Scintilla
    {
        public ScintillaControl_WinForms(): base()
        {
            SetStyle();
        }

        public void SetStyle()
        {

            this.StyleResetDefault();
            this.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            this.Styles[ScintillaNET.Style.Default].Size = 8;
            this.StyleClearAll();
            
            //  Set the lexer
            this.Lexer = Lexer.Python;

            this.AutomaticFold = AutomaticFold.Click;

            //  Some properties we like
            this.SetProperty("tab.timmy.whinge.level", "1");
            this.SetProperty("fold", "1");
            this.Margins[0].Width = 20;

            //  Use margin 2 for fold markers
            this.Margins[1].Type = MarginType.Symbol;
            this.Margins[1].Mask = Marker.MaskFolders;
            this.Margins[1].Sensitive = true;
            this.Margins[1].Width = 20;

            //  Reset folder markers
            for (int i = Marker.FolderEnd; (i <= Marker.FolderOpen); i++)
            {
                this.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                this.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            //  Style the folder markers
            this.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            this.Markers[Marker.Folder].SetBackColor(SystemColors.ControlText);
            this.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            this.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            this.Markers[Marker.FolderEnd].SetBackColor(SystemColors.ControlText);
            this.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            this.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            this.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            this.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;
            this.Styles[ScintillaNET.Style.Python.Default].ForeColor = Color.FromArgb(128, 128, 128);
            this.Styles[ScintillaNET.Style.Python.CommentLine].ForeColor = Color.FromArgb(0, 127, 0);
            this.Styles[ScintillaNET.Style.Python.CommentLine].Italic = true;
            this.Styles[ScintillaNET.Style.Python.Number].ForeColor = Color.FromArgb(0, 127, 127);
            this.Styles[ScintillaNET.Style.Python.String].ForeColor = Color.FromArgb(127, 0, 127);
            this.Styles[ScintillaNET.Style.Python.Character].ForeColor = Color.FromArgb(127, 0, 127);
            this.Styles[ScintillaNET.Style.Python.Word].ForeColor = Color.FromArgb(0, 0, 127);
            this.Styles[ScintillaNET.Style.Python.Word].Bold = true;
            this.Styles[ScintillaNET.Style.Python.Triple].ForeColor = Color.FromArgb(127, 0, 0);
            this.Styles[ScintillaNET.Style.Python.TripleDouble].ForeColor = Color.FromArgb(127, 0, 0);
            this.Styles[ScintillaNET.Style.Python.ClassName].ForeColor = Color.FromArgb(0, 0, 255);
            this.Styles[ScintillaNET.Style.Python.ClassName].Bold = true;
            this.Styles[ScintillaNET.Style.Python.DefName].ForeColor = Color.FromArgb(0, 127, 127);
            this.Styles[ScintillaNET.Style.Python.DefName].Bold = true;
            this.Styles[ScintillaNET.Style.Python.Operator].Bold = true;
            this.Styles[ScintillaNET.Style.Python.CommentBlock].ForeColor = Color.FromArgb(127, 127, 127);
            this.Styles[ScintillaNET.Style.Python.CommentBlock].Italic = true;
            this.Styles[ScintillaNET.Style.Python.StringEol].ForeColor = Color.FromArgb(0, 0, 0);
            this.Styles[ScintillaNET.Style.Python.StringEol].BackColor = Color.FromArgb(224, 192, 224);
            this.Styles[ScintillaNET.Style.Python.StringEol].FillLine = true;
            this.Styles[ScintillaNET.Style.Python.DefName].ForeColor = Color.Brown;
            this.Styles[ScintillaNET.Style.Python.DefName].Bold = true;
            this.Styles[ScintillaNET.Style.Python.Word2].ForeColor = Color.DarkRed;
            this.Styles[ScintillaNET.Style.Python.Word2].Bold = true;
           
            //  Keyword lists:
            //  0 "Keywords",
            //  1 "Highlighted identifiers"
            object python2 = "and as assert break class continue def del elif else except exec finally for from global if import in" +
            " is lambda not or pass print raise return try while with yield";
            object python3 = "False None True and as assert break class continue def del elif else except finally for from global i" +
            "f import in is lambda nonlocal not or pass raise return try while with yield";

            this.SetKeywords(0, (python2 + (" " + python3)));
            
        }



    }
}
