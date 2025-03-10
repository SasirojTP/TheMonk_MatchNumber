using UnityEngine;
using UnityEngine.UI;

public class AnswerEgg : MonoBehaviour
{
    [SerializeField] string inputNumber;
    [SerializeField] Image IMG_AnswerEgg;
    [SerializeField] EggAsset eggAsset;

    public void InitializeAnswerEgg(string InputNumber)
    {
        inputNumber = InputNumber;
        SetIMG_Egg(int.Parse(inputNumber));
    }
    void SetIMG_Egg(int inputNumber)
    {
        IMG_AnswerEgg.sprite = eggAsset.eggImageList[inputNumber];
    }
}
