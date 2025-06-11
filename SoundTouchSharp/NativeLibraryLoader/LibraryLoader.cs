using System;
using System.Runtime.InteropServices;

namespace SoundTouchSharp.NativeLibraryLoader
{
   public class LibraryLoader
   {
      public static IntPtr LoadNativeLibrary(string name)
      {
         return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
          ? LoadLibrary(name)
          : dlopen(name, RTLD_NOW);
      }

      public void FreeNativeLibrary(IntPtr handle)
      {
         if (handle == IntPtr.Zero)
            throw new ArgumentException("Parameter must not be zero.", nameof(handle));

         if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            FreeLibrary(handle);
         else
            dlclose(handle);
      }

    
      public static IntPtr CoreLoadFunctionPointer(IntPtr handle, string functionName)
      {
         return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
             ? GetProcAddress(handle, functionName)
             : dlsym(handle, functionName);
      }

      public static IntPtr CoreLoadNativeLibrary(string name)
      {
         return LoadLibrary(name);
      }

      // Windows

      [DllImport("kernel32")]
      public static extern IntPtr LoadLibrary(string fileName);

      [DllImport("kernel32")]
      public static extern IntPtr GetProcAddress(IntPtr module, string procName);

      [DllImport("kernel32")]
      public static extern int FreeLibrary(IntPtr module);

      // macOS 
      private const int RTLD_NOW = 2;

      [DllImport("libdl")]
      private static extern IntPtr dlopen(string fileName, int flags);

      [DllImport("libdl")]
      private static extern IntPtr dlsym(IntPtr handle, string symbol);

      [DllImport("libdl")]
      private static extern int dlclose(IntPtr handle);

   }
}
