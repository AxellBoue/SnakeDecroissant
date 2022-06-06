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
	public Sprite[] AngleDB;
	public Sprite[] AngleDH;
	public Sprite[] AngleGB;
	public Sprite[] AngleGH;
	public GameObject FumeePrefab;
	public int ratioFumee = 10;
	public GameObject PetrolePrefab;
	public int ratioPetrole = 10;

	public void popFumee(string direction,Vector3 partPos)
    {
		if (Random.Range(0, ratioFumee) == 0)
		{
			Vector3 pos;
			int r = Random.Range(0, 2);
			float decalage = Random.Range(-0.3f, 0.3f);
			if (direction == "vertical")
			{
				if (r == 0)
				{
					pos = new Vector3(2+ decalage, 0, 0);
				}
				else
				{
					pos = new Vector3(-2+ decalage, 0, 0);
				}
			}
			else
			{
				if (r == 0)
				{
					pos = new Vector3(0, 2+ decalage, 0);
				}
				else
				{
					pos = new Vector3(0, -2+ decalage, 0);
				}
			}
			GameObject newFumee = Instantiate(FumeePrefab);
			newFumee.transform.position = partPos + pos;
			newFumee.transform.parent = this.transform.parent;

		}
	}

	public void PopPetrole(string direction, Vector3 partPos)
    {
		if (Random.Range(0, ratioFumee) == 0)
		{
			Vector3 pos;
			int r = Random.Range(0, 2);
			float decalage = Random.Range(-0.6f, 0.6f);
			if (direction == "vertical")
			{
				if (r == 0)
				{
					pos = new Vector3(1.5f + decalage, 0, 0);
				}
				else
				{
					pos = new Vector3(-1.5f + decalage, 0, 0);
				}
			}
			else
			{
				if (r == 0)
				{
					pos = new Vector3(0, 1.5f + decalage, 0);
				}
				else
				{
					pos = new Vector3(0, -1.5f + decalage, 0);
				}
			}
			GameObject newPetrole = Instantiate(PetrolePrefab);
			newPetrole.transform.position = partPos + pos;
			newPetrole.transform.parent = this.transform.parent;
			newPetrole.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
		}
	}
}
 