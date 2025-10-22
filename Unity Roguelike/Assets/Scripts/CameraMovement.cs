using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 20f;
    public float followSpeed = 10f;
    public float edgeThickness = 10f;
    public float minZoom = 12f;
    public float maxZoom = 22f;
    public float zoomSpeed = 300f;

    public Vector2 xBounds = new Vector2(-30f, 30f);
    public Vector2 zBounds = new Vector2(-30f, 30f);

    private float currentZoom = 15f;
    private bool isHoldingSpace;

    void Start()
    {
        currentZoom = maxZoom;
    }

    void Update()
    {
        Vector3 move = Vector3.zero;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Edge Scrolling
        if (!isHoldingSpace)
        {
            if (mousePos.x >= screenWidth - edgeThickness)
                move += Vector3.right;
            if (mousePos.x <= edgeThickness)
                move += Vector3.left;
            if (mousePos.y >= screenHeight - edgeThickness)
                move += Vector3.forward;
            if (mousePos.y <= edgeThickness)
                move += Vector3.back;

            transform.position += move * moveSpeed * Time.deltaTime;
        }

        // Snap to player on space press
        if (Keyboard.current.spaceKey.wasPressedThisFrame && player != null)
        {
            transform.position = GetTargetFollowPosition();
        }

        // Follow player while holding space
        if (Keyboard.current.spaceKey.isPressed && player != null)
        {
            isHoldingSpace = true;
            Vector3 targetPos = GetTargetFollowPosition();
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        }
        else
        {
            isHoldingSpace = false;
        }

        // Clamp camera position to map bounds
        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, xBounds.x, xBounds.y);
        clampedPos.z = Mathf.Clamp(clampedPos.z, zBounds.x, zBounds.y);
        transform.position = clampedPos;

        // Handle zoom
        float scroll = Mouse.current.scroll.ReadValue().y;
        currentZoom -= scroll * zoomSpeed * Time.deltaTime;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        transform.position = new Vector3(transform.position.x, currentZoom, transform.position.z);
    }

    Vector3 GetTargetFollowPosition()
    {
        return player.position + new Vector3(0, currentZoom, -14);
    }
}
