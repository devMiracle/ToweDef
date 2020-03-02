using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower
{
    public int type;
    public float cooldown, range, currentCooldown = 0;
    int price;
    public Sprite sprite;
    public Tower(int type, int price, float cooldown, float range, string path)
    {
        this.type = type;
        this.price = price;
        this.cooldown = cooldown;
        this.range = range;
        sprite = Resources.Load<Sprite>(path);
    }
}

public class Bullet
{
    public float speed;
    public int damage;
    public Sprite sprite;

    public Bullet(float speed, int damage, string path)
    {
        this.speed = speed;
        this.damage = damage;
        sprite = Resources.Load<Sprite>(path);
    }
}

public class Enemy
{
    public float health, speed, startSpeed;
    public Sprite sprite;
    string path;

    public Enemy(float health, float speed, string path)
    {
        this.health = health;
        this.speed = speed;
        sprite = Resources.Load<Sprite>(path);
        startSpeed = speed;
        this.path = path;
    }
    public Enemy(Enemy enemy)
    {
        health = enemy.health;
        speed = enemy.speed;
        startSpeed = enemy.startSpeed;
        sprite = enemy.sprite;
        //sprite = Resources.Load<Sprite>(path);
    }
}

enum EnemyType
{

}
public enum TowerType
{
    fireTower,
    earthTower,
    waterTower,
    windTower
}
public enum BulletType
{
    fireBullet,
    earthBullet,
    waterBullet,
    windBullet
}

public class GameScript : MonoBehaviour
{
    public List<Tower> towers = new List<Tower>();
    public List<Bullet> bullets = new List<Bullet>();
    public List<Enemy> enemies = new List<Enemy>();
    void Awake()
    {
        towers.Add(new Tower(0, 300, .3f, 2, "TowerSprites/fireTower"));
        towers.Add(new Tower(1, 300, 1f, 5, "TowerSprites/earthTower"));

        bullets.Add(new Bullet(7, 5, "BulletSprites/fireBullet"));
        bullets.Add(new Bullet(4, 30, "BulletSprites/earthBullet"));

        enemies.Add(new Enemy(50, 2, "EnemySprites/standardEnemy"));
        enemies.Add(new Enemy(10, 4, "EnemySprites/greenEnemy"));

    }


    void Update()
    {
        
    }
}
