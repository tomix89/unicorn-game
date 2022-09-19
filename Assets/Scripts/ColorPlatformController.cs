using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPlatformController : MonoBehaviour
{
    public DiamondCountController.DiamondType type;

    // Start is called before the first frame update
    void Start()
    {
        DiamondCountController.OnDiamondCollected += OnDiamondCollected;

        // on start the platforms are almost transparent
        Color clr = DiamondCountController.colorDict[type];
        clr.a = 0.15f;
        GetComponent<SpriteRenderer>().color = clr;
    }

   
    private void OnDiamondCollected(DiamondCountController.DiamondType allOwned) {
        if (allOwned.HasFlag(type)) {
            // make the color a bit darker - to mimic the diamond surface
            Color clr = DiamondCountController.colorDict[type];
            clr.r *= 0.75f;
            clr.g *= 0.75f;
            clr.b *= 0.75f;
            GetComponent<SpriteRenderer>().color = clr;

            // enable the collider
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}
