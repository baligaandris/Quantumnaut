using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    Up, Down, Left, Right
}

public class PlayerScript : MonoBehaviour
{
    public delegate void ChangeDirectionCallback(Directions direction);

    #region Fields

    public ChangeDirectionCallback onChangedDirections;

    public Vector2 positionInLevel;

    private Tile currentTile;

    private Tile previousTile;

    private Directions currentDirection;

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

    public Directions CurrentDirection
    {
        get
        {
            return currentDirection;
        }
        set
        {
            onChangedDirections(value);
        }
    }

    #endregion

    public void Start()
    {
        CurrentTile = DataHandler.tilesInLevel[0, (int)Mathf.Ceil(DataHandler.numberOfTilesInLevel / 2)].GetComponent<Tile>();

        DataHandler.player = this;

        StartCoroutine(SetInitialDirection(Directions.Up));
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
            MoveIntoTile((int)CurrentTile.positionInLevel.y + 1, (int)CurrentTile.positionInLevel.x);
        }

        #endregion

        #region DirectionInputChecks

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.CurrentDirection = Directions.Up;
            //onChangedDirections(Directions.Up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.CurrentDirection = Directions.Down;
            //onChangedDirections(Directions.Down);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.CurrentDirection = Directions.Left;
            //onChangedDirections(Directions.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.CurrentDirection = Directions.Right;
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

    private IEnumerator SetInitialDirection(Directions directionToSetTo)
    {
        yield return new WaitForEndOfFrame();
        CurrentDirection = Directions.Up;
    }
}
