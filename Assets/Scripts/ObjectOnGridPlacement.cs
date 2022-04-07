using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectOnGridPlacement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3Int currentTile = GridContainer.GroundTilemap.WorldToCell(transform.position);
        transform.position = GridContainer.GroundTilemap.GetCellCenterWorld(currentTile);
    }
}
