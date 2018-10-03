using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Controla o painel de records do level */
public class HighscorePanel : MonoBehaviour {
    [SerializeField]
    private Text highscoreValue;

    private void OnEnable() {
        refreshHighscoreValue();
    }

    /* Atualiza pontuacao maxima */
    public void refreshHighscoreValue() {
        int currentLevel = LevelManager.instance.Level;
        int score = Player.instance.GetHighscore(LevelManager.instance.CurrentSceneName);
        highscoreValue.text = score.ToString();
    }
}
