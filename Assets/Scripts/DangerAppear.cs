using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerAppear : MonoBehaviour
{
    GameObject[] details;
    GameObject fond;
    PolygonCollider2D collideZone;
    int i = 0;
    float vitesseAppear = 0.2f;

    // Start is called before the first frame update
    void Start() {
        collideZone = GetComponent<PolygonCollider2D>();
        collideZone.enabled = false;
        fond = transform.GetChild(1).gameObject;
        fond.SetActive(false);
        Transform detailsParent = transform.GetChild(0);
        details = new GameObject[detailsParent.childCount];
        for(int i = 0; i <= details.Length-1; i++)
        {
            details[i] = detailsParent.GetChild(i).gameObject;
            details[i].SetActive(false);
        }
    }

    public void Appear()
    {
        StartCoroutine("AppearLater");
    }


    IEnumerator AppearLater()
    {
        yield return new WaitForSeconds(vitesseAppear);
        if (i <= details.Length - 1)
        {
            details[i].SetActive(true);
            i++;
            StartCoroutine("AppearLater");
        }
        else
        {
            fond.SetActive(true);
            collideZone.enabled = true;

        }
    }


}


