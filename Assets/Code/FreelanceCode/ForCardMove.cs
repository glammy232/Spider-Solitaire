/*using UnityEngine;
using Assets.Code;
using System.Threading;
using System.Collections.Generic;

public class ForCardMove : MonoBehaviour
{
    public GameManager GameManager;
    public int i;
    public float jX;
    public float zY;
    public bool ReadyToMove = false;
    public Vector2 Target;

    public void FixedUpdate()
    {
        Go();
    }
    public void CiklFor()
    {
        ReadyToMove = false;
        if (ReadyToMove == false)
        {
            if (i < 54)
            {
                if ((i - zY) / 6 == 1 | (i - zY) / 6 == 2 | (i - zY) / 6 == 3 | i == 0 | i == 1 | i == 2 | i == 3 | i == 4 | i == 5)
                {
                    Target = new Vector2(jX * 1.1f, zY * -0.2f);
                    print(i);
                    ReadyToMove = true;
                    jX++;
                    i += 6;
                }
                else if (i == 49 | i == 50 | i == 51 | i == 52 | i == 53)
                {
                    Target = new Vector2(jX * 1.1f, zY * -0.2f);
                    print(i);
                    ReadyToMove = true;
                    i -= 48;
                    jX = 0;
                    zY++;
                }
                else
                {
                    Target = new Vector2(jX * 1.1f, zY * -0.2f);
                    print(i);
                    ReadyToMove = true;
                    i += 5;
                    jX++;
                }
            }
        }
    }
    public void Go()
    {
        if (ReadyToMove == true)
        {
            if (i == 13)
            {
                CiklFor();
            }
            if (GameManager.Cards[i].transform.position.x != Target.x && GameManager.Cards[i].transform.position.y != Target.y)
            {
                GameManager.Cards[i].transform.position = Vector2.MoveTowards(GameManager.Cards[i].transform.position, Target, 50f * Time.fixedDeltaTime);
            }
            else
            {
                CiklFor();
            }
        }
    }
    public void Start()
    {
        Time.timeScale = 1f;
        jX = 0;
        i = 0;
        CiklFor();
    }
}
*/