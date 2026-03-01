using UnityEngine;

public class ScrollTexture : MonoBehaviour {
	public Material material;
	public Vector2 scrollSpeed;

	void Start() {
		if (material == null) { 
			material = GetComponent<Renderer>()?.material;
		}
		if (material == null) {
			this.enabled = false;
		}
	}

	void Update() {
		Vector2 offset = material.mainTextureOffset;
		offset = offset + scrollSpeed * Time.deltaTime;
		offset = new Vector2 (offset.x % 1.0f, offset.y % 1.0f);

		material.mainTextureOffset = offset;
	}
}
