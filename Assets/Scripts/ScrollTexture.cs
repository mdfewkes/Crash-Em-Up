using UnityEngine;

public class ScrollTexture : MonoBehaviour {
	public Material material;
	public Vector2 scrollSpeed;
    private Vector2 startingOffset;

	void Start() {
		if (material == null) { 
            material = GetComponent<Renderer>()?.material;
            startingOffset = material.mainTextureOffset;
    	}
		if (material == null) {
			this.enabled = false;
		}

	}

	void Update() {
		if (!material) return;
        Vector2 offset = material.mainTextureOffset;
		offset = offset + scrollSpeed * Time.deltaTime;
		offset = new Vector2 (offset.x % 1.0f, offset.y % 1.0f);

        // note: this will force unity to
        // update the .mat file on disk
		material.mainTextureOffset = offset;
	}

    void onApplicationQuit()
    {
		if (!material) return;
        // reset the offset to the previously saved value
        // to avoid github spam .mat file updates every run
        material.mainTextureOffset = startingOffset;
    }
}
