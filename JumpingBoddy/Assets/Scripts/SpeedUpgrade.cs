using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpeedUpgrade : MonoBehaviour
{
    [SerializeField] private JumpingScriptable playerData = null;
    [SerializeField] private int currentSpeedLevel = 0;   // Speed upgradesi level degeri.
    [SerializeField] Animator playerAnim1, playerAnim2;
    private int currentLevelText;                          //Textler bir level onde gitmesi gerektigi icin ayr? bir deger.
    public float levelUpFeeSpeed = 0;          //Speed upgradesi upgrade icin gereken money degeri.
    private float levelUpFeeSpeedText;
    private float levelIncreaseFeeRateSpeed = 1.45f;     //Speed upgradesi speed artis orani.
    private float moveTimeDecrease = 0.12f ;
    private float jumpAnimationTimerDecrease = 0.13f;
    private float bendingAnimationTimerDecrease = 0.01f;
    private float jumpDistanceIncrease = 0.5f *2 ;
    [SerializeField] GameObject secondPlayer;
    [SerializeField] GameObject firstPlayer;
    private GameManager gameManager;
    private bool maxLevelBool;


    private void Awake()
    {

        PlayerPrefsCheck();

    }


    public void IncrementSpeed()
    {
        if (playerData.GetMoney > levelUpFeeSpeedText && playerData.GetSpeedMaxLevel <= 8)
        {
            playerData.GetSpeedMaxLevel++;
            PlayerPrefs.SetInt("speedMaxLevel", playerData.GetSpeedMaxLevel);
            
            playerData.GetSpeedUpgradePrefs +=1;
            PlayerPrefs.SetInt("speedUpgradePrefs", playerData.GetSpeedUpgradePrefs);
            Debug.Log("speed upgrade prefs" + playerData.GetSpeedUpgradePrefs);
            gameManager.upgradeParticle[2].Play();
            gameManager.upgradeParticle[3].Play();
            currentLevelText += 1;
            PlayerPrefs.SetInt("currentLevelText", currentLevelText);

            if (playerData.GetSpeedUpgradePrefs <6 && playerData.GetSpeedUpgradePrefs >0)
            {
                playerData.GetAnimationSpeed += 0.15f;
              
            }
            else if (playerData.GetSpeedUpgradePrefs < 10 && playerData.GetSpeedUpgradePrefs >= 6)
            {
                jumpAnimationTimerDecrease = 0.085f;
                moveTimeDecrease = 0.08f;
                playerData.GetAnimationSpeed += 0.15f;
            }

            if (currentSpeedLevel < 5 && currentSpeedLevel > 0)
            {
                
                levelUpFeeSpeed *= levelIncreaseFeeRateSpeed;
                levelUpFeeSpeed = Mathf.Ceil(levelUpFeeSpeed);
                PlayerPrefs.SetFloat("levelUpFee", levelUpFeeSpeed);
                Debug.Log("levelUpFeeSpeed: " + PlayerPrefs.GetFloat("levelUpFee"));
                levelIncreaseFeeRateSpeed -= 0.075f;
                PlayerPrefs.SetFloat("speedFeeRate", levelIncreaseFeeRateSpeed);

            }
            else if (currentSpeedLevel < 15 && currentSpeedLevel >= 5)
            {
                
                levelIncreaseFeeRateSpeed = 1.15f;       //Asagidaki else if içerisindeki //burada  yazan float degeri degistirin.
                PlayerPrefs.SetFloat("speedFeeRate", levelIncreaseFeeRateSpeed);
                levelUpFeeSpeed *= levelIncreaseFeeRateSpeed;
                levelUpFeeSpeed = Mathf.Ceil(levelUpFeeSpeed);
                PlayerPrefs.SetFloat("levelUpFee", levelUpFeeSpeed);
            }
            else if (currentSpeedLevel <= 70 && currentSpeedLevel >= 15)
            {
              
                levelIncreaseFeeRateSpeed = 1.05f;       //Asagidaki else if içerisindeki //burada  yazan float degeri degistirin.
                PlayerPrefs.SetFloat("speedFeeRate", levelIncreaseFeeRateSpeed);
                levelUpFeeSpeed *= levelIncreaseFeeRateSpeed;
                levelUpFeeSpeed = Mathf.Ceil(levelUpFeeSpeed);
                PlayerPrefs.SetFloat("levelUpFee", levelUpFeeSpeed);
            }

            if (currentLevelText < 5 && currentLevelText > 0)
            {
                float levelIncrease = levelIncreaseFeeRateSpeed;

                float carpim = levelUpFeeSpeed * (levelIncrease);
                levelUpFeeSpeedText = Mathf.Ceil(carpim);
                PlayerPrefs.SetFloat("levelUpFeeSpeedText", levelUpFeeSpeedText);

            }
            if (currentLevelText < 15 && currentLevelText >= 5)
            {

                float carpim = levelUpFeeSpeed * (1.15f);        //burada
                levelUpFeeSpeedText = Mathf.Ceil(carpim);
                PlayerPrefs.SetFloat("levelUpFeeSpeedText", levelUpFeeSpeedText);
            }
            if (currentLevelText <= 70 && currentLevelText >= 15)
            {
                float carpim = levelUpFeeSpeed * (1.05f);        //burada
                levelUpFeeSpeedText = Mathf.Ceil(carpim);
                PlayerPrefs.SetFloat("levelUpFeeSpeedText", levelUpFeeSpeedText);
            }

            playerData.GetMoveTime -= moveTimeDecrease;
            PlayerPrefs.SetFloat("moveTime", playerData.GetMoveTime);
            Debug.Log("move time " + playerData.GetMoveTime);
            playerData.GetJumpAnimationTimer -= jumpAnimationTimerDecrease;
            
            PlayerPrefs.SetFloat("jumpAnimationTimer", playerData.GetJumpAnimationTimer);
            Debug.Log("animation timer" + playerData.GetJumpAnimationTimer);
            playerData.GetBendingAnimationTimer -= bendingAnimationTimerDecrease;
            PlayerPrefs.SetFloat("bendingAnimationTimer", playerData.GetBendingAnimationTimer);

            
            PlayerPrefs.SetFloat("getAnimationSpeed", playerData.GetAnimationSpeed);

            playerAnim1.speed = playerData.GetAnimationSpeed;
            playerAnim2.speed = playerData.GetAnimationSpeed;
            Debug.Log("player anim speed" + playerAnim1.speed);


            playerData.GetJumpDistance += jumpDistanceIncrease;
            PlayerPrefs.SetFloat("jumpDistance", playerData.GetJumpDistance);

            playerData.GetSecondPlayerPosZ += 0.25f *2;
            PlayerPrefs.SetFloat("secondPlayerPosZ", playerData.GetSecondPlayerPosZ);
            if (secondPlayer.transform.position.z> firstPlayer.transform.position.z)
            {
                secondPlayer.transform.DOMoveZ(secondPlayer.transform.position.z + (jumpDistanceIncrease / 2), 0f);
            }else
            {
              firstPlayer.transform.DOMoveZ(firstPlayer.transform.position.z + (jumpDistanceIncrease / 2), 0f);
            }
            


            playerData.atlarkenAzalanStamina -= playerData.atlarkenUpgradeStaminaAzalmasi;
            PlayerPrefs.SetFloat("atlarkenAzalma", playerData.atlarkenAzalanStamina);

            playerData.egilmeKalkmaAzalmaStamina -= playerData.egilmeKalkmaUpgradeStaminaAzalmasi;
            PlayerPrefs.SetFloat("egilmeKalkmaAzalma", playerData.egilmeKalkmaAzalmaStamina);


            playerData.GetMoney -= levelUpFeeSpeed;
            playerData.GetMoney = Mathf.Ceil(playerData.GetMoney);
            PlayerPrefs.SetFloat("totalMoney", playerData.GetMoney);


            currentSpeedLevel += 1;
            PlayerPrefs.SetInt("currentSpeedLevel", currentSpeedLevel);
            

        }
    }

    public void PlayerPrefsCheck()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerData.GetSpeedMaxLevel = PlayerPrefs.GetInt("speedMaxLevel");
        if (PlayerPrefs.GetFloat("secondPlayerPosZ") == 0)
        {
            playerData.GetSecondPlayerPosZ = 0.0001f;
            PlayerPrefs.SetFloat("secondPlayerPosZ", playerData.GetSecondPlayerPosZ);
        }

        secondPlayer.transform.DOMoveZ(secondPlayer.transform.position.z + PlayerPrefs.GetFloat("secondPlayerPosZ"), 0f);
        playerAnim1.speed = playerData.GetAnimationSpeed;
        playerAnim2.speed = playerData.GetAnimationSpeed;

        playerData.GetJumpDistance = PlayerPrefs.GetFloat("jumpDistance");
        if (PlayerPrefs.GetFloat("jumpDistance") == 0)
        {
            playerData.GetJumpDistance = 8;
            PlayerPrefs.SetFloat("jumpDistance", playerData.GetJumpDistance);
        }

 

        playerData.GetAnimationSpeed = PlayerPrefs.GetFloat("getAnimationSpeed");
        if (PlayerPrefs.GetFloat("getAnimationSpeed") == 0)
        {
            playerData.GetAnimationSpeed = 1f;
            PlayerPrefs.SetFloat("getAnimationSpeed", playerData.GetAnimationSpeed);
        }
        playerData.GetBendingAnimationTimer = PlayerPrefs.GetFloat("bendingAnimationTimer");
        if (PlayerPrefs.GetFloat("bendingAnimationTimer") == 0)
        {
            playerData.GetBendingAnimationTimer = 0.20f;
            PlayerPrefs.SetFloat("bendingAnimationTimer", playerData.GetBendingAnimationTimer);
        }
        playerData.GetJumpAnimationTimer = PlayerPrefs.GetFloat("jumpAnimationTimer");
        if (PlayerPrefs.GetFloat("jumpAnimationTimer") == 0)
        {
            playerData.GetJumpAnimationTimer = 1.400f;
            PlayerPrefs.SetFloat("jumpAnimationTimer", playerData.GetJumpAnimationTimer);
        }
        playerData.GetMoveTime = PlayerPrefs.GetFloat("moveTime");
        if (PlayerPrefs.GetFloat("moveTime") == 0)
        {
            playerData.GetMoveTime = 1.65f;
            PlayerPrefs.SetFloat("moveTime", playerData.GetMoveTime);
        }


        currentSpeedLevel = PlayerPrefs.GetInt("currentSpeedLevel");

        levelUpFeeSpeedText = PlayerPrefs.GetFloat("levelUpFeeSpeedText");
        if (PlayerPrefs.GetFloat("levelUpFeeSpeedText") <= 0)
        {
            levelUpFeeSpeedText += 25f;
            PlayerPrefs.SetFloat("levelUpFeeSpeedText", levelUpFeeSpeedText);
        }

        levelUpFeeSpeed = PlayerPrefs.GetFloat("levelUpFee");
        if (PlayerPrefs.GetFloat("levelUpFee") == 0)
        {
            levelUpFeeSpeed += 25f;
            PlayerPrefs.SetFloat("levelUpFee", levelUpFeeSpeed);
        }

        levelIncreaseFeeRateSpeed = PlayerPrefs.GetFloat("speedFeeRate");

        if (PlayerPrefs.GetFloat("speedFeeRate") == 0)
        {
            levelIncreaseFeeRateSpeed = 1.85f;
            PlayerPrefs.SetFloat("speedFeeRate", levelIncreaseFeeRateSpeed);
        }
        currentLevelText = PlayerPrefs.GetInt("currentLevelText");

        playerData.GetSpeedUpgradePrefs = PlayerPrefs.GetInt("speedUpgradePrefs");
    }
}
