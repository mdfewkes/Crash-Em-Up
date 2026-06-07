using UnityEngine;

public class FastEnemy : EnemyBase {
	[SerializeField] private EnemyAction fuAttack;
	[SerializeField] private EnemyAction fdAttack;
	[SerializeField] private EnemyAction bAttack;

	protected override void MoveState() {
		base.MoveState();

		FindAction();
	}

	private void FindAction() {
		if (PlayerTag.playerTag == null) return;

		Vector2 direction = new Vector2(PlayerTag.playerTag.transform.position.x - transform.position.x, PlayerTag.playerTag.transform.position.z - transform.position.z).normalized;
		float distance = Vector3.Distance(transform.position, PlayerTag.playerTag.transform.position);
        float forwardProduct = Vector2.Dot(Vector2.right, direction);
		float upProduct = Vector2.Dot(Vector2.up, direction);

		if (distance < 3.0f) {
            if (forwardProduct < -0.98 && distance <= 4.0f) RequestStartActionState(bAttack);
            else if (forwardProduct > 0.0f && upProduct > 0.0f) RequestStartActionState(fuAttack);
            else if (forwardProduct > 0.0f && upProduct < 0.0f) RequestStartActionState(fdAttack);
		}
	}
}
