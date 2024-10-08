using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

[RequireComponent(typeof(ThrowObject), typeof(PlayerTimer))]
public class PlayerController : PlayerModule
{
    [SerializeField] private ThrowObject throwScript;
    [SerializeField] private PlayerTimer playerTimer;
    [SerializeField] private int waitTime = 3000;
    private bool isPlayable = false;
    [Header("Throw Settings")]
    [SerializeField] float maxThrowForce = 45f; 
    [SerializeField] float minThrowForce = 10f;
    [SerializeField] float chargeSpeed = 30f;
    private float currentThrowForce;
    private bool isCharging = false;
    private bool isPowerAttack = false;
    private bool isDoubleAttack = false;
    [Header("UI Settings")]
    [SerializeField] Slider chargeSlider;
    [SerializeField] GameObject playerIndicator;
    [SerializeField] Collider2D clickArea;

    private void Reset() 
    {
        throwScript = GetComponent<ThrowObject>();
        playerTimer = GetComponent<PlayerTimer>();
    }
    private void Start() 
    {
        ResetChargeBar();
        SetTimerAction();
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
        {
            CheckStartCharge();
        }
        
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
        StartTimer();
        characterManager.powerUpManager.PowerupEnable(true);
    }
    private void CheckStartCharge()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);
        foreach (RaycastHit2D h in hits)
        {
            if (h.collider == clickArea)
            {
                isCharging = true;
                CancelTimer();
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
        if (isPowerAttack)
        {
            isPowerAttack = false;
            throwScript?.Throw(GlobalThrowObjectKey.POWER_THROW, currentThrowForce);
        }
        else if (isDoubleAttack)
        {
            SetPlayable(false);
            isCharging = false;
            isDoubleAttack = false;
            var count = GameManager.Instance.gameSetting.GetValueByKey(GlobalKey.DOUBLE_ATTACK_COUNT);
            for (int i = 1; i <= count; i++)
            {
                throwScript?.Throw(GlobalThrowObjectKey.SMALL_THROW, currentThrowForce);
                if (count == i) break;
                await Task.Delay(2000);
            }
        }
        else
            throwScript?.Throw(GlobalThrowObjectKey.NORMAL_THROW, currentThrowForce);

        SetPlayable(false);
        isCharging = false;
        characterManager.powerUpManager.PowerupEnable(false);

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
        characterManager.hitBoxManager.SetColliderActive(isActive);
    }
    private void SetIndicatorActive(bool isActive)
    {
        playerIndicator.SetActive(isActive);
    }
    private void StartTimer()
    {
        playerTimer.StartCountdown();
    }
    private void CancelTimer()
    {
        playerTimer.CancelCountdown();
    }
    private void SetTimerAction()
    {
        playerTimer.OnDone += () =>
        {
            SetPlayable(false);
            SetHitBox(true);
            SetIndicatorActive(false);
            characterManager.powerUpManager.PowerupEnable(false);
            GameManager.Instance.NextTurn();
        };
    }

    public void DoubleAttackSkill()
    {
        isDoubleAttack = true;
    }
    public void PowerAttackSkill()
    {
        isPowerAttack = true;
    }
}

public static class GlobalThrowObjectKey
{
    public const string NORMAL_THROW = "NORMAL_THROW";
    public const string POWER_THROW = "POWER_THROW";
    public const string SMALL_THROW = "SMALL_THROW";
}
