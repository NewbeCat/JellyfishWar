using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float spd;
    public float gameSpd;
    protected scoreeffects scoreEffects;
    // Reference to the ScoreEffects script

    private void Start()
    {
        gameSpd = GameManager.instance.speed;
        scoreEffects = FindObjectOfType<scoreeffects>();
    }

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.position += Vector3.left * gameSpd * spd * Time.deltaTime;
    }

    protected virtual void ItemEvent()
    {
        AudioManager.Instance.PlaySFX("아이템획득");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ItemEvent();
        }

        if (collision.CompareTag("ScreenOut"))
        {
            GameManager.instance.spawn.items.Remove(this);
            Destroy(gameObject);
        }
    }
}