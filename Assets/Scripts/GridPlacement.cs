using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridPlacement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3Int currentTile = GameManager.GroundTilemap.WorldToCell(transform.position);
        transform.position = GameManager.GroundTilemap.GetCellCenterWorld(currentTile);
    }
}
