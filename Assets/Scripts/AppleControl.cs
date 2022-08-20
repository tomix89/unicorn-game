using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleControl : MonoBehaviour
{
    [SerializeField] private AppleCount m_appleCountController;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player") {
            // apple have been captured 
            this.gameObject.SetActive(false);
            m_appleCountController.gainApple();
        }
    }
}
