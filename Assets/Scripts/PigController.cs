using UnityEngine;

public class PigController : MonoBehaviour
{
    private AngryBird playerScript;
    private int countCollisionsWithPlayer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<AngryBird>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("In Pig.onCollisionEnter2D");

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Pig"))
        {
            countCollisionsWithPlayer++;
            if (countCollisionsWithPlayer == 1)
            {
                GameManager.instance.incScore();
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.red;
            }
        }

    }

}
