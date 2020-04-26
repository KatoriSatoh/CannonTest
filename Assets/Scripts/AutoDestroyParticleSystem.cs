using System.Collections;
using UnityEngine;
 
public class AutoDestroyParticleSystem : MonoBehaviour 
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(GetComponent<ParticleSystem>().duration);
        Destroy(gameObject);
    }
}