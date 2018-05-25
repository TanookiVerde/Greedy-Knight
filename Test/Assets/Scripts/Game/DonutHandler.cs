using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DonutHandler : MonoBehaviour {

    Tilemap tileMap;
    List<TileBase> validTiles;
    List<Vector3> tilePositions;

    void Start ()
    {
        GetTiles();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (Vector3 v in tilePositions)
        {
            if((v - collision.transform.position).magnitude < 2)
            {
                Portal(v);
                break;
            }
        }
    }
    void GetTiles()
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
    void Portal(Vector3 origin)
    {
        foreach(Vector3 v in tilePositions)
        {
            if(v != origin && v.x == origin.x)
            {
                FindObjectOfType<Character>().gameObject.transform.position = v;
                Camera.main.transform.position = new Vector3(v.x, v.y, Camera.main.transform.position.z);
                break;
            }
        }
    }
}
