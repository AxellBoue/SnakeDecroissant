using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentPerdu : MonoBehaviour
{
    public bool detach;
    public Transform target;
    public Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (detach && target && transform.position != target.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.1f);

            if(transform.position == targetPos && target.tag == "GreenWash")
            {
                target.gameObject.GetComponent<GreenWash>().ActivationMilitantisme();
                Destroy(gameObject);
            }

            else if(transform.position == targetPos && target.tag == "Militant")
            {
                Destroy(gameObject);
            }
            else if(transform.position == targetPos && target == null)
            {
                Destroy(gameObject);

            }
        }
    }
    public void detacher() {
        detach = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GameObject newDisolve = Instantiate(GameAssets.instance.snakeDissolve, this.transform);
        newDisolve.transform.localPosition = Vector3.zero;
        // change rotations 
        newDisolve.transform.GetChild(1).Rotate(-transform.rotation.eulerAngles);
        newDisolve.transform.GetChild(2).Rotate(-transform.rotation.eulerAngles);
    }

}