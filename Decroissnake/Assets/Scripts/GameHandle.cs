using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandle : MonoBehaviour
{
    [SerializeField] private Snake snake;
    [SerializeField] private FoodManager foodManager;
    private LvlGrid lvlgrid;
    // Start is called before the first frame update
    void Start()
    {
        /*GameObject snakeHeadGameObject = new GameObject();
        SpriteRenderer snakeSpriteRender = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRender.sprite = GameAssets.instance.snakeHeadSprite;
        snakeHeadGameObject.transform.localScale = new Vector3(4, 4, 1);*/

        lvlgrid = new LvlGrid(120,120);
        snake.Setup(lvlgrid);
        lvlgrid.Setup(snake,foodManager);
    }

    // Update is called once per frame
    void Update()
    {
        //lvlgrid.SpawnFood();

    }
}
