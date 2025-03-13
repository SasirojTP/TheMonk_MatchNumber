using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GameSlot : MonoBehaviour
{
    [Header("Logic")]
    int answerCount = 0;
    [SerializeField] List<string> answerList = new List<string>();
    int inputCount = 0;
    List<string> inputList = new List<string>();
    List<string> checkAnswerList = new List<string>();

    [Header ("UI")]
    [SerializeField] TextMeshProUGUI TEXT_Round;
    [SerializeField] GameObject GRP_InputSlotHolder;

    [SerializeField] GameObject GRP_InputSlotPrefab;
    List<GameObject> GRP_InputSlotList = new List<GameObject>();

    [SerializeField] GameObject IMG_EggTab;

    public void InitializeGameSlot(int AnswerCount ,List<string> AnswerList, int round)
    {
        answerCount = AnswerCount;
        answerList = AnswerList;
        TEXT_Round.text = round.ToString();
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
