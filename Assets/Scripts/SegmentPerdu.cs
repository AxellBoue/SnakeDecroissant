using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentPerdu : MonoBehaviour
{
    public bool detach;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (detach)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.2f);

            if(transform.position == target.transform.position)
            {

            }
        }
    }
}
