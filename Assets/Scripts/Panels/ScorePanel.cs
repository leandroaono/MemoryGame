using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Controla o painel de pontuacao */
public class ScorePanel : MonoBehaviour {
    [SerializeField]
    private Text scoreValue;

    private ScoreManager scoreManager;

    private void Awake() {
        scoreManager = FindObjectOfType<ScoreManager>();
        if(scoreManager == null) {
            Debug.Log("ScorePanel: scoreManager not found!");
            return;
        }
    }

    private void Start() {
        scoreManager.ChangedScoreEvent.AddListener(RefreshScore);
        RefreshScore();
    }

    /* Atualiza texto da pontuacao */
    public void RefreshScore() {
        scoreValue.text = scoreManager.Score.ToString();
    }
}
