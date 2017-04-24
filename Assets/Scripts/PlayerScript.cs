using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerScript : MonoBehaviour
{
    public delegate void ChangeDirectionCallback(Direction direction);
    public delegate void ChangeDirectionPublicCallback(PlayerScript player, Direction direction);

    public Text winText;

    #region Fields

    public ChangeDirectionCallback onChangedDirections;
    public ChangeDirectionPublicCallback onChangedDirectionsExposed;

    public Vector2 positionInLevel;

    private Tile currentTile;

    private Tile previousTile;

    private Direction currentDirection = Direction.Up;

    #endregion

    #region Properties

    public Tile CurrentTile
    {
        get
        {
            return this.currentTile;
        }

        set
        {
            PreviousTile = currentTile;
            currentTile = value;

            positionInLevel = currentTile.positionInLevel;

            gameObject.transform.position = currentTile.gameObject.transform.position;

            onChangedDirectionsExposed(this, currentDirection);
        }
    }

    public Tile PreviousTile
    {
        get
        {
            return previousTile;
        }
        set
        {
            previousTile = value;
        }
    }

    public Direction CurrentDirection
    {
        get
        {
            return currentDirection;
        }
        set
        {
            currentDirection = value;

            onChangedDirectionsExposed(this, value);
            onChangedDirections(value);
        }
    }

    #endregion

    public void Start()
    {
        StartCoroutine(SetCurrentTile());

        DataHandler.player = this;

        //StartCoroutine(SetInitialDirection(Directions.Up));
    }

    public void Update()
    {
        #region MovementInputChecks

        if (Input.GetKeyDown(KeyCode.A))
        {
            //CurrentTile = DataHandler.tilesInLevel[(int)CurrentTile.positionInLevel.y, (int)CurrentTile.positionInLevel.x - 1].GetComponent<Tile>();
            MoveIntoTile((int)CurrentTile.positionInLevel.y, (int)CurrentTile.positionInLevel.x - 1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //CurrentTile = DataHandler.tilesInLevel[(int)CurrentTile.positionInLevel.y, (int)CurrentTile.positionInLevel.x + 1].GetComponent<Tile>();
            MoveIntoTile((int)CurrentTile.positionInLevel.y, (int)CurrentTile.positionInLevel.x + 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //CurrentTile = DataHandler.tilesInLevel[(int)CurrentTile.positionInLevel.y - 1, (int)CurrentTile.positionInLevel.x].GetComponent<Tile>();
            MoveIntoTile((int)CurrentTile.positionInLevel.y - 1, (int)CurrentTile.positionInLevel.x);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //CurrentTile = DataHandler.tilesInLevel[(int)CurrentTile.positionInLevel.y + 1, (int)CurrentTile.positionInLevel.x].GetComponent<Tile>();
            if (CurrentTile.positionInLevel == new Vector2(3, 6))
            {
                if (DataHandler.currentLevel < 9)
                {
                    DataHandler.currentLevel += 1;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TileBuilder>().LoadLevel(DataHandler.currentLevel);
                    CurrentTile = DataHandler.tilesInLevel[0, 3].GetComponent<Tile>();
                }
                else {
                    Debug.Log("win");
                    winText.enabled = true;
                }

            }
            else
            {
                MoveIntoTile((int)CurrentTile.positionInLevel.y + 1, (int)CurrentTile.positionInLevel.x);
            }
        }

        #endregion

        #region DirectionInputChecks

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.CurrentDirection = Direction.Up;
            //onChangedDirections(Directions.Up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.CurrentDirection = Direction.Down;
            //onChangedDirections(Directions.Down);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.CurrentDirection = Direction.Left;
            //onChangedDirections(Directions.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.CurrentDirection = Direction.Right;
            //onChangedDirections(Directions.Right);
        }

        #endregion
    }

    private void MoveIntoTile(int x, int y)
    {
        //Chech that the co-ordinates don't exceed the array.
        if(x >= 0 && y >= 0 && x <= DataHandler.numberOfTilesInLevel - 1 && y <= DataHandler.numberOfTilesInLevel - 1)
        {
            CurrentTile.GetComponent<Tile>().MoveTo(x, y);
        }
    }

    private IEnumerator SetCurrentTile()
    {
        yield return new WaitForEndOfFrame();
        CurrentTile = DataHandler.tilesInLevel[0, (int)Mathf.Ceil(DataHandler.numberOfTilesInLevel / 2)].GetComponent<Tile>();
    }

    
}
