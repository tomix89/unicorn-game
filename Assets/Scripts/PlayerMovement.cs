using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;

    enum AnimationType {
        IDLE,
        WALKING,
        JUMP,
        JUMP_WALK,
    }

    Animator animator;
    AnimationType animatroCurrentState = AnimationType.IDLE; // this needsa to be the default in the Animator itself

    float walkSpeed = 40;
    float hMoveAmount = 0;
    bool jumpRequest = false;


    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    //    print(animator);

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



    private void changeAnimation(AnimationType anim) {

        if (animatroCurrentState == anim) {
            return;
        }
        animatroCurrentState = anim;

      //  print(anim);

        string animText = "";

        switch (anim) {
            case AnimationType.IDLE:
                animText = "player_idle";
                break;

            case AnimationType.WALKING:
                animText = "player_walk";
                break;

            case AnimationType.JUMP:
                animText = "player_jump";
                GetComponent<AudioSource>().Play();
                break;

            case AnimationType.JUMP_WALK:
                animText = "player_jump_walk";
                break;
        }

        animator.Play(animText);
    }

}
