using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandle : MonoBehaviour
{
    [SerializeField] private Snake snake;
    [SerializeField] private FoodManager foodManager;
    private LvlGrid lvlgrid;
    public List<Transform> dangerZones;
    // Start is called before the first frame update
    void Start()
    {
        /*GameObject snakeHeadGameObject = new GameObject();
        SpriteRenderer snakeSpriteRender = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRender.sprite = GameAssets.instance.snakeHeadSprite;
        snakeHeadGameObject.transform.localScale = new Vector3(4, 4, 1);*/

        lvlgrid = new LvlGrid(160,120);
        snake.Setup(lvlgrid,foodManager);
        lvlgrid.Setup(snake,foodManager,dangerZones);
    }

    // Update is called once per frame
    void Update()
    {
        //lvlgrid.SpawnFood();

    }
}
