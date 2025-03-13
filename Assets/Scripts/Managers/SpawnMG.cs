using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMG : MonoBehaviour
{

    [SerializeField] private float enemySpCool = 3;
    [SerializeField] private float scoreSpCool;
    [SerializeField] private float itemSpCool;
    [SerializeField] private Enemy[] enemy;
    [SerializeField] private Item[] itemObjs;

    public List<Enemy> enemies = new List<Enemy>();
    public List<Item> items = new List<Item>();

    private float enemyT = 0;
    private float scoreItemT = 0;
    private float ItemT = 0;

    [SerializeField] private int enemyLevel = 1;

    private void Start()
    {
        enemyT = 0;
        StartCoroutine(IncrementEnemyLevelCoroutine());
    }

    private IEnumerator IncrementEnemyLevelCoroutine()
    {
        while (enemyLevel < 4)
        {
            yield return new WaitForSeconds(30f);

            enemyLevel++;
            Debug.Log("Enemy Level increased to: " + enemyLevel);
        }
    }

    void Update()
    {
        int randomEnemy = 0;
        enemyT += Time.deltaTime * GameManager.instance.speed;
        scoreItemT += Time.deltaTime;
        ItemT += Time.deltaTime;
        if (enemyT >= enemySpCool)
        {
            if (enemyLevel == 4)
            {
                float randomValue = Random.value;
                if (randomValue < 1f / 7f)
                {
                    randomEnemy = 3;
                }
                else
                {
                    randomEnemy = Random.Range(0, 3);
                }
            }
            else
            {
                randomEnemy = Random.Range(0, enemyLevel);
            }
            Spawn(enemy[randomEnemy], new Vector2(10.25f, Random.Range(-4, 5)));
            enemyT = 0;

            enemySpCool = Random.Range(1f, 5.5f - (float)enemyLevel);

        }

        if (scoreItemT >= scoreSpCool)
        {
            SpawnItem(itemObjs[0], new Vector2(10.25f, Random.Range(-4, 5)));
            scoreItemT = 0;
        }

        if (ItemT >= itemSpCool)
        {
            SpawnItem(itemObjs[Random.Range(1, 3)], new Vector2(10.25f, Random.Range(-4, 5)));
            ItemT = 0;

            itemSpCool = Random.Range(20, 30);
        }
    }

    public void GameSpdEvent()
    {
        float mSpd = GameManager.instance.speed;

        foreach (var e in enemies)
        {
            e.gameSpd = mSpd;
        }

        foreach (var item in items)
        {
            item.gameSpd = mSpd;
        }
    }
    private void Spawn(Enemy enemy, Vector2 pos)
    {
        Enemy e = Instantiate(enemy);
        e.transform.position = pos;
        enemies.Add(e);
    }

    private void SpawnItem(Item item, Vector2 pos)
    {

        Item i = Instantiate(item);
        i.transform.position = pos;
        items.Add(i);
    }

}
