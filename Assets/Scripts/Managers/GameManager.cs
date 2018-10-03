using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Classe principal do motor do jogo */
public class GameManager : MonoBehaviour {
    /* Primeira carta selecionada */
    public CardButton FirstCard { get; set; }
    /* Segunda carta selecionada */
    public CardButton SecondCard { get; set; }
    /* Evento de jogo vitorioso */
    public UnityEvent GameVictoryEvent { get; set; }
    /* Evento de jogo derrotado */
    public UnityEvent GameDefeatEvent { get; set; }

    /* Numero de vezes seguidas que o jogador acertou */
    private int combosMatch;
    /* Quantidade de vezes que o jogador deve acertar para ganhar o jogo */
    private int matchesToWin;
    /* Quantidade de vezes que o jogador acertou */
    private int currentMatches;
    private GamePanel gamePanel;
    private GameTimers gameTimers;
    private TimeManager timeManager;
    private ScoreManager scoreManager;

    private void Awake() {
        if(GameVictoryEvent == null) {
            GameVictoryEvent = new UnityEvent();
        }
        if(GameDefeatEvent == null) {
            GameDefeatEvent = new UnityEvent();
        }
        gamePanel = FindObjectOfType<GamePanel>();
        gameTimers = FindObjectOfType<GameTimers>();
        timeManager = FindObjectOfType<TimeManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        if (gamePanel == null) {
            Debug.Log("GameManager: gamePanel not found!");
            return;
        }
        if(gameTimers == null) {
            Debug.Log("GameManager: gameTimers not found!");
            return;
        }
        if(timeManager == null) {
            Debug.Log("GameManager: timeManager not found!");
            return;
        }
        if (scoreManager == null) {
            Debug.Log("GameManager: scoreManager not found!");
            return;
        }
    }

    private void Start() {
        if(matchesToWin == -1) {
            Debug.Log("Há um numero impar de cartas, o jogo nunca vai terminar vitorioso");
            return;
        }
        timeManager.EndTimerEvent.AddListener(EndDefeatGame);
        StartGame();
    }

    /* Seleciona carta cardButton */
    public void SelectCard(CardButton cardButton) {
        if(FirstCard == null) {
            FirstCard = cardButton;
            FirstCard.ShowImage();
        } else if(FirstCard != null && FirstCard != cardButton && SecondCard == null) {
            SecondCard = cardButton;
            SecondCard.ShowImage();
            checkMatch();
        }
    }

    /* Limpa referencias de selecao de cartas */
    private void ClearSelection() {
        FirstCard = null;
        SecondCard = null;
    }

    /* Verifica se duas cartas foram combinadas e chama sua respectiva funcao de tratamento */
    private void checkMatch() {
        if(FirstCard.GetSprite() == SecondCard.GetSprite()) {
            Debug.Log("Cards Match!");
            runMatch();
            if(checkWin()) {
                EndVictoryGame();
            }
        } else {
            Debug.Log("Different Cards, try again");
            Invoke("runNotMatch", gameTimers.fadeCardTime);
            combosMatch = 0;
        }
        
    }

    /* Realiza o tratamento quando duas cartas forem combinadas */
    private void runMatch() {
        FirstCard.SetInteractable(false);
        SecondCard.SetInteractable(false);
        scoreManager.AddScore(1 + combosMatch);
        combosMatch++;
        currentMatches++;
        ClearSelection();
    }

    /* Realiza tratamento quando duas cartas nao foram combinadas */
    private void runNotMatch() {
        FirstCard.ShowBackground();
        SecondCard.ShowBackground();
        FirstCard = null;
        SecondCard = null;
    }

    /* Comeca jogo */
    public void StartGame() {
        ResetGame();
        ResumeGame();
        pauseGameTimer();
        showCards();
        Invoke("hideCards", gameTimers.fadeCardTime);
        Invoke("resumeGameTimer", gameTimers.fadeCardTime);
    }

    /* Termina jogo */
    public void EndGame() {
        PauseGame();
    }

    /* Termina o jogo de modo vitorioso */
    public void EndVictoryGame() {
        Player.instance.ChangeLevelRecord(scoreManager.Score, LevelManager.instance.CurrentSceneName);
        SaveLoadManager.instance.SaveGame();
        EndGame();
        GameVictoryEvent.Invoke();
    }

    /* Termina o jogo de modo derrotado */
    public void EndDefeatGame() {
        EndGame();
        GameDefeatEvent.Invoke();
    }

    /* Pausa o jogo todo */
    public void PauseGame() {
        Time.timeScale = 0;
    }

    /* Despausa o jogo todo */
    public void ResumeGame() {
        Time.timeScale = 1;
    }

    /* Reseta o jogo */
    public void ResetGame() {
        currentMatches = 0;
        combosMatch = 0;
        matchesToWin = getNumberOfMatchesToWin();
        ClearSelection();
        scoreManager.ResetScore();
        timeManager.ResetTimer();
    }

    /* Mostra todas as cartas do jogo */
    private void showCards() {
        gamePanel.ShowAllCards();
        gamePanel.SetAllCardsInteractable(false);
    }

    /* Esconde todas as cartas do jogo */
    private void hideCards() {
        gamePanel.HideAllCards();
        gamePanel.SetAllCardsInteractable(true);
    }
    
    /* Despausa o timer do jogo */
    private void resumeGameTimer() {
        timeManager.ResumeTimer();
    }

    /* Pausa timer do jogo */
    private void pauseGameTimer() {
        timeManager.PauseTimer();
    }
    
    /* Devolve o numero de combinacoes para que o jogo termine vitorioso */
    private int getNumberOfMatchesToWin() {
        if (gamePanel.CardButtons.Length % 2 != 0) {
            return -1;
        }
        return gamePanel.CardButtons.Length / 2;
    }

    /* Devolve true se o jogo acabou vitorioso */
    private bool checkWin() {
        if(currentMatches == matchesToWin) {
            return true;
        }
        return false;
    }


}
