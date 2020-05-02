using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private float followDistance;
    [SerializeField]
    private float followHeight;

    private Vector3 targetVelocity;

    private void Update()
    {
        if (targetVelocity != Vector3.zero)
        {
            if (targetTransform == null) return;

            Vector3 dir = -targetVelocity.normalized;
            Vector3 target = targetTransform.position + Vector3.up * followHeight + dir * followDistance;
            transform.position = target;
            transform.LookAt(targetTransform);
        }
    }

    private void FixedUpdate()
    {
        if (targetTransform == null) return;
        targetVelocity = targetTransform.GetComponent<Rigidbody>().velocity;
    }
}
