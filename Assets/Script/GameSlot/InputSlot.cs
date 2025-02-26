using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputSlot : MonoBehaviour
{
    GameSlot gameSlot;
    [SerializeField] Image IMG_BG_InputSlot;
    [SerializeField] TextMeshProUGUI TEXT_InputSlot;

    public void SetTEXT_InputSlot(string inputText)
    {
        TEXT_InputSlot.text = inputText;
    }

    public void SetIMG_BG_InputSlotColor(Color color)
    {
        IMG_BG_InputSlot.color = color;
    }
}
