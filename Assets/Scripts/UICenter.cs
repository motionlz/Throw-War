using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICenter : Singleton<UICenter>
{
    [SerializeField] WindSlider windSliderPanel;
    public WindSlider windSlider => windSliderPanel;
    [SerializeField] Slider rightLifeSlider;
    public Slider rightLifeBar => rightLifeSlider;
    [SerializeField] Slider leftLifeSlider;
    public Slider leftLifeBar => leftLifeSlider;
    protected override void InitAfterAwake()
    {

    }
}
