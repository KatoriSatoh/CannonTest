using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float followDistance;
    [SerializeField]
    private float followHeight;

    private Transform targetTransform;
    private Vector3 targetVelocity;
    private bool isFollowing;

    private void Awake()
    {
        CannonBall.StartFlying += StartFollow;
        CannonBall.OnFlyingTransform += GetTargetPosition;
        CannonBall.OnFlyingVelocity += GetTargetVelocity;
    }

    private void Update()
    {
        if (isFollowing && targetVelocity != Vector3.zero)
        {
            if (targetTransform == null) return;

            Vector3 dir = -targetVelocity.normalized;
            Vector3 target = targetTransform.position + Vector3.up * followHeight + dir * followDistance;
            // transform.position = Vector3.Lerp(transform.position, target, 7.5f * Time.deltaTime);
            transform.position = target;
            transform.LookAt(targetTransform);
        }
    }

    private void OnDestroy()
    {
        CannonBall.StartFlying -= StartFollow;
        CannonBall.OnFlyingTransform -= GetTargetPosition;
        CannonBall.OnFlyingVelocity -= GetTargetVelocity;
    }

    private void StartFollow()
    {
        transform.parent = null;
        isFollowing = true;
    }

    private void GetTargetPosition(Transform target)
    {
        targetTransform = target;
    }

    private void GetTargetVelocity(Vector3 vel)
    {
        targetVelocity = vel;
    }
}
