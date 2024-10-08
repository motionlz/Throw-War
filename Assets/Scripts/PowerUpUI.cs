using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : DialogSystem
{
    [SerializeField] Button powerUpButton1;
    public Action OnSkill1;
    [SerializeField] Button powerUpButton2;
    public Action OnSkill2;
    [SerializeField] Button powerUpButton3;
    public Action OnSkill3;

    private void Awake() 
    {
        EnableAllButton(false);
        powerUpButton1.onClick.AddListener(() =>
        {
            OnSkill1?.Invoke();
            OnUsed(powerUpButton1);
        });
        powerUpButton2.onClick.AddListener(() =>
        {
            OnSkill2?.Invoke();
            OnUsed(powerUpButton2);
        });
        powerUpButton3.onClick.AddListener(() =>
        {
            OnSkill3?.Invoke();
            OnUsed(powerUpButton3);
        });
    }

    private void OnUsed(Button btn)
    {
        EnableAllButton(false);
        btn.gameObject.SetActive(false);
    }

    public void EnableAllButton(bool isEnable)
    {
        powerUpButton1.interactable = isEnable;
        powerUpButton2.interactable = isEnable;
        powerUpButton3.interactable = isEnable;
    }
}