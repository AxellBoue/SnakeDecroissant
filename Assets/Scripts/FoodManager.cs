using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
   
    public List<GameObject> ZoneDanger;
    public List<GameObject> ItemList;
    public List<Vector2Int> ItemPos;
    public GameObject prefab3;
    public GameObject prefab5;
    public GameObject prefab10;
    public GameObject prefabMoins1;

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
        List<GameObject> listTarget = new List<GameObject>();
        int num = -1;
        int prevNum = -1; 

        foreach (GameObject item in ItemList)
        {
            if(item.tag == "GreenWash")
            {
                listTarget.Add(item);
            }

        }

        if(listTarget.Count == 0)
        {
            foreach (GameObject item in ItemList)
            {
                if (item.tag == "Militant")
                {
                    listTarget.Add(item);
                }

            }
        }

        while (num == -1 || num == prevNum)
        {
            num = Random.Range(0, listTarget.Count);
        }
        prevNum = num;
        target = listTarget[num].transform;
        return target;
    }
}
