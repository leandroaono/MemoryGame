using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Botao de mudanca de cenas */
public class SceneButton : MonoBehaviour {
    public void ChangeScene(string sceneName) {
        LevelManager.instance.ChangeScene(sceneName);
    }
}
