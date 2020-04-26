using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField]
    private float size;
    [SerializeField]
    private GameObject[] decorations;

    public float Size {
        get {
            if (size == 0)
            {
                return GetComponent<MeshFilter>().mesh.bounds.size.x;
            }
            return size;
        }
    }

    private void Start()
    {
        int num = Random.Range(GameDefine.GROUND_DECORATION_PER_AREA_MIN, GameDefine.GROUND_DECORATION_PER_AREA_MAX);
        for (int n = 0; n < num; n++)
        {
            int idx = (int)Mathf.Floor(Random.value * decorations.Length);
            GameObject deco = Instantiate(decorations[idx], transform);

            float x = Random.Range(-Size / 2, Size / 2);
            float z = Random.Range(-Size / 2, Size / 2);
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
            float scale = Random.Range(.2f, 1f);
            
            deco.transform.localPosition = new Vector3(x, 0, z);
            deco.transform.rotation = rotation;
            deco.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
