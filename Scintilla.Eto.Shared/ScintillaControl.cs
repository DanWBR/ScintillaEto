using System;
using Eto.Forms;

namespace Eto.Forms.Controls.Scintilla.Shared
{

    [Eto.Handler(typeof(IScintillaControl))]
    public class ScintillaControl : Eto.Forms.Control
    {

        IScintillaControl Handler { get { return (IScintillaControl)base.Handler; } }

        public interface IScintillaControl : Eto.Forms.Control.IHandler
        {
          string ScriptText { get; set; }

        }

    }
}


