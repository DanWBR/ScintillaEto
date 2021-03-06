﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eto.Forms.Controls.Scintilla.Shared
{
    public static class Extensions
    {
        public static IntPtr ToIntPtr(this int value)
        {
            return new IntPtr(value);
        }

        public static IntPtr ToIntPtr(this uint value)
        {
            return new IntPtr(value);
        }

        public static IntPtr ToIntPtr(this string value)
        {
            return System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(value);
        }

        public static string ToString2(this IntPtr value)
        {
            return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(value);
        }

    }
}
