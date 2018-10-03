using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Controla pontuacao do jogo */
public class ScoreManager : MonoBehaviour {
    /* Pontuacao atual */
    private int score;
    public int Score {
        get { return score; }
        set { score = value;
            ChangedScoreEvent.Invoke();
        }
    }
    /* Evento de mudanca de pontuacao */
    public UnityEvent ChangedScoreEvent { get; set; }

    private void Awake() {
        if(ChangedScoreEvent == null) {
            ChangedScoreEvent = new UnityEvent();
        }
    }

    /* Soma pontuacao em num */
    public void AddScore(int num) {
        Score += num;
    }

    /* Reseta pontuacao */
    public void ResetScore() {
        Score = 0;
    }
}
