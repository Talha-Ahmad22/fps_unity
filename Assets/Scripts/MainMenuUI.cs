using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    void Start()
    {
        // Optionally, you can initialize any UI elements here
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
