using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_violet : Enemy
{
    [SerializeField] private float atkCool = 3f;
    private float t = 2;
    [SerializeField] private bullet bullet;

    protected override void Start()
    {
        base.Start();

        isAtkEnemy = true;
    }

    protected override void Atk()
    {
        t += Time.deltaTime;

        if (t >= atkCool && transform.position.x > -4.0)
        {
            Attack();
            t = 0;
        }
    }

    private void Attack()
    {
        bullet b = Instantiate(bullet, transform.position, Quaternion.identity);
    }


    protected override void Die()
    {
        base.Die();
    }
    protected override void Move()
    {
        base.Move();
    }

}