using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Classe dos botoes das cartas */
public class CardButton : MonoBehaviour {
    [SerializeField]
    private Image cardBackground;
    [SerializeField]
    private Image cardImage;
    private GameManager gameManager;

    private void Awake() {
        ShowBackground();
        gameManager = FindObjectOfType<GameManager>();
        if(gameManager == null) {
            Debug.Log("CardButton: gameManager not found!");
            return;
        }
    }

    /* Realiza tratamento ao clicar no botao */
    public void ButtonClick() {
        gameManager.SelectCard(this);
    }

    /* Esconde a face da carta */
    public void ShowBackground() {
        cardBackground.gameObject.SetActive(true);
        cardImage.gameObject.SetActive(false);
    }

    /* Mostra a face da carta */
    public void ShowImage() {
        cardImage.gameObject.SetActive(true);
        cardBackground.gameObject.SetActive(false);
    }

    /* Altera a interatividade do botao da carta*/
    public void SetInteractable(bool b) {
        GetComponent<Button>().interactable = b;
    }

    /* Devolve o sprite da carta*/
    public Sprite GetSprite() {
        return cardImage.sprite;
    }

}
