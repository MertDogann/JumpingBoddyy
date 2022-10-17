using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.NiceVibrations;

public class Money : MonoBehaviour
{
    public static Money Current;
    [SerializeField] JumpingScriptable playerData = null;
    [SerializeField] private Animator moneyPickText;
    [SerializeField] private TextMeshPro pick;
    //[SerializeField] private Canvas canvas;
    public Animator coinAnim;
    private LevelController levelCon;
    private bool HapticsAllowed = true;

    // Start is called before the first frame update

    void Start()
    {
        Current = this;
        levelCon = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        MMVibrationManager.SetHapticsActive(HapticsAllowed);
        //canvas.transform.LookAt(Camera.main.transform);

    }


    public void AddingMoney()
    {
        if (levelCon.vibration)
        {
            StartCoroutine(WaitVibration());
        }
       
        
        coinAnim.SetTrigger("CoinT");

        pick.text = "$" + playerData.GetUnitOfMoney.ToString().Replace(",", ".");
        moneyPickText.SetTrigger("Pick");
        playerData.GetMoney += playerData.GetUnitOfMoney;
        PlayerPrefs.SetFloat("totalMoney", playerData.GetMoney);
        levelCon.totalMoneyText.text = Mathf.Floor(playerData.GetMoney).ToString();
    }

    IEnumerator WaitVibration()
    {
        yield return new WaitForSeconds((playerData.GetMoveTime / 3));
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    }

    
}
