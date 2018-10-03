using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Controla painel de tempo */
public class TimePanel : MonoBehaviour {
    [SerializeField]
    private Slider sliderTime;
    [SerializeField]
    private Text timeValue;

    private TimeManager timeManager;

    private void Awake() {
        timeManager = FindObjectOfType<TimeManager>();
        if (timeManager == null) {
            Debug.Log("TimePanel: timeManager not found!");
            return;
        }
        RefreshAll();
    }

    private void Start() {
        timeManager.ChangedTimerEvent.AddListener(RefreshAll);
    }

    /* Atualiza tudo*/
    public void RefreshAll() {
        refreshSlider();
        refreshTime();
    }

    /* Atualiza slider de tempo */
    private void refreshSlider() {
        float timeleft = timeManager.EndGameTime - timeManager.CurrentTime;
        sliderTime.maxValue = timeManager.EndGameTime;
        sliderTime.value = timeleft;
    }

    /* Atualiza texto de tempo */
    private void refreshTime() {
        float timeleft = timeManager.EndGameTime - timeManager.CurrentTime;
        timeValue.text = timeleft.ToString("F1");
    }
}
