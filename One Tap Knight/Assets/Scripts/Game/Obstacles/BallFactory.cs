using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory : MonoBehaviour {

    [Header("Generation Settings")]
    public GameObject ball;
    public float interval;
    public int ballLifeTime = 2;
    public GameObject generationPoint;

    private float timer = 0;

	void Update ()
    {
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            timer = 0;
            GameObject g =(Instantiate(ball, generationPoint.transform.position, generationPoint.transform.rotation));
            Destroy(g, ballLifeTime);
        }
        
	}
}
