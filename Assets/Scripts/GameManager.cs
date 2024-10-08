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
    private bool isGameEnd = false;
    protected override void InitAfterAwake()
    {
        Init(false);
    }
    private void Init(bool isBot)
    {
        player1Manager.OnInit += (l, p) => 
        { 
            l.SetLifeBar(UICenter.Instance.rightLifeBar);
            p.AssignButton(UICenter.Instance.rightPowerUpUI);
        };
        if (isBot)
        {
            botManager.OnInit += (l, p) => 
            { 
                l.SetLifeBar(UICenter.Instance.leftLifeBar);
                p.AssignButton(UICenter.Instance.leftPowerUpUI);
            };
        }
        else
        {
            player2Manager.OnInit += (l, p) => 
            { 
                l.SetLifeBar(UICenter.Instance.leftLifeBar);
                p.AssignButton(UICenter.Instance.leftPowerUpUI);
            };
        }
    }
    public void OnStartGame()
    {
        StartSetup(false);
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
        if(isGameEnd) return;
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

    public void GameEnd(CharacterManager character)
    {
        DisableAllPlayer();
        isGameEnd = true;

        UICenter.Instance.gameOverUI.SetWinPlayer(GetPlayerName(character));
    }
    private void DisableAllPlayer()
    {
        player1Manager.playerController.enabled = false;
        player2Manager.playerController.enabled = false;
        //botManager.enabled = false;
    }

    private string GetPlayerName(CharacterManager character)
    {
        if (character == player1Manager)
            return "P2";
        if (character == player2Manager)
            return "P1";
        else
            return "P1";
    }
}
