using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
