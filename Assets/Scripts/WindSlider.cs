using UnityEngine;
using UnityEngine.UI;

public class WindSlider : DialogSystem
{
    [SerializeField] Slider leftSlider;
    [SerializeField] Slider rightSlider;

    public void UpdateUI(float value)
    {
        SetZeroSlider();
        if (value > 0)
            rightSlider.value = value;
        else if (value < 0)
            leftSlider.value = Mathf.Abs(value);
    }

    private void SetZeroSlider()
    {
        leftSlider.value = 0;
        rightSlider.value = 0;
    }
}
