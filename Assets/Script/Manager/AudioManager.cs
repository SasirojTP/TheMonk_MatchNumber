using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager inst;
    [SerializeField] bool isMuteSound = false;
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
        isMuteSound = !isMuteSound;
        return isMuteSound;
    }

    public void PlayClickSound()
    {
        if(isMuteSound == false)
            clickSound.Play();
    }
    public void PlayWinSound()
    {
        if (isMuteSound == false)
            winSound.Play();
    }
    void PlayBGMSound()
    {
        BGM_Classic.sound.loop = true;
        BGM_Classic.sound.Play();
    }
}
