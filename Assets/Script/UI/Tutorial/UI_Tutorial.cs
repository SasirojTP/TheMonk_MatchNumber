using UnityEngine;
using UnityEngine.UI;

public class UI_Tutorial : MonoBehaviour
{
    [SerializeField] Button Tutorial_BT_GotIt;

    public void InitializeUI_Tutorial()
    {
        Tutorial_BT_GotIt.onClick.AddListener(OnClickTutorial_BT_GotIt);
    }

    void OnClickTutorial_BT_GotIt()
    {
        TimeManager.inst.StartTimer();
        Destroy(gameObject);
    }
}
