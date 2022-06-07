using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWash : MonoBehaviour
{
    public bool activation;
    public GameObject zoneMilitante;
    public FoodManager foodManager;

    // Start is called before the first frame update
    void Start()
    {
        zoneMilitante.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivationMilitantisme()
    {
        int index;
        Vector2Int posMilitant = new Vector2Int((int)zoneMilitante.transform.position.x, (int)zoneMilitante.transform.position.y);
        zoneMilitante.SetActive(true);
        index = foodManager.ItemList.IndexOf(this.gameObject);
        foodManager.ItemList.RemoveAt(index);
        foodManager.ItemPos.RemoveAt(index);
        foodManager.ItemList.Add(zoneMilitante);
        foodManager.ItemPos.Add(posMilitant);
        Destroy(gameObject);

    }
}
