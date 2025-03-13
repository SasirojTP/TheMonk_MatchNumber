using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    [Header("Prefab")]
    [SerializeField] GameObject GameSlotPrefab;
    [SerializeField] GameObject IMG_AnswerEggPrefab;

    [Header("UI")]
    [SerializeField] Canvas Canvas_GamePlay;
    [SerializeField] int minScroll = 0;
    [SerializeField] int gameSlotHeight = 170;
    [SerializeField] int maxScroll;

    [SerializeField] GameObject GRP_AnswerEgg;
    [SerializeField] Image IMG_HideAnswer;
    [SerializeField] Transform POS_HideAnswerPos;

    [SerializeField] GameObject GRP_GameSlotList;
    RectTransform GRP_GameSlotList_RectTransform;
    [SerializeField] InputGroup GRP_InputGroup_1;

    [SerializeField] TextMeshProUGUI GamePlay_TEXT_Score;
    [SerializeField] Button GamePlay_BT_Pause;
    [SerializeField] Image IMG_BlockPressButton;
    [SerializeField] TextMeshProUGUI TEXT_TimeUp;

    [Header("Finish")]
    [SerializeField] Canvas Canvas_Finish;
    [SerializeField] TextMeshProUGUI Finish_TEXT_Score;
    [SerializeField] Button Finish_BT_Restart;
    [SerializeField] Button Finish_BT_MainMenu;

    [SerializeField] Button Finish_BT_Trophy;
    [SerializeField] Canvas Canvas_HallOfFame;

    [Header("Pause")]
    [SerializeField] Canvas Canvas_Pause;
    [SerializeField] Button Pause_BT_SFX;

    [SerializeField] Button Pause_BT_BGM;

    [SerializeField] Sprite ICON_MuteSFX;
    [SerializeField] Sprite ICON_PlaySFX;
    [SerializeField] Sprite ICON_MuteBGM;
    [SerializeField] Sprite ICON_PlayBGM;

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
    List<GameObject> gameSlotList = new List<GameObject>();
    GameSlot currentGameSlot;
    int score = 0;

    void Start()
    {
        inst = this;
        GRP_GameSlotList_RectTransform = GRP_GameSlotList.GetComponent<RectTransform>();

        AddListenerToBT();
        HideUI();
    }
    void AddListenerToBT()
    {
        GamePlay_BT_Pause.onClick.AddListener(OnClickGamePlay_BT_Pause);

        Pause_BT_SFX.onClick.AddListener(OnClickPause_BT_SFX);
        Pause_BT_BGM.onClick.AddListener(OnClickPause_BT_BGM);
        Pause_BT_Back.onClick.AddListener(OnClickPause_BT_Back);
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
        TEXT_TimeUp.transform.localScale = Vector3.zero;
        IMG_BlockPressButton.gameObject.SetActive(false);
    }
    void ContinueGame()
    {
        SetAnswerCount();
        round = 0;
        GRP_InputGroup_1.InitializeInputGroup();
        ClearGameSlotList();
        RandomAnswer();
        Canvas_GamePlay.gameObject.SetActive(true);
        TEXT_TimeUp.transform.localScale = Vector3.zero;
        IMG_BlockPressButton.gameObject.SetActive(false);
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
        gameSlotList.Clear();
        foreach (Transform child in GRP_GameSlotList.transform)
        {
            //DOTween.KillAll(child);
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

        SetAnswer();
        for(int i = 0; i <= 6; i++)
        {
            SpawnGRP_GameSlot();
        }
        SetCurrentGameSlot();
    }
    void SetAnswer()
    {
        foreach (Transform child in GRP_AnswerEgg.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var n in answerList)
        {
            GameObject answerEggPrefab = Instantiate(IMG_AnswerEggPrefab);
            answerEggPrefab.transform.SetParent(GRP_AnswerEgg.transform);
            answerEggPrefab.GetComponent<RectTransform>().localScale = Vector3.one;
            answerEggPrefab.GetComponent<AnswerEgg>().InitializeAnswerEgg(n);
        }
    }

    public void SpawnGRP_GameSlot()
    {
        GameObject prefab = Instantiate(GameSlotPrefab);
        prefab.transform.SetParent(GRP_GameSlotList.transform);
        prefab.GetComponent<RectTransform>().localScale = Vector3.one;
        gameSlotList.Add(prefab);
    }
    public void SetCurrentGameSlot()
    {
        round++;
        currentGameSlot = gameSlotList[round - 1].GetComponent<GameSlot>();
        currentGameSlot.InitializeGameSlot(answerCount, answerList, round);

        if (round >= 2)
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
    public void PlayerSentCorrectAnswer()
    {
        AudioManager.inst.PlayWinSound();
        GRP_InputGroup_1.EnableBT_InputList(false);
        CalculateScore();
        GamePlay_BT_Pause.interactable = false;
        IMG_HideAnswer.gameObject.transform.DOMove(POS_HideAnswerPos.transform.position, 0.75f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            ContinueGame();
            GRP_InputGroup_1.EnableBT_InputList(true);
            GamePlay_BT_Pause.interactable = true;
        });
    }

    public void TimeOut()
    {
        //GRP_InputGroup_1.EnableBT_InputList(false);
        IMG_BlockPressButton.gameObject.SetActive(true);
        Sequence timeUpSequence = DOTween.Sequence();
        timeUpSequence.Append(TEXT_TimeUp.gameObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutSine));
        timeUpSequence.Append(TEXT_TimeUp.gameObject.transform.DOScale(Vector3.one, 2.0f));
        timeUpSequence.OnComplete(() =>
        {
            Finish_TEXT_Score.text = "Score : " + score.ToString();
            Canvas_Finish.gameObject.SetActive(true);
        });

    }

    public void EnableBT_InputList(bool isEnable)
    {
        GRP_InputGroup_1.EnableBT_InputList(isEnable);
    }
    #endregion

    void OnClickGamePlay_BT_Pause()
    {
        Canvas_Pause.gameObject.SetActive(true);
        TimeManager.inst.StopTime();
        IsOpenEggTab(true);
        InitializeCanvas_Pause();
        AudioManager.inst.PlayClickSound();
    }
    void IsOpenEggTab(bool isOpen)
    {
        if (isOpen == true)
        {
            for (int i = 0; i <= gameSlotList.Count - 1; i++)
            {
                gameSlotList[i].GetComponent<GameSlot>().IsOpenEggTab(true);
            }
        }
        else
        {
            for (int i = 0; i <= round - 1; i++)
            {
                gameSlotList[i].GetComponent<GameSlot>().IsOpenEggTab(false);
            }
        }
    }
    void InitializeCanvas_Pause()
    {
        SetIMG_BT_SFX(AudioManager.inst.GetIsMuteSFXSound());
        SetIMG_BT_BGM(AudioManager.inst.GetIsMuteBGMSound());
    }
    void SetIMG_BT_SFX(bool isMuteSFXSound)
    {
        if (isMuteSFXSound == true)
            Pause_BT_SFX.image.sprite = ICON_MuteSFX;
        else
            Pause_BT_SFX.image.sprite = ICON_PlaySFX;
    }
    void SetIMG_BT_BGM(bool isMuteBGMSound)
    {
        if (isMuteBGMSound == true)
            Pause_BT_BGM.image.sprite = ICON_MuteBGM;
        else
            Pause_BT_BGM.image.sprite = ICON_PlayBGM;
    }
    void OnClickPause_BT_SFX()
    {
        bool isMuteSFXSound = AudioManager.inst.ToggleMuteSFXSound();
        SetIMG_BT_SFX(isMuteSFXSound);
        AudioManager.inst.PlayClickSound();
    }
    void OnClickPause_BT_BGM()
    {
        bool isMuteBGMSound = AudioManager.inst.ToggleMuteBGMSound();
        SetIMG_BT_BGM(isMuteBGMSound);
        AudioManager.inst.PlayClickSound();
    }

    void OnClickPause_BT_Back()
    {
        Canvas_Pause.gameObject.SetActive(false);
        TimeManager.inst.ContinueTime();
        IsOpenEggTab(false);
        AudioManager.inst.PlayClickSound();
    }
    void OnClickBTRestart()
    {
        Canvas_Finish.gameObject.SetActive(false);
        Canvas_Pause.gameObject.SetActive(false);
        ResetScore();
        StartGame(gameMode);
        AudioManager.inst.PlayClickSound();
    }
    void OnClickBT_MainMenu()
    {
        Canvas_GamePlay.gameObject.SetActive(false);
        Canvas_Finish.gameObject.SetActive(false);
        Canvas_Pause.gameObject.SetActive(false);
        ResetScore();
        MainMenuManager.inst.GoToMainMenu();
        AudioManager.inst.PlayClickSound();
    }
    void OnClickFinish_BT_Trophy()
    {
        Canvas_HallOfFame.gameObject.SetActive(true);
        AudioManager.inst.PlayClickSound();
    }
    void ResetScore()
    {
        score = 0;
    }
}
