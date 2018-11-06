using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mushroom : MonoBehaviour {
    public float poundModifier;
    public float bounceIntensity;
    public float hatBounceIntensity;
    public float hatBounceDuration;

	private void Start () {
		
	}
	private void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Bounce(collision.gameObject.GetComponent<Rigidbody2D>());
    }
    private void Bounce(Rigidbody2D player)
    {
        float mod = player.GetComponent<KnightController>().isPounding ? poundModifier : 1;
        player.GetComponent<KnightSound>().PlaySound(SoundType.JUMP);
        player.GetComponent<KnightController>().isPounding = false;
        transform.GetChild(0).DOPunchScale(new Vector3(0, hatBounceIntensity, 0), hatBounceDuration);
        player.velocity = new Vector2(player.velocity.x, 0);
        player.AddForce(Vector2.up * bounceIntensity * mod);
        Camera.main.GetComponent<CameraMovement>().Bounce();
    }
}
