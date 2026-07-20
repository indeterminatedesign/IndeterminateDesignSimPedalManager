using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct PedalData
{
    public int startValue;
    public int maxValue;
    public int range;

    public float upperDeadzonePercent;
    public float lowerDeadZonePercent;

    public int lowerDeadZoneThreshold;
    public int upperDeadZoneThreshold;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public short[] inputToOutputRatios;
}


[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ConfigurationData
{
    public UInt32 signature;
    public UInt16 version;

    public PedalData throttle;
    public PedalData brake;
    public PedalData clutch;
}


namespace IndeterminateDesignSimPedalManager
{
    public static class ConfigurationUtils
    {
        public const UInt32 CONFIG_MAGIC_NUMBER = 0x5045444F;

        // Convert a struct into a byte[] for raw serial transmission
        public static byte[] StructureToByteArray<T>(T obj) where T : struct
        {
            int len = Marshal.SizeOf<T>();
            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);

            try
            {
                Marshal.StructureToPtr(obj, ptr, false);
                Marshal.Copy(ptr, arr, 0, len);
                return arr;
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }


        // Convert byte[] back into a struct
        public static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            int len = Marshal.SizeOf<T>();

            if (bytes.Length < len)
            {
                throw new ArgumentException(
                    $"Byte array too small. Expected {len}, got {bytes.Length}");
            }

            IntPtr ptr = Marshal.AllocHGlobal(len);

            try
            {
                Marshal.Copy(bytes, 0, ptr, len);
                return Marshal.PtrToStructure<T>(ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }


        // Create a pedal with factory defaults
        public static PedalData CreateDefaultPedal()
        {
            return new PedalData
            {
                startValue = 0,
                maxValue = 0,
                range = 0,

                upperDeadzonePercent = 10,
                lowerDeadZonePercent = 10,

                lowerDeadZoneThreshold = 0,
                upperDeadZoneThreshold = 0,

                inputToOutputRatios = new short[]
                {
                    20, 20,
                    40, 40,
                    60, 60,
                    80, 80
                }
            };
        }


        // Create full default configuration
        public static ConfigurationData CreateDefaultConfiguration()
        {
            return new ConfigurationData
            {
                signature = CONFIG_MAGIC_NUMBER,

                throttle = CreateDefaultPedal(),
                brake = CreateDefaultPedal(),
                clutch = CreateDefaultPedal()
            };
        }


        // Get LUT input percentage
        public static short GetCurveInput(PedalData pedal, int point)
        {
            if (point < 0 || point >= 4)
                throw new ArgumentOutOfRangeException(nameof(point));

            return pedal.inputToOutputRatios[point * 2];
        }


        // Get LUT output percentage
        public static short GetCurveOutput(PedalData pedal, int point)
        {
            if (point < 0 || point >= 4)
                throw new ArgumentOutOfRangeException(nameof(point));

            return pedal.inputToOutputRatios[point * 2 + 1];
        }


        // Set LUT point
        public static void SetCurvePoint(
            ref PedalData pedal,
            int point,
            short input,
            short output)
        {
            if (point < 0 || point >= 4)
                throw new ArgumentOutOfRangeException(nameof(point));

            pedal.inputToOutputRatios[point * 2] = input;
            pedal.inputToOutputRatios[point * 2 + 1] = output;
        }


        // Debug helper - dump the curve
        public static string CurveToString(PedalData pedal)
        {
            string result = "";

            for (int i = 0; i < 4; i++)
            {
                result +=
                    $"({GetCurveInput(pedal, i)}, {GetCurveOutput(pedal, i)}) ";

            }

            return result.Trim();
        }
    }
}
