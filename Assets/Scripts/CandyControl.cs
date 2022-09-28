using UnityEngine;
using UnityEngine.SceneManagement;

public class CandyControl : MonoBehaviour
{
 
    // here player took the gem
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {

            GameInfoHolder.setLevelResult(SceneManager.GetActiveScene().name, true);
            SceneManager.LoadScene("LevelEnded_Scene");
        } 
    }


}
