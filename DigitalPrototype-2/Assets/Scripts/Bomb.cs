using UnityEngine;

public class Bomb : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] public float blastSpeed;
    private readonly ContactPoint2D[] contacts = new ContactPoint2D[8];
    private PlayerMovement playerMovement;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        playerMovement = Player.GetComponent<PlayerMovement>();
    }
    // Calculate normal force and check if it points down, if so reverse the y

    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerMovement.rb.linearVelocity.y > blastSpeed)
        {
            playerMovement.rb.linearVelocity = new Vector2(playerMovement.rb.linearVelocity.x, blastSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Player)
        {
            /*
            Vector2 pos = transform.position;
            Vector2 playerPos = Player.transform.position;

            Vector2 direction = Vector2.Normalize(playerPos - pos);
            Vector2 blastForce = direction * blastSpeed;
            Debug.Log(blastForce);
            playerMovement.rb.linearVelocity = Vector2.zero;
            playerMovement.rb.angularVelocity = 0f;
            playerMovement.rb.AddForce(blastForce, ForceMode2D.Impulse);
            */

            playerMovement.rb.linearVelocity += Vector2.up * blastSpeed;
            Destroy(gameObject);
        }

        /*
        if (col.contactCount > 0)
        {
            ContactPoint2D contact = col.GetContact(0);
            Vector2 normal = contact.normal;

            Debug.Log(normal);
        }
        */
    }
}
