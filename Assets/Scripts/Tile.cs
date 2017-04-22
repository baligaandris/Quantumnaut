using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;

    public Vector2 positionInLevel;

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
                    gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    break;
                case Directions.Down:
                    gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case Directions.Left:
                    gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                case Directions.Right:
                    gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
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
}
