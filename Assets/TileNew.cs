﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileNew : MonoBehaviour {

    public Vector2 coords;
    public TileInfo info;

    private bool visible;

    public bool Visible
    {
        get
        {
            return visible;
        }
        set
        {
            visible = value;
            if (!visible)
            {
                GetComponent<Image>().color = Color.black;
            }
            else
            {
                GetComponent<Image>().color = Color.white;
            }
        }
    }

    private Direction _currentDir;
    public Direction currentDir
    {
        get { return _currentDir; }
        set
        {
            _currentDir = value;
            sprite = info.FromDirection(value).sprite;
            //DEBUG
            //GetComponentInChildren<UnityEngine.UI.Text>().text = info.FromDirection(value).neighbourString.Insert(6,"\n").Insert(3,"\n").Insert(6,"</b></color>").Insert(5, "<color=#000000><b>");
        }

    }

    //shortcut for playerscript
    public TileState currentState
    {
        get
        {
            return info.FromDirection(currentDir).state;
        }
    }
    

    public Sprite sprite
    {
        get
        {
            return GetComponent<UnityEngine.UI.Image>().sprite;
        }
        set
        {
            GetComponent<UnityEngine.UI.Image>().sprite = value;
        }
    }

    
    public string getNeighbourString(Direction dir)
    {
        string res = "";

        for (int y = (int)coords.y - 1; y <= (int)coords.y + 1; y++)
        {
            for (int x = (int)coords.x - 1; x <= (int)coords.x + 1; x++)
            {

                //check if outside grid
                if (y < 0 || x < 0 || y >= Public.level.states.GetLength(1) || x >= Public.level.states.GetLength(2))
                { res += "e"; continue; }

                TileState ts = Public.level.states[(int)dir,x, y];
                if (ts == TileState.Path)
                { res += "p"; continue; }

                if (ts == TileState.Wall)
                { res += "w"; continue; }

                if (ts == TileState.Bridge)
                { res += "b"; continue; }
            }
        }


        return res;
    }

}
public class TileInfo
{
    public class DirectionInfo
    {
        public TileState state;
        public string neighbourString;
        public Sprite sprite;
    }
    public DirectionInfo Up;
    public DirectionInfo Down;
    public DirectionInfo Left;
    public DirectionInfo Right;

    public TileInfo()
    {
        Up = new DirectionInfo();
        Down = new DirectionInfo();
        Left = new DirectionInfo();
        Right = new DirectionInfo();
    }


    public DirectionInfo FromDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                return Up;
            case Direction.Down:
                return Down;
            case Direction.Left:
                return Left;
            case Direction.Right:
                return Right;
            default:
                return null;
        }
    }
    public static TileInfo FromString(string s)
    {
        if (s.Length < 4) return null;
        TileInfo info = new TileInfo();
        info.Up.state = (TileState)int.Parse(s[0].ToString());
        info.Down.state = (TileState)int.Parse(s[1].ToString());
        info.Left.state = (TileState)int.Parse(s[2].ToString());
        info.Right.state = (TileState)int.Parse(s[3].ToString());
        return info;
    }
}