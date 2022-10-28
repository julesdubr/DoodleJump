using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private TextMeshProUGUI highScoreText;

    void Awake()
    {
        highScoreText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        highScoreText.text = $"highscore: {PlayerPrefs.GetInt("HighScore", 0)}";
    }
}
