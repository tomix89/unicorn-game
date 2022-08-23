using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuControl : MonoBehaviour
{

    public void OnRestartClick() {
         SceneManager.LoadScene("MainScene");
    }

    public void OnQuitClick() {
        // works only on desktop release ?
        Application.Quit();
    }

}
