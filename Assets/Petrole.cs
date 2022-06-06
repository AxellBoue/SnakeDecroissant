using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrole : MonoBehaviour
{
    public Sprite[] sprites;

    float lifeTime = 1.8f;
    float startFade = 1.5f;
    float t = 1;
    float vitesseGrow = 0.4f;
    float alphaFade = 1;
    float vitesseFade = 0.6f;

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
        if (t < startFade) { 
        t += Time.deltaTime * vitesseGrow;
        }
        else
        {
            t += Time.deltaTime * vitesseGrow/4;
            alphaFade = Mathf.Clamp(alphaFade - Time.deltaTime * vitesseFade, 0, 1);
            sr.color = new Color(1, 1, 1, alphaFade);
        }
        transform.localScale = new Vector3(t, t, 1);
        if (alphaFade == 0)//(t > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
