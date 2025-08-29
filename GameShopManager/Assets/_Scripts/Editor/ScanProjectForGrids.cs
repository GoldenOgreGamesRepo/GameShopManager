using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ScanProjectForGrids
{
    [MenuItem("Tools/Scan All Prefabs for Grids")]
    public static void ScanPrefabs()
    {
        Debug.Log("🔍 Scanning all prefabs in project for Grid components...");

        // Find all prefabs in the project
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        int foundCount = 0;

        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab == null) continue;

            Grid grid = prefab.GetComponentInChildren<Grid>(true); // include inactive children

            if (grid != null)
            {
                Debug.Log($"✔ Grid found in prefab: {path}");
                foundCount++;
            }
        }

        if (foundCount == 0)
            Debug.Log("✅ No Grid components found in any prefab.");
        else
            Debug.Log($"🔹 Total prefabs with Grid components: {foundCount}");
    }
}
