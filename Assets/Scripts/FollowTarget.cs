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
    // pour smooth le mouvement
    private Vector3 targetPos;
    private Vector3 direction;
    public float vitesseNormale = 0.1f;
    public float vitesseInZoom = 0.1f;
    private float vitesse;

    // zoom décroissance
    private bool isZoom = false;
    private float zoomDestination;
    public float vitesseZoom = 5f;
    public float distanceBouge = 0.2f;
    // zoom final
    private bool isZoomFinal = false;
    private float zoomDeLaFin;
    private float zoomDepart;
    private float compteur = 0;
    public float vitesseZoomFinal = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        bounds = GameObject.Find("CameraBounds").GetComponent<BoxCollider2D>();
        cam = GetComponent<Camera>();
        zoomDestination = cam.orthographicSize;
        vitesse = vitesseNormale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isZoom)
        {
            if(cam.orthographicSize > zoomDestination)
            {
                cam.orthographicSize -= Time.deltaTime * vitesseZoom;
            }
            else
            {
                isZoom = false;
                vitesse = vitesseNormale;
            }
        }
        if (isZoomFinal)
        {
            compteur += Time.deltaTime * vitesseZoomFinal;
            cam.orthographicSize = Mathf.Lerp(zoomDepart, zoomDeLaFin,compteur);
            if (compteur >= 1)
            {
                isZoomFinal = false;
            }
        }
        cameraSize = new Vector2(cam.orthographicSize * Screen.width / Screen.height, cam.orthographicSize);
        targetPos = new Vector3(Mathf.Clamp(target.position.x, bounds.bounds.min.x + cameraSize.x, bounds.bounds.max.x - cameraSize.x), Mathf.Clamp(target.position.y, bounds.bounds.min.y + cameraSize.y, bounds.bounds.max.y - cameraSize.y), transform.position.z);
        print(targetPos);
        if (Vector3.Distance(transform.position, targetPos) > distanceBouge)
        {
            direction = (targetPos - transform.position).normalized;
            transform.position += direction * vitesse;
            print("moove");
        }
        //transform.position = new Vector3(Mathf.Clamp(target.position.x, bounds.bounds.min.x + cameraSize.x, bounds.bounds.max.x - cameraSize.x), Mathf.Clamp(target.position.y, bounds.bounds.min.y + cameraSize.y, bounds.bounds.max.y - cameraSize.y), transform.position.z);

    }


    public void startZoom(float zoomToAdd)
    {
        zoomDestination += zoomToAdd;
        isZoom = true;
        vitesse = vitesseInZoom;
    }

    public void zoomFinal()
    {
        zoomDepart = cam.orthographicSize;
        zoomDeLaFin = zoomDepart - 8;
        distanceBouge /= 2;
        isZoomFinal = true;  
    }
}