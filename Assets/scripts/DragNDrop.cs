using UnityEngine;

public class DragNDrop : MonoBehaviour
{

    private float wid, hgt;
    private int lineSize = 2;

    //to use in GameManager 
    //linking
    private GameObject[] fourdirbound = new GameObject[4]; //0up 1right 2down 3left

    



    private void Start()
    {
        wid = gameObject.transform.localScale.x;
        hgt = gameObject.transform.localScale.y;


    }


    private bool dragging = false;
    private RaycastHit2D[] raycasthits = new RaycastHit2D[4];
    private Vector2[] howfar = new Vector2[4];



    private void Update()
    {
        if (dragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x, mousePosition.y);

            Debug.DrawRay(transform.position, transform.right * lineSize, Color.red);
            Debug.DrawRay(transform.position, transform.right * -1 * lineSize, Color.red);
            Debug.DrawRay(transform.position, transform.up * lineSize, Color.red);
            Debug.DrawRay(transform.position, transform.up * -1 * lineSize, Color.red);

        }
    }

    private void OnMouseDown()
    {
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;


        //print the nearest gameobject detected 
        FindAndPrintClosestObject();
    }

    private void FindAndPrintClosestObject()
    {
        //enable false gameObject boxcolider2D
        //detects itself
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        raycasthits[1] = Physics2D.Raycast(transform.position, transform.right, lineSize);
        raycasthits[3] = Physics2D.Raycast(transform.position, transform.right * -1, lineSize);
        raycasthits[0] = Physics2D.Raycast(transform.position, transform.up, lineSize);
        raycasthits[2] = Physics2D.Raycast(transform.position, transform.up * -1, lineSize);
        //ray up:0 right:1 down:2 left:3

        //enable boxcolider 
        gameObject.GetComponent<BoxCollider2D>().enabled = true;


        Vector2 nearestblock = new Vector2(0, 0);
        int nearest = -1;


        for (int i = 0; i < raycasthits.Length; i++) //run all
        {
            if (raycasthits[i].collider != null) //detected
            {

                print(i);
                howfar[i] = raycasthits[i].transform.position - gameObject.transform.position;
                howfar[i] = new Vector2(Mathf.Abs(howfar[i].x), Mathf.Abs(howfar[i].y)); //absolute vector2 (left- right+)=> (left+ right+)
                nearestblock = raycasthits[i].transform.position;
                nearest = i;

            }
        }


        //run all
        for (int i = 0; i < raycasthits.Length; i++)
        {
            if (raycasthits[i].collider != null)//detected
            {
                if (howfar[nearest].magnitude >= howfar[i].magnitude) //find the nearest
                {
                    nearestblock = raycasthits[i].transform.position;
                    nearest = i;
                }
            }
        }



        if (nearest != -1) //-1 : not found
        {
            print("nearest : " + nearest);
            gameObject.transform.position = nearestblock; //snap 
                                                          //wid,hgt < localscale . all blocks same sized

            switch (nearest)
            {
                case 0: //up
                    gameObject.transform.position = new Vector2(nearestblock.x, nearestblock.y - hgt);
                    break;
                case 1://right
                    gameObject.transform.position = new Vector2(nearestblock.x-wid, nearestblock.y );
                    break;
                case 2://down
                    gameObject.transform.position = new Vector2(nearestblock.x, nearestblock.y + hgt);
                    break;
                case 3://left
                    gameObject.transform.position = new Vector2(nearestblock.x+wid, nearestblock.y);
                    break;

            }
        }

        //yay we can now snap
    }


    //four direction bound
    private void FourDirBound()
    {
        //what I know : the block I snapped
        //스냅할 때 받은 것에만 먼저 연결
        //받은 것의 나랑 닿은 쪽에 스냅 
        //스냅된애들끼리 라인렌더러로 선 그려주기 
    }
}
