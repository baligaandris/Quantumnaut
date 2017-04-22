using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBuilder : MonoBehaviour
{
    public GameObject tilePrefab;

    public GameObject[,] tilesInLevel;
    public float desiredTiles = 7;
    public float desiredScale = 10;

    public void Start()
    {
        tilesInLevel = new GameObject[(int)desiredTiles, (int)desiredTiles];
        
        for (int i = 0; i < desiredTiles; i++)
        {
            for (int j = 0; j < desiredTiles; j++)
            {
                tilesInLevel[i,j] = Instantiate(tilePrefab, new Vector3((float)(j * desiredScale) - ((desiredTiles/2) * desiredScale) + desiredScale/2 , (float)(i * desiredScale) - ((desiredTiles/2) * desiredScale) + desiredScale / 2), Quaternion.identity);
            }
        }
    }
}
