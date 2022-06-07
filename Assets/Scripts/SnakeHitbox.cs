using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHitbox : MonoBehaviour
{
    public Snake snake;
    public Camera cam;
    public bool inMilit;
    public Vector3 camOldPos;
    public float camOldSize;
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        camOldPos = cam.transform.position;
        camOldSize = cam.orthographicSize;
        inMilit = false;
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Vector3.MoveTowards(transform.position, snake.transform.position, 1f);

        if(!inMilit)
        {
            StartCoroutine("UnfocusCamera");

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("In");
        if(collision.gameObject.tag == "Militant")
        {
            inMilit = true;
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
            //cam.transform.position = camOldPos;
            //cam.orthographicSize=camOldSize;            
            inMilit = false;
        }


    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(cam.orthographicSize);

        if (collision.gameObject.tag == "Militant")
        {
            if(snake.gridMoveTimerMax > 0.05f)
            {
                snake.gridMoveTimerMax += 0.001f;

            }
            target = new Vector3(collision.transform.position.x, collision.transform.position.y, cam.transform.position.z);
            //Vector3 target = new Vector3(this.transform.position.x, this.transform.position.y, cam.transform.position.z);

            StartCoroutine("FocusCamera");

        }
      
    }

    IEnumerator FocusCamera()
    {
        //Debug.Log("Fin !");
        yield return new WaitForSeconds(3f);

        if(inMilit)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, target, 0.5f);
            //cam.transform.position = target;

            if (cam.transform.position == target && cam.orthographicSize >= 40f)
            {
                cam.orthographicSize--;
            }


        }
    }

    IEnumerator UnfocusCamera()
    {
        //Debug.Log("Fin !");
        yield return new WaitForSeconds(1f);

        if(!inMilit)
        {
            if (cam.orthographicSize <= 60f)
            {
                cam.orthographicSize += 0.1f;
            }

            if (cam.transform.position != camOldPos)
            {
                cam.transform.position = Vector3.MoveTowards(cam.transform.position, camOldPos, 0.1f);

            }

            

        }

    }
}
