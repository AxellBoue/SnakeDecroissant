using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentPerdu : MonoBehaviour
{
    public bool detach;
    public bool marche = false; 
    public Transform target;
    public Vector3 targetPos;
    public FoodManager foodManager;

    // Start is called before the first frame update
    void Start()
    {
        foodManager = FindObjectOfType<FoodManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detach && marche && target && transform.position != target.transform.position)
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
            
        }
        else if (target == null)
        {
            target = foodManager.SetTargetBody();
        }
    }
    public void detacher() {
        detach = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GameObject newDisolve = Instantiate(GameAssets.instance.snakeDissolve, this.transform);
        newDisolve.transform.localPosition = Vector3.zero;
        // change rotations 
        newDisolve.transform.GetChild(1).Rotate(-transform.rotation.eulerAngles);
        //newDisolve.transform.GetChild(0).Rotate(-transform.rotation.eulerAngles);
        StartCoroutine("startMarche");
    }

    IEnumerator startMarche()
    {
        yield return new WaitForSeconds(1f);
        marche = true;
    }

}