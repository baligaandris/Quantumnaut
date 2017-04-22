using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TileBuilder : MonoBehaviour
{
    public GameObject tilePrefab;

    public GameObject[,] tilesInLevel;

    public Dictionary<Directions, TileState[,]> currentLevelStates = new Dictionary<Directions, TileState[,]>();

    public TextAsset[] levelAssetArray;

    public float desiredTiles = 7;
    public float desiredScale = 10;

    public void Start()
    {
        DataHandler.statesOfLevel = currentLevelStates;
        DataHandler.numberOfTilesInLevel = (int)desiredTiles;
        tilesInLevel = new GameObject[(int)desiredTiles, (int)desiredTiles];

        GameObject parentObject = Instantiate(new GameObject());
        parentObject.name = "CurrentLevel";


        for (int j = 0; j < desiredTiles; j++)
        {
            for (int i = 0; i < desiredTiles; i++)
            {
                tilesInLevel[i,j] = Instantiate(tilePrefab, new Vector3((float)(j * desiredScale) - ((desiredTiles/2) * desiredScale) + desiredScale/2 , (float)(i * desiredScale) - ((desiredTiles/2) * desiredScale) + desiredScale / 2), Quaternion.identity, parentObject.transform);
                tilesInLevel[i, j].AddComponent<Tile>().positionInLevel = new Vector2(j, i);
                DataHandler.tilesInLevel[i, j] = tilesInLevel[i, j];
            }
        }

        LoadLevel(2);
    }

    public void LoadLevel(int levelNumber)
    {
        currentLevelStates = LevelLoader.LoadLevel(levelAssetArray[levelNumber]);
        DataHandler.statesOfLevel = currentLevelStates;
    }
}
