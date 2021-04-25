using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TPlayer
{
    public class APlayerPlugin
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool OpenMedia([MarshalAs(UnmanagedType.LPTStr)] string pcszUrl, long nFileSize, int nDuration);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CloseMedia();


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void VIDEO_FRAME_CALLBACK(IntPtr pFrame);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AUDIO_FRAME_CALLBACK(AUDIO_FRAME_INFO pFrame);

        [DllImport("PluginDemo.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void CallOpenMedia(OpenMedia callback);

        [DllImport("PluginDemo.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void CallCloseMedia(CloseMedia callback);



        [DllImport("PluginDemo.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void CallVideoFrame(VIDEO_FRAME_CALLBACK callback);
        [DllImport("PluginDemo.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void CallAudioFrame(AUDIO_FRAME_CALLBACK callback);
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct VIDEO_FRAME_INFO
    {
        public Guid type;
        public int width;
        public int height;

        public IntPtr frame;
        public int length;
        public long time_stamp;
        public long position;
    };

    public struct AUDIO_FRAME_INFO
    {
        public int channel;
        public int bit_per_sample;
        public bool is_float;
        public int sample_count;
        public byte frame;
        public int length;
        public long time_stamp;
        public long position;
    };

}
