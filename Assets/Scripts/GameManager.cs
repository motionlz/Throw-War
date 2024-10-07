using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Util;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GoogleSheetData gameSettingData;
    public GoogleSheetData gameSetting => gameSettingData;
    [SerializeField] WindSpeed windSpeed;
    [SerializeField] PlayerManager player1Manager;
    [SerializeField] PlayerManager player2Manager;
    [SerializeField] BotManager botManager;
    private Queue<CharacterManager> turnQueue = new Queue<CharacterManager>();
    private CharacterManager currentPlayer;
    protected async override void InitAfterAwake()
    {
        await Task.Delay(2);
        player1Manager.lifeManager.SetLifeBar(UICenter.Instance.rightLifeBar);
        player2Manager.lifeManager.SetLifeBar(UICenter.Instance.leftLifeBar);
        botManager.lifeManager.SetLifeBar(UICenter.Instance.leftLifeBar);
    }

    private async void Start() 
    {
        StartSetup(false);
        await Task.Delay(10);
        NextTurn();
    }
    private void StartSetup(bool isBot)
    {
        turnQueue.Enqueue(player1Manager);

        SetActiveBot(isBot);
        if (isBot)
        {
            turnQueue.Enqueue(botManager);
        }
        else
        {
            turnQueue.Enqueue(player2Manager);
        }
    }
    private void SetActiveBot(bool b)
    {
        botManager.gameObject.SetActive(b);
        player2Manager.gameObject.SetActive(!b);
    }

    public void NextTurn()
    {
        windSpeed.RandomWindSpeed();

        var nextPlayer = turnQueue.Dequeue();
        currentPlayer = nextPlayer;
        if (nextPlayer.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            player.playerController.turnReady();
        }
        else if (nextPlayer.TryGetComponent<BotManager>(out BotManager bot))
        {

        }
        turnQueue.Enqueue(nextPlayer);
    }

    public void actionToNextQueue(Action<CharacterManager> action)
    {
        var next = turnQueue.Peek();
        action.Invoke(next);
    }
    public void actionToCurrentPlayer(Action<CharacterManager> action)
    {
        action.Invoke(currentPlayer);
    }

    public void GameEnd()
    {
        DisableAllPlayer();
    }
    private void DisableAllPlayer()
    {
        player1Manager.playerController.enabled = false;
        player2Manager.playerController.enabled = false;
        //botManager.enabled = false;
    }
}
