using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    private float wid, hgt;
    private int lineSize = 2;
    internal DragNDrop[] fourdirbound = new DragNDrop[4]; // 0up 1right 2down 3left
    string blocktype; // start, end

    private bool dragging = false;
    private RaycastHit2D[] raycasthits = new RaycastHit2D[4];
    private Vector2[] howfar = new Vector2[4];

    private void Start()
    {
        wid = gameObject.transform.localScale.x;
        hgt = gameObject.transform.localScale.y;
    }

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
        FindAndPrintClosestObject();
    }

    protected virtual void FindAndPrintClosestObject()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        raycasthits[1] = Physics2D.Raycast(transform.position, transform.right, lineSize);
        raycasthits[3] = Physics2D.Raycast(transform.position, transform.right * -1, lineSize);
        raycasthits[0] = Physics2D.Raycast(transform.position, transform.up, lineSize);
        raycasthits[2] = Physics2D.Raycast(transform.position, transform.up * -1, lineSize);

        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        Vector2 nearestblock = new Vector2(0, 0);
        int nearest = -1;

        DetectRay(ref nearest, ref nearestblock);
        RayHItFind(raycasthits, ref nearestblock, ref nearest);

        GameObject linkedblock;
        LInkTheBlocks(nearest, out linkedblock, nearestblock);
    }

    protected virtual void DetectRay(ref int nearest, ref Vector2 nearestblock)
    {
        for (int i = 0; i < raycasthits.Length; i++)
        {
            if (raycasthits[i].collider != null)
            {
                print(i);
                howfar[i] = raycasthits[i].transform.position - gameObject.transform.position;
                howfar[i] = new Vector2(Mathf.Abs(howfar[i].x), Mathf.Abs(howfar[i].y));
                nearestblock = raycasthits[i].transform.position;
                nearest = i;
            }
        }
    }

    protected virtual void RayHItFind(RaycastHit2D[] raycasthits, ref Vector2 nearestblock, ref int nearest)
    {
        for (int i = 0; i < raycasthits.Length; i++)
        {
            if (raycasthits[i].collider != null)
            {
                if (howfar[nearest].magnitude >= howfar[i].magnitude)
                {
                    nearestblock = raycasthits[i].transform.position;
                    nearest = i;
                }
            }
        }
    }

    protected virtual void LInkTheBlocks(int nearest, out GameObject linkedblock, Vector2 nearestblock)
    {
        linkedblock = null;

        if (nearest != -1)
        {
            print("nearest : " + nearest);
            gameObject.transform.position = nearestblock;
            linkedblock = raycasthits[nearest].transform.gameObject;
            fourdirbound[nearest] = linkedblock.GetComponent<DragNDrop>();

            switch (nearest)
            {
                case 0:
                    gameObject.transform.position = new Vector2(nearestblock.x, nearestblock.y - hgt);
                    fourdirbound[0].fourdirbound[2] = this;
                    print(fourdirbound[nearest].name + " is linked to " + fourdirbound[nearest].fourdirbound[2]);
                    break;
                case 1:
                    gameObject.transform.position = new Vector2(nearestblock.x - wid, nearestblock.y);
                    fourdirbound[1].fourdirbound[3] = this;
                    print(fourdirbound[nearest].name + " is linked to " + fourdirbound[nearest].fourdirbound[3]);
                    break;
                case 2:
                    gameObject.transform.position = new Vector2(nearestblock.x, nearestblock.y + hgt);
                    fourdirbound[2].fourdirbound[0] = this;
                    print(fourdirbound[nearest].name + " is linked to " + fourdirbound[nearest].fourdirbound[0]);
                    break;
                case 3:
                    gameObject.transform.position = new Vector2(nearestblock.x + wid, nearestblock.y);
                    fourdirbound[3].fourdirbound[1] = this;
                    print(fourdirbound[nearest].name + " is linked to " + fourdirbound[nearest].fourdirbound[1]);
                    break;
            }
        }
        else
        {
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
        }
    }
}