using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject StartBlock, Endblock;
    
   private void Start()
    {
        GameSettings();

    }

    private void GameSettings()
    {
        Application.targetFrameRate = 60;
        int setWidth =1220; // 화면 너비
        int setHeight = 800; // 화면 높이
        Screen.SetResolution(setWidth, setHeight, false); //창모드
    }

    private void GameStart()
    {
        GameObject now, next;
        //탐색은 하->우 우선순위임 
        now= StartBlock;

        //fourdirbound[4]; 0up 1right 2down 3left
        StartBlock.TryGetComponent<DragNDrop>(out DragNDrop drag);
        if (drag.fourdirbound[1]!=null) { next = drag.fourdirbound[1].gameObject; }
        if (drag.fourdirbound[2]!= null) { next = drag.fourdirbound[2].gameObject; }


     //   while (now!=Endblock || next!=null) 
   //     {
        
    //    }
        
    }
}
