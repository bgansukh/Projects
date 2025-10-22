using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Dash Ability", menuName = "Abilities/Fighter/Dash")]
public class DashAbility : BaseAbility
{
    public float dashDistance = 6f;
    public float dashDuration = 0.2f;
    public float dashDamage = 20f;
    public float knockupDuration = 0.75f;
    public LayerMask enemyLayers;

    public override void Activate(GameObject caster)
    {
        if (caster.TryGetComponent(out MonoBehaviour runner))
        {
            runner.StartCoroutine(PerformDash(caster));
        }
    }

    private IEnumerator PerformDash(GameObject caster)
    {
        Camera cam = Camera.main;
        if (cam == null) yield break;

        Vector3 mouseWorldPosition = Vector3.zero;
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            mouseWorldPosition = hit.point;
        }
        else
        {
            yield break;
        }

        Vector3 dashDirection = (mouseWorldPosition - caster.transform.position).normalized;
        dashDirection.y = 0f;
        caster.transform.rotation = Quaternion.LookRotation(dashDirection);

        var agent = caster.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
        }

        float elapsed = 0f;
        Vector3 start = caster.transform.position;
        Vector3 end = start + dashDirection * dashDistance;

        HashSet<EnemyHealth> hitAlready = new HashSet<EnemyHealth>();
        bool hitEnemy = false;

        while (elapsed < dashDuration && !hitEnemy)
        {
            caster.transform.position = Vector3.Lerp(start, end, elapsed / dashDuration);

            Collider[] hitEnemies = Physics.OverlapSphere(caster.transform.position, 1.25f, enemyLayers);
            foreach (Collider col in hitEnemies)
            {
                if (col.TryGetComponent(out EnemyHealth eh) && !hitAlready.Contains(eh))
                {
                    eh.TakeDamage(dashDamage);
                    hitAlready.Add(eh);
                    hitEnemy = true;

                    if (col.TryGetComponent(out Rigidbody rb))
                    {
                        rb.linearVelocity = Vector3.zero;
                        rb.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);
                    }

                    break;
                }
            }

            if (!hitEnemy)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        caster.transform.position = Vector3.Lerp(start, end, Mathf.Clamp01(elapsed / dashDuration));

        if (agent != null)
        {
            agent.enabled = true;
        }
    }
}
