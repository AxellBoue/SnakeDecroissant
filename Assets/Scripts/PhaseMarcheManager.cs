using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhaseMarcheManager : MonoBehaviour
{
    public static PhaseMarcheManager instance;

    public GameObject fondTexte;
    private TextMeshProUGUI zoneTexte;
    int compteur = 0;
    bool isDialogueActif = false;
    public GameObject playerPrefab;
    PlayerPhaseMarche player;
    string[] texteEnCours;

    private void Awake()
    {
        instance = this;
        //demarrePhaseMarche(new Vector3(0, 0, 0));
    }

    // Start is called before the first frame update
    void Start()
    {
        //fondTexte = GameObject.Find("fondTexte");
        zoneTexte = fondTexte.GetComponentInChildren<TextMeshProUGUI>();
        fondTexte.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDialogueActif)
        {
            texteSuivant();
        }
    }

    public void demarrePhaseMarche(Vector3 pos)
    {
        GameObject NewPlayer = GameObject.Instantiate(playerPrefab);
        NewPlayer.transform.position = pos;
        player = NewPlayer.GetComponent<PlayerPhaseMarche>();
        // ajouter que ça met les zones de collisions des zones destroy en pas trigger pour pas qu'on puisse aller dessus)
        // activer les zones de coll du texte ? ou elles y sont des le début ?
    }

    public void demarreTexte(string[] dialogueZone)
    {
        compteur = 0;
        texteEnCours = dialogueZone;
        afficherTexte();
        isDialogueActif = true;
        fondTexte.SetActive(true);
        player.stopPlayer();
    }

    void texteSuivant()
    {
        compteur += 1;
        if (compteur < texteEnCours.Length)
        {
            afficherTexte();
        }
        else
        {
            stopTexte();
        }
    }

    void afficherTexte()
    {
        zoneTexte.text = texteEnCours[compteur];
    }

    void stopTexte()
    {
        isDialogueActif = false;
        fondTexte.SetActive(false);
        player.canMove = true;
    }


}
