using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GRP_PlayerDetail : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] PlayerRankAsset playerRankAsset;
    [Header("UI")]
    [SerializeField] Image IMG_BG_You;
    [SerializeField] Image IMG_PlayerRank;
    [SerializeField] TextMeshProUGUI TEXT_PlayerRank;
    [SerializeField] TextMeshProUGUI TEXT_PlayerName;
    [SerializeField] TextMeshProUGUI TEXT_PlayerScore;

    public void InitializeGRP_PlayerDetail(int playerRank, string playerName, int playerScore, bool isLastPlay)
    {
        SetPlayerRank(playerRank);
        TEXT_PlayerName.text = playerName;
        TEXT_PlayerScore.text = playerScore.ToString();
        SetActiveIMG_BG_You(isLastPlay);
    }
    void SetPlayerRank(int playerRank)
    {
        switch(playerRank)
        {
            case 1:
                IMG_PlayerRank.sprite = playerRankAsset.playerRankImageList[0];
                TEXT_PlayerRank.gameObject.SetActive(false);
                break;
            case 2:
                IMG_PlayerRank.sprite = playerRankAsset.playerRankImageList[1];
                TEXT_PlayerRank.gameObject.SetActive(false);
                break;
            case 3:
                IMG_PlayerRank.sprite = playerRankAsset.playerRankImageList[2];
                TEXT_PlayerRank.gameObject.SetActive(false);
                break;
            default:
                IMG_PlayerRank.gameObject.SetActive(false);
                TEXT_PlayerRank.text = playerRank.ToString();
                break;
        }
    }
    void SetActiveIMG_BG_You(bool isSetActive)
    {
        if(isSetActive == true)
        {
            IMG_BG_You.gameObject.SetActive(true);
        }
        else
        {
            IMG_BG_You.gameObject.SetActive(false);
        }
    }
}
