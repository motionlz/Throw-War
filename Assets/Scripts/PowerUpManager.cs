using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : PlayerModule
{
    [SerializeField] PlayerController playerController;
    private PowerUpUI powerUpUI;
    private void OnHealSkill()
    {
        characterManager.lifeManager.TakeDamage(-GameManager.Instance.gameSetting.GetValueByKey(GlobalKey.HEAL_POWER));
    }

    private void OnDoubleAttackSkill()
    {
        playerController?.DoubleAttackSkill();
    }

    private void OnPowerAttackSkill()
    {
        playerController?.PowerAttackSkill();
    }

    public void AssignButton(PowerUpUI p)
    {
        powerUpUI = p;
        powerUpUI.OnSkill1 += OnDoubleAttackSkill;
        powerUpUI.OnSkill2 += OnPowerAttackSkill;
        powerUpUI.OnSkill3 += OnHealSkill;
    }
    public void PowerupEnable(bool isEnable)
    {
        powerUpUI.EnableAllButton(isEnable);
    }
}
