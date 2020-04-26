using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;

    private float degX, degY = 0;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !GameMgr.Instance.IsGameOver)
        {
            degX += Input.GetAxis("Mouse Y") * rotateSpeed;
            degY -= Input.GetAxis("Mouse X") * rotateSpeed;
            degX = Mathf.Clamp(degX, GameDefine.CANNON_ROTATION_X_MIN, GameDefine.CANNON_ROTATION_X_MAX);

            Quaternion target = Quaternion.Euler(degX, degY, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, 7.5f * Time.deltaTime);
        }
    }
}
