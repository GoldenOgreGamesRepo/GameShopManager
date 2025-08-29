using UnityEngine;
using UnityEngine.Tilemaps; // for Grid + Tilemap support

public class FindBrokenGrids : MonoBehaviour
{
    [ContextMenu("Scan for Broken Grids")]
    void Scan()
    {
        Debug.Log("🔍 Scanning scene for Grid components...");

        Grid[] grids = FindObjectsByType<Grid>(FindObjectsSortMode.None);
        if (grids.Length == 0)
        {
            Debug.Log("✅ No Grid objects found in the scene.");
            return;
        }

        foreach (var grid in grids)
        {
            if (grid == null)
            {
                Debug.LogError("❌ Found a NULL Grid reference (this should not happen).");
                continue;
            }

            try
            {
                // Try to access a harmless property
                var cellSize = grid.cellSize;
                Debug.Log("✔ Grid OK on GameObject: " + grid.gameObject.name);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("❌ Broken Grid on GameObject: " + grid.gameObject.name +
                               " | Exception: " + ex.Message);
            }
        }
    }
}
