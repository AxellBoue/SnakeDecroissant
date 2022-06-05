using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
    public enum State
    {
        Alive,
        Dead
    }
    public State state;
    public Vector2Int gridPosition;
    private Direction gridMoveDirection;
    private float gridMoveTimer;
    public float gridMoveTimerMax;
    public int gridMoveSpeed;
    private LvlGrid lvlgrid;
    public int snakeSize;
    private List<SnakeMovePosition> snakePosList;
    private List<SnakeBodypart> snakeBodList;

    public void Setup(LvlGrid lvlgrid)
    {
        this.lvlgrid = lvlgrid;
    }

    void Awake()
    {

        gridPosition = new Vector2Int(0, 0);
        gridMoveSpeed = 2;
        gridMoveDirection = Direction.Right;
        gridMoveTimerMax = 0.1f;
        gridMoveTimer = gridMoveTimerMax;

        snakePosList = new List<SnakeMovePosition>();
        snakeBodList = new List<SnakeBodypart>();
        snakeSize = 8;
        for (int i = 0; i < snakeSize; i++)
        {
            SnakeMovePosition snakeMovPos = new SnakeMovePosition(null,gridPosition, gridMoveDirection);
            snakePosList.Insert(0, snakeMovPos);

            CreateSnakeBod();


        }
        state = State.Alive;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Alive:
                OnControl();
                GridMovement();
                break;
            case State.Dead:
                break;

        }

    }

    private void OnControl()
    {
        //Controles
        if (Input.GetKeyDown(KeyCode.UpArrow) && gridMoveDirection != Direction.Down)
        {


            gridMoveDirection = Direction.Up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && gridMoveDirection != Direction.Up)
        {
            gridMoveDirection = Direction.Down;

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && gridMoveDirection != Direction.Right)
        {
            gridMoveDirection = Direction.Left;

        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && gridMoveDirection != Direction.Left)
        {
            gridMoveDirection = Direction.Right;

        }
    }

    private void GridMovement()
    {

        //Timer du pas 
        gridMoveTimer += Time.deltaTime;

        if (gridMoveTimer >= gridMoveTimerMax)
        {

            gridMoveTimer -= gridMoveTimerMax;
            SnakeMovePosition previousSnakeMovPos = null;
            if (snakePosList.Count > 0)
            {
                previousSnakeMovPos = snakePosList[0];
            }
            SnakeMovePosition snakeMovPos = new SnakeMovePosition(previousSnakeMovPos, gridPosition, gridMoveDirection);
            snakePosList.Insert(0, snakeMovPos);

            Vector2Int gridMoveDirectionVector; //vecteur de direction
            switch (gridMoveDirection)
            {
                default:
                case Direction.Up: gridMoveDirectionVector = new Vector2Int(0, +gridMoveSpeed); break;
                case Direction.Down: gridMoveDirectionVector = new Vector2Int(0, -gridMoveSpeed); break;
                case Direction.Right: gridMoveDirectionVector = new Vector2Int(+gridMoveSpeed, 0); break;
                case Direction.Left: gridMoveDirectionVector = new Vector2Int(-gridMoveSpeed, 0); break;

            }
            gridPosition += gridMoveDirectionVector;

            lvlgrid.ValidateGridPos(gridPosition);

            bool snakeNomed = lvlgrid.SnakeMoved(gridPosition);
            if (snakeNomed)
            {
                snakeSize++;
                CreateSnakeBod();
            }

            if (snakePosList.Count >= snakeSize + 1)
            {
                snakePosList.RemoveAt(snakePosList.Count - 1);

            }


            UpdateSnakeBodyPart();

            foreach (SnakeBodypart snakeBodyPart in snakeBodList) //Vérifier que tête ne touche pas corps, sinon game over
            {
                Vector2Int snakeBodPos = snakeBodyPart.GetGridPosition();
                if (gridPosition == snakeBodPos)
                {
                    Debug.Log("Game Over ! " + transform.position);
                    state = State.Dead;

                }
            }



            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAnglefromVector(gridMoveDirectionVector) - 90);


        }
    }

    private void CreateSnakeBod()
    {

        snakeBodList.Add(new SnakeBodypart(snakeBodList.Count));

    }

    private void UpdateSnakeBodyPart()//associe la liste des données de position à la liste des objets segments
    {
        for (int i = 0; i < snakeBodList.Count; i++)
        {
            snakeBodList[i].SetSnakeMovePos(snakePosList[i]);

        }

    }

    private float GetAnglefromVector(Vector2Int dir) {

        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;

    }

    public List<Vector2Int> GetFullSnakePosList()
    {
        List<Vector2Int> gridPosList = new List<Vector2Int>() { gridPosition };
        foreach(SnakeMovePosition snakeMovPos in snakePosList)
        {
            gridPosList.Add(snakeMovPos.GetGridPos());
        }
        return gridPosList;
    }

    private class SnakeBodypart
    {
        private SnakeMovePosition snakeMovePos;
        private Transform transform;
        private GameObject bodypart;
        public SnakeBodypart(int bodyIndex) //constructeur
        {
            GameObject bodypart = new GameObject("SnakeBody", typeof(SpriteRenderer));
            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite;
            bodypart.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            bodypart.transform.localScale = new Vector3(2, 2, 1);
            transform = bodypart.transform;
            this.bodypart = bodypart;

        }

        public void SetSnakeMovePos(SnakeMovePosition snakeMovePos)
        {
            this.snakeMovePos = snakeMovePos;
            transform.position = new Vector3(snakeMovePos.GetGridPos().x, snakeMovePos.GetGridPos().y);

            float angle;
            //Fait pointer les segments dans la direction où le snake avance
            switch(snakeMovePos.GetDirection())
            {
                default:
                case Direction.Up:
                    //Modifie l'angle des segments par rapport à la position du segment précédent, pour créer un angle
                    switch (snakeMovePos.GetPreviousSnakeDirection())
                    {
                        default:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite;
                            angle = 0;

                            break;
                        case Direction.Left:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.Angle;
                            angle = 0 + 45;

                            break;

                        case Direction.Right:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.Angle;
                            angle = 0 - 45;

                            break;
                    }
                    break;
                case Direction.Down:
                    switch (snakeMovePos.GetPreviousSnakeDirection())
                    {
                        default:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite;
                            angle = 180;

                            break;
                        case Direction.Left:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.Angle;
                            angle = 180 + 45;

                            break;

                        case Direction.Right:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.Angle;
                            angle = 180 - 45;

                            break;
                    }
                    break;
                case Direction.Left:
                    switch (snakeMovePos.GetPreviousSnakeDirection())
                    {
                        default:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite;
                            angle = -90;

                            break;
                        case Direction.Down:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.Angle;
                            angle = -45;

                            break;

                        case Direction.Up:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.Angle;
                            angle = 45;

                            break;
                    }
                    break;
                case Direction.Right:
                    switch(snakeMovePos.GetPreviousSnakeDirection())
                    {
                        default: 
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite;
                            angle = 90;

                            break;
                        case Direction.Down: 
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.Angle;
                            angle = 45;

                            break;

                        case Direction.Up:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.Angle;
                            angle = -45;

                            break;
                    }
                    break;

            }
            transform.eulerAngles = new Vector3(0,0,angle+90);
        }
        
        public Vector2Int GetGridPosition()
        {
            return snakeMovePos.GetGridPos();
        }
    }

    private class SnakeMovePosition //stock la position, direction d'un segment et la position et direction du segment précédent
    {

        private SnakeMovePosition previousSnakePos;
        private Vector2Int gridPosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakePos, Vector2Int gridPosition, Direction direction)
        {
            this.previousSnakePos = previousSnakePos;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        //renvoient position, direction et direction du segment précédent
        public Vector2Int GetGridPos()
        {
            return gridPosition;
        }

        public Direction GetDirection()
        {
            return direction;
        }

        public Direction GetPreviousSnakeDirection() 
        {
            if (previousSnakePos == null)
            {
                return Direction.Up;
            }
            return previousSnakePos.direction;
        }

    }
}
