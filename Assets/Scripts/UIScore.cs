using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIScore : MonoBehaviour
{
    private Text score;
    private float currentScore;

    private void Awake()
    {
        CannonBall.OnFlyingTransform += UpdateScore;
    }

    private void Start()
    {
        score = GetComponent<Text>();
    }

    private void Update()
    {
        score.text = currentScore + "m";
    }

    private void UpdateScore(Transform target)
    {
        float dist = target.position.x * target.position.x + target.position.z * target.position.z;
        currentScore = Mathf.Round(dist / 100);
    }
}
