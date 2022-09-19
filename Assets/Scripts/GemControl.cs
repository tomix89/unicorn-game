using UnityEngine;
using UnityEngine.SceneManagement;

public class GemControl : MonoBehaviour
{
    private ParticleSystem _particleSystem = null;
    public GameObject rainbow_comp;
    public GameObject devil;
    public GameObject player;
    public CameraController camController;
    public DevilController devilController;

    private SpriteRenderer devilRenderer;

    private Transform rainbowTrans;
    public bool animateRainbow = false;
    public bool purgeDevil = false;


    void Start() {
        _particleSystem = GetComponent<ParticleSystem>();
        rainbowTrans = rainbow_comp.GetComponent<Transform>();
        devilRenderer = devil.GetComponent <SpriteRenderer>();
        devilController = devil.GetComponent<DevilController>();
    }

    // here player took the gem
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {

            // stop player movement - in the air
            player.GetComponent<Rigidbody2D>().simulated = false;
            // devil will try to escape
            devilController.runAway = true;
            // move the cam to the devil
            camController.cameraTarget = devil.transform;
            animateRainbow = true;
        } 
    }

    // rainbow hit the devil
    public void OnChildTriggerEnter2D(Collider2D collision) {

       // print(collision.gameObject.name);
       // print(collision.name);

        if (collision.gameObject.name == "devil_slice") {
            if (purgeDevil == false) {

                Invoke("endLevel", 2.1f); // start the end
                purgeDevil = true;
            }
        }
    }

    const float sizeScale = 1.2f;
    const float devilColorScale = 1.2f;
    private void FixedUpdate() {
        if (animateRainbow) {
            rainbowTrans.localScale *= (1 + sizeScale * Time.deltaTime);
        }

        if (purgeDevil && devilRenderer.color.r != 0) {
            Color color = devilRenderer.color;
            color *= (1 - (devilColorScale * Time.deltaTime));
            devilRenderer.color = color;
        }
    }


    private void endLevel() {
        GameInfoHolder.setLevelResult(SceneManager.GetActiveScene().name, true);
        SceneManager.LoadScene("LevelEnded_Scene");
    }


}
