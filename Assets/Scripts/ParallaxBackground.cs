using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {
    private Vector2 startpos;
    public Camera cam;
    [SerializeField] private float effectDepth;
    public bool fixedYpos;


    // Start is called before the first frame update
    void Start() {
      //  length = GetComponent<SpriteRenderer>().bounds.size.x;
        startpos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate() {
        float temp = cam.transform.position.x * (1 - effectDepth);
        float dist = cam.transform.position.x * effectDepth;
        transform.position = new Vector3(startpos.x + dist, fixedYpos ? startpos.y : transform.position.y, transform.position.z);

       // if (temp > startpos + length) startpos += length;
      //  else if (temp < startpos + length) startpos -= length;
    }
}
