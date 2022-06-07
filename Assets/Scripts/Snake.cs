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
    public float decroisTimer;
    public float decroisTimerMax;
    public FoodManager foodManager;
    public GameObject UIGameOver;
    public GameObject imageWin;
    public GameObject imageLose;
    private SpriteRenderer sr;


    public void Setup(LvlGrid lvlgrid, FoodManager foodManager)
    {
        this.lvlgrid = lvlgrid;
        this.foodManager = foodManager;
    }

    void Awake()
    {

        gridPosition = new Vector2Int(0, 0);
        gridMoveSpeed = 3;
        gridMoveDirection = Direction.Right;
        gridMoveTimerMax = 0.2f;
        gridMoveTimer = gridMoveTimerMax;

        decroisTimerMax = 10f;
        decroisTimer = 0f ;

        UIGameOver.SetActive(false);
        imageLose.SetActive(false);
        imageWin.SetActive(false);

        snakePosList = new List<SnakeMovePosition>();
        snakeBodList = new List<SnakeBodypart>();
        snakeSize = 20;
        for (int i = 0; i < snakeSize; i++)
        {
            SnakeMovePosition snakeMovPos = new SnakeMovePosition(null, gridPosition, gridMoveDirection);
            snakePosList.Insert(0, snakeMovPos);

            CreateSnakeBod();


        }
        sr = GetComponent<SpriteRenderer>();
        state = State.Alive;
    }

    // Update is called once per frame
    void Update()
    {
        if (snakeSize <= 0)
        {
            state = State.Dead;
            imageWin.SetActive(true);

        }

        switch (state)
        {
            case State.Alive:
                OnControl();
                GridMovement();
                Decroissance();
                break;
            case State.Dead:
                StartCoroutine("GameOver");
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
    private void Decroissance()
    {
        decroisTimer += Time.deltaTime;
        if (decroisTimer >= decroisTimerMax)
        {
            //Debug.Log("Oh no");
            decroisTimer -= decroisTimerMax;
            snakeBodList[snakeBodList.Count - 1].detach = true;
            snakeBodList[snakeBodList.Count - 1].bodypart.GetComponent<SegmentPerdu>().detach = true;
            Transform target = foodManager.SetTargetBody();
            snakeBodList[snakeBodList.Count - 1].bodypart.GetComponent<SegmentPerdu>().target = target;
            snakeBodList[snakeBodList.Count - 1].bodypart.GetComponent<SegmentPerdu>().targetPos = target.transform.position;
            snakeBodList.RemoveAt(snakeBodList.Count - 1);
            snakePosList.RemoveAt(snakePosList.Count - 1);
            snakeSize--;
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

            /*bool snakeNomed = lvlgrid.SnakeMoved(gridPosition);

            if (snakeNomed)
            {
                snakeSize++;
                CreateSnakeBod();
            }*/
            lvlgrid.SnakeNomed(gridPosition);

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
            transform.eulerAngles = new Vector3(0, 0, GetAnglefromVector(gridMoveDirectionVector));
            if (gridMoveDirection == Direction.Left)
            {
                sr.flipY = true;
            }
            else
            {
                sr.flipY = false;
            }
   


        }
    }

    public void CreateSnakeBod()
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

    private float GetAnglefromVector(Vector2Int dir)
    {

        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;

    }

    public List<Vector2Int> GetFullSnakePosList()
    {
        List<Vector2Int> gridPosList = new List<Vector2Int>() { gridPosition };
        foreach (SnakeMovePosition snakeMovPos in snakePosList)
        {
            gridPosList.Add(snakeMovPos.GetGridPos());
        }
        return gridPosList;
    }
    IEnumerator GameOver()
    {
        //Debug.Log("Fin !");
        yield return new WaitForSeconds(2f);
        imageLose.SetActive(true);
        UIGameOver.SetActive(true);
    }

    private class SnakeBodypart
    {
        private SnakeMovePosition snakeMovePos;
        private Transform transform;
        public GameObject bodypart;
        public bool detach;
        private int num = -1; 
        static int prevNum = -1; 
        public SnakeBodypart(int bodyIndex) //constructeur
        {
            GameObject bodypart = new GameObject("SnakeBody", typeof(SpriteRenderer), typeof(SegmentPerdu));
            while (num == -1 || num == prevNum) 
            {
                num = Random.Range(0, 10);
            }
            prevNum = num; 
            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite[num];
            bodypart.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            bodypart.transform.localScale = new Vector3(1, 1, 1);
            transform = bodypart.transform;
            this.bodypart = bodypart;
            detach = false;

        }

       
        void Update()
        {
        }
        public void SetSnakeMovePos(SnakeMovePosition snakeMovePos)
        {
            this.snakeMovePos = snakeMovePos;
            transform.position = new Vector3(snakeMovePos.GetGridPos().x, snakeMovePos.GetGridPos().y);

            // definit si c'est un morceau en or ou pas
            int isOr = 0;
            if (num == 6) isOr = 1;


            float angle;
            //Fait pointer les segments dans la direction où le snake avance
            switch (snakeMovePos.GetDirection())
            {
                default:
                case Direction.Up:
                    //Modifie l'angle des segments par rapport à la position du segment précédent, pour créer un angle
                    switch (snakeMovePos.GetPreviousSnakeDirection())
                    {
                        default:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite[num];
                            angle = 0;
                            if (num == 8 || num == 9 || num == 10)
                            {
                                GameAssets.instance.popFumee("vertical", transform.position);
                            }
                            else if (num == 7)
                            {
                                GameAssets.instance.PopPetrole("vertical", transform.position);
                            }
                            break;
                        case Direction.Left:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.AngleGH[isOr];
                            angle = 180;

                            break;

                        case Direction.Right:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.AngleDH[isOr];
                            angle = 0;

                            break;
                    }
                    break;
                case Direction.Down:
                    switch (snakeMovePos.GetPreviousSnakeDirection())
                    {
                        default:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite[num];
                            angle = 180;
                            if (num == 8 || num == 9 || num == 10)
                            {
                                GameAssets.instance.popFumee("vertical", transform.position);
                            }
                            else if (num == 7)
                            {
                                GameAssets.instance.PopPetrole("vertical", transform.position);
                            }
                            break;
                        case Direction.Left:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.AngleGB[isOr];
                            angle = 0;

                            break;

                        case Direction.Right:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.AngleDB[isOr];
                            angle = 180;

                            break;
                    }
                    break;
                case Direction.Left:
                    switch (snakeMovePos.GetPreviousSnakeDirection())
                    {
                        default:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite[num];
                            angle = -90;
                            if (num == 8 || num == 9 || num == 10)
                            {
                                GameAssets.instance.popFumee("horizontal", transform.position);
                            }
                            else if (num == 7)
                            {
                                GameAssets.instance.PopPetrole("horizontal", transform.position);
                            }
                            break;
                        case Direction.Down:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.AngleGB[isOr];
                            angle = 180;

                            break;

                        case Direction.Up:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.AngleGH[isOr];
                            angle = 0;

                            break;
                    }
                    break;
                case Direction.Right:
                    switch (snakeMovePos.GetPreviousSnakeDirection())
                    {
                        default:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite[num];
                            angle = 90;
                            if (num == 8 || num == 9 || num == 10)
                            {
                                GameAssets.instance.popFumee("horizontal", transform.position);
                            }
                            else if (num == 7)
                            {
                                GameAssets.instance.PopPetrole("horizontal", transform.position);
                            }
                            break;
                        case Direction.Down:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.AngleDB[isOr];
                            angle = 0;

                            break;

                        case Direction.Up:
                            bodypart.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.AngleDH[isOr];
                            angle = 180;

                            break;
                    }
                    break;

            }
            transform.eulerAngles = new Vector3(0, 0, angle + 90);
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
