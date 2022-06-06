using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //Le script se résume à une fonction Load où on entre l'identifiant de la scène voulue
	public void Load(int idScene)
	{
		//Appeler le script Autofade pour faire la transition vers la scène demandée dans la fonction Load
		AutoFade.LoadLevel (idScene,0f,0f,Color.black);
		//Debug.Log ("CA PASSE");
	}

    public void Quit()
    {
		Application.Quit();
    }
}
