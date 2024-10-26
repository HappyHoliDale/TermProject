using UnityEngine;

public class DragNDrop : MonoBehaviour
{

    private float wid, hgt;
    private int lineSize = 2;

    //to use in GameManager 
    //linking
    internal DragNDrop[] fourdirbound = new DragNDrop[4]; //0up 1right 2down 3left

    string blocktype;
    //start, end, 


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

    protected virtual void FindAndPrintClosestObject()
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

        DetectRay(nearest, nearestblock);
        RayHItFind(raycasthits, nearestblock, nearest);

        GameObject linkedblock;

    }

    protected virtual void DetectRay(int nearest, Vector2 nearestblock)
    {
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
    }


    //run all
    protected virtual void RayHItFind(RaycastHit2D[] raycasthits, Vector2 nearestblock, int nearest)
    {
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
    }

    protected virtual void LInkTheBlocks(int nearest, GameObject linkedblock, Vector2 nearestblock)

    {
        if (nearest != -1) //-1 : not found
        {
            print("nearest : " + nearest);
            gameObject.transform.position = nearestblock; //snap 
                                                          //wid,hgt < localscale . all blocks same sized

            linkedblock = raycasthits[nearest].transform.gameObject;//nearest link
            fourdirbound[nearest] = linkedblock.GetComponent<DragNDrop>();//dir->link the block in direction



            switch (nearest)
            {
                case 0: //up
                    gameObject.transform.position = new Vector2(nearestblock.x, nearestblock.y - hgt);
                    fourdirbound[0].fourdirbound[2] = gameObject.GetComponent<DragNDrop>();

                    print(fourdirbound[nearest].name + " is linked to " + fourdirbound[nearest].fourdirbound[2]);
                    break;
                case 1://right
                    gameObject.transform.position = new Vector2(nearestblock.x - wid, nearestblock.y);
                    fourdirbound[1].fourdirbound[3] = gameObject.GetComponent<DragNDrop>();

                    print(fourdirbound[nearest].name + " is linked to " + fourdirbound[nearest].fourdirbound[3]);
                    break;
                case 2://down
                    gameObject.transform.position = new Vector2(nearestblock.x, nearestblock.y + hgt);
                    fourdirbound[2].fourdirbound[0] = gameObject.GetComponent<DragNDrop>();

                    print(fourdirbound[nearest].name + " is linked to " + fourdirbound[nearest].fourdirbound[0]);
                    break;
                case 3://left
                    gameObject.transform.position = new Vector2(nearestblock.x + wid, nearestblock.y);
                    fourdirbound[3].fourdirbound[1] = gameObject.GetComponent<DragNDrop>();

                    print(fourdirbound[nearest].name + " is linked to " + fourdirbound[nearest].fourdirbound[1]);
                    break;

            }




        }
        else //no nearest block:
        {
            linkedblock = null;

            if (fourdirbound[0] != null)
            {
                fourdirbound[0].fourdirbound[2] = null;
                fourdirbound[0] = null;
            }

            if (fourdirbound[1] != null)
            {
                fourdirbound[1].fourdirbound[3] = null;
                fourdirbound[1] = null;
            }

            if (fourdirbound[2] != null)
            {
                fourdirbound[2].fourdirbound[0] = null;
                fourdirbound[2] = null;
            }

            if (fourdirbound[3] != null)
            {
                fourdirbound[3].fourdirbound[1] = null;
                fourdirbound[3] = null;
            }



            //for (int i = 0; i < 4; i++)
            //  fourdirbound[i] = null;

        }

        //yay we can now snap
    }


    //four direction bound

}
