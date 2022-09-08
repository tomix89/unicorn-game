using UnityEngine;
using UnityEngine.SceneManagement;

public class GemControl : MonoBehaviour
{
    private ParticleSystem _particleSystem = null;

    void Start() {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player") {
            GameInfoHolder.setLevelResult(SceneManager.GetActiveScene().name, true);
            SceneManager.LoadScene("LevelEnded_Scene");
        }
    }

}
