using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    // We use GameManager as a singleton    
    public static GameManager instance;
    private int score;

    private GameObject[] pigs;

    void Awake()
    {
        Debug.Log("In GameManager.Awake()");

        if (instance == null)
        {
            instance = this;
            // We set the GameManager singleton object
            // to stay alive in all scenes.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void incScore()
    {
        score++;
    }

    void Start()
    {
        //score = PlayerPrefs.GetInt("score");
        score = 1;  // First level starts with score = 1 to be easier
        pigs = GameObject.FindGameObjectsWithTag("Pig");
    }

    private void SetScore(int newScore)
    {
        score = newScore;
        //PlayerPrefs.SetInt("BasketballScore", score);
        //print("Score: " + score);
    }

    public int GetNumOfPigs()
    {
        return pigs.Length;
    }

    public int getScore()
    {
        return score;
    }

    public void RestartScene()
    {
        SetScore(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextScene()
    {
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < totalScenes - 1)
        {
            SetScore(0);
            SceneManager.LoadScene(currentSceneIndex+1);
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
        style.fontSize = 40;
        style.normal.textColor = Color.black;
        GUI.Label(new Rect(70, 0, 400, 200), "Score: " + score, style);

        if (score == pigs.Length)
        {
            style.fontSize = 40;
            style.normal.textColor = Color.yellow;
            int totalScenes = SceneManager.sceneCountInBuildSettings;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex < totalScenes-1)
            {
                GUI.Label(new Rect(400, 0, 500, 200), "Next Level!", style);
            }
            else
            {
                GUI.Label(new Rect(400, 0, 500, 200), "You Win!", style);
            }
        }
    }
}