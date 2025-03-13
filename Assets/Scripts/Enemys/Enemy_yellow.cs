using System.Collections;
using UnityEngine;

public class Enemy_yellow : Enemy
{
    [SerializeField] private float atkCool=3;
    private float t;

    [SerializeField] private electric electricFieldPrefab;
    private electric electricFieldInstance;

    protected override void Start()
    {
        base.Start();
        isAtkEnemy = true;
        t = Random.Range(0, 3);
    }

    protected override void Atk()
    {
        t += Time.deltaTime;

        if (t >= atkCool)
        {
            Attack();
            t = 0;
        }
    }

    private void Attack()
    {
        if (electricFieldPrefab != null)
        {
            electricFieldInstance = Instantiate(electricFieldPrefab, transform.position, Quaternion.identity);
            electricFieldInstance.transform.parent = transform;
        }
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

