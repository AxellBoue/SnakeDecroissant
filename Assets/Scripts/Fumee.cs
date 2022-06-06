using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fumee : MonoBehaviour
{
    public Sprite[] sprites;

    float lifeTime = 1.8f;
    float startFade = 0.8f;
    float t = 0;
    float alphaFade = 1;
    Vector3 direction = new Vector3(-1, 1, 0);
    float vitesse = 0.02f;
    SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Length - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * vitesse;
        t += Time.deltaTime;
        if (t > startFade)
        {
            alphaFade = Mathf.Clamp(alphaFade - Time.deltaTime, 0, 1);
            sr.color = new Color(1, 1, 1, alphaFade);
        }
        if (t > lifeTime)
        {
            Destroy(gameObject);
        }
    }

}
