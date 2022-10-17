using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUpgrade : MonoBehaviour
{
    [SerializeField] JumpingScriptable playerData = null;
    private GameManager gameManager;
    private LevelController levelController;


    [SerializeField] private int currentStaminaLevel = 0;   // Stamina upgradesi level degeri.
    private int currentStaminaText;
    [SerializeField] private float levelUpFeeStamina = 0;          //Stamina upgradesi upgrade icin gereken money degeri.
    private float levelUpFeeStaminaText;
    [SerializeField] private float levelIncreaseFeeRateStamina = 1.45f;     //Stamina upgradesi stamina artis orani.




    private void Awake()
    {

        PlayerPrefsCheck();


    }


    public void IncrementStamina()
    {
        if (playerData.GetMoney > levelUpFeeStaminaText && playerData.GetStaminaMaxLevel <=8)
        {
            playerData.GetStaminaMaxLevel++;
            PlayerPrefs.SetInt("staminaMaxLevel", playerData.GetStaminaMaxLevel);
            

            gameManager.upgradeParticle[0].Play();
            gameManager.upgradeParticle[1].Play();
            currentStaminaText += 1;
            PlayerPrefs.SetInt("currentStaminaText", currentStaminaText);
            if (currentStaminaLevel < 5 && currentStaminaLevel > 0)
            {
                levelUpFeeStamina *= levelIncreaseFeeRateStamina;
                levelUpFeeStamina = Mathf.Ceil(levelUpFeeStamina);

                PlayerPrefs.SetFloat("levelUpFeeStamina", levelUpFeeStamina);
                levelIncreaseFeeRateStamina -= 0.075f;
                PlayerPrefs.SetFloat("staminaFeeRate", levelIncreaseFeeRateStamina);
            }
            else if (currentStaminaLevel < 15 && currentStaminaLevel >= 5)
            {
                levelIncreaseFeeRateStamina = 1.15f;       //Asagidaki else if içerisindeki //burada  yazan float degeri degistirin.
                PlayerPrefs.SetFloat("staminaFeeRate", levelIncreaseFeeRateStamina);
                levelUpFeeStamina *= levelIncreaseFeeRateStamina;
                levelUpFeeStamina = Mathf.Ceil(levelUpFeeStamina);
                PlayerPrefs.SetFloat("levelUpFeeStamina", levelUpFeeStamina);
            }
            else if (currentStaminaLevel <= 70 && currentStaminaLevel >= 15)
            {
                levelIncreaseFeeRateStamina = 1.05f;       //Asagidaki else if içerisindeki //burada  yazan float degeri degistirin.
                PlayerPrefs.SetFloat("staminaFeeRate", levelIncreaseFeeRateStamina);
                levelUpFeeStamina *= levelIncreaseFeeRateStamina;
                levelUpFeeStamina = Mathf.Ceil(levelUpFeeStamina);
                PlayerPrefs.SetFloat("levelUpFeeStamina", levelUpFeeStamina);
            }



            if (currentStaminaText < 5 && currentStaminaText > 0)
            {
                float levelIncrease = levelIncreaseFeeRateStamina;
                float carpim = levelUpFeeStamina * (levelIncrease);
                levelUpFeeStaminaText = Mathf.Ceil(carpim);
                PlayerPrefs.SetFloat("levelUpFeeStaminaText", levelUpFeeStaminaText);
            }
            if (currentStaminaText < 15 && currentStaminaText >= 5)
            {
                float carpim = levelUpFeeStamina * (1.15f);        //burada
                levelUpFeeStaminaText = Mathf.Ceil(carpim);
                PlayerPrefs.SetFloat("levelUpFeeStaminaText", levelUpFeeStaminaText);
            }

            if (currentStaminaText <= 70 && currentStaminaText >= 15)
            {
                float carpim = levelUpFeeStamina * (1.05f);        //burada
                levelUpFeeStaminaText = Mathf.Ceil(carpim);
                PlayerPrefs.SetFloat("levelUpFeeStaminaText", levelUpFeeStaminaText);
            }


            playerData.GetMoney -= levelUpFeeStamina;
            playerData.GetMoney = Mathf.Ceil(playerData.GetMoney);
            PlayerPrefs.SetFloat("totalMoney", playerData.GetMoney);

            

            currentStaminaLevel += 1;
            PlayerPrefs.SetInt("currentStaminaLevel", currentStaminaLevel);


            Debug.Log("isgame active" + levelController.isGameActive);

            //playerData.GetResting = playerData.GetMaxStamina;
            //playerData.GetStamina = playerData.GetResting;
            if (!levelController.isGameActive)
            {
                playerData.GetMaxStamina += playerData.GetIncreaseToStamina;
                PlayerPrefs.SetFloat("maxStamina", playerData.GetMaxStamina);
                playerData.GetStamina = playerData.GetMaxStamina;
                
            }else
            {
                playerData.GetMaxStamina += playerData.GetIncreaseToStamina;
                PlayerPrefs.SetFloat("maxStamina", playerData.GetMaxStamina);
                StartCoroutine(Delay(playerData.GetIncreaseToStamina));

            }
            

            playerData.GetRestingRate += 0.7f;
            PlayerPrefs.SetFloat("getRestingRate", playerData.GetRestingRate);
            playerData.GetStartRestingRate = playerData.GetRestingRate;

            playerData.GetStaminaIncreaseTime += 4;
            PlayerPrefs.SetInt("staminaIncreaseTime", playerData.GetStaminaIncreaseTime);




            //Ter
            playerData.GetSweat = playerData.GetMaxStamina * playerData.terlemeYuzde / 100;

            //Kizarma
            gameManager.GetHot = playerData.GetMaxStamina * playerData.kizarmaYuzde / 100;
            
        }
    }

    private void PlayerPrefsCheck()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        playerData.GetStaminaMaxLevel = PlayerPrefs.GetInt("staminaMaxLevel");
        playerData.GetRestingRate = PlayerPrefs.GetFloat("getRestingRate");
        if (PlayerPrefs.GetFloat("getRestingRate") == 0)
        {
            playerData.GetRestingRate = 8f;
            PlayerPrefs.SetFloat("getRestingRate", playerData.GetRestingRate);
        }

        playerData.GetMaxStamina = PlayerPrefs.GetFloat("maxStamina");
        if (playerData.GetMaxStamina == 0)
        {
            playerData.GetMaxStamina = playerData.GetLevelMinStamina;
            PlayerPrefs.SetFloat("maxStamina", playerData.GetMaxStamina);
        }
        currentStaminaLevel = PlayerPrefs.GetInt("currentStaminaLevel");
        currentStaminaText = PlayerPrefs.GetInt("currentStaminaText");
        levelUpFeeStamina = PlayerPrefs.GetFloat("levelUpFeeStamina");
        if (PlayerPrefs.GetFloat("levelUpFeeStamina") == 0)
        {
            levelUpFeeStamina += 25f;
            PlayerPrefs.SetFloat("levelUpFeeStamina", levelUpFeeStamina);
        }
        levelUpFeeStaminaText = PlayerPrefs.GetFloat("levelUpFeeStaminaText");
        if (PlayerPrefs.GetFloat("levelUpFeeStaminaText") == 0)
        {
            levelUpFeeStaminaText += 25f;
            PlayerPrefs.SetFloat("levelUpFeeStaminaText", levelUpFeeStaminaText);
        }
        levelIncreaseFeeRateStamina = PlayerPrefs.GetFloat("staminaFeeRate");
        if (PlayerPrefs.GetFloat("staminaFeeRate") == 0)
        {
            levelIncreaseFeeRateStamina = 1.85f;
            PlayerPrefs.SetFloat("staminaFeeRate", levelIncreaseFeeRateStamina);
        }

        playerData.GetStaminaIncreaseTime = PlayerPrefs.GetInt("staminaIncreaseTime");
        if (PlayerPrefs.GetInt("staminaIncreaseTime") == 0)
        {
            playerData.GetStaminaIncreaseTime = 32;
            PlayerPrefs.SetInt("staminaIncreaseTime", playerData.GetStaminaIncreaseTime);
        }

        playerData.atlarkenAzalanStamina = PlayerPrefs.GetFloat("atlarkenAzalma");

        if (PlayerPrefs.GetFloat("atlarkenAzalma") == 0)
        {
            playerData.atlarkenAzalanStamina = playerData.levelMinAtlama;
            PlayerPrefs.SetFloat("atlarkenAzalma", playerData.atlarkenAzalanStamina);
        }

        playerData.egilmeKalkmaAzalmaStamina = PlayerPrefs.GetFloat("egilmeKalkmaAzalma");
        if (PlayerPrefs.GetFloat("egilmeKalkmaAzalma") == 0)
        {
            playerData.egilmeKalkmaAzalmaStamina = playerData.levelMinEgilmeKalkma;
            PlayerPrefs.SetFloat("egilmeKalkmaAzalma", playerData.egilmeKalkmaAzalmaStamina);
        }

    }


    public IEnumerator Delay(float addAmount)
    {
        float newAmount = playerData.GetStamina + addAmount;
        while (playerData.GetStamina < newAmount)
        {
            playerData.GetStamina += Time.deltaTime *85f;


            yield return null;
        }
    }
}
