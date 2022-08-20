using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleCountController : MonoBehaviour {

    public delegate void AppleCountChanged(int count);
    public static event AppleCountChanged OnAppleCountChanged;


    public Image[] imageArray;
    public Sprite appleFull;
    public Sprite appleSilouette;

    private int maxApples;
    private int appleCount = 0;

    private void Start() {
        maxApples = imageArray.Length;
        updateImages();
    }

    public int getAppleCount() {
        return appleCount;
    }

    public void gainApple() {
        if (appleCount < maxApples) {
            appleCount++;
            updateImages();

            if (OnAppleCountChanged != null) {
                OnAppleCountChanged(appleCount);
            }
        } else {
            print("Max already reached, no way to add more");
        }
    }

    private void updateImages() {
        for (int i = 0; i < maxApples; i++) {

            if (i < appleCount) {
                imageArray[i].sprite = appleFull;
            } else {
                imageArray[i].sprite = appleSilouette;
            }

        }

    }



}
