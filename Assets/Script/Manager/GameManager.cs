using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    [Header("Prefab")]
    [SerializeField] GameObject GameSlotPrefab;
    [Header("UI")]
    [SerializeField] Canvas Canvas_GamePlay;
    [SerializeField] int minScroll = 0;
    [SerializeField] int gameSlotHeight = 170;
    [SerializeField] int maxScroll;

    [SerializeField] GameObject GRP_GameSlotList;
    RectTransform GRP_GameSlotList_RectTransform;
    [SerializeField] InputGroup GRP_InputGroup_1;

    [SerializeField] TextMeshProUGUI GamePlay_TEXT_Score;
    [SerializeField] Button GamePlay_BT_Pause;

    [Header("Finish")]
    [SerializeField] Canvas Canvas_Finish;
    [SerializeField] TextMeshProUGUI Finish_TEXT_FinishTitle;
    [SerializeField] TextMeshProUGUI Finish_TEXT_Score;
    [SerializeField] Button Finish_BT_Restart;
    [SerializeField] Button Finish_BT_MainMenu;

    [SerializeField] Button Finish_BT_Trophy;
    [SerializeField] Canvas Canvas_ScoreBoard;

    [Header("Pause")]
    [SerializeField] Canvas Canvas_Pause;
    [SerializeField] Button Pause_BT_SetSound;
    [SerializeField] Image Pause_IMG_SetSound;
    [SerializeField] Sprite ICON_Mute;
    [SerializeField] Sprite ICON_UnMute;
    [SerializeField] Button Pause_BT_Back;
    [SerializeField] Button Pause_BT_Restart;
    [SerializeField] Button Pause_BT_MainMenu;

    public enum GameMode { Easy , Normal , Hard};
    [Header("Logic")]
    public GameMode gameMode;
    [SerializeField] int baseScore_Easy;
    [SerializeField] int baseScore_Normal;
    [SerializeField] int baseScore_Hard;
    int round;
    int answerCount;
    [SerializeField] List<string> answerList = new List<string>();
    GameSlot currentGameSlot;
    int score = 0;

    void Start()
    {
        inst = this;
        GRP_GameSlotList_RectTransform = GRP_GameSlotList.GetComponent<RectTransform>();

        Canvas_GamePlay.gameObject.SetActive(false);
        Canvas_Pause.gameObject.SetActive(false);
        Canvas_Finish.gameObject.SetActive(false);
        AddListenerToBT();
        HideUI();
    }

    void AddListenerToBT()
    {
        GamePlay_BT_Pause.onClick.AddListener(OnClickGamePlay_BT_Pause);

        Pause_BT_SetSound.onClick.AddListener(OnClickSetSound);
        Pause_BT_Back.onClick.AddListener(OnClickBTBack);
        Pause_BT_Restart.onClick.AddListener(OnClickBTRestart);
        Pause_BT_MainMenu.onClick.AddListener(OnClickBT_MainMenu);

        Finish_BT_Restart.onClick.AddListener(OnClickBTRestart);
        Finish_BT_MainMenu.onClick.AddListener(OnClickBT_MainMenu);
        Finish_BT_Trophy.onClick.AddListener(OnClickFinish_BT_Trophy);
    }
    void HideUI()
    {
        Canvas_GamePlay.gameObject.SetActive(false);
        Canvas_Finish.gameObject.SetActive(false);
        Canvas_Pause.gameObject.SetActive(false);
    }

    #region GamePlay
    private void Update()
    {
        OnUpdateScroll();
    }

    void OnUpdateScroll()
    {
        if(round > 6)
        {
            if (GRP_GameSlotList_RectTransform.localPosition.y < minScroll)
            {
                ScrollToPosition(minScroll);
            }
            else if(GRP_GameSlotList_RectTransform.localPosition.y > maxScroll)
            {
                ScrollToPosition(maxScroll);
            }
        }
        else
        {
            ScrollToPosition(minScroll);
        }
    }

    public void StartGame(GameMode GameMode)
    {
        gameMode = GameMode;
        SetAnswerCount();
        round = 0;
        GRP_InputGroup_1.InitializeInputGroup();
        ClearGameSlotList();
        RandomAnswer();
        TimeManager.inst.StartTimer();
        GamePlay_TEXT_Score.text = "Score : " + score.ToString();
        Canvas_GamePlay.gameObject.SetActive(true);

    }
    void ContinueGame()
    {
        SetAnswerCount();
        round = 0;
        GRP_InputGroup_1.InitializeInputGroup();
        ClearGameSlotList();
        RandomAnswer();
        Canvas_GamePlay.gameObject.SetActive(true);
    }
    void SetAnswerCount()
    {
        switch (gameMode)
        {
            case GameMode.Easy:
                answerCount = 3;
                break;
            case GameMode.Normal:
                answerCount = 4;
                break;
            case GameMode.Hard:
                answerCount = 5;
                break;
            default:
                answerCount = 3;
                break;
        }
    }

    void ClearGameSlotList()
    {
        foreach(Transform child in GRP_GameSlotList.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void RandomAnswer()
    {
        answerList.Clear();

        switch (gameMode)
        {
            case GameMode.Easy:
                for (int i = 0; i <= answerCount - 1; i++)
                {
                    int randomNumber = 0;
                    while(answerList.Contains(randomNumber.ToString()))
                    {
                        randomNumber = Random.Range(0, answerCount + 1);
                    }
                    answerList.Add(randomNumber.ToString());
                }
                break;
            case GameMode.Normal:
                bool isHaveSameNumber_hard_normal = false;
                for (int i = 0; i <= answerCount - 1; i++)
                {
                    int randomNumber = 0;
                    if (isHaveSameNumber_hard_normal == false)
                    {
                        randomNumber = Random.Range(0, 4);
                        if (answerList.Contains(randomNumber.ToString()))
                        {
                            isHaveSameNumber_hard_normal = true;
                        }
                    }
                    else
                    {
                        while (answerList.Contains(randomNumber.ToString()))
                        {
                            randomNumber = Random.Range(0, 4);
                        }
                    }
                    answerList.Add(randomNumber.ToString());
                }

                break;
            case GameMode.Hard:
                bool isHaveSameNumber_hard = false;
                for (int i = 0; i <= answerCount - 1; i++)
                {
                    int randomNumber = 0;
                    if(isHaveSameNumber_hard == false)
                    {
                        randomNumber = Random.Range(0, 5);
                        if(answerList.Contains(randomNumber.ToString()))
                        {
                            isHaveSameNumber_hard = true;
                        }
                    }
                    else
                    {
                        while (answerList.Contains(randomNumber.ToString()))
                        {
                            randomNumber = Random.Range(0, 5);
                        }
                    }
                    answerList.Add(randomNumber.ToString());
                }
                break;
        }

        string answer = "";
        foreach(string n in answerList)
        {
            answer += n;
        }
        print(answer);
        SpawnGRP_GameSlot();
    }

    public void SpawnGRP_GameSlot()
    {
        round++;
        GameObject prefab = Instantiate(GameSlotPrefab);
        prefab.transform.SetParent(GRP_GameSlotList.transform);
        prefab.GetComponent<RectTransform>().localScale = Vector3.one;
        currentGameSlot = prefab.GetComponent<GameSlot>();
        currentGameSlot.InitializeGameSlot(answerCount, answerList, round);

        if(round > 6)
        {
            SetMaxScroll();
            ScrollToPosition(maxScroll);
        }
    }

    void SetMaxScroll()
    {
        maxScroll = (round - 6) * gameSlotHeight;
    }

    void ScrollToPosition(int positionY)
    {
        GRP_GameSlotList_RectTransform.localPosition = new Vector3(0, positionY, 0);
    }

    public void SendDataToCurrentGameSlot(string input)
    {
        currentGameSlot.GetInput(input);
        ScrollToPosition(maxScroll);
    }

    void CalculateScore()
    {
        //score
        int additionScore = 0;
        switch (gameMode)
        {
            case GameMode.Easy:
                additionScore = baseScore_Easy;
                break;
            case GameMode.Normal:
                additionScore = baseScore_Normal;
                break;
            case GameMode.Hard:
                additionScore = baseScore_Hard;
                break;
        }
        if(round == 1)
        {
            // No Decrease
        }
        else if(round >= 2 && round <= 5)
        {
            additionScore = additionScore * 80 / 100;
        }
        else if (round >= 6 && round <= 10)
        {
            additionScore = additionScore * 50 / 100;
        }
        else
        {
            additionScore = additionScore * 10 / 100;
        }

        score += additionScore;
        GamePlay_TEXT_Score.text = "Score : " + score.ToString();
    }
    public void GameFinish()
    {
        CalculateScore();
        ContinueGame();
        AudioManager.inst.PlayWinSound();
    }

    public void TimeOut()
    {
        Finish_TEXT_FinishTitle.text = "Time Out!!!";
        Finish_TEXT_Score.text = "Score : " + score.ToString();
        Canvas_Finish.gameObject.SetActive(true);
    }
    #endregion

    void OnClickGamePlay_BT_Pause()
    {
        Canvas_Pause.gameObject.SetActive(true);
    }
    void OnClickSetSound()
    {
        bool isMute = AudioManager.inst.ToggleMuteSound();
        if (isMute == true)
            Pause_IMG_SetSound.sprite = ICON_Mute;
        else
            Pause_IMG_SetSound.sprite = ICON_UnMute;
    }
    void OnClickBTBack()
    {
        Canvas_Pause.gameObject.SetActive(false);
    }
    void OnClickBTRestart()
    {
        Canvas_Finish.gameObject.SetActive(false);
        Canvas_Pause.gameObject.SetActive(false);
        ResetScore();
        StartGame(gameMode);
    }
    void OnClickBT_MainMenu()
    {
        Canvas_GamePlay.gameObject.SetActive(false);
        Canvas_Finish.gameObject.SetActive(false);
        Canvas_Pause.gameObject.SetActive(false);
        ResetScore();
        MainMenuManager.inst.GoToMainMenu();
    }
    void OnClickFinish_BT_Trophy()
    {
        Canvas_GamePlay.gameObject.SetActive(false);
        Canvas_Finish.gameObject.SetActive(false);
        Canvas_ScoreBoard.gameObject.SetActive(true);
    }
    void ResetScore()
    {
        score = 0;
    }
}
