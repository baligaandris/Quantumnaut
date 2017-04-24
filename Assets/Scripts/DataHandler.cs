using System.Collections.Generic;
using UnityEngine;

public static class DataHandler
{
    public static int numberOfTilesInLevel = 7;
    public static GameObject[,] tilesInLevel = new GameObject[numberOfTilesInLevel, numberOfTilesInLevel];
    public static Dictionary<Direction, TileState[,]> statesOfLevel;
    public static PlayerScript player;
    public static int currentLevel=0;
}

