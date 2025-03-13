using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_orange : Enemy
{
    [SerializeField] private float verticalRange;
    [SerializeField] private float verticalSpeed;
    private float randomstart;

    private float startPosition;

    protected override void Start()
    {
        base.Start();
        isAtkEnemy = true;
        startPosition = Random.Range(-4, 5);
        randomstart = Random.Range(-1, 1);
    }

    protected override void Atk()
    {
        base.Atk();
        // Implement attack logic here
    }

    protected override void Die()
    {
        base.Die();
    }

    protected override void Move()
    {
        base.Move();

        float verticalMovement = Mathf.Sin(Time.time * verticalSpeed + randomstart) * verticalRange;
        transform.position = new Vector3(transform.position.x, startPosition + verticalMovement, transform.position.z);
    }
}
