using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class GameSlot : MonoBehaviour
{
    [Header("Logic")]
    [SerializeField] List<string> answerList = new List<string>();
    int answerCount = 0;
    int inputCount = 0;
    List<string> inputList = new List<string>();
    List<string> checkAnswerList = new List<string>();
    [SerializeField] Sprite ICON_EggShelf_1;
    [SerializeField] Sprite ICON_EggShelf_2;

    [Header("UI")]
    [SerializeField] Image IMG_BG_Slot;
    [SerializeField] GameObject GRP_InputSlotHolder;

    [SerializeField] GameObject GRP_InputSlotPrefab;
    List<GameObject> GRP_InputSlotList = new List<GameObject>();

    [SerializeField] GameObject IMG_EggTab;
    [SerializeField] GameObject GRP_Spider;
    [SerializeField] Image IMG_ShelfCrack_2;

    public void OnSpawnGameSlot(int spawnGRP_GameSlotCount)
    {
        if(spawnGRP_GameSlotCount <= 1)
        {
            IMG_BG_Slot.sprite = ICON_EggShelf_1;
        }
        else
        {
            IMG_BG_Slot.sprite = ICON_EggShelf_2;
        }
        if(spawnGRP_GameSlotCount >= 1 && spawnGRP_GameSlotCount <= 3)
        {
            IMG_ShelfCrack_2.gameObject.SetActive(true);
            GRP_Spider.SetActive(false);
        }
        else if(spawnGRP_GameSlotCount >= 7)
        {
            IMG_ShelfCrack_2.gameObject.SetActive(false);
            GRP_Spider.SetActive(true);
        }
        else
        {
            IMG_ShelfCrack_2.gameObject.SetActive(false);
            GRP_Spider.SetActive(false);
        }
    }
    public void InitializeGameSlot(int AnswerCount ,List<string> AnswerList, int round)
    {
        answerCount = AnswerCount;
        answerList = AnswerList;
        inputCount = 0;

        PlayAnimationEggTab();
        StartCoroutine(WaitForEnableBT_InputList(0.3f));
        SpawnGRP_InputSlot();
    }
    void PlayAnimationEggTab()
    {
        IMG_EggTab.transform.DOScaleY(0,0.5f);
    }
    IEnumerator WaitForEnableBT_InputList(float seconds)
    {
        GameManager.inst.EnableBT_InputList(false);
        yield return new WaitForSeconds(seconds);
        GameManager.inst.EnableBT_InputList(true);
    }
    void SpawnGRP_InputSlot()
    {
        for(int i = 0; i <= answerCount - 1; i++)
        {
            GameObject prefab = Instantiate(GRP_InputSlotPrefab);
            prefab.transform.SetParent(GRP_InputSlotHolder.transform);
            prefab.GetComponent<RectTransform>().localScale = Vector3.one;
            GRP_InputSlotList.Add(prefab);
        }
    }

    public void GetInput(string input)
    {
        inputList.Add(input);
        GRP_InputSlotList[inputCount].GetComponent<InputSlot>().SetIMG_Egg(input);
        inputCount++;
        CheckIsAllInputEdit();
    }

    public void CheckIsAllInputEdit()
    {
        if(inputCount >= answerList.Count)
        {
            CheckAnswer();
        }
    }

    void CheckAnswer()
    {
        bool isPass = true;
        checkAnswerList.Clear();
        checkAnswerList.AddRange(answerList);
        for (int i = 0; i < inputList.Count; i++)
        {
            if (inputList[i] == answerList[i])
            {
                inputList[i] = "-";
                checkAnswerList[i] = "*";
            }
            else
            {
                isPass = false;
            }
        }
        HintGameSlotDisplay();

        if (isPass)
        {
            GameManager.inst.PlayerSentCorrectAnswer();
        }
        else
        {
            GameManager.inst.SpawnGRP_GameSlot();
            GameManager.inst.SetCurrentGameSlot();
        }
    }

    void HintGameSlotDisplay()
    {
        for (int i = 0; i < inputList.Count; i++)
        {
            switch (checkAnswerList[i])
            {
                case "*":
                    GRP_InputSlotList[i].GetComponent<InputSlot>().SetIMG_StatusColor(Color.green);
                    break;
                default:
                    if (checkAnswerList.Contains(inputList[i]))
                    {
                        GRP_InputSlotList[i].GetComponent<InputSlot>().SetIMG_StatusColor(Color.yellow);
                    }
                    else
                    {
                        GRP_InputSlotList[i].GetComponent<InputSlot>().SetIMG_StatusColor(Color.grey);
                    }
                    break;
            }
        }
    }
    public void IsOpenEggTab(bool isOpen)
    {
        DOTween.Kill(IMG_EggTab.transform);
        if (isOpen == true)
        {
            IMG_EggTab.gameObject.transform.localScale = Vector3.one;
        }
        else
        {
            IMG_EggTab.gameObject.transform.localScale = new Vector3(0, 1.0f,0);
        }
    }
}
