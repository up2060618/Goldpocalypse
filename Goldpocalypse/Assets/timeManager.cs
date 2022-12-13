using UnityEngine;

public class timeManager : MonoBehaviour
{

    public float slowFactor = 0.1f;
    public float slowLength = 2f;
    private float startingTimeScale;
    private float startingFixedDeltaTime;
    void Start()
    {
        startingTimeScale = Time.timeScale;
        startingFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void timeSlowing()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void timeReturn()
    {
        Time.timeScale = startingTimeScale;
        Time.fixedDeltaTime = startingFixedDeltaTime * slowFactor;
    }
}
