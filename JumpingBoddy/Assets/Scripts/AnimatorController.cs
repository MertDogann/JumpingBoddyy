using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] JumpingScriptable playerData = null;
    [SerializeField] GameObject moveCamera;
    private LevelController levelController;
    public Animator playerAnim;
    private GameManager gameManager;

    //Particle
    public ParticleSystem sweatParticle;
    
    public float particleRate = 0f;


    public State currentState;
    public int stateIndex;
    


    public float dieAnimFloat;
    public bool jumpDie;

    public float winAnimFloat;

    private bool HapticsAllowed = true;

    private void Awake()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        PlayerPrefs.GetFloat("highScorePos");
    }

    void Start()
    {
        stateIndex =(int)currentState;
        AnimationSpeedCheck();
        sweatParticle.Play();
        playerData.GetSweat = playerData.GetMaxStamina * playerData.terlemeYuzde / 100;
        MMVibrationManager.SetHapticsActive(HapticsAllowed);
        //Debug.Log("terleme baþlangýcý" + playerData.GetSweat);
        playerAnim.speed= playerData.GetAnimationSpeed;
        //Debug.Log("animator speed" + playerAnim.speed);
        playerAnim.SetInteger("starting", 2);
    }

    
    void Update()
    {
        LevelFailed();
        SweatParticleSystem();
        LevelWinAnim();
 
    }

    private void LevelWinAnim()
    {
        if (levelController.levelFinish)
        {
            
            playerAnim.SetFloat("Win", winAnimFloat);
        }
        
    }

    private void SweatParticleSystem()
    {
        var ems = sweatParticle.emission;
        if ((playerData.GetStamina <= playerData.GetSweat) && levelController.isGameActive && !levelController.upgradeOnOff)
        {
            ems.rateOverTime = particleRate;
            //Debug.Log("Particle artiyor");
            //Debug.Log("zort");
        }
        else
        {
            particleRate = 0;
            ems.rateOverTime = 0;
        }
    }

    private void LevelFailed()
    {
        if (!levelController.isGameActive )
        {
            playerAnim.SetFloat("Die", dieAnimFloat);
            //levelController.isGameActive = false;
            

           
            //Debug.Log("highScore" + PlayerPrefs.GetFloat("highScorePos"));
            
        }
    }

    public void Controller()
    {
        playerAnim.SetTrigger("start");

        playerAnim.SetFloat("Blend" , stateIndex);
    }

    public void StateControl()
    {

        stateIndex++;
        if (stateIndex > 2)
        {
            stateIndex = 0;
        }
        currentState = (State)stateIndex;

        if (currentState == State.Atlama)
        {
            if ((playerData.GetStamina - playerData.atlarkenAzalanStamina) >= 0)
            {
                if (playerAnim.GetInteger("starting") == 2)
                {
                    playerAnim.SetInteger("starting", 0);
                }
                playerAnim.SetFloat("Idle", 0f);
                Money.Current.AddingMoney();

                moveCamera.transform.DOMoveZ((transform.position.z + PlayerPrefs.GetFloat("jumpDistance")), playerData.GetMoveTime);

                //RandomIdleAnimtion();
                StartCoroutine(Delay(playerData.atlarkenAzalanStamina));
                transform.DOMoveZ((transform.position.z + PlayerPrefs.GetFloat("jumpDistance")),playerData.GetMoveTime);

                playerData.GetAnimationTimerControl = playerData.GetJumpAnimationTimer;
            }else
            {
                jumpDie = true;
                //levelController.isGameActive = false;
                Debug.Log("atlama ölmesi");
                playerData.GetStamina = 0f;
                transform.DOMoveZ((transform.position.z + PlayerPrefs.GetFloat("jumpDistance")), playerData.GetMoveTime);
                
            }
        }else if (currentState == State.Kalkma)
        {
            
            jumpDie= false;
            //StartCoroutine(IdleDelay());
            RandomIdleAnimtion();
            //playerAnim.SetFloat("Idle", 0f);

            if (levelController.vibration)
            {
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            }
            playerData.GetAnimationTimerControl = playerData.GetBendingAnimationTimer;
            
            
            StartCoroutine(Delay(playerData.egilmeKalkmaAzalmaStamina));


        }else if (currentState == State.Egilme)
        {
            playerData.GetAnimationTimerControl = 0f;
            
        }

    }


    IEnumerator IdleDelay()
    {
        yield return new WaitForSeconds((0));   //(playerData.GetBendingAnimationTimer-0.05f)
        RandomIdleAnimtion();
    }



    public IEnumerator Delay(float addAmount)
    {
        float newAmount = playerData.GetStamina - addAmount;
        while(playerData.GetStamina> newAmount)
        {
            if (addAmount >10)
            {
                playerData.GetStamina -= Time.deltaTime * playerData.GetStaminaIncreaseTime;
            }else
            {
                playerData.GetStamina -= Time.deltaTime *( playerData.GetStaminaIncreaseTime +25);
            }

            
            yield return null;
        }
    }

    public void AnimationSpeedCheck()
    {
        playerAnim.speed = playerData.GetAnimationSpeed;
    }


    public void RandomIdleAnimtion()
    {
        float random = Random.Range(1, 3);
        playerAnim.SetFloat("Idle", random);
    }

    

}
