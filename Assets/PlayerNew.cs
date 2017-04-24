using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}
public enum Heading
{
    Horizontal = 0,
    Vertical = 1
}

public struct iVector2
{
    public int x, y;
    public iVector2(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class PlayerNew : MonoBehaviour {


    public iVector2 positionTile;
    public Vector2 positionWorld
    {
        get { return ((RectTransform)transform).anchoredPosition; }
        set { ((RectTransform)transform).anchoredPosition = value; }
    }

    public Heading heading = Heading.Vertical;
    private Direction _currentDir = Direction.Up;
    public Direction currentDir
    {
        get { return _currentDir; }
        set {
            _currentDir = value;
            Public.World.SwitchDirection(value);

        }
    }

    

    public void ResetPosition()
    {
        positionTile = new iVector2(3, 6);
        positionWorld = ((RectTransform)Public.World.tileScripts[positionTile.x,positionTile.y].transform).anchoredPosition;
        heading = Heading.Vertical;
    }


    public void Move(Direction dir)
    {
        //get next tile coords
        int x = positionTile.x, y = positionTile.y;
        switch (dir)
        {
            case Direction.Up:
                y--; break;
            case Direction.Down:
                y++; break;
            case Direction.Left:
                x--; break;
            case Direction.Right:
                x++; break;
        }

        //check if tile exists
        if (x < 0 || y < 0 || x > Public.World.tileScripts.GetLength(0) || y > Public.World.tileScripts.GetLength(1)) return;


        //check if tile is walkable
        //REMEMBER: dir is the movement direction, currentDir is the looking direction
        if (Public.World.tileScripts[x, y].info.FromDirection(currentDir).state == TileState.Wall) return;

        //check for bridge movement conditions
        if (Public.World.tileScripts[positionTile.x, positionTile.y].info.FromDirection(currentDir).state == TileState.Bridge)
        {
            //move is not horizontal
            if (heading == Heading.Horizontal && (dir == Direction.Up || dir == Direction.Down)) return;
            //move is not vertical
            if (heading == Heading.Vertical && (dir == Direction.Left || dir == Direction.Right)) return;
        }


        //move
        positionTile = new iVector2(x, y);
        positionWorld = ((RectTransform)Public.World.tileScripts[x, y].transform).anchoredPosition;

        //update heading
        heading = (dir == Direction.Up || dir == Direction.Down) ? Heading.Vertical : Heading.Horizontal; 


    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) currentDir = Direction.Up;
        if (Input.GetKeyDown(KeyCode.DownArrow)) currentDir = Direction.Down;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) currentDir = Direction.Left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) currentDir = Direction.Right;
        if (Input.GetKeyDown(KeyCode.W)) Move(Direction.Up);
        if (Input.GetKeyDown(KeyCode.S)) Move(Direction.Down);
        if (Input.GetKeyDown(KeyCode.A)) Move(Direction.Left);
        if (Input.GetKeyDown(KeyCode.D)) Move(Direction.Right);
    }
    
	// Update is called once per frame
	void Update () {
        HandleInput();
        positionWorld = ((RectTransform)Public.World.tileScripts[positionTile.x, positionTile.y].transform).anchoredPosition;
	}
}
