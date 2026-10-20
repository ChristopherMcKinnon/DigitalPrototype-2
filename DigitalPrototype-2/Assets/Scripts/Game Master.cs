using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private GameObject Player;
    private Vector2 playerPos;
    private int currentScore;
    private int topScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = Player.transform.position;
        ScoreUpdate();
    }
    void ScoreUpdate()
    {
        currentScore = (int)Mathf.Round(playerPos.x);
        if (currentScore > topScore) topScore = currentScore;
        Score.text = "Score: " + topScore.ToString();
    }
}
