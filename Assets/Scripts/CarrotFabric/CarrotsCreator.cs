using Grid;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CarrotFabric
{
    public class CarrotsCreator : MonoBehaviour
    {
        public const int CarrotsNumber = 10;
        public GameObject CarrotPrefab;
        public Transform CarrotParent;

        private Tilemap _ground;
        
        private void Start()
        {
            _ground = GridContainer.GroundTilemap;
            Create();
        }

        private void Create()
        {
            for(int i=0; i<CarrotsNumber; i++)
            {
                var carrot = Instantiate(CarrotPrefab, CarrotParent);
                carrot.transform.position = GetRandomCarrotPosition();
            }
        }

        private Vector3 GetRandomCarrotPosition()
        {
            var rangeX = (_ground.size.x - 1) / 2f;
            var rangeY = (_ground.size.y - 1) / 2f;
            var xRandom = Random.Range(-rangeX, rangeX);
            var yRandom = Random.Range(-rangeY, rangeY);
            
            var tileForCarrot = _ground.WorldToCell(new Vector3(xRandom, yRandom, 0));
            var carrotPos = _ground.GetCellCenterWorld(tileForCarrot); 
            
            while(!GridContainer.IsCellClear(carrotPos))
                return GetRandomCarrotPosition();
        
            return carrotPos;
        }
    }
}
