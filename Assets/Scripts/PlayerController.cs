using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

[RequireComponent(typeof(ThrowObject))]
public class PlayerController : PlayerModule
{
    [SerializeField] private ThrowObject throwScript;
    [SerializeField] private int waitTime = 3000;
    private bool isPlayable = false;
    [Header("Throw Settings")]
    [SerializeField] string throwObjectName;
    [SerializeField] float maxThrowForce = 45f; 
    [SerializeField] float minThrowForce = 10f;
    [SerializeField] float chargeSpeed = 30f;
    private float currentThrowForce;
    private bool isCharging = false;
    [Header("UI Settings")]
    [SerializeField] Slider chargeSlider;
    [SerializeField] GameObject playerIndicator;
    [SerializeField] Collider2D clickArea;

    private void Reset() 
    {
        throwScript = GetComponent<ThrowObject>();
    }
    private void Start() 
    {
        ResetChargeBar();
    }
    private void Update()
    {
        if (isPlayable)
            GetInput();
        UpdateChargeBar();
    }
    private async void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
            StartCharge();
        
        else if (Input.GetMouseButton(0) && isCharging)
        {
            ChargeThrowPower();
            await ChargeBarShow(true);
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
            ReleaseCharge();
    }
    public void turnReady()
    {
        SetPlayable(true);
        SetHitBox(false);
        SetIndicatorActive(true);
    }
    private void StartCharge()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);
        foreach (RaycastHit2D h in hits)
        {
            if (h.collider == clickArea)
            {
                isCharging = true;
            }
        }
    }
    private void ChargeThrowPower()
    {
        currentThrowForce += chargeSpeed * Time.deltaTime;
        currentThrowForce = Mathf.Clamp(currentThrowForce, minThrowForce, maxThrowForce);
    }
    private async void ReleaseCharge()
    {
        throwScript?.Throw(throwObjectName, currentThrowForce);
        SetPlayable(false);
        isCharging = false;

        await ChargeBarShow(false, 1000);
        ResetThrowForce();
        SetHitBox(true);

        await Task.Delay(waitTime);
        SetIndicatorActive(false);
        GameManager.Instance.NextTurn();
    }
    private void UpdateChargeBar()
    {
        if (chargeSlider)
            chargeSlider.value = currentThrowForce;
    }
    private void ResetThrowForce()
    {
        currentThrowForce = minThrowForce;
    }
    private void ResetChargeBar()
    {
        chargeSlider.maxValue = maxThrowForce;
        chargeSlider.minValue = minThrowForce;
        chargeSlider.value = currentThrowForce;
    }
    private async Task ChargeBarShow(bool isShow, int time = 0)
    {
        await Task.Delay(time);
        chargeSlider.gameObject.SetActive(isShow);
    }
    public void SetPlayable(bool p)
    {
        isPlayable = p;
    }
    private void SetHitBox(bool isActive)
    {
        playerManager.hitBoxManager.SetColliderActive(isActive);
    }
    private void SetIndicatorActive(bool isActive)
    {
        playerIndicator.SetActive(isActive);
    }
}
