using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowTarget : MonoBehaviour
{
    public Transform target;
    private BoxCollider2D bounds;
    private Camera cam;
    Vector2 cameraSize;
    public bool inMove;
    private bool zoom = false;
    private float zoomDeLaFin;
    private float zoomDepart;
    private float compteur = 0;
    public float vitesseZoom = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        bounds = GameObject.Find("CameraBounds").GetComponent<BoxCollider2D>();
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (zoom)
        {
            compteur += Time.deltaTime * vitesseZoom;
            cam.orthographicSize = Mathf.Lerp(zoomDepart, zoomDeLaFin,compteur);
            if (compteur >= 1)
            {
                zoom = false;
            }
        }
        cameraSize = new Vector2(cam.orthographicSize * Screen.width / Screen.height, cam.orthographicSize);
        transform.position = new Vector3(Mathf.Clamp(target.position.x, bounds.bounds.min.x + cameraSize.x, bounds.bounds.max.x - cameraSize.x), Mathf.Clamp(target.position.y, bounds.bounds.min.y + cameraSize.y, bounds.bounds.max.y - cameraSize.y), transform.position.z);
    }

    public void zoomFinal()
    {
        zoomDepart = cam.orthographicSize;
        zoomDeLaFin = zoomDepart - 8;
        zoom = true;
    }
}