using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RainDropControl : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collision) {
		// layer 8 is enemy
		if (collision.gameObject.layer >= 6)
			{
			// Put a particle effect here
			Destroy(gameObject);
		}
	}
}