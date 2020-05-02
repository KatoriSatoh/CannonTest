using System;
using UnityEngine;

public class CannonShooter : MonoBehaviour
{
    [SerializeField]
    private CannonBall cannonBall;
    [SerializeField]
    private GameObject vfxShoot;
    [SerializeField]
    private GameObject vfxFire;
    [SerializeField]
    private float swellMax;

    public static event Action<float> OnCharging = delegate {};

    private float chargePower;
    private float chargeSpeed;

    private bool runOnce;
    private bool isCharging;

    private void Start()
    {
        chargePower = 0;
        chargeSpeed = GameDefine.CANNON_CHARGE_SPEED;

        runOnce = true;
        isCharging = false;
    }

    private void Update()
    {
        if (runOnce && Input.GetKeyDown("space"))
        {
            isCharging = true;
        }
        if (runOnce && Input.GetKeyUp("space"))
        {
            if (chargePower <= GameDefine.CANNON_CHARGE_LIMIT)
            {
                Shoot();
            }
            else
            {
                Explode();
            }
        }

        if (isCharging)
        {
            chargePower += chargeSpeed * Time.deltaTime;
            if (chargePower > GameDefine.CANNON_CHARGE_MAX)
            {
                chargePower = GameDefine.CANNON_CHARGE_MAX;
                Explode();
            }

            float swellValue = 1 + swellMax * chargePower / GameDefine.CANNON_CHARGE_MAX;
            transform.localScale = Vector3.one * swellValue;
            OnCharging(chargePower);
        }
    }

    private void Shoot()
    {
        if (!runOnce) return;

        runOnce = false;
        isCharging = false;

        cannonBall.transform.parent = null;
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(transform.forward * chargePower * GameDefine.CANNON_POWER_PER_CHARGE);

        Instantiate(vfxShoot, cannonBall.transform.position, Quaternion.identity);
    }

    private void Explode()
    {
        runOnce = false;
        isCharging = false;

        Instantiate(vfxFire, transform.position, Quaternion.identity);

        GameMgr.Instance.GameOver();
    }
}