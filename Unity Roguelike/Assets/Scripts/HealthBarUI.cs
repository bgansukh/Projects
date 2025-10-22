using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image healthFill;
    public Transform followTarget;
    public Vector3 offset = new Vector3(0, 2.5f, 0);
    public Camera cam;

    void Update()
    {
        // Make the health bar face the camera
        if (cam != null)
        {
            transform.LookAt(transform.position + cam.transform.forward, cam.transform.up);
        }



        // Keep it above the enemy
        if (followTarget != null)
        {
            transform.position = followTarget.position + offset;
        }
    }

    public void SetHealth(float current, float max)
    {
        healthFill.fillAmount = current / max;
    }
}
