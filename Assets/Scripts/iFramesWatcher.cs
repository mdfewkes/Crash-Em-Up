using System.Collections.Generic;
using UnityEngine;

public class iFramesWatcher : MonoBehaviour {
    [SerializeField] private List<GameObject> objectsToToggleOniFrames;
    private bool iFramesActive = false;
    private CharacterBase character = null;

	void Start() {
        character = GetComponent<CharacterBase>();
        if (character == null) character = GetComponentInParent<CharacterBase>();
        if (character == null) enabled = false;
	}

	void Update() {
        if (character.ActiveiFrames() != iFramesActive) {
            iFramesActive = character.ActiveiFrames();
            foreach (var go in objectsToToggleOniFrames) {
                go.SetActive(!go.activeSelf);
            }
        }
    }
}
