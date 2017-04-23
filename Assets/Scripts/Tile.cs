﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //public bool walkable = true;

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
            tileState = value;

            if(value == TileState.Bridge)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (value == TileState.Wall)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if (value == TileState.Walkable)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }

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

            switch (value)
            {
                case Directions.Up:
                    //gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    this.TileState = DataHandler.statesOfLevel[Directions.Up][(int)positionInLevel.x, 6-(int)positionInLevel.y];
                    break;
                case Directions.Down:
                    //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    this.TileState = DataHandler.statesOfLevel[Directions.Down][(int)positionInLevel.x, 6-(int)positionInLevel.y];
                    break;
                case Directions.Left:
                    //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    this.TileState = DataHandler.statesOfLevel[Directions.Left][(int)positionInLevel.x, 6-(int)positionInLevel.y];
                    break;
                case Directions.Right:
                    //gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                    this.TileState = DataHandler.statesOfLevel[Directions.Right][(int)positionInLevel.x, 6-(int)positionInLevel.y];
                    break;
                default:
                    break;
            }

            //DEBUGGING!!!

            //if (walkable)
            //{
            //    gameObject.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            //}
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

    public void MoveTo(int x, int y)
    {
        if(this.TileState == TileState.Walkable)
        {
            if(DataHandler.tilesInLevel[x, y].GetComponent<Tile>().TileState == TileState.Walkable || DataHandler.tilesInLevel[x, y].GetComponent<Tile>().TileState == TileState.Bridge)
            {
                DataHandler.player.CurrentTile = DataHandler.tilesInLevel[x, y].GetComponent<Tile>();
            }
        }
        else if(this.TileState == TileState.Bridge)
        {
            if (DataHandler.tilesInLevel[x, y].GetComponent<Tile>().TileState == TileState.Walkable || DataHandler.tilesInLevel[x, y].GetComponent<Tile>().TileState == TileState.Bridge)
            {
                if (DataHandler.player.PreviousTile.positionInLevel.x - x == 0 || DataHandler.player.PreviousTile.positionInLevel.y - y == 0)
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
                return TileState.Walkable;
            case 2:
                return TileState.Bridge;
            default:
                return TileState.Wall;
        }
    }
}

public enum TileState
{
    Wall, Walkable, Bridge
}
