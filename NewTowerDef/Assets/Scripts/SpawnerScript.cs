using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerScript : MonoBehaviour
{
    public LevelManagerScript levelManagerScript;
    public GameObject enemy;
    //public GameObject wayPointsParent;
    GameScript gameScript;
    float spawnTimer = 3f;
    int numberWave = 0;
    void Start()
    {
        gameScript = FindObjectOfType<GameScript>();
    }

    void Update()
    {
        if (spawnTimer <= 0)//если времени до волны осталось 0, то вызываем корутину
        {
            StartCoroutine(Spawn(numberWave + 1));//вызов
            spawnTimer = 3;//одновляем счетчик
        }
        spawnTimer -= Time.deltaTime;//отнимаем по 1сек.

        //GameObject.Find("NextSpawn").GetComponent<Text>().text = "Next wave: " + Mathf.Round(spawnTimer).ToString();//счетчик времени
    }

    //Корутина для спавна волн мобов
    IEnumerator Spawn(int enemyCount)
    {
        numberWave++;
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject tempEnemy = Instantiate(enemy);//создаём и инициализируем локальную переменную "врага"
            tempEnemy.transform.SetParent(gameObject.transform, false);//присваиваем родительский объект

            Transform startCellPos = levelManagerScript.wayPoints[0].transform;
            Vector3 startPos = new Vector3(startCellPos.position.x + startCellPos.GetComponent<SpriteRenderer>().bounds.size.x / 2, 
                                           startCellPos.position.y + startCellPos.GetComponent<SpriteRenderer>().bounds.size.y);

            tempEnemy.transform.position = startPos;
            tempEnemy.GetComponent<EnemyScript>().enemy = new Enemy(gameScript.enemies[Random.Range(0, gameScript.enemies.Count)]);

            //enemy.GetComponent<EnemyScript>().wayPointParrent = wayPointsParent;

            yield return new WaitForSeconds(0.3f);
        }
    }
}
