
using UnityEngine;

public class TriggerForwarder : MonoBehaviour
{
    public GemControl gemControl;

    private void OnTriggerEnter2D(Collider2D collision) {
        gemControl.OnChildTriggerEnter2D(collision);
    }
}
