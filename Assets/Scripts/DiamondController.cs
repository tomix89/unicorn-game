using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    [SerializeField] private DiamondCountController m_diamondCountController;

    public DiamondCountController.DiamondType type;


    private void Start() {
        // set the color based on the type
        GetComponent<SpriteRenderer>().color = DiamondCountController.colorDict[type];
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.name == "Player") {
            // apple have been captured 

            m_diamondCountController.diamondCollected(type); // this triggers the sound
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
