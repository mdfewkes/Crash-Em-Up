using UnityEngine;

public class HeavyEnemy : EnemyBase {
    [SerializeField] private EnemyAction fuAttack;
    [SerializeField] private EnemyAction ffAttack;
    [SerializeField] private EnemyAction fdAttack;
    [SerializeField] private EnemyAction bAttack;
    
    private PlayerController player;

    new void Start() {
		base.Start();

        player = FindAnyObjectByType<PlayerController>();
    }

    protected override void MoveState() {
        base.MoveState();

        FindAction();
    }

    private void FindAction() {
        if (player == null) return;

        Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.z - transform.position.z).normalized;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        float forwardProduct = Vector2.Dot(Vector2.right, direction);
        float upProduct = Vector2.Dot(Vector2.up, direction);

        if (forwardProduct > 0.0f && distance <= 8.0f) {
            if (forwardProduct > 0.98) RequestStartActionState(ffAttack);
            else if (forwardProduct > 0.93 && upProduct > 0.0f) RequestStartActionState(fuAttack);
            else if (forwardProduct > 0.93 && upProduct < 0.0f) RequestStartActionState(fdAttack);
        } else if (forwardProduct < -0.98 && distance <= 4.0f) RequestStartActionState(bAttack);
    }
}
