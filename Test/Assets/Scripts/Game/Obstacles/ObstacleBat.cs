using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ObstacleBat : MonoBehaviour {

    [Header("Movement Parameters")]
    public float movementSpeed;

    private Rigidbody2D myRB;
    
	void Start () {
        myRB = GetComponent<Rigidbody2D>();
        myRB.velocity = new Vector2(movementSpeed, 0);
	}
}
