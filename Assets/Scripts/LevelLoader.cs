
using System.Collections.Generic;
using UnityEngine;

public static class LevelLoader
{
    public static Dictionary<Direction, TileState[,]> LoadLevel(TextAsset levelFile)
    {
        Dictionary<Direction, TileState[,]> levelDictionary = new Dictionary<Direction, TileState[,]>();
        levelDictionary.Add(Direction.Up, new TileState[7, 7]);
        levelDictionary.Add(Direction.Down, new TileState[7, 7]);
        levelDictionary.Add(Direction.Left, new TileState[7, 7]);
        levelDictionary.Add(Direction.Right, new TileState[7, 7]);


        string textForLevel = levelFile.text;
        //Debug.Log(textForLevel);

        string[] linesForLevel = textForLevel.Split('\n');

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0 + (i*8); j < 7+(i * 8); j++)
            {
                //Debug.Log(linesForLevel[j]);
                string[] tilesInLine = linesForLevel[j].Split('\t');

                //foreach (string tile in tilesInLine)
                for(int q = 0; q < 7; q++)
                {
                    //Up
                    if(i == 0)
                    {
                        if(levelDictionary[Direction.Up] == null)
                        {
                            levelDictionary[Direction.Up] = new TileState[7, 7];
                        }
                        levelDictionary[Direction.Up][q, (j % 8)/*(int)Mathf.Floor(j / 8)*/] = Tile.Convert(int.Parse(tilesInLine[q]));

                        Debug.Log(7-(j % 8) + " : " + q);
                    }
                    //Down
                    else if(i == 1)
                    {
                        if (levelDictionary[Direction.Down] == null)
                        {
                            levelDictionary[Direction.Down] = new TileState[7, 7];
                        }
                        levelDictionary[Direction.Down][q, (j % 8)] = Tile.Convert(int.Parse(tilesInLine[q]));

                        Debug.Log(7 - (j % 8) + " : " + q);
                    }
                    //Left
                    else if(i == 2)
                    {
                        if (levelDictionary[Direction.Left] == null)
                        {
                            levelDictionary[Direction.Left] = new TileState[7, 7];
                        }
                        levelDictionary[Direction.Left][q, (j % 8)] = Tile.Convert(int.Parse(tilesInLine[q]));

                        Debug.Log(7 - (j % 8) + " : " + q);
                    }
                    //Right
                    else if(i == 3)
                    {
                        if (levelDictionary[Direction.Right] == null)
                        {
                            levelDictionary[Direction.Right] = new TileState[7, 7];
                        }
                        levelDictionary[Direction.Right][q, (j % 8)] = Tile.Convert(int.Parse(tilesInLine[q]));

                        Debug.Log(7 - (j % 8) + " : " + q);
                    }
                }
            }
        }

        return levelDictionary;
    }
}

