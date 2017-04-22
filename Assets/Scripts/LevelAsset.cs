using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public struct LevelAsset
{
    public TextAsset upLevel;
    public TextAsset downLevel;
    public TextAsset leftLevel;
    public TextAsset rightLevel;
}
