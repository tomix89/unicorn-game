using UnityEngine;
using System.Collections;
 
 public class CameraController : MonoBehaviour {
    public float xMargin = 1.0f;
    public float yMargin = 1.0f;

    public float xSmooth = 1.1f;
    public float ySmooth = 1.1f;

    public Vector2 maxXAndY = new Vector2(1e6f, 1e6f);
    public Vector2 minXAndY = new Vector2(-1e6f, -1e6f);

    public Transform cameraTarget;

    // Use this for initialization
    void Awake() {
     //   cameraTarget = GameObject.FindGameObjectWithTag("CameraTarget").transform;
      //  print(cameraTarget);
    }

    bool CheckXMargin() {
        float delta = transform.position.x - cameraTarget.position.x;
        return Mathf.Abs(delta) > xMargin;
    }

    bool CheckYMargin() {
        return Mathf.Abs(transform.position.y - cameraTarget.position.y) > yMargin;
    }


    // Update is called once per frame
    void FixedUpdate() {
        TrackPlayer();
    }

    void TrackPlayer() {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

 

        if (CheckXMargin()) {
            targetX = Mathf.Lerp(transform.position.x, cameraTarget.position.x, xSmooth * Time.deltaTime);
        }

        if (CheckYMargin()) {
            targetY = Mathf.Lerp(transform.position.y, cameraTarget.position.y, ySmooth * Time.deltaTime);
        }

      //  print("targetX: " + targetX + " targetY: " + targetY);

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

      //  print("targetX: " + targetX + " targetY: " + targetY);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}