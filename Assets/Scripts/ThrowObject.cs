using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ThrowObject : MonoBehaviour
{
    [Header("Throw Settings")]
    [Range(0, 90)]
    public float throwAngle = 60f;
    public bool throwLeft = false;

    public void Throw(string objectToThrow, float throwForce)
    {
        if (objectToThrow == null) return;

        GameObject thrownObject = ObjectPooling.Instance.GetFromPool(objectToThrow, transform.position, Quaternion.identity);
        Rigidbody2D rb = thrownObject.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector3 throwDirection = ThrowDirection();
            rb.velocity = throwDirection * throwForce;
        }
    }

    private Vector3 ThrowDirection()
    {
        float radians = throwAngle * Mathf.Deg2Rad;
        float directionMultiplier = throwLeft ? -1f : 1f;
        return new Vector2(directionMultiplier * Mathf.Cos(radians), Mathf.Sin(radians));
    }

}
