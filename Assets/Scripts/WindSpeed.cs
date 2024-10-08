
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WindSpeed : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;

    [Header("Wind Settings")]
    [Range(-10, 10)]
    public float windStrength;

    private Rigidbody2D rb;
    private void Start() 
    {
        RandomWindSpeed();
    }
    private void Reset() 
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if ((col.CompareTag(GlobalTag.THROW_OBJECT) || col.CompareTag(GlobalTag.POWER_THROW_OBJECT)) && col.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            Vector2 windDirection = new Vector2(Mathf.Sign(windStrength), 0);
            rb.AddForce(windDirection * Mathf.Abs(windStrength));
        }
    }

    public void RandomWindSpeed()
    {
        windStrength = Random.Range(-10, 11);
        UpdateMeterUI();
    }

    public void UpdateMeterUI()
    {
        UICenter.Instance.windSlider.UpdateUI(windStrength);
    }
}
