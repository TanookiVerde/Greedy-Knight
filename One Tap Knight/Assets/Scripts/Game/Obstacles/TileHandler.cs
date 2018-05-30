using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHandler : MonoBehaviour
{
    protected Tilemap tileMap;
    protected List<TileBase> validTiles;
    protected List<Vector3> tilePositions;

    protected void GetTiles()
    {
        tileMap = GetComponent<Tilemap>();
        validTiles = new List<TileBase>();
        tilePositions = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localTilePosition = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 tilePosition = tileMap.CellToWorld(localTilePosition);
                if (tileMap.HasTile(localTilePosition))
                {
                    validTiles.Add(tileMap.GetTile(localTilePosition));
                    tilePositions.Add(tilePosition);
                }
            }
        }
    }
    public List<Vector3> GetTilePositions()
    {
        return tilePositions;
    }
}
