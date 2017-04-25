using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Public : MonoBehaviour {
    
    public static World World { get { return GameObject.Find("WorldPanel").GetComponent<World>(); } }
    public static LevelInfo level { get { return World.currentLevel; } }
    public static SpriteInfo SpriteInfo {  get { return Camera.main.GetComponent<SpriteInfo>(); } }

}
