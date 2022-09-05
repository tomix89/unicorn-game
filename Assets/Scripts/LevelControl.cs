using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour {

    [SerializeField]
    private Text text_holder;
    [SerializeField]
    private Image smilie_holder;
    [SerializeField]
    private Sprite smilie_sad;
    [SerializeField]
    private Sprite smilie_happy;

    // when the scene loads we need to read out the last state
    private void Start() {
        if (GameInfoHolder.wasLastLevelSucess) {
            smilie_holder.sprite = smilie_happy;
            text_holder.text = "Well Done!";
            text_holder.color = Color.green;
        } else {
            smilie_holder.sprite = smilie_sad;
            text_holder.text = "Game Over";
            text_holder.color = Color.red;
        }
    }


    public void OnRestartClick() {
        // restarts the CURRENT level
        string lvl = GameInfoHolder.lastLevelName;

        Debug.Log(lvl);
        SceneManager.LoadScene(lvl);
    }

    public void OnQuitClick() {
        Debug.Log("Quit requested");
        // works only on desktop release ?
        Application.Quit();
    }

}
