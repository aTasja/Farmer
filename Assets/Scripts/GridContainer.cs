using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid
{
    public class GridContainer : MonoBehaviour
    {
        
        [SerializeField] private Tilemap obstacles;
        [SerializeField] private Tilemap ground;

        private static GridContainer Instance;
        public static Tilemap GroundTilemap => Instance.ground;

        public static bool IsCellClear(Vector3 pos)
        {
            Vector3Int obstacleMapTile = Instance.obstacles.WorldToCell(pos);
            return !Instance.obstacles.HasTile(obstacleMapTile);
        }
    }
}
