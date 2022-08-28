using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chiffres : MonoBehaviour
{
    float lifetime = 3;
    SpriteRenderer sr;

    float alpha = 1.3f;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0) 
        { 
            Destroy(this.gameObject);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
            alpha = alpha - Time.deltaTime/(lifetime*1.3f);
            sr.color = new Color(1, 1, 1, Mathf.Clamp01(alpha));
        }
    }
}
