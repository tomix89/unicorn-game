using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CloudController : MonoBehaviour {
    public GameObject rainDrop;
    public float rainRateOrig = 25.1f;
    public float rainWidth = 12.6f;


    private int pokesByPlayer = 0;
    private SpriteRenderer _sr = null;
    public bool isRaining = false;

    private Vector2 colliderSize;

    private void Start() {
        _sr = GetComponent<SpriteRenderer>();
        colliderSize = GetComponent<BoxCollider2D>().bounds.size;

        //  print("colliderSize: " + colliderSize);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // player is layer 6
        if (collision.gameObject.layer == 6) {

            pokesByPlayer++;
            updateColor();
        }
    }

    private void updateColor() {

        switch (pokesByPlayer) {
            // case 0 #BFC7E3

            case 1:
                Color clr1;
                ColorUtility.TryParseHtmlString("#9AA4CB", out clr1);
                _sr.color = clr1;
                break;

            case 2:
                Color clr2;
                ColorUtility.TryParseHtmlString("#6F7BA6", out clr2);
                _sr.color = clr2;
                break;

            case 3:
                Color clr3;
                ColorUtility.TryParseHtmlString("#4C5C8E", out clr3);
                _sr.color = clr3;
                isRaining = true;
                break;
        }

    }


    private void FixedUpdate() {

        if (isRaining) {
            float rainRateScaled = rainRateOrig * Mathf.Pow(_sr.color.a, 2);
            // when we begin to fade the rainbow, stop the rain

            if (rainRateScaled < 0.5) {
                isRaining = false;
            }

            //    print("alpha: " + _sr.color.a + " rainRate: " + rainRateScaled);

            // roll the random number if it shall generate a raindrop
            float rn = Random.Range(0.0f, 100.0f);
            //   print(rn);
            if (rn < rainRateScaled) {
                Quaternion qt = Quaternion.Euler(0, 0, 0);
                Instantiate(rainDrop, new Vector3(transform.position.x + rainWidth * Random.Range(-1.0f, 1.0f), transform.position.y + colliderSize.y - 1, 0), qt);
            }
        }
    }


}
