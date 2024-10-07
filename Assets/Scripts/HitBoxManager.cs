using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class HitBoxManager : PlayerModule
{
    [SerializeField] CircleCollider2D headCollider;
    [SerializeField] BoxCollider2D bodyCollider;

    private void Reset() 
    {
        headCollider = GetComponent<CircleCollider2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.CompareTag(GlobalTag.THROW_OBJECT))
        {
            if (col.IsTouching(GetComponent<BoxCollider2D>()))
            {
                BodyHit();
            }
            else if (col.IsTouching(GetComponent<CircleCollider2D>()))
            {
                HeadHit();
            }

            col.gameObject.SetActive(false);
        }
    }

    private void BodyHit()
    {
        TakeDamage(GameManager.Instance.gameSetting.GetValueByKey(GlobalKey.SMALL_ATTACK_DAMAGE), () => 
        {
            PlayAnimation(AnimationKey.BODY_HIT_ANIMATION, 1500);
        });
    }
    private void HeadHit()
    {
        TakeDamage(GameManager.Instance.gameSetting.GetValueByKey(GlobalKey.NORMAL_ATTACK_DAMAGE), () =>
        {
            PlayAnimation(AnimationKey.HEAD_HIT_ANIMATION, 1500);
        });
    }

    private void PlayAnimation(string key, int delay)
    {
        playerManager.animationController.PlayAnimation(key, delay);
    }
    private void TakeDamage(float value, Action OnDamage = null)
    {
        playerManager.lifeManager.TakeDamage(value, OnDamage);
    }

    public void SetColliderActive(bool isActive)
    {
        headCollider.enabled = isActive;
        bodyCollider.enabled = isActive;
    }
}
