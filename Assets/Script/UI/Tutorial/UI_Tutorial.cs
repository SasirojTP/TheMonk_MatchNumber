using UnityEngine;
using UnityEngine.UI;

public class UI_Tutorial : MonoBehaviour
{
    [SerializeField] Button Tutorial_BT_Back;

    public void InitializeUI_Tutorial()
    {
        Tutorial_BT_Back.onClick.AddListener(OnClickTutorial_BT_Back);
    }

    void OnClickTutorial_BT_Back()
    {
        TimeManager.inst.StartTimer();
        Destroy(gameObject);
    }
}
