using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Militant : MonoBehaviour
{  
    GameObject persos;
    GameObject drapeau;

    public bool isSnakeClose = false;

    float compteur = 0;
    float palierPersos = 1;
    float palierDrapeau = 3;
    bool persoAffiche = false;
    bool drapeauAffiche = false;

    // Start is called before the first frame update
    void Start()
    {
        persos = transform.GetChild(0).gameObject;
        drapeau = transform.GetChild(1).gameObject;
        persos.SetActive(false);
        drapeau.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (isSnakeClose && !drapeauAffiche)
        {
            compteur += 1 * Time.deltaTime;
            if (!persoAffiche && compteur >= palierPersos)
            {
                persos.SetActive(true);
                persoAffiche = true;
                compteur = 0;
            }
            else if (!drapeauAffiche && compteur >= palierDrapeau)
            {
                drapeau.SetActive(true);
                drapeauAffiche = true;
            }
        }
    }
}
