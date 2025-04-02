using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BT_Input : MonoBehaviour
{
    [SerializeField] string inputNumber;
    [Header("Egg")]
    [SerializeField] Image IMG_Egg;
    [SerializeField] EggAsset eggAsset;
    [Header("EggHolder")]
    [SerializeField] Image IMG_EggHolder;
    [SerializeField] Sprite ICON_EggHolder_1;
    [SerializeField] Sprite ICON_EggHolder_2;
    float waitForSwitchEggHolder = 2.0f;
    bool isUseEggHolder_1 = false;
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClickBT_Input);
    }

    public void InitializeBT_Input(string InputNumber, bool isRandomSwitchEggHolder)
    {
        inputNumber = InputNumber;
        SetIMG_Egg(int.Parse(inputNumber));
        if(isRandomSwitchEggHolder == true)
        {
            StartCoroutine(WaitForSwitchEggHolder(waitForSwitchEggHolder));
        }
    }

    void OnClickBT_Input()
    {
        GameManager.inst.SendDataToCurrentGameSlot(inputNumber);
        AudioManager.inst.PlaySFX_ClickInputBT();
    }
    void SetIMG_Egg(int inputNumber)
    {
        IMG_Egg.sprite = eggAsset.eggImageList[inputNumber];
    }
    IEnumerator WaitForSwitchEggHolder(float seconds)
    {
        isUseEggHolder_1 = !isUseEggHolder_1;
        yield return new WaitForSeconds(seconds);
        if(isUseEggHolder_1 == true)
        {
            IMG_EggHolder.sprite = ICON_EggHolder_1;
        }
        else
        {
            IMG_EggHolder.sprite = ICON_EggHolder_2;
        }
        StartCoroutine(WaitForSwitchEggHolder(waitForSwitchEggHolder));
    }
}
