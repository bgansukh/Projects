using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    private ChampionStats stats;
    private float lastAttackTime = -Mathf.Infinity;

    private void Start()
    {
        stats = GetComponent<ChampionStats>();
    }

    private void Update()
    {
        if (Time.time >= lastAttackTime + stats.attackCooldown)
        {
            GameObject target = FindNearestEnemy();
            if (target != null)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance <= stats.attackRange)
                {
                    Attack(target);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }
        }

        return closest;
    }

    void Attack(GameObject target)
    {
        if (target.TryGetComponent(out EnemyHealth eh))
        {
            eh.TakeDamage(stats.attackDamage);
            Debug.Log($"{gameObject.name} auto-attacked {target.name} for {stats.attackDamage} damage");
        }
    }
}
