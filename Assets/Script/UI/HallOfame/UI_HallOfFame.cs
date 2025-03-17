using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_HallOfFame : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Transform GRP_PlayerList;
    [SerializeField] Button BT_PreviousPage;
    [SerializeField] Image IMG_DifficultyPage;
    [SerializeField] Button BT_NextPage;
    [SerializeField] Button HallOfFame_BT_Back;

    [Header("Image")]
    [SerializeField] List<Sprite> ICON_Page_list = new List<Sprite>();

    [Header("Prefab")]
    [SerializeField] GameObject GRP_PlayerDetail;

    int pageIndex = 0;

    public void InitializeUI_HallOfFame()
    {
        AddListenerToBT();
        SetPage();
    }
    void AddListenerToBT()
    {
        BT_PreviousPage.onClick.AddListener(OnClickBT_PreviousPage);
        BT_NextPage.onClick.AddListener(OnClickBT_NextPage);
        HallOfFame_BT_Back.onClick.AddListener(OnClickHallOfFame_BT_Back);
    }
    void SetPage()
    {
        SetChangePageBT();
        ClearPlayerList();
        SpawnGRP_PlayerDetailByPageIdex();
    }
    void SetChangePageBT()
    {
        switch (pageIndex)
        {
            case 0:
                BT_PreviousPage.interactable = false;
                BT_NextPage.interactable = true;
                break;
            case 1:
                BT_PreviousPage.interactable = true;
                BT_NextPage.interactable = true;
                break;
            case 2:
                BT_PreviousPage.interactable = true;
                BT_NextPage.interactable = false;
                break;
        }
        IMG_DifficultyPage.sprite = ICON_Page_list[pageIndex];
    }
    void ClearPlayerList()
    {
        foreach (Transform child in GRP_PlayerList)
        {
            Destroy(child.gameObject);
        }
    }
    void SpawnGRP_PlayerDetailByPageIdex()
    {
        switch (pageIndex)
        {
            case 0:
                for (int i = 0; i < SaveManager.inst.GetEasy_Name_List().Count; i++)
                {
                    GameObject prefab = Instantiate(GRP_PlayerDetail, GRP_PlayerList);
                    prefab.GetComponent<GRP_PlayerDetail>().InitializeGRP_PlayerDetail(i + 1, SaveManager.inst.GetEasy_Name_List()[i], SaveManager.inst.GetEasy_Score_List()[i]);
                }
                break;
            case 1:
                for (int i = 0; i < SaveManager.inst.GetMedium_Name_List().Count; i++)
                {
                    GameObject prefab = Instantiate(GRP_PlayerDetail, GRP_PlayerList);
                    prefab.GetComponent<GRP_PlayerDetail>().InitializeGRP_PlayerDetail(i + 1, SaveManager.inst.GetMedium_Name_List()[i], SaveManager.inst.GetMedium_Score_List()[i]);
                }
                break;
            case 2:
                for (int i = 0; i < SaveManager.inst.GetHard_Name_List().Count; i++)
                {
                    GameObject prefab = Instantiate(GRP_PlayerDetail, GRP_PlayerList);
                    prefab.GetComponent<GRP_PlayerDetail>().InitializeGRP_PlayerDetail(i + 1, SaveManager.inst.GetHard_Name_List()[i], SaveManager.inst.GetHard_Score_List()[i]);
                }
                break;
        }
    }
    void OnClickBT_PreviousPage()
    {
        pageIndex--;
        SetPage();
    }
    void OnClickBT_NextPage()
    {
        pageIndex++;
        SetPage();
    }
    void OnClickHallOfFame_BT_Back()
    {
        Destroy(this.gameObject);
    }
}
