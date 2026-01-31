using UnityEngine;

public class AudioTimer : MonoBehaviour
{
    [SerializeField] private float totalTime = 60f; // 1 minute
    private float currentTime;

    private bool tickingPlayed = false;
    private bool timesUpPlayed = false;

    private void Start()
    {
        currentTime = totalTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0)
            currentTime = 0;

        // Play ticking sound once at 15 seconds
        if (!tickingPlayed && currentTime <= 15f && currentTime > 0f)
        {
            tickingPlayed = true;
            SoundManager.Instance.PlayTicking15Sec();
        }

        // Play time's up sound once at 0 seconds
        if (!timesUpPlayed && currentTime <= 0f)
        {
            timesUpPlayed = true;
            SoundManager.Instance.PlayTimesUp();
        }
    }
}