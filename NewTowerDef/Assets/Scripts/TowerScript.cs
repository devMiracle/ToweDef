using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public GameObject bullet;
    GameScript gameScript;
    Tower tower;
    public TowerType towerType;
    public BulletType bulletType;
    void Start()
    {
        gameScript = FindObjectOfType<GameScript>();
        tower = gameScript.towers[(int)towerType];
        GetComponent<SpriteRenderer>().sprite = tower.sprite;

        InvokeRepeating("SearchTarget", 0, .1f);
    }

    void Update()
    {
        //if (CanShoot())
        //    SearchTarget();

        if (tower.currentCooldown > 0)
            tower.currentCooldown -= Time.deltaTime;
    }

    bool CanShoot()
    {
        if (tower.currentCooldown <= 0)
            return true;
        return false;
    }

    void SearchTarget()
    {
        if (CanShoot())
        {
            Transform nearEnemy = null;
            float enemyDist = Mathf.Infinity;
            GameObject[] itemArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject item in itemArray)
            {
                float currDistance = Vector2.Distance(transform.position, item.transform.position);
                if (currDistance < enemyDist && currDistance <= tower.range)
                {
                    nearEnemy = item.transform;
                    enemyDist = currDistance;
                }
            }
            if (nearEnemy != null)
                Shoot(nearEnemy);
        }
    }
    void Shoot(Transform enemy)
    {
        tower.currentCooldown = tower.cooldown;
        GameObject bulletTemp = Instantiate(bullet);//инициализируем переменную префабом
        bulletTemp.GetComponent<BulletScript>().tower = tower;
        bulletTemp.transform.position = transform.position;//приравниваем позицию снаряда к позиции башни
        bulletTemp.GetComponent<BulletScript>().SetTarget(enemy);//передаем врага в метод SetTarget
    }
}
