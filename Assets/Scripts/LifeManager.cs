using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : PlayerModule
{
    [SerializeField]private float maxLife;
    [SerializeField]private float currentLife;
    private Slider lifeBar;
    
    public void SetLifeBar(Slider slider)
    {
        if(lifeBar == null)
            lifeBar = slider;
    }
    public void SetLife(string key)
    {
        maxLife = GameManager.Instance.gameSetting.GetValueByKey(key);
        currentLife = maxLife;
        UpdateUI();
    }

    public void TakeDamage(float dmg, Action OnDamage = null)
    {
        currentLife -= dmg;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);

        UpdateUI();
        if (currentLife <= 0)
            OnOutOfLife();
        else if (OnDamage != null)
            OnDamage.Invoke();
    }

    private void OnOutOfLife()
    {
        characterManager.animationController.PlayOverrideAnimation(AnimationKey.LOSE_ANIMATION);
        GameManager.Instance.actionToCurrentPlayer((c) => 
        {
            c.animationController.PlayOverrideAnimation(AnimationKey.WIN_ANIMATION);
        });

        GameManager.Instance.GameEnd(characterManager);
    }
    private void UpdateUI()
    {
        lifeBar.maxValue = maxLife;
        lifeBar.value = currentLife;
    }
}
