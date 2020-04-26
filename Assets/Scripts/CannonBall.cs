using System;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField]
    private GameObject vfxExplosion;

    public static event Action StartFlying = delegate{};
    public static event Action<Transform> OnFlyingTransform = delegate {};
    public static event Action<Vector3> OnFlyingVelocity = delegate {};

    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        StartFlying();
    }

    private void Update()
    {
        OnFlyingTransform(transform);
    }

    private void FixedUpdate()
    {
        OnFlyingVelocity(rigidbody.velocity);
    }

    private void OnDestroy()
    {
        StartFlying = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(vfxExplosion, transform.position, Quaternion.identity);

            GameMgr.Instance.GameOver();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            rigidbody.AddForce(Vector3.up * GameDefine.ITEM_POWER);
            Destroy(other.gameObject);
        }
    }
}
