using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : TileHandler
{
    private void Start()
    {
        GetTiles();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (Vector3 position in tilePositions)
        {
            if((position - collision.transform.position).magnitude < 2)
            {
                Portal(position,collision.gameObject);
                break;
            }
        }
    }
    private void Portal(Vector3 origin, GameObject go)
    {
        foreach(Vector3 position in tilePositions)
        {
            if(position != origin && position.x == origin.x)
            {
                Vector3 targetPosition = new Vector3(
                    position.x, 
                    position.y, 
                    Camera.main.transform.position.z
                    );
                go.transform.position = position + Vector3.right/2;
                go.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if(go.tag == "Player")
                {
                    //Transicao suave
                }
                break;
            }
        }
    }
}
