using System;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static int LineCount(this String str)
        {
            return str.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}