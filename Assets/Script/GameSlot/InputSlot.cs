using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InputSlot : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Image IMG_Status;
    [SerializeField] Image IMG_Egg;
    [SerializeField] EggAsset eggAsset;

    public void SetIMG_Egg(string inputText)
    {
        SetEggAnimation();
        IMG_Egg.sprite = eggAsset.eggImageList[int.Parse(inputText)];
    }
    void SetEggAnimation()
    {
        IMG_Egg.rectTransform.localScale = new Vector3(0, 0, 0);
        IMG_Egg.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            IMG_Egg.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.InOutSine);
        });
    }
    
    public void SetIMG_StatusColor(Color color)
    {
        IMG_Status.color = color;
    }
}
