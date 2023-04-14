using Vintagestory.API.Common;
using CropsConfigMod.Configuration;

[assembly: ModInfo("Crops Config",
    Authors = new[] { "Craluminum2413" })]

namespace CropsConfigMod;

class Core : ModSystem
{
    public override void AssetsFinalize(ICoreAPI api)
    {
        // Reading it twice because it creates empty dictionary on first load
        ModConfig.ReadConfig(api);
        ModConfig.ReadConfig(api);
        api.World.Logger.Event("started 'Crops Config' mod");
    }
}