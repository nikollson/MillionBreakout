using System;
using System.Runtime.InteropServices;

namespace Stool.CSharp
{
    public static class ConvertUtility
    {
        public static byte[] StructToByte<T>(T obj) where T : struct
        {
            int size = Marshal.SizeOf(obj);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            byte[] bytes = new byte[size];
            Marshal.StructureToPtr(obj, ptr, false);
            Marshal.Copy(ptr, bytes, 0, size);
            return bytes;
        }
        
    }
}
