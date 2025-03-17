using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerRankAsset", menuName = "Scriptable Objects/PlayerRankAsset")]
public class PlayerRankAsset : ScriptableObject
{
    public List<Sprite> playerRankImageList;
}
