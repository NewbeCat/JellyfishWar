using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float gameSpd;
    [SerializeField] private float spd;
    protected bool isAtkEnemy = false;

    protected bool isItemDie = false;

    protected virtual void Start()
    {
        gameSpd = GameManager.instance.speed;
    }


    private void Update()
    {
        Move();

        if (isAtkEnemy) Atk();

    }

    protected virtual void Atk()
    {

    }

    protected virtual void Move()
    {
        transform.position += Vector3.left * gameSpd * spd * Time.deltaTime;
    }


    protected virtual void Die()
    {
        GameManager.instance.spawn.enemies.Remove(this);
        GameObject scoreEffectsObject = GameObject.Find("ScoreEffects");
        scoreeffects scoreEffectScript = scoreEffectsObject.GetComponent<scoreeffects>();
        scoreEffectScript.DisplayScore(200, transform.position);

        if (!isItemDie)
            GameManager.instance.player.SkillGauge += 20;
        GameManager.instance.Score += 200;
        AudioManager.Instance.PlaySFX("피격_적");

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Skill"))
        {
            isItemDie = (false);
            Die();
        }

        if (collision.CompareTag("BoomRange"))
        {
            isItemDie = (true);
            Die();
        }

        if (GameManager.instance.player.isInvincibility || GameManager.instance.player.isAttack) return;

        if (collision.CompareTag("Player"))
        {
            GameManager.instance.player.HP--;
        }

        if (collision.CompareTag("ScreenOut"))
        {
            GameManager.instance.spawn.enemies.Remove(this);
            Destroy(gameObject);
        }
    }


}
