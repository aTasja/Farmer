using Grid;
using UnityEngine;

namespace CarrotFabric
{
    public class CarrotCreator:MonoBehaviour
    {
        public const int CarrotsNumber = 10;
        public GameObject CarrotPrefab;
        public Transform CarrotParent;
        
        private void Start()
        {
            for(int i=0; i<CarrotsNumber; i++)
            {
                GameObject carrot = Instantiate(CarrotPrefab, CarrotParent);
                carrot.transform.position = GetRandomCarrotPosition();
            }
        }

        private Vector3 GetRandomCarrotPosition()
        {
            var ground = GridContainer.GroundTilemap;
            var rangeX = (ground.size.x - 1) / 2f;
            var rangeY = (ground.size.y - 1) / 2f;
            var xRandom = Random.Range(-rangeX, rangeX);
            var yRandom = Random.Range(-rangeY, rangeY); 
            
            Vector3Int tileForCarrot = ground.WorldToCell(new Vector3(xRandom, yRandom, 0));
            var carrotPos = ground.GetCellCenterWorld(tileForCarrot); 
            
            if (!GridContainer.IsCellClear(carrotPos))
                return GetRandomCarrotPosition();
        
            return carrotPos;
        }
    }
}
