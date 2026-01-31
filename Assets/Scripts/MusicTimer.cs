using UnityEngine;

public class MusicTimer : MonoBehaviour
{
    [SerializeField] public float TotalTime = 60f;
    private float CurrentTime;
    [SerializeField] public AudioSource Music;

    private bool TickingIsPlayed = false;
    private bool TimesUpIsPlayed = false;
    private bool IsVolumeLow = false;
    private bool IsVolumeRestoed = false;

    [SerializeField] private float NormalVolume = 1.0f;
    [SerializeField] private float LowVolume = 0.7f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentTime = TotalTime;
        Music.volume = NormalVolume;
        Music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime = CurrentTime - Time.deltaTime;

        

        if (!IsVolumeLow && CurrentTime <= 15f && CurrentTime > 0)
        {
            IsVolumeLow = true;
            Music.volume = LowVolume;
        }

        if(!IsVolumeRestoed && CurrentTime <= -1f)
        {
            IsVolumeRestoed = true;
            Music.volume = NormalVolume;
        }
    }
}
