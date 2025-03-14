using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager inst;
    [Header ("UI")]
    [SerializeField] TextMeshProUGUI TEXT_Timer;

    [Header("Logic")]
    [SerializeField] float maxTime = 60;
    float timeRemining;
    bool startTimer = false;

    private void Start()
    {
        inst = this;
    }

    private void Update()
    {
        if(startTimer)
        {
            timeRemining = timeRemining - Time.deltaTime;
            TEXT_Timer.text = Mathf.Floor(timeRemining).ToString();
            if(timeRemining <= 5)
            {
                AudioManager.inst.SetBGMSoundTo_BGM_ClockSound();
            }
            if (timeRemining < 1)
            {
                GameManager.inst.TimeOut();
                AudioManager.inst.PlayTimeUpSound();
                AudioManager.inst.SetBGMSoundTo_BGM_Classic();
                StopTime();
            }
        }
    }

    public void StartTimer()
    {
        timeRemining = maxTime;
        startTimer = true;
    }
    public void ContinueTime()
    {
        startTimer = true;
    }

    public void StopTime()
    {
        startTimer = false;
    }
}
