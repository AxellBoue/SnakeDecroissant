using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoPersoDissolve : MonoBehaviour
{
    // Start is called before the first frame update

    public float coloAnim = 0;
    private SpriteRenderer srParent;
    private SpriteRenderer sr;
    private float a;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        srParent = transform.parent.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        a = Mathf.Clamp(srParent.color.a + coloAnim, 0, 1);
        sr.color = new Color(1, 1, 1,a );
    }
}
