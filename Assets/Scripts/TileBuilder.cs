﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TileBuilder : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject parentObject;

    public GameObject[,] tilesInLevel;

    public Dictionary<Direction, TileState[,]> currentLevelStates = new Dictionary<Direction, TileState[,]>();

    public TextAsset[] levelAssetArray;

    public float desiredTiles = 7;
    public float desiredScale = 10;

    public void Start()
    {
        if(DataHandler.player != null)
        {

            DataHandler.player.onChangedDirectionsExposed += CalculateVision;
        }
        else
        {
            StartCoroutine(SubscribeFOV());
        }
        
        DataHandler.statesOfLevel = currentLevelStates;
        DataHandler.numberOfTilesInLevel = (int)desiredTiles;
        tilesInLevel = new GameObject[(int)desiredTiles, (int)desiredTiles];

        
        //parentObject.name = "CurrentLevel";


        for (int j = 0; j < desiredTiles; j++)
        {
            for (int i = 0; i < desiredTiles; i++)
            {
                //tilesInLevel[i,j] = Instantiate(tilePrefab, new Vector3((float)(j * desiredScale) - ((desiredTiles/2) * desiredScale) + desiredScale/2 , (float)(i * desiredScale) - ((desiredTiles/2) * desiredScale) + desiredScale / 2), Quaternion.identity, parentObject.transform);
                tilesInLevel[i, j] = Instantiate(tilePrefab, parentObject.transform);
                tilesInLevel[i, j].transform.localScale = new Vector3(1, 1, 1);
                tilesInLevel[i, j].AddComponent<Tile>().positionInLevel = new Vector2(j, i);
                DataHandler.tilesInLevel[i, j] = tilesInLevel[i, j];
            }
        }

        LoadLevel(0);
        
        //update tile states by calling event
        //DataHandler.player.onChangedDirections(Directions.Up);

    }
    public void LoadLevel(int levelNumber)
    {
        currentLevelStates = LevelLoader.LoadLevel(levelAssetArray[levelNumber]);
        DataHandler.statesOfLevel = currentLevelStates;
    }

    public void CalculateVision(PlayerScript viewer, Direction direction)
    {
        Debug.Log("Calculating");
        return;
        foreach (GameObject tileObject in DataHandler.tilesInLevel)
        {
            tileObject.GetComponent<Tile>().Visible = false;
        }
        if (direction == Direction.Up)
        {
            for (int y = (int)viewer.positionInLevel.y; y < DataHandler.tilesInLevel.GetLength(1); y++)
            {
                for (int x = (int)viewer.positionInLevel.x - (Mathf.Abs(y) - (int)viewer.positionInLevel.y); x < (int)viewer.positionInLevel.x + (Mathf.Abs(y) - (int)viewer.positionInLevel.y) + 1; x++)
                {
                    if (y >= 0 && y < DataHandler.tilesInLevel.GetLength(0) && x >= 0 && x < DataHandler.tilesInLevel.GetLength(1))
                        DataHandler.tilesInLevel[y, x].GetComponent<Tile>().Visible = true;
                }
            }
        }
        if (direction == Direction.Down)
        {
            for (int y = (int)viewer.positionInLevel.y; y >= 0; y--)
            {
                for (int x = (int)viewer.positionInLevel.x - (Mathf.Abs(y - (int)viewer.positionInLevel.y)); x < (int)viewer.positionInLevel.x + (Mathf.Abs(y - (int)viewer.positionInLevel.y) + 1); x++)
                {
                    if (y >= 0 && y < DataHandler.tilesInLevel.GetLength(0) && x >= 0 && x < DataHandler.tilesInLevel.GetLength(1))
                        DataHandler.tilesInLevel[y, x].GetComponent<Tile>().Visible = true;
                }
            }
        }

        if (direction == Direction.Right)
        {
            for (int x = (int)viewer.positionInLevel.x; x < DataHandler.tilesInLevel.GetLength(0); x++)
            {
                for (int y = (int)viewer.positionInLevel.y - (Mathf.Abs(x) - (int)viewer.positionInLevel.x); y < (int)viewer.positionInLevel.y + (Mathf.Abs(x) - (int)viewer.positionInLevel.x) + 1; y++)
                {
                    if (y >= 0 && y < DataHandler.tilesInLevel.GetLength(0) && x >= 0 && x < DataHandler.tilesInLevel.GetLength(1))
                        DataHandler.tilesInLevel[y, x].GetComponent<Tile>().Visible = true;
                }
            }
        }
        if (direction == Direction.Left)
        {
            for (int x = (int)viewer.positionInLevel.x; x >= 0; x--)
            {
                for (int y = (int)viewer.positionInLevel.y - (Mathf.Abs(x - (int)viewer.positionInLevel.x)); y < (int)viewer.positionInLevel.y + (Mathf.Abs(x - (int)viewer.positionInLevel.x) + 1); y++)
                {
                    if (y >= 0 && y < DataHandler.tilesInLevel.GetLength(0) && x >= 0 && x < DataHandler.tilesInLevel.GetLength(1))
                        DataHandler.tilesInLevel[y, x].GetComponent<Tile>().Visible = true;
                }
            }
        }

    }

    public IEnumerator SubscribeFOV()
    {
        yield return new WaitForEndOfFrame();
        DataHandler.player.onChangedDirectionsExposed += CalculateVision;

    }
}
