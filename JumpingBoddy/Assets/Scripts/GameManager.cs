using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using MoreMountains.NiceVibrations;

public class GameManager : MonoBehaviour
{
    [SerializeField] JumpingScriptable playerData = null;
    public static GameManager Instance;
    private LevelController levelControler;
    public AnimatorController playerAnim1;
    public AnimatorController playerAnim2;
    public GameObject player1, player2;
    public GameObject settingButton;
    private AnimatorController current;
    private bool gecisKontrol;
    public float timer;
    private float staminaReductionRate;
    private bool moveControl;
    private int animC = 0;
    [SerializeField] private Material shaderMat;
    float animationIdle;
    private float hot;
    private float levelWinHot = 0;
    public float touchPos = 2300;
    float hotValuee;
    [SerializeField] GameObject upgradeding ,upgradeButton;
    private float upgradePanelLimit;
    public SliderController slidercont;

    private Vector3 touchStart;
    [SerializeField] private float restReduction;
    public List<ParticleSystem> upgradeParticle;
    public int indexUpgrade =0;
    
    public float GetHot
    {
        get { return hot; }
        set { hot = value; }
    }

    public bool GetMoveControl
    {
        get { return moveControl; }
        set { moveControl = value; }
    }
    public float GetStaminaReductionRate
    {
        get { return staminaReductionRate; }
        set { staminaReductionRate = value; }
    }



    private void Awake()
    {
        levelControler = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>(); //Açýlacak birazdan.
        playerData.GetRestingRate = PlayerPrefs.GetFloat("getRestingRate");
        
    }
    void Start()
    {
        current = playerAnim1;
        Instance = this;
        //playerData.GetResting = playerData.GetMaxStamina;
        //playerData.GetStamina = playerData.GetResting;
        playerData.GetStamina = playerData.GetMaxStamina;
        hot = playerData.GetMaxStamina * playerData.kizarmaYuzde / 100;
        playerData.GetStartRestingRate = playerData.GetRestingRate;
        timer = 2f;
        touchPos = settingButton.transform.position.y-300f;
        
        upgradePanelLimit = (playerData.GetMaxStamina / 100) * 19f;
        


    }


    void Update()
    {
        TurningRed();
        timer += Time.deltaTime;
        Touched();
        LevelFailedAnimation();
        LevelWinAnimation();
        TouchIdle();
        GameInUpgrade();
    }



    private void GameInUpgrade()
    {
        
        if (playerData.GetStamina<=upgradePanelLimit && indexUpgrade<1 &&levelControler.isGameActive)
        {
            indexUpgrade = 1;
            Debug.Log("zart zort" + indexUpgrade);
            levelControler.upgradeOnOff = true;
            upgradeding.SetActive(true);
            upgradeding.transform.GetChild(0).gameObject.SetActive(false);
            upgradeButton.SetActive(true);
        }
        
    }

    private void LevelWinAnimation()
    {
        if (levelControler.levelFinish)
        {
            playerAnim1.winAnimFloat = 1;
            playerAnim2.winAnimFloat = 2;
        }
    }

    private void LevelFailedAnimation()
    {
        if (!levelControler.isGameActive && (playerAnim1.jumpDie || playerAnim2.jumpDie) )
        {

            if ((player1.transform.position.z - player2.transform.position.z) >0)
            {
                if (animC <1)
                {
                    //player 1 önde
                    playerAnim1.dieAnimFloat = 4;
                    playerAnim2.dieAnimFloat = 3;
                    animC = 1;
                }
            }
            else
            {
                if (animC <1)
                {

                    //player 2 önde
                    playerAnim1.dieAnimFloat = 3;
                    playerAnim2.dieAnimFloat = 4;
                    animC=1;
                }
             
            }
        }
        else if(playerData.GetStamina <= 0 )
        {
            if ((player1.transform.position.z - player2.transform.position.z) > 0)
            {
                //player 1 önde
                playerAnim1.dieAnimFloat = 1;
                playerAnim2.dieAnimFloat = 2;

            }
            else
            {
                //player 2 önde
                playerAnim1.dieAnimFloat = 2;
                playerAnim2.dieAnimFloat = 1;
            }
            
        }
    }

