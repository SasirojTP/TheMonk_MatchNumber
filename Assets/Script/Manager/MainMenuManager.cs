using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager inst;
    [Header("MainMenu")]
    [SerializeField] Canvas Canvas_MainMenu;
    [SerializeField] Button BT_Play;
    [SerializeField] Button BT_Trophy;
    [SerializeField] Button BT_Setting;

    [Header("SelectLevel")]
    [SerializeField] Canvas Canvas_SelectLevel;
    [SerializeField] Button BT_Easy;
    [SerializeField] Button BT_Normal;
    [SerializeField] Button BT_Hard;

    [Header("GamePlay")]
    [SerializeField] Canvas Canvas_GamePlay;

    [Header("Settings")]
    [SerializeField] Canvas Canvas_Settings;
    [SerializeField] Button Settings_BT_SFX;

    [SerializeField] Button Settings_BT_BGM;

    [SerializeField] Button Settings_BT_Credit;
    [SerializeField] Button Settings_BT_Back;

    [SerializeField] Sprite ICON_MuteSFX;
    [SerializeField] Sprite ICON_PlaySFX;
    [SerializeField] Sprite ICON_MuteBGM;
    [SerializeField] Sprite ICON_PlayBGM;

    [Header("Credit")]
    [SerializeField] Canvas Canvas_Credit;
    [SerializeField] Button Credit_BT_Back;

    [Header("HallOfFame")]
    [SerializeField] Canvas Canvas_HallOfFame;
    [SerializeField] Button HallOfFame_BT_Back;

    [Header("BlockOrientation")]
    [SerializeField] Canvas Canvas_BlockOrientation;
    private void Start()
    {
        inst = this;
        HideUI();
        AddListenerTOBT();
    }

    void HideUI()
    {
        Canvas_SelectLevel.gameObject.SetActive(false);
        Canvas_Settings.gameObject.SetActive(false);
        Canvas_Credit.gameObject.SetActive(false);
        Canvas_HallOfFame.gameObject.SetActive(false);
    }

    void AddListenerTOBT()
    {
        BT_Play.onClick.AddListener(OnClickBT_Play);
        BT_Trophy.onClick.AddListener(OnClickBT_Trophy);
        BT_Setting.onClick.AddListener(OnClickBT_Setting);

        BT_Easy.onClick.AddListener(OnClickBT_Easy);
        BT_Normal.onClick.AddListener(OnClickBT_Normal);
        BT_Hard.onClick.AddListener(OnClickBT_Hard);

        Settings_BT_SFX.onClick.AddListener(OnClickSettings_BT_SFX);
        Settings_BT_BGM.onClick.AddListener(OnClickSettings_BT_BGM);
        Settings_BT_Credit.onClick.AddListener(OnClickSettings_BT_Credit);
        Settings_BT_Back.onClick.AddListener(OnClickSettings_BT_Back);

        Credit_BT_Back.onClick.AddListener(OnClickCredit_BT_Back);

        HallOfFame_BT_Back.onClick.AddListener(OnClickHallOfFame_BT_Back);
    }

    private void Update()
    {
        if(Screen.height > Screen.width)
        {
            Canvas_BlockOrientation.gameObject.SetActive(false);
        }
        else
        {
            Canvas_BlockOrientation.gameObject.SetActive(true);
        }
    }

    public void GoToMainMenu()
    {
        Canvas_MainMenu.gameObject.SetActive(true);
    }

    void OnClickBT_Play()
    {
        Canvas_MainMenu.gameObject.SetActive(false);
        Canvas_SelectLevel.gameObject.SetActive(true);
        AudioManager.inst.PlayClickSound();
    }
    void OnClickBT_Trophy()
    {
        Canvas_HallOfFame.gameObject.SetActive(true);
        AudioManager.inst.PlayClickSound();
    }
    void OnClickBT_Setting()
    {
        Canvas_MainMenu.gameObject.SetActive(false);
        Canvas_Settings.gameObject.SetActive(true);
        InitializeCanvas_Settings();
        AudioManager.inst.PlayClickSound();
    }

    void OnClickBT_Easy()
    {
        Canvas_MainMenu.gameObject.SetActive(false);
        Canvas_SelectLevel.gameObject.SetActive(false);
        Canvas_GamePlay.gameObject.SetActive(true);
        GameManager.inst.StartGame(GameManager.GameMode.Easy);
        AudioManager.inst.PlayClickSound();
    }
    void OnClickBT_Normal()
    {
        Canvas_MainMenu.gameObject.SetActive(false);
        Canvas_SelectLevel.gameObject.SetActive(false);
        Canvas_GamePlay.gameObject.SetActive(true);
        GameManager.inst.StartGame(GameManager.GameMode.Normal);
        AudioManager.inst.PlayClickSound();
    }
    void OnClickBT_Hard()
    {
        Canvas_MainMenu.gameObject.SetActive(false);
        Canvas_SelectLevel.gameObject.SetActive(false);
        Canvas_GamePlay.gameObject.SetActive(true);
        GameManager.inst.StartGame(GameManager.GameMode.Hard);
        AudioManager.inst.PlayClickSound();
    }
    void InitializeCanvas_Settings()
    {
        SetIMG_BT_SFX(AudioManager.inst.GetIsMuteSFXSound());
        SetIMG_BT_BGM(AudioManager.inst.GetIsMuteBGMSound());
    }
    void SetIMG_BT_SFX(bool isMuteSFXSound)
    {
        if (isMuteSFXSound == true)
            Settings_BT_SFX.image.sprite = ICON_MuteSFX;
        else
            Settings_BT_SFX.image.sprite = ICON_PlaySFX;
    }
    void SetIMG_BT_BGM(bool isMuteBGMSound)
    {
        if (isMuteBGMSound == true)
            Settings_BT_BGM.image.sprite = ICON_MuteBGM;
        else
            Settings_BT_BGM.image.sprite = ICON_PlayBGM;
    }
    void OnClickSettings_BT_SFX()
    {
        bool isMuteSFXSound = AudioManager.inst.ToggleMuteSFXSound();
        SetIMG_BT_SFX(isMuteSFXSound);
        AudioManager.inst.PlayClickSound();
    }
    void OnClickSettings_BT_BGM()
    {
        bool isMuteBGMSound = AudioManager.inst.ToggleMuteBGMSound();
        SetIMG_BT_BGM(isMuteBGMSound);
        AudioManager.inst.PlayClickSound();
    }
    void OnClickSettings_BT_Credit()
    {
        Canvas_Settings.gameObject.SetActive(false);
        Canvas_Credit.gameObject.SetActive(true);
        AudioManager.inst.PlayClickSound();
    }

    void OnClickSettings_BT_Back()
    {
        Canvas_Settings.gameObject.SetActive(false);
        Canvas_MainMenu.gameObject.SetActive(true);
        AudioManager.inst.PlayClickSound();
    }

    void OnClickCredit_BT_Back()
    {
        Canvas_Credit.gameObject.SetActive(false);
        Canvas_Settings.gameObject.SetActive(true);
        AudioManager.inst.PlayClickSound();
    }
    void OnClickHallOfFame_BT_Back()
    {
        Canvas_HallOfFame.gameObject.SetActive(false);
        AudioManager.inst.PlayClickSound();
    }
    //void OnClickBT_Quit()
    //{
    //    Application.Quit();
    //}
}
