using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{
    [SerializeField]private float countdownTime;
    [SerializeField]private float warningTime;
    [SerializeField]private float currentTime;
    [SerializeField] private Slider timerSlider;
    private bool isCanceled = false;
    public Action OnDone;

    private void Start() 
    {
        SetTimer(GameManager.Instance.gameSetting.GetValueByKey(GlobalKey.THINK_TIME),GameManager.Instance.gameSetting.GetValueByKey(GlobalKey.WARN_TIME));
        ResetUI();

        OnDone += () =>
        {
            SetActiveUI(false);
        };
    }
    public void SetTimer(float countdown, float warning)
    {
        countdownTime = countdown;
        warningTime = warning;
    }
    public async void StartCountdown()
    {
        isCanceled = false;
        currentTime = countdownTime;
        await CountdownAsync();
    }

    public void CancelCountdown()
    {
        isCanceled = true;
        SetActiveUI(false);
    }

    private async Task CountdownAsync()
    {
        while (currentTime > 0)
        {
            if (isCanceled)
                return;

            await Task.Delay(1000);
            currentTime--;

            if (currentTime <= warningTime && !isCanceled)
            {
                OnWarning();
            }
        }

        if (!isCanceled)
        {
            OnDone?.Invoke();
        }
    }

    private void OnWarning()
    {
        SetActiveUI(true);
        UpdateUI();
    }
    private void ResetUI()
    {
        timerSlider.maxValue = warningTime;
    }
    private void UpdateUI()
    {
        timerSlider.value = currentTime;
    }
    public void SetActiveUI(bool active)
    {
        timerSlider.gameObject.SetActive(active);
    }
}
