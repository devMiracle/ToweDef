using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Transform enemy;
    public Bullet bullet;
    public Tower tower;
    GameScript gameScript;
    void Start()
    {
        gameScript = FindObjectOfType<GameScript>();
        bullet = gameScript.bullets[tower.type];
        GetComponent<SpriteRenderer>().sprite = bullet.sprite;
    }
    void Update()
    {
        if (enemy != null)
            Move();
        else
            Destroy(gameObject);
    }
    public void SetTarget(Transform enemy)
    {
        this.enemy = enemy;
    }
    void Move()
    {
        if (Vector2.Distance(transform.position, enemy.position) < .1f)
            Hit();
        if (enemy != null)
        {
            Vector2 vec = enemy.position - transform.position;

            transform.Translate(vec.normalized * Time.deltaTime * bullet.speed);
        }
        else
            Destroy(gameObject);
    }
    void Hit()
    {
        switch (tower.type)
        {
            case (int)TowerType.fireTower:
                enemy.GetComponent<EnemyScript>().StartSlow(4, 1);
                enemy.GetComponent<EnemyScript>().TakeDamage(bullet.damage);
                break;
            case (int)TowerType.earthTower:
                enemy.GetComponent<EnemyScript>().TakeDamage(bullet.damage);
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }

}
