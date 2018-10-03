using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Gerencia cenas e leveis do jogo */
public class LevelManager : MonoBehaviour {
    /* Level atual do jogo */
    public int Level { get; set; }
    /* Quantidade maxima de leveis */
    public static int MaxLevel;
    /* Mapeia o nome do level para um inteiro */
    public static Dictionary<string, int> LevelNameToIndex;
    /* Nomes dos leveis do jogo */
    public static string[] LevelsName;
    /* Devolve o nome da cena atual */
    public string CurrentSceneName {
        get { return SceneManager.GetActiveScene().name; }
    }
    public static LevelManager instance;
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Level = -1;
        MaxLevel = SceneManager.sceneCountInBuildSettings - 1;
        createLevelsName();
        createLevelNameToIndex();
    }

    /* Gera LevelsName */
    private void createLevelsName() {
        LevelsName = new string[MaxLevel];
        for (int i = 0; i < MaxLevel; i++) {
            LevelsName[i] = (i + 1).ToString();
        }
    }
    
    /* Gera LevelNameToIndex */
    private void createLevelNameToIndex() {
        LevelNameToIndex = new Dictionary<string, int>();
        for(int i = 0; i < MaxLevel; i++) {
            LevelNameToIndex[LevelsName[i]] = i;
        }
    }

    /* Muda cena para sceneName */
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
        if (sceneName != "Menu") {
            Level = int.Parse(sceneName) - 1;
            Debug.Log("Current Level: " + Level);
        }
    }
    
}
