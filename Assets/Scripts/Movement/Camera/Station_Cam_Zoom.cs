using UnityEngine;
using Cinemachine;

/*
    Function:   Zooms camera between specific x positions on player
    Usage:      Used for lerping camera zoom effect
*/
public class Station_Cam_Zoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtual_camera;
    public Transform player;                      
    public float min_distance = 5f;                
    public float max_distance = 7f;                
    public float start_zoom = 20f;                
    public float end_zoom = 25f;                  

    void Update()
    {
        if (virtual_camera == null || player == null) return;

        float player_x = player.position.x;
        float target_distance;

        //Handle edge cases
        if (player_x < start_zoom)
        {
            target_distance = max_distance;
        }
        else if (player_x > end_zoom)
        {
            target_distance = min_distance;
        }

        //Lerp camera zoom based on player x position
        else
        {
            float t = (player_x - start_zoom) / (end_zoom - start_zoom);
            target_distance = Mathf.Lerp(max_distance, min_distance, t);
        }

        var component = virtual_camera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        if (component != null)
        {
            component.CameraDistance = target_distance;
        }
    }
}
