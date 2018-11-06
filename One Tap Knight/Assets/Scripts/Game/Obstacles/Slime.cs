using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {
    public float tax;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            collider.gameObject.GetComponent<KnightController>().ModifyVelocity(tax);
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            collider.gameObject.GetComponent<KnightController>().ModifyVelocity(1);
    }
}
