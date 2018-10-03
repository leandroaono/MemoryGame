using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Classe com dados do jogador */
public class Player : MonoBehaviour {
    /* Dados do jogador */
    public PlayerData playerData { get; set; }

    public static Player instance;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        if(playerData == null) {
            CreateNewPlayerData("PlayerName");
        }
    }

    /* Muda nome do jogador */
    public void ChangeName(string name) {
        playerData.name = name;
    }

    /* Verifica e altera a pontuacao maxima para score do level de nome levelName */
    public void ChangeLevelRecord(int score, string levelName) {
        int index = LevelManager.LevelNameToIndex[levelName];
        if (playerData.levelData[index].highscore < score) {
            playerData.levelData[index].highscore = score;
        }
    }

    /* Cria novos dados do jogador */
    public void CreateNewPlayerData(string name) {
        playerData = new PlayerData(LevelManager.MaxLevel);
        ChangeName(name);
    }

    /* Devolve pontuacao maxima do level de indice index */
    public int GetHighscore(int index) {
        return playerData.GetHighscore(index);
    }

    /* Devolve pontuacao maxima do level de nome levelName */
    public int GetHighscore(string levelName) {
        int index = LevelManager.LevelNameToIndex[levelName];
        return playerData.GetHighscore(index);
    }
}

/* Dados do level */
[System.Serializable]
public class LevelData {
    public string levelName;
    public int highscore;
}

/* Dados do jogador */
[System.Serializable]
public class PlayerData {
    public string name;
    public LevelData[] levelData;
    public PlayerData(int maxLevel) {
        levelData = new LevelData[maxLevel];
        for(int i = 0; i < maxLevel; i++) {
            levelData[i] = new LevelData();
            string levelname = LevelManager.LevelsName[i];
            levelData[i].levelName = levelname;
            levelData[i].highscore = 0;
        }
    }
    public int GetHighscore(int index) {
        return levelData[index].highscore;
    }
}