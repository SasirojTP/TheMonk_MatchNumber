using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InputGroup : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject BT_InputPrefab;
    int spawnButtonCount;
    List<GameObject> BT_InputList = new List<GameObject>();

    public void InitializeInputGroup()
    {
        switch (GameManager.inst.gameMode)
        {
            case GameManager.GameMode.Easy:
                spawnButtonCount = 4;
                break;
            case GameManager.GameMode.Medium:
                spawnButtonCount = 5;
                break;
            case GameManager.GameMode.Hard:
                spawnButtonCount = 5;
                break;
            default:
                spawnButtonCount = 3;
                break;
        }
        SpawnBT_InputPrefab();
    }

    void SpawnBT_InputPrefab()
    {
        BT_InputList.Clear();
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        int randomSwitchEggHolderIndex = Random.Range(0, spawnButtonCount);
        for (int i = 0; i <= spawnButtonCount - 1; i++)
        {
            GameObject prefab = Instantiate(BT_InputPrefab);
            prefab.transform.SetParent(this.transform);
            prefab.GetComponent<RectTransform>().localScale = Vector3.one;

            if(i == randomSwitchEggHolderIndex)
                prefab.GetComponent<BT_Input>().InitializeBT_Input(i.ToString(),true);
            else
                prefab.GetComponent<BT_Input>().InitializeBT_Input(i.ToString(),false);

            BT_InputList.Add(prefab);
        }
    }

    public void EnableBT_InputList(bool isEnable)
    {
        foreach(GameObject n in BT_InputList)
        {
            n.GetComponent<Button>().interactable = isEnable;
        }
    }
}
