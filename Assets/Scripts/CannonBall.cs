using System;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField]
    private GameObject vfxExplosion;

    public static event Action<Transform> OnFlyingTransform = delegate {};
    public static event Action<Vector3> OnFlyingVelocity = delegate {};

    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        OnFlyingTransform(transform);
    }

    private void FixedUpdate()
    {
        OnFlyingVelocity(rigidbody.velocity);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Tree"))
        {
            Instantiate(vfxExplosion, transform.position, Quaternion.identity);

            GameMgr.Instance.GameOver();
            Destroy(gameObject);
        }
    }
}
