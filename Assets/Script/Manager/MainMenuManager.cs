using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager inst;
    [SerializeField] Canvas Canvas_MainMenu;
    [SerializeField] Button BT_Easy;
    [SerializeField] Button BT_Normal;
    [SerializeField] Button BT_Hard;
    [SerializeField] Button BT_Quit;

    private void Start()
    {
        inst = this;
        AddListenerTOBT();
    }

    void AddListenerTOBT()
    {
        BT_Easy.onClick.AddListener(GoToLevel1);
        BT_Normal.onClick.AddListener(GoToLevel2);
        BT_Hard.onClick.AddListener(GoToLevel3);
        BT_Quit.onClick.AddListener(OnClickBT_Quit);
    }

    public void GoToMainMenu()
    {
        Canvas_MainMenu.gameObject.SetActive(true);
    }

    void GoToLevel1()
    {
        Canvas_MainMenu.gameObject.SetActive(false);
        GameManager.inst.StartGame(GameManager.GameMode.Easy);
    }
    void GoToLevel2()
    {
        Canvas_MainMenu.gameObject.SetActive(false);
        GameManager.inst.StartGame(GameManager.GameMode.Normal);
    }
    void GoToLevel3()
    {
        Canvas_MainMenu.gameObject.SetActive(false);
        GameManager.inst.StartGame(GameManager.GameMode.Hard);
    }
    void OnClickBT_Quit()
    {
        Application.Quit();
    }
}
