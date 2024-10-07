using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class HitBoxManager : MonoBehaviour
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
        }
    }

    private void BodyHit()
    {
        Debug.Log("Body");
    }
    private void HeadHit()
    {
        Debug.Log("Head");
    }

    public void SetColliderActive(bool isActive)
    {
        headCollider.enabled = isActive;
        bodyCollider.enabled = isActive;
    }
}
