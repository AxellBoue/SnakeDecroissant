using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
	public static GameAssets instance;

	private void Awake(){

		instance = this;

	}

	public Sprite snakeHeadSprite;
	public Sprite[] snakeBodySprite;
	public Sprite foodSprite;
	public Sprite AngleDB;
	public Sprite AngleDH;
	public Sprite AngleGB;
	public Sprite AngleGH;


}
 