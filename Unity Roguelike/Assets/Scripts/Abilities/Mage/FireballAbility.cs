using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Fireball Ability", menuName = "Abilities/Mage/Fireball")]
public class FireballAbility : BaseAbility
{
    public GameObject fireballPrefab;
    public float range = 6f;
    public float speed = 15f;
    public float damage = 25f;

    public override void Activate(GameObject caster)
    {
        Camera cam = Camera.main;

        if (cam == null || fireballPrefab == null)
        {
            Debug.LogWarning("Missing camera or fireball prefab in FireballAbility.");
            return;
        }

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 shootDirection = (hit.point - caster.transform.position);
            shootDirection.y = 0f;
            shootDirection.Normalize();

            if (shootDirection != Vector3.zero)
                caster.transform.rotation = Quaternion.LookRotation(shootDirection);

            Vector3 spawnPos = caster.transform.position + shootDirection + Vector3.up * 1f;
            GameObject proj = Instantiate(fireballPrefab, spawnPos, Quaternion.LookRotation(shootDirection));

            // Ignore all player colliders
            Collider[] casterColliders = caster.GetComponentsInChildren<Collider>();
            Collider[] projectileColliders = proj.GetComponentsInChildren<Collider>();

            foreach (Collider projCol in projectileColliders)
            {
                foreach (Collider casterCol in casterColliders)
                {
                    Physics.IgnoreCollision(projCol, casterCol);
                    Debug.Log($"Ignoring collision between {projCol.name} and {casterCol.name}");
                }
            }

            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = shootDirection * speed;
            }

            Projectile projectileScript = proj.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.damage = damage;
                projectileScript.maxDistance = range;
            }

            Debug.Log("Fireball cast!");
        }
    }
}
