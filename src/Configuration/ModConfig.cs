using Vintagestory.API.Common;

namespace CropsConfigMod.Configuration;

static class ModConfig
{
    private const string jsonConfig = "ConfigureEverything/CropProperties.json";
    private static CropsConfig config;

    public static void ReadConfig(ICoreAPI api)
    {
        try
        {
            config = LoadConfig(api);
            config = api.FillConfig(config);

            if (config == null)
            {
                GenerateConfig(api);
                config = LoadConfig(api);
                config = api.FillConfig(config);
            }
            else
            {
                GenerateConfig(api, config);
                config = api.FillConfig(config);
            }
        }
        catch
        {
            GenerateConfig(api);
            config = LoadConfig(api);
            config = api.FillConfig(config);
        }

        api.ApplyPatches(config);
    }

    private static CropsConfig LoadConfig(ICoreAPI api) => api.LoadModConfig<CropsConfig>(jsonConfig);
    private static void GenerateConfig(ICoreAPI api) => api.StoreModConfig(new CropsConfig(), jsonConfig);
    private static void GenerateConfig(ICoreAPI api, CropsConfig previousConfig) => api.StoreModConfig(new CropsConfig(previousConfig), jsonConfig);
}