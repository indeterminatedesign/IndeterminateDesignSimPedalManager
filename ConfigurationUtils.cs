using System;
using System.Runtime.InteropServices;
public class PedalData
{
    public int startValue { get; set; }
    public int maxValue { get; set; }
    public int range { get; set; }
    public short[] inputToOutputRatios { get; set; } = new short[12];

}

public class ConfigurationData
{
    public uint signature { get; set; }
    public short version { get; set; }
    public PedalData throttle { get; set; } = new PedalData();
    public PedalData brake { get; set; } = new PedalData();
    public PedalData clutch { get; set; } = new PedalData();
}


namespace IndeterminateDesignSimPedalManager
{
    public static class ConfigurationUtils
    {
        public const UInt32 CONFIG_MAGIC_NUMBER = 0x5045444E;

       public static PedalData CreateDefaultPedal()
        {
            return new PedalData
            {
                startValue = 0,
                maxValue = 0,
                range = 0,

                inputToOutputRatios = new short[]
                { 0, 0, 20, 20, 40, 40, 60, 60, 80, 80, 100, 100
                }
            };
        }
    



    // Create full default configuration
    public static ConfigurationData CreateDefaultConfiguration()
    {
        return new ConfigurationData
        {
            signature = CONFIG_MAGIC_NUMBER,
            version = 1,

            throttle = CreateDefaultPedal(),
            brake = CreateDefaultPedal(),
            clutch = CreateDefaultPedal()
        };
    }





}
}
