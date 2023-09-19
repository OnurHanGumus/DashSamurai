using UnityEngine;
using Zenject;

public class EnemyWaterDetector : MonoBehaviour
{
    public float raycastDistance = 10f; // Maximum distance for the raycast
    [Inject] private PoolSignals _poolSignals { get; set; }
    private bool _isDropable = true;

    void Update()
    {
        if (!_isDropable)
        {
            return;
        }
        // Create a ray from the current GameObject's position to the target position
        Ray ray = new Ray(transform.position, transform.forward);

        // Draw the ray so it's visible even if it doesn't hit a collider
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.blue);

        // Create a RaycastHit variable to store information about the hit
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, raycastDistance, 9))
        {
            // Check if the ray hit something
            if (hit.collider != null)
            {

                // You can do something with the hit information here
                Debug.Log("Raycast hit " + hit.collider.name);

                // You can also get the hit point and do something with it
                Vector3 hitPoint = hit.point;
                if (!hit.transform.CompareTag("Water"))
                {
                    return;
                }
                Debug.DrawLine(ray.origin, hitPoint, Color.red);
                GameObject particle = _poolSignals.onGetObject(PoolEnums.WaterSplash, hitPoint);
                particle.SetActive(true);
                _isDropable = false;
            }
        }
        else
        {
            // If the raycast didn't hit anything, you can handle that here
        }
    }

    private void OnEnable()
    {
        _isDropable = true;
    }
}
