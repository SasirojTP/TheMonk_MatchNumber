using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class UI_HallOfFame : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image IMG_Header_HallOfFame;
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
    GameManager.GameMode scorePageGameMode;
    Sequence header_HallOfFameSequence;
    bool isLastPlay;
    bool isOpenPageLastPlayGameMode;
    int lastPlayPlayerRank;

    public void InitializeUI_HallOfFame()
    {
        AddListenerToBT();
        PlayAnimationIMG_Header_HallOfFame();
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
        CheckIsHaveLastPlayData();
        SpawnGRP_PlayerDetailByPageIdex();
    }
    void PlayAnimationIMG_Header_HallOfFame()
    {
        IMG_Header_HallOfFame.transform.localScale = Vector3.zero;
        header_HallOfFameSequence = DOTween.Sequence();
        header_HallOfFameSequence.Append(IMG_Header_HallOfFame.gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).SetEase(Ease.InOutSine));
        header_HallOfFameSequence.Append(IMG_Header_HallOfFame.gameObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutSine));
        header_HallOfFameSequence.OnComplete(() =>
        {
            IMG_Header_HallOfFame.gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        });
    }
    void SetChangePageBT()
    {
        switch (pageIndex)
        {
            case 0:
                scorePageGameMode = GameManager.GameMode.Easy;
                BT_PreviousPage.interactable = false;
                BT_NextPage.interactable = true;
                break;
            case 1:
                scorePageGameMode = GameManager.GameMode.Medium;
                BT_PreviousPage.interactable = true;
                BT_NextPage.interactable = true;
                break;
            case 2:
                scorePageGameMode = GameManager.GameMode.Hard;
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
    void CheckIsHaveLastPlayData()
    {
        isLastPlay = SaveManager.inst.GetIsLastPlay();

        isOpenPageLastPlayGameMode = false;
        if (SaveManager.inst.GetLastPlayData().lastPlayGameMode == scorePageGameMode)
            isOpenPageLastPlayGameMode = true;
        else
            isOpenPageLastPlayGameMode = false;

       lastPlayPlayerRank = SaveManager.inst.GetLastPlayData().playerRank;
    }
    void SpawnGRP_PlayerDetailByPageIdex()
    {
        GameObject prefab;
        switch (pageIndex)
        {
            case 0:
                for (int i = 0; i < SaveManager.inst.GetEasy_Name_List().Count; i++)
                {
                    prefab = Instantiate(GRP_PlayerDetail, GRP_PlayerList);
                    if (isLastPlay == true && isOpenPageLastPlayGameMode == true && i == lastPlayPlayerRank - 1)
                        prefab.GetComponent<GRP_PlayerDetail>().InitializeGRP_PlayerDetail(i + 1, SaveManager.inst.GetEasy_Name_List()[i], SaveManager.inst.GetEasy_Score_List()[i], true);
                    else
                        prefab.GetComponent<GRP_PlayerDetail>().InitializeGRP_PlayerDetail(i + 1, SaveManager.inst.GetEasy_Name_List()[i], SaveManager.inst.GetEasy_Score_List()[i], false);
                }
                break;
            case 1:
                for (int i = 0; i < SaveManager.inst.GetMedium_Name_List().Count; i++)
                {
                    prefab = Instantiate(GRP_PlayerDetail, GRP_PlayerList);
                    if (isLastPlay == true && isOpenPageLastPlayGameMode == true && i == lastPlayPlayerRank - 1)
                        prefab.GetComponent<GRP_PlayerDetail>().InitializeGRP_PlayerDetail(i + 1, SaveManager.inst.GetMedium_Name_List()[i], SaveManager.inst.GetMedium_Score_List()[i], true);
                    else
                        prefab.GetComponent<GRP_PlayerDetail>().InitializeGRP_PlayerDetail(i + 1, SaveManager.inst.GetMedium_Name_List()[i], SaveManager.inst.GetMedium_Score_List()[i], false);
                }
                break;
            case 2:
                for (int i = 0; i < SaveManager.inst.GetHard_Name_List().Count; i++)
                {
                    prefab = Instantiate(GRP_PlayerDetail, GRP_PlayerList);
                    if (isLastPlay == true && isOpenPageLastPlayGameMode == true && i == lastPlayPlayerRank - 1)
                        prefab.GetComponent<GRP_PlayerDetail>().InitializeGRP_PlayerDetail(i + 1, SaveManager.inst.GetHard_Name_List()[i], SaveManager.inst.GetHard_Score_List()[i], true);
                    else
                        prefab.GetComponent<GRP_PlayerDetail>().InitializeGRP_PlayerDetail(i + 1, SaveManager.inst.GetHard_Name_List()[i], SaveManager.inst.GetHard_Score_List()[i], false);
                }
                break;
        }
        if(isLastPlay == true && isOpenPageLastPlayGameMode == true)
        {
            prefab = Instantiate(GRP_PlayerDetail, GRP_PlayerList);
            prefab.GetComponent<GRP_PlayerDetail>().InitializeGRP_PlayerDetail(SaveManager.inst.GetLastPlayData().playerRank, SaveManager.inst.GetLastPlayData().lastPlayPlayerName, SaveManager.inst.GetLastPlayData().lastPlayScore, true);
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
        header_HallOfFameSequence.Kill();
        DOTween.Kill(IMG_Header_HallOfFame.transform);
        Destroy(this.gameObject);
    }
}
