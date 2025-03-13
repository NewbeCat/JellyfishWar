using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    [SerializeField] private float atkCool;
    private float t = 0;


    protected override void Start()
    {
        base.Start();

        isAtkEnemy = true;
    }

    protected override void Atk()
    {
        t += Time.deltaTime;

        if(t >= atkCool)
        {
            Attack();
        }

    }

    private void Attack()
    {

    }


    protected override void Die()
    {
        base.Die();
    }

    protected override void Move()
    {
        //d
    }


}