    private void Touched()
    {
        animationIdle = (playerData.GetBendingAnimationTimer * 2) + playerData.GetJumpAnimationTimer;
        if (Input.touchCount>0)
        {
            touchStart = Input.GetTouch(0).position;

        }
        if (touchStart.y < touchPos && Input.touchCount > 0 && levelControler.isGameActive && timer > animationIdle && !levelControler.upgradeOnOff) //playerData.GetAnimationTimerControl
        {
            playerData.GetRestingRate = playerData.GetStartRestingRate  ;
            

            timer = 0f;
            StartCoroutine(WaitAnim());
            //GecisKontrol();
            //current.StateControl();
            //current.Controller();

        }
    }

    private void TouchIdle()
    {
        
        if ( levelControler.isGameActive && timer > animationIdle)
        {
            
            if (playerAnim1.particleRate >= 0)
            {
                playerAnim1.particleRate -= 6f * Time.deltaTime;
                playerAnim2.particleRate -= 6f * Time.deltaTime;
            }
            playerData.GetStamina += playerData.GetRestingRate * Time.deltaTime;
            if ((playerData.GetStartRestingRate / playerData.GetRestingRate ) >2f)
            {
                playerData.GetRestingRate -= Time.deltaTime /( restReduction *5);
                //Debug.Log("yarýya düþtü");
                
            }else if(playerData.GetRestingRate<= 0.5f)
            {
                playerData.GetRestingRate = 0.5f;
            }
            else
            {
                playerData.GetRestingRate -= Time.deltaTime / restReduction;
            }
            

        }else
        {
            //Debug.Log("particle artýyor");
            if (playerData.GetStamina >= hot)
            {
                playerAnim1.particleRate += 8f * Time.deltaTime;
                playerAnim2.particleRate += 8f * Time.deltaTime;
                
            }
            else
            {
                playerAnim1.particleRate += 3.5f * Time.deltaTime;
                playerAnim2.particleRate += 3.5f * Time.deltaTime;
                

            }
        }
    }

    private void TurningRed()
    {
        hotValuee = (1 - (playerData.GetStamina / hot)) / 1.67f;
        
        if (playerData.GetStamina >= hot)
        {

            shaderMat.SetFloat("_TiredRange", 0);
        }
        else if (playerData.GetStamina <=hot )
        {
            shaderMat.SetFloat("_TiredRange", hotValuee);

        }
        if (!levelControler.isGameActive && !levelControler.settingPanel.activeInHierarchy)
        {
            levelWinHot -= Time.deltaTime / 15f;
            
            float finishHot = hotValuee + levelWinHot;
            if (finishHot <0)
            {
                finishHot = 0;
                
            }
            shaderMat.SetFloat("_TiredRange", finishHot);
        }
    }

    IEnumerator WaitAnim()
    {
        if (!current.jumpDie )
        {
            gecisKontrol = !gecisKontrol;
            current = gecisKontrol ? playerAnim2 : playerAnim1;
            current.StateControl();
            current.Controller();
            yield return new WaitForSeconds(0);
        }
        if (!current.jumpDie)
        {
            gecisKontrol = !gecisKontrol;
            current = gecisKontrol ? playerAnim2 : playerAnim1;
            current.StateControl();
            current.Controller();
            yield return new WaitForSeconds(playerData.GetJumpAnimationTimer);
        }


        if (!current.jumpDie)
        {
            gecisKontrol = !gecisKontrol;
            current = gecisKontrol ? playerAnim2 : playerAnim1;
            current.StateControl();
            current.Controller();
            yield return new WaitForSeconds(0);
        }
        
    }

    public void GecisKontrol()
    {
        gecisKontrol = !gecisKontrol;
        current = gecisKontrol ? playerAnim2 : playerAnim1;
        Debug.Log("gecis kontrol");
    }
}

public enum State
{
    Egilme, Kalkma, Atlama
}


