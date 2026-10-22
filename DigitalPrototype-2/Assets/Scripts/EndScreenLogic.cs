using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndScreenLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TotalScore;
    [SerializeField] private TextMeshProUGUI RestartText;
    private float restartCooldown = 1;
    private InputAction restart;

    void Awake()
    {
        restart = new InputAction("Restart", InputActionType.Button);
        restart.AddBinding("<keyboard>/r");
    }
    void OnEnable()
    {
        restart.Enable();
    }
    void OnDisable()
    {
        restart.Disable();
    }
    void OnDestroy()
    {
        restart.Dispose();
    }
    void Start()
    {
        
    }

    void Update()
    {
        TotalScore.text = "Total Score: " + GameMaster.topScore.ToString("n0");
        Restart();
    }
    void Restart()
    {
        if (restart.IsPressed())
        {
            restartCooldown -= Time.deltaTime;
            if (restartCooldown <= 0)
            {
                restartCooldown = 0;
                PlayerMovement.reset = true;
                SceneManager.LoadScene("ChristopherScene");
            }
        }
        if (restart.WasReleasedThisFrame())
        {
            restartCooldown = 1;
        }
        RestartText.text = restartCooldown.ToString("f2");
    }
}
