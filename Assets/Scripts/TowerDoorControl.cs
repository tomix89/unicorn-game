using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDoorControl : MonoBehaviour
{
    [SerializeField] private GameObject m_key;
    private bool openingAlreadyStarted = false;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    bool AnimatorIsPlaying() {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player") {
            // check if key was collected, and the animation was not started already
            if (!m_key.activeSelf && !openingAlreadyStarted) {
                openingAlreadyStarted = true;
                GetComponent<AudioSource>().Play();
                animator.Play("tower_door_opens");

                // start polling for animation ended
                StartCoroutine(Deactivate(0.2f));
            }
        }
    }

    // make the door disappear a bit later to be able to play the sound and animation
    IEnumerator Deactivate(float time) {
        yield return new WaitForSeconds(time);
        // Code to execute after the delay

        if (AnimatorIsPlaying()) {
            StartCoroutine(Deactivate(0.2f));
        } else {
            this.gameObject.SetActive(false);
        }
    }

}
