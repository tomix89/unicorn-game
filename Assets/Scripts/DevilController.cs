using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilController : MonoBehaviour
{

    public GameObject L_border;
    public GameObject R_border;

    float walkSpeed = 42;
    float direction = 1; // +-1
    float hMoveAmount = 0;
    private CharacterController2D controller;
    float randomMovementSwapChance = 0.2f; // 1=100% 0.01= 1%  (base is 1sec)
    float randomMoveTimeAccu = 0; // acumulates the delta times to be able to roll on given time intervals

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update() {

        // needs to test also direction, because of movement smoothing
        if ((transform.position.x >= R_border.transform.position.x) && (direction > 0)) {
            direction = -1; // change direction upon reaching the border
        //    print("changing dir -1");
        }

        if ((transform.position.x <= L_border.transform.position.x) && (direction < 0)) {
            direction = 1; // change direction upon reaching the border
         //   print("changing dir +1");
        }

        hMoveAmount = direction * walkSpeed;
    }


    private void FixedUpdate() {

        randomMoveTimeAccu += Time.deltaTime;

        if (randomMoveTimeAccu > 1.0f) {
            randomMoveTimeAccu = 0;

            float rnd = Random.Range(0.0f, 1.0f);

         //   print("rolled: " + rnd);

            // in some rare cases swap direction on random
            if (rnd < randomMovementSwapChance) {
              //  print("swapping");
                direction *= -1;
            }
        }
        

        controller.Move(hMoveAmount * Time.fixedDeltaTime, false);
    }
}
