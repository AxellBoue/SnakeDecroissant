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
    public List<Transform> dangerZones;
    public int indexDangerZones;
    public int dimensionFood=2;

    public LvlGrid(int w, int h)
    {
        this.width = w;
        this.height = h;


    }
    public void Setup(Snake snake,FoodManager foodManager,List<Transform> dangerZones)
    {
        this.snake = snake;
        this.foodManager = foodManager;
        this.dangerZones = dangerZones;
        indexDangerZones = 0;
        foreach (Transform t in dangerZones)
        {
            t.gameObject.SetActive(false);
        }
        //SpawnFood();
       
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

    /*public bool SnakeMoved(Vector2Int snakeGridPos )
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
       
        
    }*/

    
    public bool TestCapitaliste(GameObject item)
    {//Pas utilisée
        switch (item.GetComponent<Food>().type)
        {
            default:
            case Food.ItemType.Capitaliste : return true; 
            case Food.ItemType.GreenWash : return false; 
            case Food.ItemType.Militant : return false; 

        }

    }
    public void SnakeNomed(Vector2Int snakeGridPos)
    {
        for (int i = 0 ; i < foodManager.ItemList.Count; i++)
        {
            if ((foodManager.ItemList[i].transform.position.y <= snakeGridPos.y + dimensionFood && foodManager.ItemList[i].transform.position.y >= snakeGridPos.y - dimensionFood)
            && (foodManager.ItemList[i].transform.position.x <= snakeGridPos.x + dimensionFood && foodManager.ItemList[i].transform.position.x >= snakeGridPos.x - dimensionFood))
            {
                if (foodManager.ItemList[i].GetComponent<Food>().type == Food.ItemType.Capitaliste)
                {
                    dangerZones[indexDangerZones].gameObject.SetActive(true);
                    //dangerZones[indexDangerZones].gameObject.GetComponent<DangerAppear>().Appear();
                    indexDangerZones++;
                    snake.snakeSize = snake.snakeSize+10;
                    snake.CreateSnakeBod();                   
                    snake.decroisTimer = snake.decroisTimerMax;
                }
                else if (foodManager.ItemList[i].GetComponent<Food>().type == Food.ItemType.Militant)
                {
                    dangerZones[indexDangerZones].gameObject.SetActive(true);
                    //dangerZones[indexDangerZones].gameObject.GetComponent<DangerAppear>().Appear();
                    indexDangerZones++;
                    snake.snakeSize = snake.snakeSize + 3;
                    snake.CreateSnakeBod();
                    snake.decroisTimer = snake.decroisTimerMax;
                }
                else if (foodManager.ItemList[i].GetComponent<Food>().type == Food.ItemType.GreenWash)
                {
                    dangerZones[indexDangerZones].gameObject.SetActive(true);
                    //dangerZones[indexDangerZones].gameObject.GetComponent<DangerAppear>().Appear();
                    indexDangerZones++;
                    snake.snakeSize = snake.snakeSize + 5;
                    snake.CreateSnakeBod();
                    snake.decroisTimer = snake.decroisTimerMax;
                }

                Object.Destroy(foodManager.ItemList[i]);
                Debug.Log("Nomed nomed");
                //SpawnFood();
                foodManager.ItemPos.RemoveAt(i);
                foodManager.ItemList.RemoveAt(i);
            }
            else
            {
                //return false;

            }

        }
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
