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
    [SerializeField] PowerUpUI rightPowerUpUIPanel;
    public PowerUpUI rightPowerUpUI => rightPowerUpUIPanel;
    [SerializeField] PowerUpUI leftPowerUpUIPanel;
    public PowerUpUI leftPowerUpUI => leftPowerUpUIPanel;
    [SerializeField] MainMenuUI mainMenuUIPanel;
    public MainMenuUI mainMenuUI => mainMenuUIPanel;
    [SerializeField] GameOverUI gameOverUIPanel;
    public GameOverUI gameOverUI => gameOverUIPanel;
    protected override void InitAfterAwake()
    {

    }
}
