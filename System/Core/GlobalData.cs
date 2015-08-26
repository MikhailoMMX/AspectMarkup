using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AspectCore
{
    /// <summary>
    /// Глобальные данные/опции, используемые в разных проектах, но требующих синхронизации
    /// </summary>
    public class GlobalData
    {
        internal static string ParserAssemblyMask = @"LWParser*.dll";

        private static void TraceNothing(string str)
        {
        }
        public static TraceAction traceAction = TraceNothing;
    }
}
