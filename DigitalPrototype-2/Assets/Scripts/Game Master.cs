using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private AudioClip timerLow;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private GameObject Player;
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private AudioClip bell;
    private Vector2 playerPos;
    private int currentScore;
    public static int topScore;
    public float timerDuration;
    private bool isTimer = true;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = timerDuration;
        if (PlayerMovement.reset)
        {
            currentScore = 0;
            topScore = 0;
        }
        SFXManager.Instance.SFX(bell, 0.5f, bell.length);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = Player.transform.position;
        ScoreUpdate();
        TimerUpdate();
    }
    void ScoreUpdate()
    {
        currentScore = (int)Mathf.Round(playerPos.x);
        if (currentScore > topScore) topScore = currentScore;
        Score.text = "Score: " + topScore.ToString();
    }
    void TimerUpdate()
    {
        if (isTimer)
        {
            timer -= Time.deltaTime;
            Timer.text = "Time: " + timer.ToString("f1") + "s";
            if (timer < 10 && isTimer) SFXManager.Instance.SFX(timerLow, 1f, timerLow.length);
            if (timer <= 0) isTimer = false;
        }
        if (!isTimer)
        {
            timer = timerDuration;
            SceneManager.LoadScene("EndScreen");
            isTimer = true;
        }
    }
}
