using Grid;
using UnityEngine;


public class ObjectOnGridPlacement : MonoBehaviour
{
    void Start()
    {
        Vector3Int currentTile = GridContainer.GroundTilemap.WorldToCell(transform.position);
        transform.position = GridContainer.GroundTilemap.GetCellCenterWorld(currentTile);
    }
}
