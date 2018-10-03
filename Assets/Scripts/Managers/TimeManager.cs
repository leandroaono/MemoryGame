using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Gerencia o tempo do jogo */
public class TimeManager : MonoBehaviour {
    /* Tempo de jogo corrido*/
    private float currentTime;
    public float CurrentTime {
        get { return currentTime; }
        set { currentTime = value;
            ChangedTimerEvent.Invoke();
        }
    }
    /* Tempo para termino de jogo */
    private float endGameTime;
    public float EndGameTime {
        get { return endGameTime; }
        set { endGameTime = value; }
    }
    /* True se o temporizador estiver pausado */
    private bool isPaused;
    public bool IsPaused {
        get { return isPaused; }
    }
    /* Evento de termino do tempo do jogo */
    public UnityEvent EndTimerEvent { get; set; }
    /* Evento de mudanca no tempo do jogo */
    public UnityEvent ChangedTimerEvent { get; set; }

    private void Awake() {
        GameTimers gameTimers = FindObjectOfType<GameTimers>();
        if (gameTimers == null) {
            Debug.Log("TimerManager: gameTimers not found!");
        }
        if (EndTimerEvent == null) {
            EndTimerEvent = new UnityEvent();
        }
        if (ChangedTimerEvent == null) {
            ChangedTimerEvent = new UnityEvent();
        }

        CurrentTime = 0;
        PauseTimer();
        EndGameTime = gameTimers.endGameTime;
    }

    /* Atualiza temporizador */
    private void Update() {
        if (!IsPaused) {
            CurrentTime += Time.deltaTime;
            if (CurrentTime > EndGameTime) {
                EndTimerEvent.Invoke();
            }
        }
    }

    /* Reseta temporizador */
    public void ResetTimer() {
        CurrentTime = 0;
    }

    /* Pausa temporizador */
    public void PauseTimer() {
        isPaused = true;
    }

    /* Despausa temporizador */
    public void ResumeTimer() {
        isPaused = false;
    }
}
