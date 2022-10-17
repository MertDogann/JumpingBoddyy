using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeUpgrade : MonoBehaviour
{

    [SerializeField] JumpingScriptable playerData = null;
    [SerializeField] private float levelUpFeeMoney = 0;          //Money upgradesi upgrade icin gereken money degeri.
    private float levelUpFeeMoneyText;
    private int currentMoneyText;
    [SerializeField] private int currentMoneyLevel = 0;   // Money upgradesi level degeri.
    [SerializeField] private float levelIncreaseFeeRateMoney = 1.45f;     //Money upgradesi money artis orani.
    private GameManager gameManager;
    private bool maxLevelBool;


    private void Awake()
    {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerData.GetIncomeMaxLevel = PlayerPrefs.GetInt("incomeMaxLevel");
        playerData.GetUnitOfMoney = PlayerPrefs.GetFloat("unitOfMoney");
        if (PlayerPrefs.GetFloat("unitOfMoney") == 0)
        {
            playerData.GetUnitOfMoney = 10;
            PlayerPrefs.SetFloat("unitOfMoney", playerData.GetUnitOfMoney);
        }


        playerData.GetMoney = PlayerPrefs.GetFloat("totalMoney");

        playerData.GetMoney = Mathf.Floor(playerData.GetMoney);
        if (PlayerPrefs.GetFloat("totalMoney" )== 0)
        {
            playerData.GetMoney = 3000f;
            PlayerPrefs.SetFloat("totalMoney", playerData.GetMoney);
        }
        PlayerPrefs.SetFloat("totalMoney", playerData.GetMoney);

        levelUpFeeMoney = PlayerPrefs.GetFloat("levelUpFeeMoney");
        if (PlayerPrefs.GetFloat("levelUpFeeMoney") == 0)
        {
            levelUpFeeMoney += 25f;
            PlayerPrefs.SetFloat("levelUpFeeMoney", levelUpFeeMoney);
        }

        levelUpFeeMoneyText = PlayerPrefs.GetFloat("levelUpFeeMoneyText");
        if (PlayerPrefs.GetFloat("levelUpFeeMoneyText") == 0)
        {
            levelUpFeeMoneyText += 25f;
            PlayerPrefs.SetFloat("levelUpFeeMoneyText", levelUpFeeMoneyText);
        }

        levelIncreaseFeeRateMoney = PlayerPrefs.GetFloat("moneyFeeRate");
        if (PlayerPrefs.GetFloat("moneyFeeRate") == 0)
        {
            levelIncreaseFeeRateMoney = 1.85f;
            PlayerPrefs.SetFloat("moneyFeeRate", levelIncreaseFeeRateMoney);
        }

        currentMoneyText = PlayerPrefs.GetInt("currentMoneyText");
        currentMoneyLevel = PlayerPrefs.GetInt("currentMoneyLevel");
    }



    public void IncremenetMoney()
    {
        Debug.Log("basildi money");
        if (playerData.GetMoney > levelUpFeeMoneyText && playerData.GetIncomeMaxLevel <=8)
        {
            playerData.GetIncomeMaxLevel++;
            PlayerPrefs.SetInt("incomeMaxLevel", playerData.GetIncomeMaxLevel);
            
            gameManager.upgradeParticle[4].Play();
            gameManager.upgradeParticle[5].Play();

            currentMoneyText += 1;

            PlayerPrefs.SetInt("currentMoneyText", currentMoneyText);

            playerData.GetUnitOfMoney += playerData.GetIncreaseRateToMoney;
            PlayerPrefs.SetFloat("unitOfMoney", playerData.GetUnitOfMoney);

            if (currentMoneyLevel < 5 && currentMoneyLevel > 0)
            {

                levelUpFeeMoney *= levelIncreaseFeeRateMoney;
                levelUpFeeMoney = Mathf.Ceil(levelUpFeeMoney);
                PlayerPrefs.SetFloat("levelUpFeeMoney", levelUpFeeMoney);
                Debug.Log("levelUpFeeMoney: " + PlayerPrefs.GetFloat("levelUpFeeMoney"));
                levelIncreaseFeeRateMoney -= 0.075f;
                PlayerPrefs.SetFloat("moneyFeeRate", levelIncreaseFeeRateMoney);


            }
            else if (currentMoneyLevel < 15 && currentMoneyLevel >= 5)
            {
                
                levelIncreaseFeeRateMoney = 1.12f;       //Asagidaki else if içerisindeki //burada  yazan float degeri degistirin.
                PlayerPrefs.SetFloat("moneyFeeRate", levelIncreaseFeeRateMoney);
                levelUpFeeMoney *= levelIncreaseFeeRateMoney;
                levelUpFeeMoney = Mathf.Ceil(levelUpFeeMoney);
                PlayerPrefs.SetFloat("levelUpFeeMoney", levelUpFeeMoney);
            }
            else if (currentMoneyLevel <= 70 && currentMoneyLevel >= 15)
            {
                
                levelIncreaseFeeRateMoney = 1.05f;       //Asagidaki else if içerisindeki //burada  yazan float degeri degistirin.
                PlayerPrefs.SetFloat("moneyFeeRate", levelIncreaseFeeRateMoney);
                levelUpFeeMoney *= levelIncreaseFeeRateMoney;
                levelUpFeeMoney = Mathf.Ceil(levelUpFeeMoney);
                PlayerPrefs.SetFloat("levelUpFeeMoney", levelUpFeeMoney);
            }


            if (currentMoneyText < 5 && currentMoneyText > 0)
            {
                float levelIncrease = levelIncreaseFeeRateMoney;
                float carpim = levelUpFeeMoney * (levelIncrease);
                levelUpFeeMoneyText = Mathf.Ceil(carpim);
                PlayerPrefs.SetFloat("levelUpFeeMoneyText", levelUpFeeMoneyText);

            }
            if (currentMoneyText < 15 && currentMoneyText >= 5)
            {
                float carpim = levelUpFeeMoney * (1.12f);        //burada
                levelUpFeeMoneyText = Mathf.Ceil(carpim);
                PlayerPrefs.SetFloat("levelUpFeeMoneyText", levelUpFeeMoneyText);

            }
            if (currentMoneyText <= 70 && currentMoneyText >= 15)
            {
                float carpim = levelUpFeeMoney * (1.05f);        //burada
                levelUpFeeMoneyText = Mathf.Ceil(carpim);
                PlayerPrefs.SetFloat("levelUpFeeMoneyText", levelUpFeeMoneyText);

            }


            playerData.GetMoney -= levelUpFeeMoney;
            playerData.GetMoney = Mathf.Ceil(playerData.GetMoney);
            PlayerPrefs.SetFloat("totalMoney", playerData.GetMoney);

            currentMoneyLevel += 1;
            PlayerPrefs.SetInt("currentMoneyLevel", currentMoneyLevel);

        }
    }
}
