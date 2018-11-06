using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeathSpawn : MonoBehaviour {

    public GameObject bloodCellPrefab;
    public int numberOfCells;
    public float forceIntensity;

    private void Start()
    {
        for(int i = 0; i < numberOfCells; i++)
        {
            var go = Instantiate(bloodCellPrefab, transform.position, Quaternion.identity);
            var v = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0.75f, 1f)).normalized;
            go.GetComponent<Rigidbody2D>().AddForce(v * forceIntensity * Random.Range(0f, 1f));
            go.transform.DOScale(0, 3f);
            Destroy(go, 10f);
        }
        Destroy(gameObject);
    }

}
