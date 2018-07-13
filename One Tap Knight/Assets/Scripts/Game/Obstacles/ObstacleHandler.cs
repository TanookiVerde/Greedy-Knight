using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : TileHandler {
    private const int SEGMENT_AMOUNT                =   7;
    private const float SEGMENT_LENGTH              =  30;
    private const float INITIAL_TILE                = -50;
    private const float LINE_VERTICAL_BOUND_MIN     = -20;
    private const float LINE_VERTICAL_BOUND_MAX     =  20;

    private Dictionary<int, List<GameObject>> obstaclesPerSegment;
    private int currentSegment = 1;

    private GameObject player;

	private void Start ()
    {
        GetTiles();
        SeparateObstaclesInSegments();
    }	
	private void Update ()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (player.transform.position.x > INITIAL_TILE + (SEGMENT_LENGTH * (currentSegment - 1)) && currentSegment <= SEGMENT_AMOUNT - 1)
            {
                SetSegmentActive(true, currentSegment + 1);
                if (currentSegment >= 3)
                {
                    SetSegmentActive(false, currentSegment - 2);
                }
                currentSegment++;
            }
        }
	}
    private void SeparateObstaclesInSegments()
    {
        List<GameObject> obstacles = GetAllObstacles();
        obstaclesPerSegment = InitializeDictionaryWithIndex();

        foreach (GameObject obstacle in obstacles)
        {
            for(int currentSegment = 1; currentSegment < SEGMENT_AMOUNT; currentSegment++)
            {
                if (obstacle.transform.position.x < INITIAL_TILE + (SEGMENT_LENGTH * currentSegment))
                {
                    obstaclesPerSegment[currentSegment].Add(obstacle);
                    obstacle.SetActive(false);
                    break;
                }
            }
        }
        SetSegmentActive(true,1);
    }
    private List<GameObject> GetAllObstacles()
    {
        List<GameObject> obstacles = new List<GameObject>();

        foreach (Transform child in transform)
        {
            obstacles.Add(child.gameObject);
        }
        obstacles.Remove(gameObject);
        return obstacles;
    }
    private Dictionary<int, List<GameObject>> InitializeDictionaryWithIndex()
    {
        Dictionary<int, List<GameObject>> obstaclesDictionary = new Dictionary<int, List<GameObject>>();
        for (int i = 1; i <= SEGMENT_AMOUNT; i++)
        {
            obstaclesDictionary.Add(i, new List<GameObject>());
        }
        return obstaclesDictionary;
    }
    private void SetSegmentActive(bool active, int segment)
    {
        foreach (GameObject obstacle in obstaclesPerSegment[segment])
        {
            if (obstacle != null)
                obstacle.SetActive(active);
        }
    }
    private void OnDrawGizmos()
    {
        for(int i = 0; i < SEGMENT_AMOUNT; i++)
        {
            Vector3 init = new Vector3(INITIAL_TILE + i * SEGMENT_LENGTH, LINE_VERTICAL_BOUND_MIN);
            Vector3 final = new Vector3(INITIAL_TILE + i * SEGMENT_LENGTH, LINE_VERTICAL_BOUND_MAX);
            Gizmos.DrawLine(init,final);
        }
    }
}
