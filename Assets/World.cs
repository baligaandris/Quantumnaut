using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    #region Events

    //public delegate void WorldCreatedArgs();
    //public static event WorldCreatedArgs onWorldCreated;
    
    #endregion



    //tile prefab
    public GameObject tilePrefab;

    //grid size (x by x)
    public int worldSize = 7;

    //all levels
    public TextAsset[] levelFiles;

    public LevelInfo currentLevel;

    public TileNew[,] tileScripts;

	void Start () {
        tileScripts = new TileNew[worldSize, worldSize];
        InstantiateTiles();

        LoadLevelFromFile(levelFiles[4]);



    }

    public void LoadLevelFromFile(TextAsset file)
    {
        //create levelinfo
        currentLevel = LevelInfo.FromFile(file);

        //create tile info and write tilestate
        UpdateTileStates(file);

        //generate neighbour strings and sprites
        UpdateTileInfo(Direction.Up);
        UpdateTileInfo(Direction.Down);
        UpdateTileInfo(Direction.Left);
        UpdateTileInfo(Direction.Right);

        //update direction and sprite
        SwitchDirection(Direction.Up);

        //player sprite needs to be at the bottom of the hierarchy
        //to be rendered above tiles
        PlayerNew p = GetComponentInChildren<PlayerNew>();
        p.transform.SetSiblingIndex(p.transform.parent.childCount - 1);

        //Reset player
        p.ResetPosition();
    }

    public void SwitchDirection(Direction dir)
    {
        foreach (TileNew tile in tileScripts)
        {
            tile.currentDir = dir;
        }
    }

    void InstantiateTiles()
    {
        //Instantiates all game objects in the world
        for (int y = 0; y < worldSize; y++)
        {
            for (int x = 0; x < worldSize; x++)
            {
                GameObject go = Instantiate(tilePrefab, transform, false);
                tileScripts[x, y] = go.GetComponent<TileNew>();
                tileScripts[x, y].coords = new Vector2(x, y);

            }

        }


    }

    #region Level Loading

    //TODO get rid of file parameter
    void UpdateTileStates(TextAsset file)
    {
        string text = file.text.Replace("\r", "").Replace("\n", "").Replace("\t", "");
        int size = worldSize * worldSize; // = 49
        for (int i = 0; i < size; i++)
        {
            string res = "";
            res += text[i];
            res += text[i + size];
            res += text[i + 2 * size];
            res += text[i + 3 * size];

            tileScripts[i % worldSize, i / worldSize].info = TileInfo.FromString(res);
        }

    }
    
    void UpdateTileInfo(Direction dir)
    {
        foreach (TileNew tile in tileScripts)
        {
            string neighbours = tile.getNeighbourString(dir);
            tile.info.FromDirection(dir).neighbourString = neighbours;
            tile.info.FromDirection(dir).sprite = Public.SpriteInfo.FetchSprite(neighbours,dir);
        }
    }

    #endregion


}

public class LevelInfo
{
    public TileState[,,] states;

    public TileState getState(Direction direction, int x, int y)
    {
        return states[(int)direction, x, y];
    }

    public static LevelInfo FromFile(TextAsset file)
    {
        string text = file.text.Replace("\r", "").Replace("\n","").Replace("\t", "");
        LevelInfo info = new LevelInfo();
        info.states = new TileState[4, 7, 7];
        for (int i = 0; i < text.Length; i++)
        {
            TileState p = (TileState)int.Parse(text[i].ToString());
            info.states[i / 49, i % 7, (i / 7) % 7] = (TileState)int.Parse(text[i].ToString());
        }
        return info;
    }
}
