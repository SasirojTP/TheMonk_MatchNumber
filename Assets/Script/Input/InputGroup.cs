using UnityEngine;
using static GameManager;

public class InputGroup : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject BT_InputPrefab;
    int spawnButtonCount;

    public void InitializeInputGroup()
    {
        switch (GameManager.inst.gameMode)
        {
            case GameMode.Easy:
                spawnButtonCount = 4;
                break;
            case GameMode.Normal:
                spawnButtonCount = 5;
                break;
            case GameMode.Hard:
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
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i <= spawnButtonCount - 1; i++)
        {
            GameObject prefab = Instantiate(BT_InputPrefab);
            prefab.transform.SetParent(this.transform);
            prefab.GetComponent<RectTransform>().localScale = Vector3.one;
            prefab.GetComponent<BT_Input>().InitializeBT_Input(i.ToString());
        }
    }
}
