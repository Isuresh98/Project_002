using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothTime = 0.3f;
    public float cameraOffsetZ = -10f;
    public float cameraOffsetY = 2f;

    private Transform playerTransform;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        // Find the player object with the "Player" tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("CameraFollow script could not find the player object with the 'Player' tag!");
        }
    }

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + cameraOffsetY, cameraOffsetZ);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
