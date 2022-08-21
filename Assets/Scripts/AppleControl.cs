using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleControl : MonoBehaviour
{
    [SerializeField] private AppleCountController m_appleCountController;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player") {
            // apple have been captured 
     
            m_appleCountController.gainApple(); // this triggers the sound
            StartCoroutine(Deactivate(0.18f));
        }
    }

    // make the apple disappear a bit later to have a better audio / video sync
    IEnumerator Deactivate(float time) {
        yield return new WaitForSeconds(time);
        // Code to execute after the delay

        this.gameObject.SetActive(false);
    }

}
