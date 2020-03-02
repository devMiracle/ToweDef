using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{
    public int fieldWidth, fieldHeight;
    public GameObject cell;
    public Transform cellParent;
    public Sprite[] sprites = new Sprite[2];



    public List<GameObject> wayPoints = new List<GameObject>();
    int currWayX, currWayY;
    GameObject firstCell;
    GameObject[,] cells = new GameObject[13, 16];


    string[] path = {   "0000000000000000",
                        "0000000000000000",
                        "0000000000000000",
                        "1101111000000000",
                        "0111001000000000",
                        "0000001000000011",
                        "0000001000000010",
                        "0000001000000010",
                        "0000001111100010",
                        "0000000000101110",
                        "0000000000111000",
                        "0000000000000000",
                        "0000000000000000"};

    void Start()
    {
        CreateLevel();
        LoadWayPoints();
    }

    void Update()
    {
        
    }

    void CreateLevel()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        for (int i = 0; i < fieldHeight; i++)
            for (int k = 0; k < fieldWidth; k++)
            {
                int sprNumber = int.Parse(path[i].ToCharArray()[k].ToString());
                Sprite sprite = sprites[sprNumber];


                bool isPath = sprite == sprites[1] ? true : false;
                CreateCell(isPath, sprite, k, i, vec);

            }
    }
    void CreateCell(bool isPath, Sprite sprite, int x, int y, Vector3 vector)
    {
        GameObject tempCell = Instantiate(cell);

        tempCell.transform.SetParent(cellParent, false);
        tempCell.GetComponent<SpriteRenderer>().sprite = sprite;

        float sizeX = tempCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float sizeY = tempCell.GetComponent<SpriteRenderer>().bounds.size.y;

        tempCell.transform.position = new Vector3(vector.x + (sizeX * x), vector.y + (sizeY * -y));

        if (isPath)
        {
            tempCell.GetComponent<CellScript>().isPath = true;
            if (firstCell == null)
            {
                firstCell = tempCell;
                currWayX = x;
                currWayY = y;
            }
        }

        cells[y, x] = tempCell;
    }

    void LoadWayPoints()
    {
        wayPoints.Add(firstCell);
        GameObject currengWayGo;
        while (true)
        {
            currengWayGo = null;

            if (currWayX > 0 && cells[currWayY, currWayX - 1].GetComponent<CellScript>().isPath && !wayPoints.Exists(x => x == cells[currWayY, currWayX - 1]))
            {
                currengWayGo = cells[currWayY, currWayX - 1];
                currWayX--;
                Debug.Log("left");
            }
            else if (currWayX < (fieldWidth - 1) && cells[currWayY, currWayX + 1].GetComponent<CellScript>().isPath && !wayPoints.Exists(x => x == cells[currWayY, currWayX + 1]))
            {
                currengWayGo = cells[currWayY, currWayX + 1];
                currWayX++;
                Debug.Log("right");
            }


            else if(currWayY > 0 && cells[currWayY - 1, currWayX].GetComponent<CellScript>().isPath && !wayPoints.Exists(x => x == cells[currWayY - 1, currWayX]))
            {
                currengWayGo = cells[currWayY - 1, currWayX];
                currWayY--;
                Debug.Log("up");
            }
            else if(currWayY < (fieldHeight - 1) && cells[currWayY + 1, currWayX].GetComponent<CellScript>().isPath && !wayPoints.Exists(x => x == cells[currWayY + 1, currWayX]))
            {
                currengWayGo = cells[currWayY + 1, currWayX];
                currWayY++;
                Debug.Log("down");
            }
            else
                break;
            wayPoints.Add(currengWayGo);
        }
    }
}
