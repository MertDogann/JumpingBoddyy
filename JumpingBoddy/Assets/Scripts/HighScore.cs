using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public static HighScore Current;
    [SerializeField] private JumpingScriptable playerData = null;
    public GameObject ins;
    [SerializeField] GameObject highScoreObject;
    void Start()
    {
        Current = this;
        GameObject.Instantiate(ins,new Vector3(2.5f,0.975f, PlayerPrefs.GetFloat("highScorePos")),Quaternion.identity);
        
    }


    public void HighScoree()
    {
        playerData.GetHighScorePos = (highScoreObject.transform.position.z + 0.32f);
        PlayerPrefs.SetFloat("highScorePos", playerData.GetHighScorePos);
    }

}
