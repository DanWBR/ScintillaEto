using System;
using Eto.Forms;

namespace Eto.Forms.Controls.Scintilla.Shared
{

    [Eto.Handler(typeof(IScintillaControl))]
    public class ScintillaControl : Eto.Forms.Control
    {
        new IScintillaControl Handler { get { return (IScintillaControl)base.Handler; } }

        public interface IScintillaControl : Eto.Forms.Control.IHandler
        {
            string ScriptText { get; set; }

            void SetKeywords(int level, string keywords);

            void SetStyle(int styleID, int item, object value);

            void SetFont(string fontname);

            void SetFontSize(int fontsize);

            void ResetDefaultStyle();

            void ClearAllStyles();
            
        }

    }
}


