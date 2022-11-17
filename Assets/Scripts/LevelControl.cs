using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelControl : MonoBehaviour {

    [SerializeField]
    private Text text_holder;
    [SerializeField]
    private Text text_countdown;
    [SerializeField]
    private Image smilie_holder;
    [SerializeField]
    private Sprite smilie_sad;
    [SerializeField]
    private Sprite smilie_happy;

    const string countdownText_next = "Next level in:";
    const string countdownText_reset = "Restarting level in:";
    int countdownTime_s = 5;

    // when the scene loads we need to read out the last state
    private void Start() {
        countdownTime_s = 5;
        if (GameInfoHolder.wasLastLevelSucess) {
            smilie_holder.sprite = smilie_happy;
            text_holder.text = "Well Done!";
            text_holder.color = Color.green;
        } else {
            smilie_holder.sprite = smilie_sad;
            text_holder.text = "Game Over";
            text_holder.color = Color.red;
        }
        StartCoroutine("handleTimer");
    }

    bool willGoNextLevel() {
        if (GameInfoHolder.wasLastLevelSucess) {

            if (GameInfoHolder.lastLevelName == "Level_02_Scene") {
                return false;
            }

            return true;
        }

        return false;
    }

    private IEnumerator handleTimer() {
        if (countdownTime_s > 0) {
            text_countdown.text = willGoNextLevel() ? countdownText_next : countdownText_reset;
        } else {
            if (willGoNextLevel()) {
                // todo we would need to have a list of levels somewhere to see which comes next and which is the last
                SceneManager.LoadScene("Level_02_Scene");
                yield break;
            } else {
                OnRestartClick();
            }
        }
               
        text_countdown.text += " " + countdownTime_s;
        countdownTime_s--;
        yield return new WaitForSeconds(1);
        StartCoroutine("handleTimer");
    }


    // keyboard shortcuts
    void Update() {

        // return is enter ...
        if (Input.GetKeyDown(KeyCode.Return)) {
            OnRestartClick();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            OnQuitClick();
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
