using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : DialogSystem
{
    [SerializeField] Button restartBtn;
    [SerializeField] TextMeshProUGUI winText;
    private void Awake() 
    {
        restartBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    public void SetWinPlayer(string winPlayer)
    {
        winText.text = winPlayer + " WIN";
        DialogShow();
    }
}
