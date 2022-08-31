using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    enum AnimationType {
        IDLE,
        WALKING,
        JUMP,
        JUMP_WALK,
    }

    private Animator animator;
    private AnimationType animatroCurrentState = AnimationType.IDLE; // this needsa to be the default in the Animator itself
    private AudioManager audioManager;
    private CharacterController2D controller;
    private Rigidbody2D _rigidbody2D;

    float walkSpeed = 40;
    float hMoveAmount = 0;
    bool jumpRequest = false;


    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        audioManager = GetComponent<AudioManager>();
        controller = GetComponent<CharacterController2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        AppleCountController.OnAppleCountChanged += OnAppleCountChanged;

        // just to set the default
        OnAppleCountChanged(0);
    }

    void OnAppleCountChanged(int count) {

        //    print(count);

        switch (count) {
            case 0:
                controller.setJumpForce(350);
                break;

            case 1:
                controller.setJumpForce(420);
                break;

            case 2:
                controller.setJumpForce(500);
                break;

            case 3:
                controller.setJumpForce(650);
                break;

        }

    }

    // Update is called once per frame
    void Update() {

        hMoveAmount = Input.GetAxisRaw("Horizontal") * walkSpeed;
        if (Input.GetButtonDown("Jump")) {
            jumpRequest = true;
        }

        // if we are below ground, we fell off the cliff
        if (transform.position.y < -16.5) {
            GameInfoHolder.setLevelResult(SceneManager.GetActiveScene().name, false);
            SceneManager.LoadScene("LevelEnded_Scene");
        }
    }


    private void FixedUpdate() {

        if (jumpRequest && controller.isGrounded()) {
            changeAnimation(AnimationType.JUMP);
        } else if ((Mathf.Abs(hMoveAmount) > 0.1) && controller.isGrounded()) {
            changeAnimation(AnimationType.WALKING);
        } else if (controller.isGrounded()) {
            changeAnimation(AnimationType.IDLE);
        } else if ((Mathf.Abs(hMoveAmount) > 0.1)
            && !controller.isGrounded()
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {
            changeAnimation(AnimationType.JUMP_WALK);
        }


        controller.Move(hMoveAmount * Time.fixedDeltaTime, jumpRequest);
        jumpRequest = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // layer 8 is 'Enemy'
        if (collision.gameObject.layer == 8) {

            controller.AirControl = false;
            Invoke("reActivateAirControl", 0.75f);




            int collisionDir = (collision.contacts[0].normal.x > 0) ? +1 : -1;
            //   _rigidbody2D.AddForce(new Vector2(1.1f * collisionDir * 0.707f, 0.707f) * 7.2f, ForceMode2D.Impulse);

            _rigidbody2D.velocity = new Vector2(1.1f * collisionDir * 0.707f, 0.707f) * 15.2f;

        }

    }

    private void reActivateAirControl() {
        controller.AirControl = true;
    }


    private void changeAnimation(AnimationType anim) {

        if (animatroCurrentState == anim) {
            return;
        }
        animatroCurrentState = anim;

        string animText = "";

        switch (anim) {
            case AnimationType.IDLE:
                animText = "player_idle";
                audioManager.Stop(); // no sound for this
                break;

            case AnimationType.WALKING:
                animText = "player_walk";
                audioManager.Play("walking", true);
                break;

            case AnimationType.JUMP:
                animText = "player_jump";
                audioManager.Play("jump");
                break;

            case AnimationType.JUMP_WALK:
                animText = "player_jump_walk";
                audioManager.Stop(); // no sound for this
                break;
        }

        animator.Play(animText);
    }

}
