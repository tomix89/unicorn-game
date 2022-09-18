using UnityEngine;

public class DevilController : MonoBehaviour {

    public GameObject[] fireSystem;

    public GameObject L_border;
    public GameObject M_border;
    public GameObject R_border;
    public GameObject player;

    public GameObject rainCloud;

    public GameObject fire_water_sound; // i know this is shitty and i shall use audio manager as on player...

    float walkSpeed = 22;
    float direction = 1; // +-1
    float hMoveAmount = 0;
    bool isPlayerInRange = false;
    private CharacterController2D controller;

    float lastCollisionTime = 0;

    private float scalerReduction = 0.033f;
    private float sizeScaler = 1.8f;
    private bool isBig = true;

    private bool fadeRainCloud = false;


    Vector3 devilOrigSize;
    Vector3[] fireSystemOrigSize;

    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController2D>();

        devilOrigSize = transform.localScale;
        fireSystemOrigSize = new Vector3[fireSystem.Length];

        for (int i = 0; i < fireSystem.Length; i++) {
            fireSystemOrigSize[i] = fireSystem[i].transform.localScale;
        }

        isBig = true;
        scaleTo(sizeScaler, false);
    }


    private void scaleTo(float scaleFactor, bool fireOnly) {

        // if we change scale it is important to check the sign,
        // other ways the sprite ends up walking backwards 
        if (fireOnly == false) {


            transform.localScale = devilOrigSize * scaleFactor;
        }

        for (int i = 0; i < fireSystem.Length; i++) {
            fireSystem[i].transform.localScale = fireSystemOrigSize[i] * scaleFactor;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision) {
        // layer 6 is 'Player'
        if (collision.gameObject.layer == 6) {
            lastCollisionTime = Time.realtimeSinceStartup;
        }

    }

    // Update is called once per frame
    void Update() {
        GameObject farBorder = isBig ? M_border : L_border;

        isPlayerInRange = player.transform.position.x <= R_border.transform.position.x
            && player.transform.position.x >= farBorder.transform.position.x;

        if (!isPlayerInRange) {
            // needs to test also direction, because of movement smoothing
            if ((transform.position.x >= R_border.transform.position.x) && (direction > 0)) {
                direction = -1; // change direction upon reaching the border
                                //    print("changing dir -1");
            }

            if ((transform.position.x <= farBorder.transform.position.x) && (direction < 0)) {
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
        // auto movement 
        controller.Move(hMoveAmount * Time.fixedDeltaTime, false);


        // rainbow fadeout
        if (fadeRainCloud) {
            SpriteRenderer sr = rainCloud.gameObject.GetComponent<SpriteRenderer>();
            Color clr = sr.color;

            if (sr.color.a > 0) {

              //  print("a: " + clr.a);

                clr.a -= Time.deltaTime * 0.25f;

              //  print("aaa: " + clr.a);

                if (clr.a < 0) {
                    Destroy(rainCloud);
                    fadeRainCloud = false;
                } else {
                    sr.color = clr;
                }
            }
       
        }

    }

    private float lastSound = 0;
    private float soundRate = 0.75f;

    private void OnTriggerEnter2D(Collider2D collision) {
       // print("hit by: " + collision.gameObject.name);

        if (collision.gameObject.name.StartsWith("rainDrop") && isBig) {
            sizeScaler -= scalerReduction;

            if (Time.time > (soundRate + lastSound)) {
                lastSound = Time.time;
                AudioClip ac = fire_water_sound.GetComponent<AudioSource>().clip;
                fire_water_sound.GetComponent<AudioSource>().PlayOneShot(ac, 1);
            }

           // print("sizeScaler: " + sizeScaler);

            if (sizeScaler < 0.2) {

                // stop the fire
                for (int i = 0; i < fireSystem.Length; i++) {
                    fireSystem[i].GetComponent<ParticleSystem>().Stop();
                }

                isBig = false;
                GetComponent<AudioSource>().Stop();

                rainCloud.gameObject.GetComponent<BoxCollider2D>().enabled = false; // stop emiting projectiles
                fadeRainCloud = true;

                sizeScaler = 1;
                scaleTo(sizeScaler, false);
            }

            scaleTo(sizeScaler, true);
        }
    }


}
