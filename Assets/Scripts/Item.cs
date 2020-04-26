using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private float activeDistance;

    private void Awake()
    {
        CannonBall.OnFlyingTransform += CheckActive;
    }

    private void OnDestroy()
    {
        CannonBall.OnFlyingTransform -= CheckActive;
    }

    private void CheckActive(Transform target)
    {
        Vector3 pos = target.position;
        if (Mathf.Abs(transform.position.x - pos.x) > activeDistance)
        {
            Destroy(gameObject);
        }
        if (Mathf.Abs(transform.position.z - pos.z) > activeDistance)
        {
            Destroy(gameObject);
        }
    }
}
