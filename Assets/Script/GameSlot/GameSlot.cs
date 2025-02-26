using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameSlot : MonoBehaviour
{
    [Header("Logic")]
    int answerCount = 0;
    [SerializeField] List<string> answerList = new List<string>();
    int inputCount = 0;
    List<string> inputList = new List<string>();
    [Header ("UI")]
    [SerializeField] TextMeshProUGUI TEXT_Round;
    [SerializeField] GameObject GRP_InputSlotHolder;

    [SerializeField] GameObject GRP_InputSlotPrefab;
    List<GameObject> GRP_InputSlotList = new List<GameObject>();

    public void InitializeGameSlot(int AnswerCount ,List<string> AnswerList, int round)
    {
        answerCount = AnswerCount;
        answerList = AnswerList;
        TEXT_Round.text = round.ToString();
        inputCount = 0;
        SpawnGRP_InputSlot();
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
        GRP_InputSlotList[inputCount].GetComponent<InputSlot>().SetTEXT_InputSlot(input);
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

    List<string> checkAnswerList = new List<string>();
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
            GameManager.inst.GameFinish();
        }
        else
        {
            GameManager.inst.SpawnGRP_GameSlot();
        }
    }

    void HintGameSlotDisplay()
    {
        for (int i = 0; i < inputList.Count; i++)
        {
            switch (checkAnswerList[i])
            {
                case "*":
                    GRP_InputSlotList[i].GetComponent<InputSlot>().SetIMG_BG_InputSlotColor(Color.green);
                    break;
                default:
                    if (checkAnswerList.Contains(inputList[i]))
                    {
                        GRP_InputSlotList[i].GetComponent<InputSlot>().SetIMG_BG_InputSlotColor(Color.yellow);
                    }
                    break;
            }
        }
    }
}
