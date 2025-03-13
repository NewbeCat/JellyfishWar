using System.Collections;
using UnityEngine;

public class electric : MonoBehaviour
{
    private float startTime;
    [SerializeField] private float killswitch;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime >= killswitch)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.player.isInvincibility) return;

        if (collision.CompareTag("Player"))
        {
            GameManager.instance.player.HP--;
        }
    }
}

