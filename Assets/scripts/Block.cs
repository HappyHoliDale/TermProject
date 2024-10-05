using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;



public class Block : MonoBehaviour
{
    protected float wid, hgt;
    protected int lineSize = 2;

    public Vector2 where;


    private void Start()
    {
        wid = gameObject.transform.localScale.x;
        hgt = gameObject.transform.localScale.y;
        where =gameObject.transform.localPosition;

    }

  


}
