using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VelocityModifier : MonoBehaviour 
{
    [Header("Preferences")]
    public float velocityModifier;
    public SlimeType type;

    private Coroutine anim;
    [SerializeField] private Character character;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Character>().velocity = Character.idealVelocity*velocityModifier;
            character.StartSlimeAnimation(type);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Character>().velocity = Character.idealVelocity;
            character.StopSlimeAnimation();
        }
    }
}
