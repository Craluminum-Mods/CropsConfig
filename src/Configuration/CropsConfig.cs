using System.Collections.Generic;
using Vintagestory.API.Common;

namespace CropsConfigMod.Configuration
{
    public class CropsConfig
    {
        public Dictionary<string, BlockCropProperties> CropProperties = new() { };

        public CropsConfig() { }
        public CropsConfig(CropsConfig previousConfig)
        {
            foreach (var item in previousConfig.CropProperties)
            {
                if (CropProperties.ContainsKey(item.Key)) continue;
                CropProperties.Add(item.Key, item.Value);
            }
        }
    }
}
