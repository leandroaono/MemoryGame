using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Classe auxiliar do motor do jogo */
public class GamePanel : MonoBehaviour {
    public CardButton[] CardButtons { get; set; }

    private void Awake() {
        CardButtons = GetComponentsInChildren<CardButton>();
    }

    /* Mostra todas as cartas do jogo */
    public void ShowAllCards() {
        for(int i = 0; i < CardButtons.Length; i++) {
            CardButtons[i].ShowImage();
        }
    }

    /* Esconde todas as cartas do jogo */
    public void HideAllCards() {
        for (int i = 0; i < CardButtons.Length; i++) {
            CardButtons[i].ShowBackground();
        }
    }

    /* Altera a interatividade de todos os botoes das cartas */
    public void SetAllCardsInteractable(bool b) {
        for (int i = 0; i < CardButtons.Length; i++) {
            CardButtons[i].SetInteractable(b);
        }
    }
}
 