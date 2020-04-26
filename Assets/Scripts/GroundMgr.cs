using UnityEngine;

public class GroundMgr : MonoBehaviour
{
    [SerializeField]
    private float coverRadius;
    [SerializeField]
    private Ground groundPrefab;
    [SerializeField]
    private Item itemPrefab;

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
                // if (i == 0 && j == 0) continue;

                Ground ground = Instantiate(groundPrefab, transform);
                ground.transform.localPosition = new Vector3(j * groundSize, 0, i * groundSize);

                SpawnItem(j * groundSize, i * groundSize);
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
                SpawnItem(child.position.x, child.position.z);
            }
            else if (child.position.x - groundSize / 2 > pos.x + coverRadius)
            {
                child.position -= Vector3.right * groundSize * groundNum * 2;
                SpawnItem(child.position.x, child.position.z);
            }

            if (child.position.z + groundSize / 2 < pos.z - coverRadius)
            {
                child.position += Vector3.forward * groundSize * groundNum * 2;
                SpawnItem(child.position.x, child.position.z);
            }
            else if (child.position.z - groundSize / 2 > pos.z + coverRadius)
            {
                child.position -= Vector3.forward * groundSize * groundNum * 2;
                SpawnItem(child.position.x, child.position.z);
            }
        }
    }

    private void SpawnItem(float x, float z)
    {
        Item item = Instantiate(itemPrefab);
        item.transform.position = new Vector3(Random.Range(x - groundSize / 2, x + groundSize / 2), Random.Range(2, 8), Random.Range(z - groundSize / 2, z + groundSize / 2));
    }
}