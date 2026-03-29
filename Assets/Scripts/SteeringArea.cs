using System;
using System.Collections.Generic;
using UnityEngine;

public class SteeringArea : MonoBehaviour {
    public static List<SteeringArea> steeringAreas;

    public float radius = 4.0f;
    public float pullStrength = -1.0f;
    public AnimationCurve curve;

	void OnEnable() {
		if (steeringAreas == null) steeringAreas = new List<SteeringArea>();

        steeringAreas.Add(this);
	}

	void OnDisable() {
		steeringAreas.Remove(this);
	}

	public Vector2 GetPull(Vector3 target)
    {
        float distance = Vector3.Distance(transform.position, target);
        if (distance > radius) return Vector2.zero;

        float steerStrength = curve.Evaluate((radius - distance) / radius);
        Vector2 steerVector = new Vector2(transform.position.x - target.x, transform.position.z - target.z);
        steerVector = steerVector.normalized * steerStrength * pullStrength;

        // Vector3 tempSteer = new Vector3(transform.position.x + steerVector.x, transform.position.y, transform.position.z + steerVector.y);
        // Debug.DrawLine(transform.position, tempSteer, Color.black, 0.1f, false);

        return steerVector;
    }

	void OnDrawGizmosSelected() {
		Gizmos.color = pullStrength > 0 ? Color.blue : Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
	}

}
