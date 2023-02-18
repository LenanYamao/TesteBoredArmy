using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameManager gm;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI timerTxt;

    private void Update()
    {
        scoreTxt.text = gm.score.ToString();
        int timeRemaining = Mathf.FloorToInt(gm.timeRemaining);
        timerTxt.text = timeRemaining.ToString();
    }
}
