using UnityEngine;
using Cinemachine;

public class CameraZoomController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine Virtual Camera
    public Transform player;                      // Reference to the player's transform
    public float minDistance = 5f;                // Zoomed-in distance
    public float maxDistance = 7f;                // Default distance
    public float startZoomX = 20f;                // Start zooming at this x position
    public float endZoomX = 25f;                  // Fully zoomed in at this x position

    void Update()
    {
        if (virtualCamera == null || player == null) return;

        // Get the player's x position
        float playerX = player.position.x;

        // Determine the target camera distance based on player's x position
        float targetDistance;

        if (playerX < startZoomX)
        {
            targetDistance = maxDistance; // Default distance before startZoomX
        }
        else if (playerX > endZoomX)
        {
            targetDistance = minDistance; // Fully zoomed-in distance after endZoomX
        }
        else
        {
            // Lerp between maxDistance and minDistance based on player's x position
            float t = (playerX - startZoomX) / (endZoomX - startZoomX);
            targetDistance = Mathf.Lerp(maxDistance, minDistance, t);
        }

        // Update the camera's distance
        var component = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        if (component != null)
        {
            component.CameraDistance = targetDistance;
        }
    }
}
