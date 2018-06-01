using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterAnimation : MonoBehaviour
{
    [Header("Slime Effect")]
    [SerializeField] private List<GameObject> slimePrefab;
    [SerializeField] private Transform slimePosition;
    private Coroutine slimeCoroutine;
    [Header("Death Effect")]
    [SerializeField] private int bloodCells;
    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private Transform bloodPosition;
    [Header("Components")]
    private Animator anmt;
    private Rigidbody2D rb;

    private void Start()
    {
        anmt = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Land(bool value)
    {
        anmt.SetBool("grounded", value);
    }
    public void Jump()
    {
        anmt.SetTrigger("jump");
    }
    public void Move()
    {
        anmt.SetFloat("velocity", Mathf.Abs(rb.velocity.x));
    }
    public void Die()
    {
        for (int i = 0; i < bloodCells; i++)
        {
            var bloodCell = Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            bloodCell.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(1f, 2f) + Vector2.right * Random.Range(-1f, 1f) * 2f);
            bloodCell.transform.DOScale(0f, 1f);
        }
    }
    public void Slime(SlimeType type, bool active)
    {
        if (active)
        {
            slimeCoroutine = StartCoroutine(SlimeAnimation(type));
        }
        else
        {
            if(slimeCoroutine != null) StopCoroutine(slimeCoroutine);
        }
    }
    private IEnumerator SlimeAnimation(SlimeType type)
    {
        while (true)
        {
            float randomSize = Random.Range(0.5f, 2f);
            float randomDuration = Random.Range(0.1f, 0.6f);
            Vector2 randomUpDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
            float randomForce = Random.Range(0.1f, 1f) * 200f;
            var slime = Instantiate(
                slimePrefab[type.GetHashCode()],
                slimePosition.position,
                Quaternion.identity
                );
            slime.GetComponent<Rigidbody2D>().AddForce(randomUpDirection * randomForce);
            slime.transform.DOScale(0, randomDuration);
            Destroy(slime, randomDuration);
            yield return new WaitForSeconds(randomDuration / 3);
        }
    }
}
public enum SlimeType {
    RED_SLIME, GREEN_SLIME
}