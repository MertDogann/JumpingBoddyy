using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = ("Player Data"), order = 1)]
public class JumpingScriptable : ScriptableObject
{
    public static JumpingScriptable Instance ;
    [Header("Stamina")]

    [SerializeField] private float stamina;
    [SerializeField] private float increaseToStamina;  // Bunu guclendirerek gucu arttiriyoruz.
    [SerializeField] private float maxStamina;

    [Range(0, 100)][SerializeField] public int terlemeYuzde;
    [Range(0, 100)][SerializeField] public int kizarmaYuzde;
    [SerializeField] private float restingRate;     //Elimizi cektigimizdeki dinlenme orani.
    [SerializeField] private float levelMinStamina;

    [Header("Money")]

    [SerializeField] private float increaseRateToMoney;     //Bunu guclendirerek her adimdaki parayi arttiriyoruz.
    [SerializeField] private float maxUnitOfMoney;  // Her levelin adim basi verdigi paraya maksimum sinir koyuyoruz.
    [SerializeField] private float finishMoney;
    public float totalMoney;          //UI'da gözüken kazandigimiz toplam para.
    [SerializeField] private float unitOfMoney;   //Her adimda kazanilacak para.    
    private float moveTime = 0;
    private float animationTimerControl;
    private float jumpAnimationTimer;
    private float bendingAnimationTimer;
    private float animationSpeed;
    private int staminaIncreaseTime;
    private float jumpDistance;
    private float secondPlayerPosZ;
    private float startRestingRate;
    private float sweat;

    [Header("Speed")]

    public float atlarkenAzalanStamina;
    public float atlarkenUpgradeStaminaAzalmasi;
    public float egilmeKalkmaAzalmaStamina;
    public float egilmeKalkmaUpgradeStaminaAzalmasi;
    public float levelMinAtlama;
    public float levelMinEgilmeKalkma;

    private int speedUpgradePrefs;

    private float highScorePos;
    private int speedmaxLevel;
    private int staminamaxLevel;
    private int incomemaxLevel;


    public int GetSpeedMaxLevel { get { return speedmaxLevel; } set { speedmaxLevel = value; } }
    public int GetStaminaMaxLevel { get { return staminamaxLevel; } set { staminamaxLevel = value; } }

    public int GetIncomeMaxLevel { get { return incomemaxLevel; } set { incomemaxLevel = value; } }


    public float GetHighScorePos { get { return highScorePos; } set { highScorePos = value; } }

    public int GetSpeedUpgradePrefs {  get { return speedUpgradePrefs; } set { speedUpgradePrefs = value; }  }
    public float GetSweat { get { return sweat; } set { sweat = value; } }

    public float GetStartRestingRate {  get { return startRestingRate; } set { startRestingRate = value; } }

    
    public float GetSecondPlayerPosZ
    {
        get { return secondPlayerPosZ; }
        set { secondPlayerPosZ = value; }
    }

    public float GetJumpDistance
    {
        get { return jumpDistance; }
        set { jumpDistance = value; }
    }

    public int GetStaminaIncreaseTime
    {
        get { return staminaIncreaseTime; }
        set { staminaIncreaseTime = value; }
    }
    public float GetAnimationSpeed
    {
       get { return animationSpeed; }
        set { animationSpeed = value; }
    }

    public float GetBendingAnimationTimer
    {
        get { return bendingAnimationTimer; }
        set { bendingAnimationTimer = value; }
    }
    public float GetJumpAnimationTimer
    {
        get { return jumpAnimationTimer; }
        set { jumpAnimationTimer = value; }
    } 


    public float GetAnimationTimerControl
    {
        get { return animationTimerControl; }
        set { animationTimerControl = value; }
    }
    public float GetMoveTime
    {
        get { return moveTime; }
        set { moveTime = value; }
    }

    public float GetLevelMinStamina
    {
        get { return levelMinStamina; }
        set { levelMinStamina = value; }
    }


    public float GetRestingRate
    {
        get { return restingRate; }
        set
        {
            restingRate = value;
            if (restingRate < 0)
            {
                restingRate = 0;
            }
        }

    }


    public float GetIncreaseRateToMoney
    {
        get { return increaseRateToMoney; }
        set
        {
            increaseRateToMoney = value;

        }
    }
    public float GetUnitOfMoney
    {
        get { return unitOfMoney; }
        set
        {
            unitOfMoney = value;
            if (unitOfMoney > maxUnitOfMoney)
            {
                unitOfMoney = maxUnitOfMoney;
            }
        }
    }



    public float GetStamina
    {
        get { return stamina; }
        set
        {
            stamina = value;
            if (stamina < 0)
            {
                stamina = 0f;
            }
            if (stamina >maxStamina)
            {
                stamina = maxStamina;
            }
        }
    }

    public float GetIncreaseToStamina
    {
        get { return increaseToStamina; }
        set { increaseToStamina = value; }
    }

    public float GetMaxStamina
    {
        get { return maxStamina; }
        set { maxStamina = value; }
    }

    public float GetMoney
    {
        get { return totalMoney; }
        set
        {
            totalMoney = value;
            if (totalMoney < 0)
            {
                totalMoney = 1;
            }
        }
    }
}
