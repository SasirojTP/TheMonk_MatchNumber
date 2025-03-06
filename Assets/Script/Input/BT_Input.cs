using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BT_Input : MonoBehaviour
{
    [SerializeField] string inputNumber;
    [SerializeField] Image IMG_Egg;
    [SerializeField] EggAsset eggAsset;
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClickBT_Input);
    }

    public void InitializeBT_Input(string InputNumber)
    {
        inputNumber = InputNumber;
        SetIMG_Egg(int.Parse(inputNumber));
    }

    void OnClickBT_Input()
    {
        GameManager.inst.SendDataToCurrentGameSlot(inputNumber);
        AudioManager.inst.PlayClickSound();
    }
    void SetIMG_Egg(int inputNumber)
    {
        IMG_Egg.sprite = eggAsset.eggImageList[inputNumber];
    }


}
