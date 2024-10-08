
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : DialogSystem
{
    [SerializeField] Button playBtn;
    [SerializeField] Button howToPlayBtn;
    [SerializeField] Button howToPlayUI;
    

    private void Awake() 
    {
        playBtn.onClick.AddListener(() => 
        {
            GameManager.Instance.OnStartGame();
            DialogHide();
        });
        howToPlayBtn.onClick.AddListener(() => 
        {
            howToPlayUI.gameObject.SetActive(true);
        });
        howToPlayUI.onClick.AddListener(() =>
        {
            howToPlayUI.gameObject.SetActive(false);
        });
    }

}
