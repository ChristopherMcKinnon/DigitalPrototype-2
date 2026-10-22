using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] public float blastSpeed;
    private PlayerMovement playerMovement;
    private GameObject CameraObj;
    private Camera Camera;

    [SerializeField] private AudioClip explosion;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        playerMovement = Player.GetComponent<PlayerMovement>();
        CameraObj = GameObject.FindWithTag("MainCamera");
        Camera = CameraObj.GetComponent<Camera>();
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
            Debug.Log(playerMovement.rb.linearVelocity.y);
            float vely = playerMovement.rb.linearVelocity.y;
            playerMovement.allowJump = true;
            //playerMovement.rb.linearVelocity = new Vector2(playerMovement.rb.linearVelocity.x, 0);
            if (vely > 0)
            {
                blastSpeed = 40f;
            }
            else if (vely == 0)
            {
                blastSpeed = 300f;
            }
            else
            {
                blastSpeed = 400f;
            }
            Debug.Log(blastSpeed);
            playerMovement.rb.linearVelocity += new Vector2(30f, blastSpeed);
            Destroy(gameObject);

            SFXManager.Instance.SFX(explosion, 0.7f, explosion.length);
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
