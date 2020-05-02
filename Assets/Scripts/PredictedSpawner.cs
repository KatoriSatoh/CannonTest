using UnityEngine;

public class PredictedSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private float objectHeight;
    [SerializeField]
    private int predictStep;

    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rigidbody.velocity.y < 0)
        {
            Check(transform.position, rigidbody.velocity);
        }
    }

    private void Check(Vector3 initPos, Vector3 initVel)
    {
        Vector3 position = initPos;
        Vector3 velocity = initVel;
        for (int i = 0; i < predictStep; ++i)
        {
            position += velocity * Time.fixedDeltaTime + 0.5f * Physics.gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
            velocity += Physics.gravity * Time.fixedDeltaTime;

            if (position.y < objectHeight)
            {
                SpawnObject(position.x, 0, position.z);
                this.enabled = false;
            }
        }
    }

    private void SpawnObject(float x, float y, float z)
    {
        Debug.Log(new Vector3(x, y, z));
        GameObject go = Instantiate(spawnObject);
        go.transform.position = new Vector3(x, y, z);
    }
}
