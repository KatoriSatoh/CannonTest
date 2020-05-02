using UnityEngine;

public class GroundMgr : MonoBehaviour
{
    [SerializeField]
    private float coverRadius;
    [SerializeField]
    private Ground groundPrefab;

    private int groundNum;
    private float groundSize;

    private void Awake()
    {
        CannonBall.OnFlyingTransform += CoverGround;
    }

    private void Start()
    {
        groundSize = groundPrefab.Size;
        groundNum = (int)Mathf.Ceil(coverRadius / groundSize);
        for (int i = -groundNum; i <= groundNum; i++)
        {
            for (int j = -groundNum; j <= groundNum; j++)
            {
                Ground ground = Instantiate(groundPrefab, transform);
                ground.transform.localPosition = new Vector3(j * groundSize, 0, i * groundSize);
            }
        }
    }

    private void OnDestroy()
    {
        CannonBall.OnFlyingTransform -= CoverGround;
    }

    private void CoverGround(Transform target)
    {
        Vector3 pos = target.position;
        foreach(Transform child in transform)
        {
            if (child.position.x + groundSize / 2 < pos.x - coverRadius)
            {
                child.position += Vector3.right * groundSize * groundNum * 2;
            }
            else if (child.position.x - groundSize / 2 > pos.x + coverRadius)
            {
                child.position -= Vector3.right * groundSize * groundNum * 2;
            }

            if (child.position.z + groundSize / 2 < pos.z - coverRadius)
            {
                child.position += Vector3.forward * groundSize * groundNum * 2;
            }
            else if (child.position.z - groundSize / 2 > pos.z + coverRadius)
            {
                child.position -= Vector3.forward * groundSize * groundNum * 2;
            }
        }
    }
}