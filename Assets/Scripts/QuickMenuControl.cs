using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickMenuControl : MonoBehaviour {

    public void OnRestartClick() {
        // restarts the CURRENT level
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitClick() {
        Debug.Log("Quit requested");
        // works only on desktop release ?
        Application.Quit();
    }

}
