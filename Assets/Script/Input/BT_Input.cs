using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BT_Input : MonoBehaviour
{
    [SerializeField] string inputNumber;
    [SerializeField] TextMeshProUGUI TEXT_Input;
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClickBT_Input);
    }

    public void InitializeBT_Input(string InputNumber)
    {
        inputNumber = InputNumber;
        TEXT_Input.text = inputNumber;
    }

    void OnClickBT_Input()
    {
        GameManager.inst.SendDataToCurrentGameSlot(inputNumber);
        AudioManager.inst.PlayClickSound();
    }
    
}
