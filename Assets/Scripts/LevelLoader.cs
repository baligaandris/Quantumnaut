
using System.Collections.Generic;
using UnityEngine;

public static class LevelLoader
{
    public static Dictionary<Directions, TileState[,]> LoadLevel(TextAsset levelFile)
    {
        Dictionary<Directions, TileState[,]> levelDictionary = new Dictionary<Directions, TileState[,]>();

        string textForLevel = levelFile.text;

        string[] linesForLevel = textForLevel.Split('\n');

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0 + (i*8); j < 7+(i * 8); j++)
            {
                string[] tilesInLine = linesForLevel[j].Split('\t');

                foreach (string tile in tilesInLine)
                {
                    //Up
                    if(i == 0)
                    {
                        if(levelDictionary[Directions.Up] == null)
                        {
                            levelDictionary[Directions.Up] = new TileState[7, 7];
                        }
                        levelDictionary[Directions.Up][(j % 8), (int)Mathf.Floor(j / 8)] = Tile.Convert(int.Parse(tile));
                    }
                    //Down
                    else if(i == 1)
                    {
                        if (levelDictionary[Directions.Down] == null)
                        {
                            levelDictionary[Directions.Down] = new TileState[7, 7];
                        }
                        levelDictionary[Directions.Down][(j % 8), (int)Mathf.Floor(j / 8)] = Tile.Convert(int.Parse(tile));
                    }
                    //Left
                    else if(i == 2)
                    {
                        if (levelDictionary[Directions.Left] == null)
                        {
                            levelDictionary[Directions.Left] = new TileState[7, 7];
                        }
                        levelDictionary[Directions.Left][(j % 8), (int)Mathf.Floor(j / 8)] = Tile.Convert(int.Parse(tile));
                    }
                    //Right
                    else if(i == 3)
                    {
                        if (levelDictionary[Directions.Right] == null)
                        {
                            levelDictionary[Directions.Right] = new TileState[7, 7];
                        }
                        levelDictionary[Directions.Right][(j % 8), (int)Mathf.Floor(j / 8)] = Tile.Convert(int.Parse(tile));
                    }
                }
            }
        }

        return levelDictionary;
    }
}

