using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour {
    [SerializeField] private AudioSource audioSourcePrefab;

    [SerializeField] private List<AudioClip> hitClips;
    [SerializeField] private List<AudioClip> attackClips;

	private DamageArea[] damageAreas;
    private Queue<ImpactData> impactDataQueue = new Queue<ImpactData>();

	void Start() {
		damageAreas = gameObject.GetComponentsInChildren<DamageArea>();
		foreach (DamageArea damageArea in damageAreas) {
			damageArea.OnCollision += ReceiveImpactData;
		}
	}

	void OnDestroy() {
		foreach (DamageArea damageArea in damageAreas) {
			damageArea.OnCollision -= ReceiveImpactData;
		}
	}

    private void ReceiveImpactData(ImpactData impactData) {
		impactDataQueue.Enqueue(impactData);
	}

	void Update() {
		if (impactDataQueue.Count > 0) {
            float largestMag = 0.0f;
            bool isAttack = false;
            foreach(ImpactData data in impactDataQueue) {
                if (data.hitMagnitude > largestMag) {
                    largestMag = data.hitMagnitude;
                }
                if (data.isAttackDamage) isAttack = true;
            }

            AudioSource source = Instantiate(audioSourcePrefab);
            source.transform.position = transform.position;
            source.volume *= Random.Range(0.8f, 1.2f);
            source.pitch *= Random.Range(0.8f, 1.2f);
            if (isAttack) {
                source.clip = attackClips[Random.Range(0, attackClips.Count)];
            } else {
                source.clip = hitClips[Random.Range(0, hitClips.Count)];
            }

            source.Play();
            impactDataQueue.Clear();
        }
	}
}
