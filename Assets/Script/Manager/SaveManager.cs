using UnityEngine;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static SaveManager inst;
    [SerializeField] string playerName = "You";
    string mode;
    string keyName;
    string keyScore;
    [Header("Score")]
    bool isLastPlay = false;
    GameManager.GameMode lastPlayGameMode;
    int lastPlayScore;

    [SerializeField] List<string> easy_Name_List = new List<string>();
    [SerializeField] List<int> easy_Score_List = new List<int>();

    [SerializeField] List<string> medium_Name_List = new List<string>();
    [SerializeField] List<int> medium_Score_List = new List<int>();

    [SerializeField] List<string> hard_Name_List = new List<string>();
    [SerializeField] List<int> hard_Score_List = new List<int>();

    private void Start()
    {
        inst = this;
        //LoadSave();
        //DeleteSave();
    }
    public void LoadSave()
    {
        switch (GameManager.inst.gameMode)
        {
            case GameManager.GameMode.Easy:
                mode = "Easy";
                break;
            case GameManager.GameMode.Medium:
                mode = "Medium";
                break;
            case GameManager.GameMode.Hard:
                mode = "Hard";
                break;
            default:
                mode = "Easy";
                break;
        }
        for (int i = 1; i <= 5; i++)
        {
            keyName = mode + "_Rank" + i.ToString() + "_Name";
            keyScore = mode + "_Rank" + i.ToString() + "_Score";

            if (PlayerPrefs.HasKey(keyName))
            {
                print(PlayerPrefs.GetString(keyName));
            }
            else
            {
                print("Dont have key " + keyName);
            }

            if (PlayerPrefs.HasKey(keyScore))
            {
                print(PlayerPrefs.GetInt(keyScore));
            }
            else
            {
                print("Dont have key " + keyScore);
            }
        }
    }
    void CalculateRank()
    {

    }

    void Save(GameManager.GameMode gameMode, int playerScore)
    {
        PlayerPrefs.SetString("Easy_Rank1_Name","OK");
        PlayerPrefs.SetInt("Easy_Rank1_Score", 1000);
        PlayerPrefs.Save();
    }
    void SetLastPlayingData(GameManager.GameMode gameMode, int playerScore)
    {
        isLastPlay = true;
        lastPlayGameMode = gameMode;
        lastPlayScore = playerScore;
    }
    void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();
    }

    #region Ex_Key
    //Easy_Rank1_Name
    //Easy_Rank1_Score

    //Medium_Rank1_Name
    //Medium_Rank1_Score

    //Hard_Rank1_Name
    //Hard_Rank1_Score
    #endregion
}
