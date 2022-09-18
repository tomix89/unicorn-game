using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskController : MonoBehaviour {

    private ParticleSystem _particleSystem = null;
    private SpriteRenderer _spriteRenderer = null;
    public GameObject player;

    // Start is called before the first frame update
    void Start() {
        _particleSystem = GetComponent<ParticleSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.color = Color.green;
        ParticleSystem.MainModule main = _particleSystem.main;
        main.startColor = Color.green;
    }

    // ugly polling 
        private void Update() {
        // when all th particles dye off, disable the whole gameobject
        // which also stops the updates
        if (_particleSystem.isStopped) {

            // re- enable if we are far enough
            float dist = Vector3.Distance(player.transform.position, transform.position);
            print(dist);
            if (dist > 5) {

                if (_spriteRenderer.color == Color.green) {
                    _spriteRenderer.color = Color.yellow;

                    ParticleSystem.MainModule main = _particleSystem.main;
                    main.startColor = Color.yellow; 
                } else {
                    _spriteRenderer.color = Color.green;
                    ParticleSystem.MainModule main = _particleSystem.main;
                    main.startColor = Color.green;
                }

                // re-enable with different color
                GetComponent<Collider2D>().enabled = true;
                _spriteRenderer.enabled = true;
                _particleSystem.Play(true);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        // layer 6 is 'Player'
        if (collision.gameObject.layer == 6) {
            GetComponent<Collider2D>().enabled = false;
            _spriteRenderer.enabled = false;
            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }


}
