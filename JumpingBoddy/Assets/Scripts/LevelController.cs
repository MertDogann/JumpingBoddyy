using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LevelController : MonoBehaviour
{
    [SerializeField] JumpingScriptable playerDataNow = null;
    [SerializeField] JumpingScriptable playerDataNext = null;
    public static LevelController Instance;
    private GameManager gameManager;
    private float currentLevel = 0;
    public bool isGameActive = false;
    public bool levelFinish = false;
    //[SerializeField] private TextMeshProUGUI levelText;
    [Header("Speed")]
    public TextMeshProUGUI speedLevelText;
    public TextMeshProUGUI speedUpgradeFeeText;

    [Header("Stamina")]
    public TextMeshProUGUI staminaLevelText;
    public TextMeshProUGUI staminaUpgradeFeeText;


    [Header("Money")]
    public TextMeshProUGUI moneyLevelText;
    public TextMeshProUGUI moneyUpgradeFeeText;

    public TextMeshProUGUI totalMoneyText;       //Toplam money.
    [SerializeField] private GameObject startMenu , startButton;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject levelVictoryMenu;
    [SerializeField] private GameObject finishMenu;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private GameObject levelFinishConfeti;
    public GameObject settingPanel;
    [SerializeField] private GameObject vibrationImage;
    [SerializeField] private Animator vibrationAnim;
    [SerializeField] private GameObject upgradeButton;
    public bool vibration;
    private int levelll = 0;
    public bool upgradeOnOff;
    private bool finishLevelControl = false;

    private float highScore;
    
    void Awake()
    {
        Instance = this;
        isGameActive = false;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        

    }

    void Start()
    {
        levelll = PlayerPrefs.GetInt("currentLevell ");
        if (levelll == 0)
        {
            levelll = 1;
            PlayerPrefs.SetInt("currentLevell ", levelll);

        }
        StartText();
        upgradeOnOff = true;
        currentLevel = PlayerPrefs.GetFloat("currentLevel ");
        
        vibration = true;
        //if (SceneManager.GetActiveScene().name != "Level " + currentLevel)
        //{
        //    SceneManager.LoadScene("Level " + 0);


        //}
        UpgradesMaxLevelControl();
        // Save boolean using PlayerPrefs
        PlayerPrefs.SetInt("foo", vibration ? 1 : 0);
        // Get boolean using PlayerPrefs
        vibration = PlayerPrefs.GetInt("foo") == 1 ? true : false;

        highScore = PlayerPrefs.GetFloat("highScore");
    }

   

    void Update()
    {
       
        totalMoneyText.text = PlayerPrefs.GetFloat("totalMoney").ToString();
        LevelFinishAndLevel0();
        LevelFailed();
    }

    private void LevelFinishAndLevel0()
    {
        if (levelFinish && !finishLevelControl)        // && levelll == 0
        {
            finishLevelControl = true;
            levelll++;
            PlayerPrefs.SetInt("currentLevell ", levelll);
            isGameActive = false;
            currentLevel++;
            PlayerPrefs.SetFloat("currentLevel ", currentLevel);
            FeatureResetOnLevelNext();
            StartCoroutine(LoadNextLevelWait());
            //playerController.sweatParticle.Stop();
        }
    }

    private void LevelFailed() 
    {
        if (playerDataNow.GetStamina <= 0.2f)
        {
            
            isGameActive = false;
            StartCoroutine(RestartGameWait());
        }
    }

    IEnumerator LoadNextLevelWait()
    {
        //yield return new WaitForSeconds(1.5f);
        //playerController.particle[3].Play();
        yield return new WaitForSeconds(0.3f);
        levelFinishConfeti.SetActive(true);
        yield return new WaitForSeconds(1f);

        levelVictoryMenu.SetActive(true);
        gameUI.SetActive(false);

        

    }



    public void TapToStart()
    {
        StartCoroutine(isgameWait());
        startMenu.SetActive(false);
        gameUI.SetActive(true);
        upgradeOnOff = false; ;
}

    public void LevelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelVictory()
    {
        if (PlayerPrefs.GetFloat("currentLevel ") >= 1)
        {
            currentLevel = 1;
            PlayerPrefs.SetFloat("currentLevel ", currentLevel);
        }
        SceneManager.LoadScene("Level " + 0);  //PlayerPrefs.GetFloat("currentLevel "))
    }

    IEnumerator RestartGameWait()
    {
        HighScore.Current.HighScoree();
        startMenu.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        gameUI.SetActive(false);
        finishMenu.SetActive(true);
    }


    private void StartText()
    {
        speedLevelText.text = PlayerPrefs.GetInt("speedMaxLevel").ToString();   //currentSpeedLevel
        speedUpgradeFeeText.text = (PlayerPrefs.GetFloat("levelUpFeeSpeedText")).ToString();


        staminaUpgradeFeeText.text = PlayerPrefs.GetFloat("levelUpFeeStaminaText").ToString();
        staminaLevelText.text = PlayerPrefs.GetInt("staminaMaxLevel").ToString();       //currentStaminaLevel

        moneyUpgradeFeeText.text = PlayerPrefs.GetFloat("levelUpFeeMoneyText").ToString();
        moneyLevelText.text = PlayerPrefs.GetInt("incomeMaxLevel").ToString();       //currentMoneyLevel




        totalMoneyText.text = PlayerPrefs.GetFloat("totalMoney").ToString();

        currentLevelText.text = (PlayerPrefs.GetInt("currentLevell ")).ToString();
        

    }

    public void SettingPanel()
    {
        gameManager.timer = 0.1f;
        
        isGameActive = false;

        settingPanel.SetActive(true);
    }

    public void Resume()
    {
        if (!startButton.activeInHierarchy)
        {
            isGameActive = true;
            Debug.Log("zart zortsafasf");
        }
        if (vibration)
        {
            vibrationImage.SetActive(false);
        }
        else
        {
            vibrationImage.SetActive(true);
        }
        settingPanel.SetActive(false);
        
    }

    public void VibrationOnOff()
    {
        if (vibration)
        {
            vibrationImage.SetActive(true);
            vibration = false;
            
        }
        else
        {
            vibrationImage.SetActive(false);
            vibrationAnim.SetTrigger("vibration");
            vibration = true;
            

            
        }
        
    }

    public void UpgradeButtonNext()
    {
        StartCoroutine(UpgradeNextWait());
        
    }

    IEnumerator UpgradeNextWait()
    {
        Debug.Log("butona basýldý");
        startMenu.SetActive(false);
        startMenu.transform.GetChild(0).gameObject.SetActive(true);
        upgradeButton.SetActive(false);
        
        yield return new WaitForSeconds(0.4f);
        upgradeOnOff = false;
        yield return new WaitForSeconds(4f);
        gameManager.indexUpgrade = 0;
        
        
    }

    public void FeatureResetOnLevelNext()       //Bir sonraki levelde script deðiþtirme
    {
        

        //Speed
        playerDataNext.GetSecondPlayerPosZ = 0f;
        PlayerPrefs.SetFloat("secondPlayerPosZ", playerDataNext.GetSecondPlayerPosZ);
        playerDataNext.GetJumpAnimationTimer = 0f;
        PlayerPrefs.SetFloat("jumpAnimationTimer", playerDataNext.GetJumpAnimationTimer);
        playerDataNext.GetBendingAnimationTimer = 0f;
        PlayerPrefs.SetFloat("bendingAnimationTimer", playerDataNext.GetBendingAnimationTimer);
        playerDataNext.GetMoveTime = 0f;
        PlayerPrefs.SetFloat("moveTime", playerDataNext.GetMoveTime);
        playerDataNext.GetAnimationSpeed = 0f;
        PlayerPrefs.SetFloat("getAnimationSpeed", playerDataNext.GetAnimationSpeed);
        playerDataNext.GetJumpDistance = 0f;
        PlayerPrefs.SetFloat("jumpDistance", playerDataNext.GetJumpDistance);
        playerDataNext.GetSpeedUpgradePrefs = 0;
        PlayerPrefs.SetInt("speedUpgradePrefs", playerDataNext.GetSpeedUpgradePrefs);
        playerDataNext.GetSpeedMaxLevel = 0;
        PlayerPrefs.SetInt("speedMaxLevel", playerDataNext.GetSpeedMaxLevel);
        playerDataNext.GetIncomeMaxLevel = 0;
        PlayerPrefs.SetInt("incomeMaxLevel", playerDataNext.GetIncomeMaxLevel);
        playerDataNext.GetStaminaMaxLevel = 0;
        PlayerPrefs.SetInt("staminaMaxLevel", playerDataNext.GetStaminaMaxLevel);
        //Stamina
        playerDataNext.GetStaminaIncreaseTime = 13;
        PlayerPrefs.SetInt("staminaIncreaseTime", playerDataNext.GetStaminaIncreaseTime);
        playerDataNext.GetRestingRate = 7.5f;
        PlayerPrefs.SetFloat("getRestingRate", playerDataNext.GetRestingRate);
        playerDataNext.GetMaxStamina = playerDataNext.GetLevelMinStamina;
        PlayerPrefs.SetFloat("maxStamina", playerDataNext.GetMaxStamina);

        //High Score
        playerDataNext.GetHighScorePos = 0f;
        PlayerPrefs.SetFloat("highScorePos", playerDataNext.GetHighScorePos);



    }
    private void UpgradesMaxLevelControl()
    {
        if (playerDataNow.GetStaminaMaxLevel >= 9)
        {
            staminaUpgradeFeeText.text = "MAX";
        }

        if (playerDataNow.GetSpeedMaxLevel >= 9)
        {
            speedUpgradeFeeText.text = "MAX";
        }
        if (playerDataNow.GetIncomeMaxLevel >= 9)
        {
            moneyUpgradeFeeText.text = "MAX";
        }

    }

    public void IncrementSpeed()
    {
        speedLevelText.text = PlayerPrefs.GetInt("speedMaxLevel").ToString();
        speedUpgradeFeeText.text = PlayerPrefs.GetFloat("levelUpFeeSpeedText").ToString();
        totalMoneyText.text = PlayerPrefs.GetFloat("totalMoney").ToString();
        if (playerDataNow.GetSpeedMaxLevel >= 9)
        {
            speedUpgradeFeeText.text = "MAX";
        }
    }

    public void IncrementStamina()
    {
        staminaLevelText.text = PlayerPrefs.GetInt("staminaMaxLevel").ToString();
        staminaUpgradeFeeText.text = PlayerPrefs.GetFloat("levelUpFeeStaminaText").ToString();
        totalMoneyText.text = PlayerPrefs.GetFloat("totalMoney").ToString();
        if (playerDataNow.GetStaminaMaxLevel >= 9)
        {
            staminaUpgradeFeeText.text = "MAX";
        }

    }

    public void IncremenetMoney()
    {
        moneyLevelText.text = PlayerPrefs.GetInt("incomeMaxLevel").ToString();
        moneyUpgradeFeeText.text = PlayerPrefs.GetFloat("levelUpFeeMoneyText").ToString();
        totalMoneyText.text = PlayerPrefs.GetFloat("totalMoney").ToString();
        if (playerDataNow.GetIncomeMaxLevel >= 9)
        {
            moneyUpgradeFeeText.text = "MAX";
        }
    }


    IEnumerator isgameWait()
    {
        yield return new WaitForSeconds(0.2f);
        isGameActive = true;
    }



}
