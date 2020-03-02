using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    List<GameObject> wayPoints = new List<GameObject>();
    //public GameObject wayPointParrent;
    public Enemy enemy;
    int index = 0;
    
    void Start()
    {
        GetWayPoints();
        GetComponent<SpriteRenderer>().sprite = enemy.sprite;
    }
    void Update()
    {
        Move();
        
    }
    private void Move()
    {
        Transform currWayPoint = wayPoints[index].transform;


        Vector3 curWayPos = new Vector3(currWayPoint.position.x + currWayPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                        currWayPoint.position.y - currWayPoint.GetComponent<SpriteRenderer>().bounds.size.y / 2);


        Vector3 vector = curWayPos - transform.position;


        transform.Translate(vector.normalized * Time.deltaTime * enemy.speed);
        if (Vector3.Distance(transform.position, curWayPos) < 0.1f)
        {
            if (index < wayPoints.Count - 1)
                index++;
            else
                Destroy(gameObject);
        }
    }
    void GetWayPoints()
    {
        //for (int i = 0; i < wayPointParrent.transform.childCount; i++)
        //    wayPoints.Add(wayPointParrent.transform.GetChild(i).gameObject);
        wayPoints = GameObject.Find("LevelGroup").GetComponent<LevelManagerScript>().wayPoints;
    }

    public void TakeDamage(int damage)
    {
        enemy.health -= damage;
        CheckIsAlive();
    }

    void CheckIsAlive()
    {
        if (enemy.health <= 0)
            Destroy(gameObject);
    }

    public void StartSlow(float time, float slowLevel)
    {
        StopCoroutine("GetSlow");
        enemy.speed = enemy.startSpeed;
        StartCoroutine(GetSlow(time, slowLevel));
    }

    IEnumerator GetSlow(float time, float slowLevel)
    {
        enemy.speed -= slowLevel;
        yield return new WaitForSeconds(time);
        enemy.speed = enemy.startSpeed;
    }
}
