using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHitbox : MonoBehaviour
{
    public Snake snake;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Vector3.MoveTowards(transform.position, snake.transform.position, 1f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("In");
        if(collision.gameObject.tag == "Militant")
        {
            snake.gridMoveTimerMax += 0.1f;
            snake.decroisTimerMax = 2f;
        }

        if (collision.gameObject.tag == "Danger")
        {
            snake.state = Snake.State.Dead;
            Debug.Log("Game Over !!");
        }


    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Out");

        if (collision.gameObject.tag == "Militant")
        {
            snake.gridMoveTimerMax = 0.1f;
            snake.decroisTimerMax = 10f;

        }


    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Stay");

        if (collision.gameObject.tag == "Militant" && snake.gridMoveTimerMax > 0.05f)
        {
            snake.gridMoveTimerMax += 0.001f;
        }

    }
}
