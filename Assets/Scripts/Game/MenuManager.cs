using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        _gameManager.ToggleMenu();
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void PlayAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

}
