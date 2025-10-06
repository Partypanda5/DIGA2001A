using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // This line is part of a "Singleton pattern" which ensures there is only ONE GameManager in the game that can be accessed from any script.
    public int playerScore = 0;
    public bool isGamePaused = false;

    void Awake()
    {
        // Ensure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        playerScore += value;
        Debug.Log("Score: " + playerScore);
    }
    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            Debug.Log("Game Paused");
        }
        else
        {
            Time.timeScale = 1f;
            Debug.Log("Game Resumed");
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}