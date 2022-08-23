using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDoorControl : MonoBehaviour
{
    [SerializeField] private GameObject m_key;
    private bool openingAlreadyStarted = false;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player") {
            if (!m_key.activeSelf && !openingAlreadyStarted) {
                openingAlreadyStarted = true;
                GetComponent<AudioSource>().Play();
                GetComponent<Animator>().Play("tower_door_opens");
                StartCoroutine(Deactivate(2.5f));
            }
        }
    }

    // make the apple disappear a bit later to have a better audio / video sync
    IEnumerator Deactivate(float time) {
        yield return new WaitForSeconds(time);
        // Code to execute after the delay

        this.gameObject.SetActive(false);
    }

}
