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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivationMilitantisme()
    {
        zoneMilitante.SetActive(true);
        foodManager.ItemList.Remove(this.gameObject);
        foodManager.ItemList.Add(zoneMilitante);
        Destroy(gameObject);

    }
}
