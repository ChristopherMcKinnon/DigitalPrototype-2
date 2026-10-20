using UnityEngine;

public class KillPlayer : MonoBehaviour
{

    [SerializeField] private GameObject Player;

    private PlayerMovement PlayerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == Player)
        {
            col.gameObject.transform.position = new Vector2(-5.5f, 0f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
