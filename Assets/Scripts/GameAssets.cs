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

	public void popFumee(string direction,Vector3 partPos)
    {
		if (Random.Range(0, ratioFumee) == 0)
		{
			Vector3 pos;
			int r = Random.Range(0, 1);
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
}
 