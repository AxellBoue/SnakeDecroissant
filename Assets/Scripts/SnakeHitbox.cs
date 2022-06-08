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
        snake.hitboxScript = this;
        camOldPos = cam.transform.position;
        camOldSize = cam.orthographicSize;
        inMilit = false;
        target = new Vector3(this.transform.position.x, this.transform.position.y, cam.transform.position.z);
        cam.GetComponent<FollowTarget>().target = this.transform;

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
            inMilit = true;
            snake.gridMoveTimerMax += 0.1f;
            snake.decroisTimerMax = 1f;
            GameAssets.instance.ClignotteMilit();


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
            inMilit = false;
            GameAssets.instance.StopCligno();

        }


    }

    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Militant")
        {
            if(snake.gridMoveTimerMax > 0.05f)
            {
                snake.gridMoveTimerMax += 0.001f;

            }
            //target = collision.transform;
            //target = new Vector3(collision.transform.position.x, collision.transform.position.y, cam.transform.position.z);

            //StartCoroutine("FocusCamera");

        }
      
    }
    public void ZoomCamera()
    {

        if (cam.orthographicSize >= 40f)
        {
            cam.gameObject.GetComponent<FollowTarget>().startZoom(-0.5f);  
           // cam.orthographicSize -= 0.5f;
        }

    }

    IEnumerator FocusCamera()
    {
        //Debug.Log("Fin !");
        yield return new WaitForSeconds(3f);

        if(inMilit)
        {
            

            //cam.transform.position = Vector3.MoveTowards(cam.transform.position, target, 0.5f);
            //cam.transform.position = target;

            /*if (cam.orthographicSize >= 40f)
            {
                cam.orthographicSize--;
            }*/



        }
    }

    IEnumerator UnfocusCamera()
    {
        //Debug.Log("Fin !");
        yield return new WaitForSeconds(1f);

        if(!inMilit)
        {
            /*if (cam.orthographicSize <= 60f)
            {
                cam.orthographicSize += 0.1f;
            }*/

            if (cam.transform.position != camOldPos)
            {
                //cam.transform.position = Vector3.MoveTowards(cam.transform.position, camOldPos, 0.1f);
                
            }

            

        }

    }
}
