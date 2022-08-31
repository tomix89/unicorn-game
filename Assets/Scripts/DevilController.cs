using UnityEngine;

public class DevilController : MonoBehaviour
{

    public GameObject L_border;
    public GameObject R_border;
    public GameObject player;

    float walkSpeed = 22;
    float direction = 1; // +-1
    float hMoveAmount = 0;
    bool isPlayerInRange = false;
    private CharacterController2D controller;

    float lastCollisionTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        // layer 6 is 'Player'
        if (collision.gameObject.layer == 6) {
            lastCollisionTime = Time.realtimeSinceStartup;
        }

    }

    // Update is called once per frame
    void Update() {

        isPlayerInRange = player.transform.position.x <= R_border.transform.position.x
            && player.transform.position.x >= L_border.transform.position.x;

        if (!isPlayerInRange) {
            // needs to test also direction, because of movement smoothing
            if ((transform.position.x >= R_border.transform.position.x) && (direction > 0)) {
                direction = -1; // change direction upon reaching the border
                                //    print("changing dir -1");
            }

            if ((transform.position.x <= L_border.transform.position.x) && (direction < 0)) {
                direction = 1; // change direction upon reaching the border
                               //   print("changing dir +1");
            }
        } else {
            direction = ((transform.position.x - player.transform.position.x) < 0) ? +1 : -1;
        }

        // clip to the maximums is not needed, because the isPlayerInRange already creates the borders

        // if player is in range move faster
        hMoveAmount = direction * walkSpeed * (isPlayerInRange ? 2 : 1);

        // slow down movement after throwing player aside
        if (Time.realtimeSinceStartup - lastCollisionTime < 0.75f) {
            hMoveAmount *= 0.51f;
        }

    }


    private void FixedUpdate() {
        controller.Move(hMoveAmount * Time.fixedDeltaTime, false);
    }

}
