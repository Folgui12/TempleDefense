using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
