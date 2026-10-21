using System;
using System.Runtime.InteropServices;
using System.Text.Json;
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

public class PedalProfile
{
    public int profileVersion { get; set; } = 1;
    public ConfigurationData configuration { get; set; } = new ConfigurationData();
}


namespace IndeterminateDesignSimPedalManager
{
    public static class ConfigurationUtils
    {
        public const UInt32 CONFIG_MAGIC_NUMBER = 0x5045444D;
        public const int CURRENT_PROFILE_VERSION = 1;

        private static readonly JsonSerializerOptions ProfileJsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        private static readonly JsonSerializerOptions ProfileJsonReadOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static string SerializeProfile(ConfigurationData configuration)
        {
            var profile = new PedalProfile
            {
                profileVersion = CURRENT_PROFILE_VERSION,
                configuration = configuration
            };

            return JsonSerializer.Serialize(profile, ProfileJsonOptions);
        }

        public static ConfigurationData DeserializeProfile(string json)
        {
            var profile = JsonSerializer.Deserialize<PedalProfile>(json, ProfileJsonReadOptions)
                ?? throw new JsonException("The profile file is empty or invalid.");

            if (profile.profileVersion != CURRENT_PROFILE_VERSION)
            {
                throw new InvalidOperationException(
                    $"Unsupported profile version {profile.profileVersion}. Expected version {CURRENT_PROFILE_VERSION}.");
            }

            if (profile.configuration?.throttle == null ||
                profile.configuration.brake == null ||
                profile.configuration.clutch == null)
            {
                throw new JsonException("The profile is missing one or more pedal configurations.");
            }

            if (profile.configuration.throttle.inputToOutputRatios?.Length != 12 ||
                profile.configuration.brake.inputToOutputRatios?.Length != 12 ||
                profile.configuration.clutch.inputToOutputRatios?.Length != 12)
            {
                throw new JsonException("The profile contains an invalid curve configuration.");
            }

            return profile.configuration;
        }

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
