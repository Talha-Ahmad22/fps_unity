using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pausePanel;
    public Button resumeButton;
    public Button exitButton;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);

        resumeButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(ExitToMainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Make sure time resumes in main menu
        SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene name
    }
}
