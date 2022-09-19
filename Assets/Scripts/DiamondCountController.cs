using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondCountController : MonoBehaviour {

    [Flags]
    public enum DiamondType {
        NONE = 0,
        RED = (1 << 0),
        ORANGE = (1 << 1),
        YELLOW = (1 << 2),
        GREEN = (1 << 3),
        BLUE = (1 << 4)
    }

    public static Dictionary<DiamondType, Color> colorDict = new Dictionary<DiamondType, Color> {
      {DiamondType.RED, Color.red},
      {DiamondType.ORANGE, new Color(1, 0.5f, 0)},
      {DiamondType.YELLOW, Color.yellow},
      {DiamondType.GREEN, Color.green},
      {DiamondType.BLUE, new Color(0, 0.5f, 1)}
};

    public delegate void DiamondCollected(DiamondType allOwned);
    public static event DiamondCollected OnDiamondCollected;

    public Image[] imageArray;

    private int maxDiamonds;
    private DiamondType diamondsOwned = DiamondType.NONE;

    private void Start() {
        maxDiamonds = imageArray.Length;
        updateImages();
    }

    public void diamondCollected(DiamondType type) {
        diamondsOwned |= type;
        updateImages();
        GetComponent<AudioSource>().Play();

        if (OnDiamondCollected != null) {
            OnDiamondCollected(diamondsOwned);
        }
    }

    private DiamondType indexToDiamondType(int i) {
        return (DiamondType)(int)(MathF.Round(Mathf.Pow(2, i)));
    }

    private void updateImages() {
        for (int i = 0; i < maxDiamonds; i++) {
            Color clr = imageArray[i].color;

            if (diamondsOwned.HasFlag(indexToDiamondType(i))) {
                clr.a = 1;
            } else {
                clr.a = 0.33f;
            }

            imageArray[i].color = clr;
        }
    }

}
