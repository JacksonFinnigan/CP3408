#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Assets.FantasyMonsters.Scripts
{
    /// <summary>
    /// Refresh monster collection when importing new monster bundles
    /// </summary>
    public class MonsterCollectionRefresh : AssetPostprocessor
    {
        public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var monsterCollection = Object.FindObjectOfType<MonsterCollection>();

            if (monsterCollection != null)
            {
                Object.FindObjectOfType<MonsterCollection>().Refresh();
            }
        }
    }
}

#endif