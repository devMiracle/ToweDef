using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public bool isPath, hasTower = false;
    public Color baseColor, CurrColor;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        if (!isPath)
        {
            GetComponent<SpriteRenderer>().color = CurrColor;
        }
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = baseColor;
    }
    private void OnMouseDown()
    {
        
    }
}
