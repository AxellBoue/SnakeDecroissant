using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
   
    public List<GameObject> ZoneDanger;
    public List<GameObject> ItemList;
    public List<Vector2Int> ItemPos;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(GameObject item in ItemList)
        {
            Vector2Int itemPos = new Vector2Int((int)item.transform.position.x, (int)item.transform.position.y);
            ItemPos.Add(itemPos);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform SetTargetBody()
    {
        Transform target;
        List<GameObject> listGreenWash = new List<GameObject>();
        foreach (GameObject item in ItemList)
        {
            if(item.tag == "GreenWash")
            {
                listGreenWash.Add(item);
            }

        }
        target = listGreenWash[Random.Range(0, listGreenWash.Count-1)].transform;

        return target;
    }
}
