using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid
{
    public class GridContainer : MonoBehaviour
    {
        [SerializeField] private Tilemap _obstacles;
        [SerializeField] private Tilemap _ground;
        private static GridContainer Instance;

        private void Awake() => Instance = this;
        
        public static Tilemap GroundTilemap => Instance._ground;

        public static bool IsCellClear(Vector3 pos)
        {
            Vector3Int obstacleMapTile = Instance._obstacles.WorldToCell(pos);
            return !Instance._obstacles.HasTile(obstacleMapTile);
        }
    }
}
