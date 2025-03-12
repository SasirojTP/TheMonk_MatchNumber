using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager inst;
    [SerializeField] bool isMuteSFXSound = false;
    [SerializeField] bool isMuteBGMSound = false;
    [Header("SFX_Sound")]
    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource winSound;
    [Header("BGM_Sound")]
    float BGMVloume;
    [SerializeField] AudioSource BGM_Classic;

    private void Start()
    {
        inst = this;
        BGMVloume = BGM_Classic.volume;
        BGM_Classic.Play();
        PlayBGMSound();
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
            clickSound.Play();
    }
    public void PlayWinSound()
    {
        if (isMuteSFXSound == false)
            winSound.Play();
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
        BGM_Classic.volume = BGMVloume;
    }
    void StopPlayBGMSound()
    {
        BGM_Classic.volume = 0;
    }
}
