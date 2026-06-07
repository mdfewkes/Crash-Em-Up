using UnityEngine;

public class PlayerTag : MonoBehaviour {
    public static PlayerTag playerTag;

    void Awake() {
        playerTag = this;
    }

	void OnDisable() {
		if (playerTag == this) playerTag = null;
	}
}
