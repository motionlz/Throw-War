using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICenter : MonoBehaviour
{
    public static UICenter Instance { get; private set; }
    [SerializeField] WindSlider windSliderPanel;
    public WindSlider windSlider => windSliderPanel;

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
