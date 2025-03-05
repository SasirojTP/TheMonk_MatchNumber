using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EggAsset", menuName = "Scriptable Objects/EggAsset")]
public class EggAsset : ScriptableObject
{
    public List<Sprite> eggImageList;
}
