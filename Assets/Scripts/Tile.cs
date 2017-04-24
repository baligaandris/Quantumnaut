using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    //public bool walkable = true;

    private bool visible = true;

    public bool Visible
    {
        get
        {
            return visible;
        }
        set
        {
            visible = value;

            if (visible)
            {
                if (TileState == TileState.Bridge)
                {
                    gameObject.GetComponent<Image>().color = Color.red;
                }
                else if (TileState == TileState.Wall)
                {
                    gameObject.GetComponent<Image>().color = Color.white;
                }
                else if (TileState == TileState.Path)
                {
                    gameObject.GetComponent<Image>().color = Color.green;
                }
            }
            else
            {
                gameObject.GetComponent<Image>().color = Color.black;
            }
        }
    }

    public Vector2 positionInLevel;
    private TileState tileState;

    public TileState TileState
    {
        get
        {
            return tileState;
        }

        set
        {
            gameObject.GetComponentInChildren<Text>().text = Neighbours();
            tileState = value;
            if (visible)
            {
                if (value == TileState.Bridge)
                {
                    gameObject.GetComponent<Image>().color = Color.red;
                }
                else if (value == TileState.Wall)
                {
                    gameObject.GetComponent<Image>().color = Color.white;
                }
                else if (value == TileState.Path)
                {
                    gameObject.GetComponent<Image>().color = Color.green;
                }
            }
            else
            {
                gameObject.GetComponent<Image>().color = Color.black;
            }
            UpdateSprite();

        }
    }

    private Directions currentDirectionState;

    public Directions CurrentDirectionState
    {
        get
        {
            return currentDirectionState;
        }
        set
        {
            currentDirectionState = value;

            UpdateTileState();

            //DEBUGGING!!!

            //if (walkable)
            //{
            //    gameObject.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            //}
        }
    }


    public void UpdateTileState()
    {
        switch (currentDirectionState)
        {
            case Directions.Up:
                //gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                this.TileState = DataHandler.statesOfLevel[Directions.Up][(int)positionInLevel.x, 6 - (int)positionInLevel.y];
                break;
            case Directions.Down:
                //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                this.TileState = DataHandler.statesOfLevel[Directions.Down][(int)positionInLevel.x, 6 - (int)positionInLevel.y];
                break;
            case Directions.Left:
                //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                this.TileState = DataHandler.statesOfLevel[Directions.Left][(int)positionInLevel.x, 6 - (int)positionInLevel.y];
                break;
            case Directions.Right:
                //gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                this.TileState = DataHandler.statesOfLevel[Directions.Right][(int)positionInLevel.x, 6 - (int)positionInLevel.y];
                break;
            default:
                break;
        }
    }


    public void Start()
    {
        DataHandler.player.onChangedDirections += OnDirectionsChanged;
        
    }

    public void OnDirectionsChanged(Directions newDirection)
    {
        CurrentDirectionState = newDirection;
    }
    public void UpdateSprite()
    {
        GetComponent<Image>().sprite = Camera.main.GetComponent<SpriteInfo>().FetchSprite(Neighbours());
    }


    public void MoveTo(int x, int y)
    {
        if(this.TileState == TileState.Path)
        {
            if(DataHandler.player.PreviousTile!= null)
            {
               // Debug.Log(DataHandler.player.PreviousTile.positionInLevel.x + "-" + x + "=" + (DataHandler.player.PreviousTile.positionInLevel.x - x));
                //Debug.Log(DataHandler.player.PreviousTile.positionInLevel.y + "-" + y + "=" + (DataHandler.player.PreviousTile.positionInLevel.y - y));
            }
            
            if (DataHandler.tilesInLevel[x, y].GetComponent<Tile>().TileState == TileState.Path || DataHandler.tilesInLevel[x, y].GetComponent<Tile>().TileState == TileState.Bridge)
            {
                DataHandler.player.CurrentTile = DataHandler.tilesInLevel[x, y].GetComponent<Tile>();
            }
        }
        else if(this.TileState == TileState.Bridge)
        {
            if (DataHandler.tilesInLevel[x, y].GetComponent<Tile>().TileState == TileState.Path || DataHandler.tilesInLevel[x, y].GetComponent<Tile>().TileState == TileState.Bridge)
            {
                //Debug.Log(DataHandler.player.PreviousTile.positionInLevel.x + "-" + x + "=" + (DataHandler.player.PreviousTile.positionInLevel.x - x));
                //Debug.Log(DataHandler.player.PreviousTile.positionInLevel.y + "-" + y + "=" + (DataHandler.player.PreviousTile.positionInLevel.y - y));
                //Debug.Log(DataHandler.player.PreviousTile.positionInLevel.y - y);
                if (DataHandler.player.PreviousTile.positionInLevel.x - y == 0 || DataHandler.player.PreviousTile.positionInLevel.y - x == 0)
                {
                    DataHandler.player.CurrentTile = DataHandler.tilesInLevel[x, y].GetComponent<Tile>();
                }
            }
        }
    }

    public static TileState Convert(int identifier)
    {
        switch (identifier)
        {
            case 0:
                return TileState.Wall;
            case 1:
                return TileState.Path;
            case 2:
                return TileState.Bridge;
            default:
                return TileState.Wall;
        }
    }




    public string Neighbours()
    {
        string res = "";

        for(int x = (int)positionInLevel.x - 1; x <= (int)positionInLevel.x + 1; x++)
        {
            for (int y = (int)positionInLevel.y - 1; y <= (int)positionInLevel.y + 1; y++)
            {
                
                //check if outside grid
                if (x < 0 || y < 0 || x >= DataHandler.tilesInLevel.GetLength(0) || y >= DataHandler.tilesInLevel.GetLength(1))
                { res += "e"; continue; }

                TileState ts = DataHandler.tilesInLevel[y,x].GetComponent<Tile>().tileState;
                if (ts == TileState.Path)
                { res += "p"; continue; }

                if (ts == TileState.Wall) 
                { res += "w"; continue; }
            }
        }


        return res;
    }

}

public enum TileState
{
    Wall = 0,
    Path = 1,
    Bridge = 2
}
