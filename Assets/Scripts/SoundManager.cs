using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tickingClip;
    [SerializeField] private AudioClip TimesUPClip;
    public void Awake()
    {
        Instance = this;
    }
    public void PlayTicking15Sec()
    {
        audioSource.PlayOneShot(tickingClip);
    }

    public void PlayTimesUp()
    {
        audioSource.PlayOneShot(TimesUPClip);
    }

}
