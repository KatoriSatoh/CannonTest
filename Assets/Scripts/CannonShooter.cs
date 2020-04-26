using System;
using UnityEngine;

public class CannonShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject cannonBall;
    [SerializeField]
    private GameObject vfxShoot;
    [SerializeField]
    private GameObject vfxFire;

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

            OnCharging(chargePower);
        }
    }

    private void Shoot()
    {
        if (!runOnce) return;

        runOnce = false;
        isCharging = false;

        GameObject go = Instantiate(cannonBall, transform.position, transform.rotation);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * chargePower * GameDefine.CANNON_POWER_PER_CHARGE);

        Instantiate(vfxShoot, transform.position, Quaternion.identity);
    }

    private void Explode()
    {
        runOnce = false;
        isCharging = false;

        Instantiate(vfxFire, transform.position - transform.forward * 2, Quaternion.identity);

        GameMgr.Instance.GameOver();
    }
}