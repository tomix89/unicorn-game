using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RainbowController : MonoBehaviour
{
    public GameObject player; // we need the position

    public GameObject projectilePrefab;
    public GameObject target;

    // Start is called before the first frame update
    void Start() {
        // set the target to the prefab 
        projectilePrefab.GetComponent<HomingMissile>().target = target.transform;
    }


    private float lastShot = 0;
    private float fireRate = 0.25f;

    private void OnTriggerStay2D(Collider2D collision) {
        // player is layer 6
        if (collision.gameObject.layer == 6) {


          //  print("--------");

            if (Time.time > (fireRate + lastShot)) {

                // Quaternion qt = Quaternion.FromToRotation(player.transform.position, target.transform.position);


                Quaternion qt = Quaternion.Euler(0, 0, 135 + Vector2.Angle(player.transform.position, target.transform.position));

            //    print(lastShot);

                Instantiate(projectilePrefab, new Vector3(player.transform.position.x, player.transform.position.y, 0), qt);
                lastShot = Time.time;
            }
        }
    }


}
