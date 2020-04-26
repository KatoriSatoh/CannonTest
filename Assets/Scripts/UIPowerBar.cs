using UnityEngine;
using UnityEngine.UI;

public class UIPowerBar : MonoBehaviour
{
    [SerializeField]
    private Image limitLine;
    [SerializeField]
    private Image explodeIcon;

    private RectTransform rt;
    private Slider powerBar;

    private void Awake()
    {
        CannonShooter.OnCharging += Charging;
    }

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        powerBar = GetComponent<Slider>();

        float y = rt.rect.height * GameDefine.CANNON_CHARGE_LIMIT / GameDefine.CANNON_CHARGE_MAX - rt.rect.height / 2;
        limitLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, y);
        explodeIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(explodeIcon.GetComponent<RectTransform>().anchoredPosition.x, y);
    }

    private void Charging(float power)
    {
        powerBar.value = power / GameDefine.CANNON_CHARGE_MAX;
    }
}
