using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlGrid : MonoBehaviour
{

    private Vector2Int foodGridPos;
    private int width;
    private int height;
    private GameObject food;
    private Snake snake;
    private FoodManager foodManager;


    public LvlGrid(int w, int h)
    {
        this.width = w;
        this.height = h;


    }
    public void Setup(Snake snake,FoodManager foodManager)
    {
        this.snake = snake;
        this.foodManager = foodManager;

        SpawnFood();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

 
    public void SpawnFood()
    {
        do
        {
            foodGridPos = new Vector2Int(Random.Range(-((width / 2) - 1), ((width / 2) - 1)), Random.Range(-((height / 2) - 1), ((height / 2) - 1)));

        }   while (snake.GetFullSnakePosList().IndexOf(foodGridPos) != -1 && foodManager.ItemPos.IndexOf(foodGridPos) != -1);

        food = new GameObject("Food", typeof(SpriteRenderer));
        food.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.foodSprite;
        food.transform.position = new Vector3(foodGridPos.x, foodGridPos.y);
        food.transform.localScale = new Vector3(2, 2, 1);

    }

    public bool SnakeMoved(Vector2Int snakeGridPos )
    {
        if((foodGridPos.y <= snakeGridPos.y+1 && foodGridPos.y >= snakeGridPos.y - 1) 
            && (foodGridPos.x <= snakeGridPos.x + 1 && foodGridPos.x >= snakeGridPos.x - 1) )
        {
            Object.Destroy(food);
            Debug.Log("Nom nom");
            SpawnFood();
            return true;
        }
        else
        {
            return false;

        }
       
        
    }
    public bool SnakeNomed(Vector2Int snakeGridPos)
    {
        
        foreach (Vector2Int itemPos in foodManager.ItemPos)
        {
            if ((itemPos.y <= snakeGridPos.y + 1 && itemPos.y >= snakeGridPos.y - 1)
            && (itemPos.x <= snakeGridPos.x + 1 && itemPos.x >= snakeGridPos.x - 1))
            {
                Object.Destroy(food);
                Debug.Log("Nom nom");
                SpawnFood();
                return true;
            }
            else
            {
                return false;

            }

        }
        return false;
    }
    public void ValidateGridPos(Vector2Int gridPos)
    {
        if(gridPos.x < -width/2 || gridPos.x > width/2 || gridPos.y < -height/2 || gridPos.y > height / 2)
        {
            snake.state = Snake.State.Dead;
            Debug.Log("Out !");
        }


    }

}
