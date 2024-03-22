using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
