using System.Linq;
using CropsConfigMod.Configuration;
using Vintagestory.API.Common;
using Vintagestory.GameContent;
using static System.Linq.Enumerable;

namespace CropsConfigMod;

public static class Patches
{
    public static void ApplyPatches(this ICoreAPI api, CropsConfig config)
    {
        if (config.CropProperties?.Count == 0) return;

        foreach (var val in config.CropProperties)
        {
            var block = api.World.GetBlock(new AssetLocation(val.Key));
            if (block == null) continue;
            if (block is not BlockCrop) continue;

            var minStage = block.CropProps.GrowthStages - block.CropProps.GrowthStages + 1;
            var maxStage = block.CropProps.GrowthStages;

            foreach (var stage in Range(minStage, maxStage).ToList())
            {
                var code = block.Code.ToString().Replace(1.ToString(), stage.ToString());
                var newBlock = api.World.GetBlock(new AssetLocation(code));
                if (newBlock == null) continue;
                newBlock.CropProps = val.Value;
            }
        }
    }

    public static CropsConfig FillConfig(this ICoreAPI api, CropsConfig config)
    {
        if (config.CropProperties == null) return config;

        foreach (var block in api.World.Blocks)
        {
            var code = block?.Code?.ToString();
            if (block is not BlockCrop) continue;
            if (block.Code.EndVariant() != "1") continue;
            if (config.CropProperties.ContainsKey(code)) continue;

            config.CropProperties.Add(code, block.CropProps);
        }

        return config;
    }
}