using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject Player;
    [SerializeField] private int minDistance;
    [SerializeField] private int maxDistance;
    [SerializeField] private int minHeight;
    [SerializeField] private int maxHeight;
    [SerializeField] private int minScale;
    [SerializeField] private int maxScale;
    private PlayerMovement PlayerMovement;
    
    void Awake()
    {
        PlayerMovement = Player.GetComponent<PlayerMovement>();
    }
    void Start()
    {
        SpawnBlocks(5);
    }
    public void SpawnBlocks(int number)
    {
        // Init starting block

        GameObject startingBlock = Instantiate(blockPrefab, new Vector2(-8f, -2f), Quaternion.identity);
        startingBlock.transform.localScale = new Vector2(5, 1);


        GameObject currentBlock = startingBlock;

        // Loop through each block
        for (int i = 0; i < number - 1; i++)
        {
            
            Vector2 blockDistance = new Vector2(
                currentBlock.transform.position.x + 
                currentBlock.transform.localScale.x +
                Random.Range(minDistance, maxDistance),
                Random.Range(minHeight, maxHeight)
                );

            Vector2 blockScale = new Vector2(Random.Range(minScale, maxScale), 1f);
            GameObject newBlock = Instantiate(blockPrefab, blockDistance, Quaternion.identity);
            newBlock.transform.localScale = blockScale;
            currentBlock = newBlock;
        }
    }
}
