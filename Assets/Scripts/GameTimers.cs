using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Classe que armazena os tempos do jogo */
public class GameTimers : MonoBehaviour {
    /* Tempo para que a carta seja escondida ao falhar numa combinacao */
    public float fadeCardTime;
    /* Tempo para que a carta seja escondida no comeco do jogo */
    public float startCardTime;
    /* Tempo para que termine o jogo de modo derrotado */
    public float endGameTime;
}
