using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject StartBlock;
    
   private void Start()
    {
         Application.targetFrameRate =60;
    }
}
