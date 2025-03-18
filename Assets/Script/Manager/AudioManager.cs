using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager inst;
    [SerializeField] bool isMuteSFXSound = false;
    [SerializeField] bool isMuteBGMSound = false;
    [Header("SFX_Sound")]
    [SerializeField] AudioSource SFX_ClickSound;
    [SerializeField] AudioSource SFX_WinSound;
    [SerializeField] AudioSource SFX_TimeUpSound;
    [SerializeField] AudioSource SFX_OpenFinishPageSound;
    [SerializeField] AudioSource SFX_ClickInputBT;
    [Header("BGM_Sound")]
    [SerializeField] AudioSource currentBGM;
    [SerializeField] AudioSource BGM_Classic;
    [SerializeField] AudioSource BGM_ClockSound;
    float BGMVloume;

    private void Start()
    {
        inst = this;
        SetBGMSoundTo_BGM_Classic();
    }

    public bool GetIsMuteSFXSound()
    {
        return isMuteSFXSound;
    }
    public bool GetIsMuteBGMSound()
    {
        return isMuteBGMSound;
    }
    public bool ToggleMuteSFXSound()
    {
        isMuteSFXSound = !isMuteSFXSound;
        return isMuteSFXSound;
    }

    public void PlayClickSound()
    {
        if(isMuteSFXSound == false)
            SFX_ClickSound.Play();
    }
    public void PlayWinSound()
    {
        if (isMuteSFXSound == false)
            SFX_WinSound.Play();
    }
    public void PlayTimeUpSound()
    {
        if (isMuteSFXSound == false)
            SFX_TimeUpSound.Play();
    }
    public void PlayOpenFinishPageSound()
    {
        if (isMuteSFXSound == false)
            SFX_OpenFinishPageSound.Play();
    }
    public void PlaySFX_ClickInputBT()
    {
        if (isMuteSFXSound == false)
            SFX_ClickInputBT.Play();
    }
    public bool ToggleMuteBGMSound()
    {
        isMuteBGMSound = !isMuteBGMSound;

        if(isMuteBGMSound == true)
        {
            StopPlayBGMSound();
        }
        else
        {
            PlayBGMSound();
        }
        return isMuteBGMSound;
    }
    void PlayBGMSound()
    {
        currentBGM.volume = BGMVloume;
    }
    void StopPlayBGMSound()
    {
        currentBGM.volume = 0;
    }
    public void SetBGMSoundTo_BGM_Classic()
    {
        currentBGM.clip = BGM_Classic.clip;
        BGMVloume = BGM_Classic.volume;

        currentBGM.Play();
        PlayBGMSound();
    }
    public void SetBGMSoundTo_BGM_ClockSound()
    {
        currentBGM.clip = BGM_ClockSound.clip;
        BGMVloume = BGM_ClockSound.volume;

        currentBGM.Play();
        PlayBGMSound();
    }
}
