using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTexte : MonoBehaviour
{
    public string[] textes;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PhaseMarcheManager.instance.demarreTexte(textes);
        }
    }
}
