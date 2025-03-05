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
    [SerializeField] SoundSource BGM_Classic;

    private void Start()
    {
        inst = this;
        PlayBGMSound();
    }

    public bool ToggleMuteSound()
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
    public void TogglePlayBGMSound()
    {
        isMuteBGMSound = !isMuteBGMSound;

        if(isMuteBGMSound == true)
        {
            PlayBGMSound();
        }
        else
        {
            StopPlayBGMSound();
        }
    }
    void PlayBGMSound()
    {
        BGM_Classic.sound.loop = true;
        BGM_Classic.sound.Play();
    }
    void StopPlayBGMSound()
    {
        BGM_Classic.sound.Stop();
    }
}
