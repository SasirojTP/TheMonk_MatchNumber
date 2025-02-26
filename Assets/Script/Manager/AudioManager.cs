using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager inst;
    [SerializeField] bool isMuteSound = false;
    [Header("Sound")]
    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource winSound;

    private void Start()
    {
        inst = this;
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
}
