using System.Collections.Generic;

namespace TimberbornAPI.AssetLoader.AssetSystem
{
    internal interface IAssetRepository
    {
        void Add(CustomAssetBundle customAssetBundle);
        CustomAssetBundle FindByPathAndFileName(string[] path, string fileName);
        IEnumerable<CustomAssetBundle> All();
        void LoadAll();
        void UnloadAll();
    }
}