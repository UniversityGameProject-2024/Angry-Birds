using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/**
* This component lets the player pull the ball and release it.
*/
public class AngryBird : MonoBehaviour
{
    [SerializeField] Rigidbody2D hook = null;
    [SerializeField] float releaseTime = .15f;
    [SerializeField] float maxDragDistance = 2f;

    //static public int HIGH_SCORE = 0;
    //static public int SCORE_FROM_PREV_ROUND = 0;

    private bool isMousePressed = false;
    //private bool isScore = false;
    //private int score = 0;

    private Rigidbody2D rb;

    private GameObject[] pigs;
    private void Start()
    {
        Debug.Log("In bird start");

        rb = GetComponent<Rigidbody2D>();
        //score = HIGH_SCORE = PlayerPrefs.GetInt("BasketballScore");

        pigs = GameObject.FindGameObjectsWithTag("Pig");
    }

    void Update()
    {
        if (isMousePressed)
        {
            Vector3 screenPos = Input.mousePosition;
            //screenPos.z = Camera.main.transform.position.z;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenPos);
            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            else
                rb.position = mousePos;
        }
    }
    private void OnMouseDown()
    {
        isMousePressed = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    private void OnMouseUp()
    {
        isMousePressed = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(ReleaseBall());
    }

    IEnumerator ReleaseBall()
    {
        // Wait a short time, to let the physics engine operate the spring and give some initial speed to the ball
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;
        yield return new WaitForSeconds(7);  // Waits for bird to fisish hiting the pigs
        if (GameManager.instance.getScore() < GameManager.instance.GetNumOfPigs())
        {
            //SetScore(0);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restart game
            GameManager.instance.RestartScene();
        }
        else
        {
            GameManager.instance.LoadNextScene();
        }
    }
}
