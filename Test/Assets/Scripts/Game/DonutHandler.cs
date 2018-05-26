using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DonutHandler : MonoBehaviour {
    private Tilemap tileMap;
    private List<TileBase> validTiles;
    private List<Vector3> tilePositions;

    private void Start(){
        GetTiles();
    }
    private void OnTriggerEnter2D(Collider2D collision){
        foreach (Vector3 position in tilePositions){
            if((position - collision.transform.position).magnitude < 2){
                Portal(position);
                break;
            }
        }
    }
    private void GetTiles(){
        tileMap = GetComponent<Tilemap>();
        validTiles = new List<TileBase>();
        tilePositions = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++){
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++){
                Vector3Int localTilePosition = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 tilePosition = tileMap.CellToWorld(localTilePosition);
                if (tileMap.HasTile(localTilePosition)){
                    validTiles.Add(tileMap.GetTile(localTilePosition));
                    tilePositions.Add(tilePosition);
                }
            }
        }
    }
    private void Portal(Vector3 origin){
        foreach(Vector3 position in tilePositions){
            if(position != origin && position.x == origin.x){
                GameObject character = FindObjectOfType<CharacterCentral>().gameObject;
                Vector3 targetPosition = new Vector3(
                    position.x, 
                    position.y, 
                    Camera.main.transform.position.z
                    );
                character.transform.position = position;
                character.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Camera.main.GetComponent<CameraMovement>().ShowTarget(
                    targetPosition - Vector3.up*7f,
                    0.5f,
                    0f
                    );
                break;
            }
        }
    }
}
