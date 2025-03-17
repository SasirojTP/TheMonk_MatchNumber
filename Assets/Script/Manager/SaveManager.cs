using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class LastPlayData
{
    GameManager.GameMode lastPlayGameMode;
    string lastPlayPlayerName;
    int lastPlayScore;

    public LastPlayData(GameManager.GameMode gameMode, string LastPlayPlayerName, int LastPlayScore)
    {
        lastPlayGameMode = gameMode;
        lastPlayPlayerName = LastPlayPlayerName;
        lastPlayScore = LastPlayScore;
    }
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager inst;
    [Header("Logic")]
    public string playerName = "You";
    string keyPrefix = "MatchNum";
    string mode;
    string keyName;
    string keyScore;

    bool isLastPlay = false;
    LastPlayData lastPlayData = new LastPlayData(GameManager.GameMode.Easy, "" , 0);

    Dictionary<int, (string name, int score)> sortDict = new Dictionary<int, (string name, int score)>();
    int saveIndex;
    [SerializeField] int maxSaveDataCount = 5;

    [Header("HallOfFame")]
    [SerializeField] Canvas Canvas_HallOfFame;

    [Header("ScoreData")]
    public List<string> easy_Name_List = new List<string>();
    public List<int> easy_Score_List = new List<int>();

    public List<string> medium_Name_List = new List<string>();
    public List<int> medium_Score_List = new List<int>();

    public List<string> hard_Name_List = new List<string>();
    public List<int> hard_Score_List = new List<int>();

    [Header("DefaultScore")]
    [SerializeField] List<string> defaultScore_Easy_Name_List = new List<string>();
    [SerializeField] List<int> defaultScore_Easy_Score_List = new List<int>();

    [SerializeField] List<string> defaultScore_Medium_Name_List = new List<string>();
    [SerializeField] List<int> defaultScore_Medium_Score_List = new List<int>();

    [SerializeField] List<string> defaultScore_Hard_Name_List = new List<string>();
    [SerializeField] List<int> defaultScore_Hard_Score_List = new List<int>();

    private void Start()
    {
        inst = this;
        //DeleteSaveData();
        LoadSave();
        //CalculateRank("You", 5000);
    }
    public void SpawnCanvas_HallOfFame()
    {
        Canvas canvas_HallOfFame = Instantiate(Canvas_HallOfFame);
        canvas_HallOfFame.GetComponent<UI_HallOfFame>().InitializeUI_HallOfFame();
    }
    public List<string> GetEasy_Name_List()
    {
        return easy_Name_List;
    }
    public List<int> GetEasy_Score_List()
    {
        return easy_Score_List;
    }
    public List<string> GetMedium_Name_List()
    {
        return medium_Name_List;
    }
    public List<int> GetMedium_Score_List()
    {
        return medium_Score_List;
    }
    public List<string> GetHard_Name_List()
    {
        return hard_Name_List;
    }
    public List<int> GetHard_Score_List()
    {
        return hard_Score_List;
    }
    public string GetPlayerName()
    {
        return playerName;
    }
    public void LoadSave()
    {
        easy_Name_List.Clear();
        easy_Score_List.Clear();
        medium_Name_List.Clear();
        medium_Score_List.Clear();
        hard_Name_List.Clear();
        hard_Score_List.Clear();
        for (int modeIndex = 0; modeIndex < 3; modeIndex++)
        {
            switch(modeIndex)
            {
                case 0:
                    mode = "_Easy";
                    break;
                case 1:
                    mode = "_Medium";
                    break;
                case 2:
                    mode = "_Hard";
                    break;
            }
            for (int i = 0; i < 5; i++)
            {
                keyName = keyPrefix + mode + "_Rank" + (i + 1).ToString() + "_Name";
                keyScore = keyPrefix + mode + "_Rank" + (i + 1).ToString() + "_Score";
                //Name
                if (PlayerPrefs.HasKey(keyName))
                {
                    switch (modeIndex)
                    {
                        case 0:
                            easy_Name_List.Add(PlayerPrefs.GetString(keyName));
                            break;
                        case 1:
                            medium_Name_List.Add(PlayerPrefs.GetString(keyName));
                            break;
                        case 2:
                            hard_Name_List.Add(PlayerPrefs.GetString(keyName));
                            break;
                    }
                }
                else
                {
                    print("Dont have key " + keyName);
                    switch (modeIndex)
                    {
                        case 0:
                            easy_Name_List.Add(defaultScore_Easy_Name_List[i]);
                            PlayerPrefs.SetString(keyName, defaultScore_Easy_Name_List[i]);
                            break;
                        case 1:
                            medium_Name_List.Add(defaultScore_Medium_Name_List[i]);
                            PlayerPrefs.SetString(keyName, defaultScore_Medium_Name_List[i]);
                            break;
                        case 2:
                            hard_Name_List.Add(defaultScore_Hard_Name_List[i]);
                            PlayerPrefs.SetString(keyName, defaultScore_Hard_Name_List[i]);
                            break;
                    }
                }
                //Score
                if (PlayerPrefs.HasKey(keyScore))
                {
                    switch (modeIndex)
                    {
                        case 0:
                            easy_Score_List.Add(PlayerPrefs.GetInt(keyScore));
                            break;
                        case 1:
                            medium_Score_List.Add(PlayerPrefs.GetInt(keyScore));
                            break;
                        case 2:
                            hard_Score_List.Add(PlayerPrefs.GetInt(keyScore));
                            break;
                    }
                }
                else
                {
                    print("Dont have key " + keyScore);
                    switch (modeIndex)
                    {
                        case 0:
                            easy_Score_List.Add(defaultScore_Easy_Score_List[i]);
                            PlayerPrefs.SetInt(keyScore, defaultScore_Easy_Score_List[i]);
                            break;
                        case 1:
                            medium_Score_List.Add(defaultScore_Medium_Score_List[i]);
                            PlayerPrefs.SetInt(keyScore, defaultScore_Medium_Score_List[i]);
                            break;
                        case 2:
                            hard_Score_List.Add(defaultScore_Hard_Score_List[i]);
                            PlayerPrefs.SetInt(keyScore, defaultScore_Hard_Score_List[i]);
                            break;
                    }
                }
            }
        }
        PlayerPrefs.Save();
    }
    
    public void CalculateRank(string playerName, int playerScore)
    {
        sortDict.Clear();
        sortDict.Add(-1 , (playerName, playerScore));
        SetLastPlayingData(GameManager.inst.gameMode, playerName, playerScore);
        for (int i = 0; i < easy_Name_List.Count; i++)
        {
            sortDict.Add(i , (easy_Name_List[i], easy_Score_List[i]));
        }
        sortDict.OrderByDescending(p => p.Value.score);

        SaveData();
        LoadSave();
    }

    void SaveData()
    {
        saveIndex = 0;
        switch (GameManager.inst.gameMode)
        {
            case GameManager.GameMode.Easy:
                mode = "_Easy";
                break;
            case GameManager.GameMode.Medium:
                mode = "_Medium";
                break;
            case GameManager.GameMode.Hard:
                mode = "_Hard";
                break;
        }
        foreach (var data in sortDict)
        {
            if(saveIndex < maxSaveDataCount)
            {
                //Debug.Log($"ID: {data.Key}, Name: {data.Value.name}, Score: {data.Value.score}");
                keyName = keyPrefix + mode + "_Rank" + (saveIndex + 1).ToString() + "_Name";
                keyScore = keyPrefix + mode + "_Rank" + (saveIndex + 1).ToString() + "_Score";
                PlayerPrefs.SetString(keyName, data.Value.name);
                PlayerPrefs.SetInt(keyScore, data.Value.score);
                saveIndex++;
            }
        }
    }
    void SetLastPlayingData(GameManager.GameMode gameMode, string playerName, int playerScore)
    {
        isLastPlay = true;
        lastPlayData = new LastPlayData(gameMode, playerName, playerScore);
    }
    
    public bool GetIsLastPlay()
    {
        return isLastPlay;
    }
    public LastPlayData GetLastPlayData()
    {
        return lastPlayData;
    }
    void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();
    }

    #region Ex_Key
    //MatchNum_Easy_Rank1_Name
    //MatchNum_Easy_Rank1_Score

    //MatchNum_Medium_Rank1_Name
    //MatchNum_Medium_Rank1_Score

    //MatchNum_Hard_Rank1_Name
    //MatchNum_Hard_Rank1_Score
    #endregion
}
