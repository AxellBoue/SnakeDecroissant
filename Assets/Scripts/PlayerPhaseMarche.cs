using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhaseMarche : MonoBehaviour
{
    Vector2 mouvement;
    float inputX;
    float inputY;
    Rigidbody2D rb;
    float vitesse = 8;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            mouvement = new Vector2(inputX, inputY) * vitesse;
            rb.velocity = mouvement;
        }
    }

    public void stopPlayer()
    {
        canMove = false;
        rb.velocity = Vector3.zero;
    }
}
