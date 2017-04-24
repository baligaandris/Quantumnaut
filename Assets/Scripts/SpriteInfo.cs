using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;
[System.Serializable]
public class SpriteInfo : MonoBehaviour {
    [System.Serializable]
    public struct TileInfo
    {
        public string name;
        public string pattern;
        public Sprite sprite;
    }

    public List<TileInfo> tilesList = new List<TileInfo>();
    public Sprite defaultSprite;

    public Sprite FetchSprite(string neighbours)
    {
        foreach (TileInfo tile in tilesList)
        {
            if (Regex.IsMatch(neighbours, tile.pattern)) return tile.sprite;
        }
        return defaultSprite;
        
    }
    
}
