using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

/* Gerencia persistencia de dados */
public class SaveLoadManager : MonoBehaviour {
    public static SaveLoadManager instance;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        LoadGame();
    }

    /* Salva dados do jogador no arquivo PlayerData.json */
    private void savePlayerData(PlayerData playerData) {
        string strJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", strJson);
    }

    /* Se existir, devolve dados do jogador armazenado em PlayerData.json
       Caso contrario devolve null */
    private PlayerData loadPlayerData() {
        PlayerData playerData = null;
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        if (File.Exists(filePath)) {
            string strJson = File.ReadAllText(filePath);
            playerData = JsonUtility.FromJson<PlayerData>(strJson);
            
        }
        return playerData;
    }

    /* Salva jogo e faz um POST do arquivo .json */
    public void SaveGame() {
        savePlayerData(Player.instance.playerData);
        PostJson();
    }

    /* Carrega jogo */
    public void LoadGame() {
        PlayerData playerData = loadPlayerData();
        if (playerData != null) {
            Player.instance.playerData = playerData;
            Debug.Log("SaveLoadManager: Loading playerData");
        }
    }

    /* Faz um POST do PlayerData.json para a endpoint da Huddle Brasil */
    public void PostJson() {
        string url = "https://us-central1-huddle-team.cloudfunctions.net/api/memory/leandro.aono@gmail.com";
        StartCoroutine(Upload(url));
    }

    /* Realiza POST na url */
    IEnumerator Upload(string url) {
        string filePath = Application.dataPath + "/PlayerData.json";
        if (File.Exists(filePath)) {
            string strJson = File.ReadAllText(filePath);
            UnityWebRequest www = UnityWebRequest.Post(url, strJson);
            yield return www.Send();
            if (www.isError) {
                Debug.Log(www.error);
            } else {
                Debug.Log("Form upload complete!");
                Debug.Log(www.responseCode);
            }
        }
    }

    /******************* Apenas para teste *******************/
    public void GetJson() {
        string url = "https://my-json-server.typicode.com/leandroaono/json/posts/1";
        StartCoroutine(GetText(url));
    }

    IEnumerator GetText(string url) {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.Send();
        if (www.isError) {
            Debug.Log(www.error);
        } else {
            Debug.Log(www.downloadHandler.text);
            byte[] results = www.downloadHandler.data;
        }
    }
}

