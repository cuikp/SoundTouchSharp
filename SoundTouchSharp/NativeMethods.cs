using SoundTouchSharp.NativeLibraryLoader;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SoundTouchSharp;

public static class NativeMethods
{

#if WINDOWS
 private const string SoundTouchLibrary = "SoundTouch.dll";
#elif OSX
 private const string SoundTouchLibrary = "SoundTouch.dylib";
#else
   private const string SoundTouchLibrary = "__unsupported__";
#endif
       
   static NativeMethods()
   {

      string arch;
      if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
      {
         if (RuntimeInformation.ProcessArchitecture != Architecture.Arm64)
            throw new PlatformNotSupportedException("Only osx-arm64 is supported on macOS.");
         arch = "osx-arm64";
      }
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
         arch = Environment.Is64BitProcess ? "win-x64" : "win-x86";
      else
         throw new PlatformNotSupportedException();

      var dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"runtimes/{arch}/native/{SoundTouchLibrary}");
      if (File.Exists(dllPath))
         LibraryLoader.LoadNativeLibrary(dllPath);
      else
         throw new FileNotFoundException("Native library not found", dllPath);

   }

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_getVersionId")]
   public static extern int GetVersionId();

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_createInstance")]
   public static extern IntPtr CreateInstance();

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_destroyInstance")]
   public static extern void DestroyInstance(IntPtr h);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_getVersionString")]
   public static extern IntPtr GetVersionString();

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_setRate")]
   public static extern void SetRate(IntPtr h, float newRate);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_setTempo")]
   public static extern void SetTempo(IntPtr h, float newTempo);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_setRateChange")]
   public static extern void SetRateChange(IntPtr h, float newRate);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_setTempoChange")]
   public static extern void SetTempoChange(IntPtr h, float newTempo);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_setPitch")]
   public static extern void SetPitch(IntPtr h, float newPitch);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_setPitchOctaves")]
   public static extern void SetPitchOctaves(IntPtr h, float newPitch);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_setPitchSemiTones")]
   public static extern void SetPitchSemiTones(IntPtr h, float newPitch);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_setChannels")]
   public static extern void SetChannels(IntPtr h, uint numChannels);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_setSampleRate")]
   public static extern void SetSampleRate(IntPtr h, uint srate);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_flush")]
   public static extern void Flush(IntPtr h);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_putSamples")]
   public static extern void PutSamples(IntPtr h, float[] samples, uint numSamples);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_putSamples_i16")]
   public static extern void PutSamples_i16(IntPtr h, short[] samples, uint numSamples);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_clear")]
   public static extern void Clear(IntPtr h);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_setSetting")]
   public static extern int SetSetting(IntPtr h, int settingId, int value);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_getSetting")]
   public static extern int GetSetting(IntPtr h, int settingId);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_numUnprocessedSamples")]
   public static extern uint NumUnprocessedSamples(IntPtr h);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_receiveSamples")]
   public static extern uint ReceiveSamples(IntPtr h, float[] outBuffer, uint maxSamples);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_receiveSamples_i16")]
   public static extern uint ReceiveSamples_i16(IntPtr h, short[] outBuffer, uint maxSamples);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_numSamples")]
   public static extern uint NumSamples(IntPtr h);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "soundtouch_isEmpty")]
   public static extern int IsEmpty(IntPtr h);
   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bpm_createInstance")]
   public static extern IntPtr BpmCreateInstance(int numChannels, int sampleRate);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bpm_destroyInstance")]
   public static extern void BpmDestroyInstance(IntPtr h);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bpm_putSamples")]
   public static extern void BpmPutSamples(IntPtr h, float[] samples, uint numSamples);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bpm_putSamples_i16")]
   public static extern void BpmPutSamples_i16(IntPtr h, short[] samples, uint numSamples);

   [DllImport(SoundTouchLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bpm_getBpm")]
   public static extern float BpmGet(IntPtr h);
}
