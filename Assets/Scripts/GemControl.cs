using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemControl : MonoBehaviour
{
    private ParticleSystem _particleSystem = null;

    void Start() {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player") {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    // ugly polling 
    private void Update() {
        // when all th particles dye off, disable the whole gameobject
        // which also stops the updates
        if (_particleSystem.isStopped) {
            this.gameObject.SetActive(false);
        }
    }
}
