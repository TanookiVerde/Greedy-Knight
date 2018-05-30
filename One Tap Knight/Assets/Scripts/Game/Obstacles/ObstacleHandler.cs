using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : TileHandler {

    [Header("Segment Settings")]
    public int segmentAmount = 3;

    private Dictionary<int, List<GameObject>> obstacles;
    private float levelLength;
    private float segmentLength;
    private int currentSegment = 1;
    private float initialPos;

    private GameObject player;

	private void Start ()
    {
        GetTiles();
        levelLength = tilePositions[tilePositions.Count - 1].x - tilePositions[0].x;
        segmentLength = levelLength / segmentAmount;
        initialPos = tilePositions[0].x;

        player = GameObject.FindGameObjectWithTag("Player");

        GetObstacles();
    }	
	private void Update ()
    {
        if (player != null)
        {
            if (player.transform.position.x > initialPos + (segmentLength * (currentSegment - 1)) && currentSegment <= segmentAmount - 1)
            {
                ActivateSegment(currentSegment + 1);
                if (currentSegment >= 3)
                    DeactivateSegment(currentSegment - 2);
                currentSegment++;
            }
        }
	}
    private void GetObstacles()
    {
        obstacles = new Dictionary<int, List<GameObject>>();
        List<GameObject> temp = new List<GameObject>();
        foreach (Transform child in transform)
        {
            temp.Add(child.gameObject);
        }
        temp.Remove(gameObject);
        for (int i = 1; i <= segmentAmount; i++)
            obstacles.Add(i, new List<GameObject>());

        int segment;
        foreach (GameObject g in temp)
        {
            for(segment = 1; segment < segmentAmount; segment++)
            {
                if (g.transform.position.x < initialPos + (segmentLength * segment))
                    break;
            }
            obstacles[segment].Add(g);
            g.SetActive(false);
        }
        ActivateSegment(1);
    }
    private void ActivateSegment(int seg)
    {
        foreach(GameObject g in obstacles[seg])
        {
            if(g != null)
                g.SetActive(true);
        }
    }
    private void DeactivateSegment(int seg)
    {
        foreach (GameObject g in obstacles[seg])
        {
            if(g != null)
                g.SetActive(false);
        }
    }
}
