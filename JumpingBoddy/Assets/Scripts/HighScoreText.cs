using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreText : MonoBehaviour
{
    public TextMeshPro highScoreText;
    private void Awake()
    {
        highScoreText=GetComponent<TextMeshPro>();
    }
    void Start()
    {
        highScoreText.text = (PlayerPrefs.GetFloat("highScorePos") / 2).ToString("0.00") + "m";
        
    }

}
