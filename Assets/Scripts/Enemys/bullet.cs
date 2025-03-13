using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 2f;
    private GameObject player;
    private Vector3 targetDirection;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            targetDirection = (player.transform.position - transform.position);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
        }
        else
        {
            Debug.LogWarning("Player not found!");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position += targetDirection * speed * Time.deltaTime;
        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.player.isInvincibility)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            GameManager.instance.player.HP--;
            Destroy(gameObject);
        }
    }
}