using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSlow : MonoBehaviour {
    public float velocityModifier;
    private float velocity;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CharacterMovement cm = collision.gameObject.GetComponent<CharacterMovement>();
            velocity = cm.velocity;
            cm.velocity *= velocityModifier;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CharacterMovement cm = collision.gameObject.GetComponent<CharacterMovement>();
            cm.velocity = velocity;
        }
    }
}
