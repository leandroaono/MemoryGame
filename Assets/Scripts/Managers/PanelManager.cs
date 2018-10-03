using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Auxilia os paineis de vitoria e derrota */
public class PanelManager : MonoBehaviour {
    [SerializeField]
    private GameObject victoryPanel;
    [SerializeField]
    private GameObject defeatPanel;
    private GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start() {
        gameManager.GameVictoryEvent.AddListener(showVictoryPanel);
        gameManager.GameDefeatEvent.AddListener(showDefeatPanel);
    }

    /* Mostra painel de derrota */
    private void showDefeatPanel() {
        defeatPanel.SetActive(true);
    }

    /* Mostra painel de vitoria */
    private void showVictoryPanel() {
        victoryPanel.SetActive(true);
    }


}


