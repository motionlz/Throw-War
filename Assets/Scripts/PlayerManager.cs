using System.Threading.Tasks;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(ThrowObject))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private ThrowObject throwScript;
    private bool isPlayable = false;
    [Header("Throw Settings")]
    public GameObject objectToThrow;
    public float maxThrowForce = 40f; 
    public float minThrowForce = 10f;
    public float chargeSpeed = 30f;
    private float currentThrowForce;

    [Header("UI Settings")]
    public Slider chargeSlider;

    private void Reset() 
    {
        throwScript = GetComponent<ThrowObject>();
    }
    private void Start() 
    {
        ResetChargeBar();
        SetPlayable(true);//Test
    }
    private void Update()
    {
        if (isPlayable)
            GetInput();
        UpdateChargeBar();
    }

    private async void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            ChargeThrowPower();
            await ChargeBarShow(true);
        }

        if (Input.GetMouseButtonUp(0))
            ReleaseCharge();
    }

    private void ChargeThrowPower()
    {
        currentThrowForce += chargeSpeed * Time.deltaTime;
        currentThrowForce = Mathf.Clamp(currentThrowForce, minThrowForce, maxThrowForce);
    }

    private async void ReleaseCharge()
    {
        throwScript?.Throw(objectToThrow, currentThrowForce);
        SetPlayable(false);

        await ChargeBarShow(false, 1000);
        ResetThrowForce();

        SetPlayable(true);//Test
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
}
