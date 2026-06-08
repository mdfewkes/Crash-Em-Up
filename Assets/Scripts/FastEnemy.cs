using UnityEngine;

public class FastEnemy : EnemyBase {
	[SerializeField] private EnemyAction fuAttack;
	[SerializeField] private EnemyAction fdAttack;
	[SerializeField] private EnemyAction bAttack;
	[SerializeField] private EnemyAction buAttack;
	[SerializeField] private EnemyAction bdAttack;
	[SerializeField] private EnemyAction bsuAttack;
	[SerializeField] private EnemyAction bsdAttack;

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

		if (distance < 4.0f) {
            if (forwardProduct < -0.98 && distance <= 4.0f) RequestStartActionState(bAttack);
            else if (forwardProduct > 0.0f && upProduct > 0.0f) RequestStartActionState(fuAttack);
            else if (forwardProduct > 0.0f && upProduct < 0.0f) RequestStartActionState(fdAttack);
            else if (forwardProduct < 0.0f && upProduct > 0.0f && upProduct < 0.7f) RequestStartActionState(buAttack);
            else if (forwardProduct < 0.0f && upProduct < 0.0f && upProduct > -0.7f) RequestStartActionState(bdAttack);
            else if (forwardProduct < 0.0f && upProduct > 0.0f) RequestStartActionState(bsuAttack);
            else if (forwardProduct < 0.0f && upProduct < 0.0f) RequestStartActionState(bsdAttack);
		} else if (distance < 6.0f) {
            if (forwardProduct < 0.0f && upProduct > 0.0f && upProduct < 0.7f) RequestStartActionState(buAttack);
            else if (forwardProduct < 0.0f && upProduct < 0.0f && upProduct > -0.7f) RequestStartActionState(bdAttack);
            else if (forwardProduct < 0.0f && upProduct > 0.0f) RequestStartActionState(bsuAttack);
            else if (forwardProduct < 0.0f && upProduct < 0.0f) RequestStartActionState(bsdAttack);
		}
	}
}
