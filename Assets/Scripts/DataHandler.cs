using UnityEngine;

public static class DataHandler
{
    public static int numberOfTilesInLevel = 7;
    public static GameObject[,] tilesInLevel = new GameObject[numberOfTilesInLevel, numberOfTilesInLevel];
    public static PlayerScript player;
}

