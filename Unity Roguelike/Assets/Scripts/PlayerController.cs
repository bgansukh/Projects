using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public GameObject clickIndicatorPrefab;
    public LayerMask enemyLayer;

    public float attackRange = 3f;
    public float attackCooldown = 1f;
    private float lastAttackTime;
    private EnemyHealth currentTarget;

    public AbilityController abilityController;

    void Update()
    {
        // Stop movement on 'S' key
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            agent.ResetPath();
            currentTarget = null;
        }

        // Right-click to move or target enemy
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    currentTarget = hit.collider.GetComponent<EnemyHealth>();
                    if (currentTarget != null)
                    {
                        agent.SetDestination(currentTarget.transform.position);
                    }
                }
                else
                {
                    currentTarget = null;
                    agent.SetDestination(hit.point);
                }

                if (clickIndicatorPrefab != null && !hit.collider.CompareTag("Enemy"))
                {
                    Quaternion rotation = Quaternion.Euler(90, 0, 0);
                    GameObject marker = Instantiate(clickIndicatorPrefab, hit.point + Vector3.up * 0.01f, rotation);
                    Destroy(marker, 0.1f);
                }
            }
        }

        // Auto-attack logic
        if (currentTarget != null)
        {
            float distance = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (distance > attackRange)
            {
                agent.SetDestination(currentTarget.transform.position);
            }
            else
            {
                agent.ResetPath();
                transform.LookAt(currentTarget.transform);

                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    currentTarget.TakeDamage(10f);
                    lastAttackTime = Time.time;
                }
            }
        }
        // Hold right click to move freely
        if (Mouse.current.rightButton.isPressed && currentTarget == null)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit moveHit) && !moveHit.collider.CompareTag("Enemy"))
            {
                agent.SetDestination(moveHit.point);
            }
        }

    }

    public EnemyHealth GetTarget()
    {
        return currentTarget;
    }

    public void ClearTarget()
    {
        currentTarget = null;
    }
}
