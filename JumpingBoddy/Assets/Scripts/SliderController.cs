using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Slider levelProgresBarr;
    [SerializeField] private Image staminaSliderImage;
    [SerializeField] private Image hotPanelImage;
    [SerializeField] Color[] myColors;
    [SerializeField] private JumpingScriptable playerData = null;
    private LevelController levelController;
    [SerializeField] GameObject cameraMove;
    [SerializeField] GameObject finishLine;
    private float startValue;
    private float staminaValue;
    private float maxDistance;
    public float sliderValue;
    private float timer;

    void Awake()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        staminaValue = playerData.GetStamina;

        maxDistance = finishLine.transform.position.z - cameraMove.transform.position.z;

    }




    void Update()
    {
        StaminaHotSlider();
        LevelProgressSlider();
        
    }

    private void StaminaHotSlider()
    {
        
        //Stamina slider
        staminaValue = Mathf.Lerp(staminaValue, staminaValue - 15f, 1f);
        //startValue = playerData.GetResting;
        startValue = playerData.GetMaxStamina;
        sliderValue = 1 - (playerData.GetStamina / startValue);

        staminaSlider.value = Mathf.Lerp(0, 1, sliderValue);
        if (staminaSlider.value < 0.5f)
        {
            staminaSliderImage.color = Color.Lerp(myColors[0], myColors[1], (staminaSlider.value * 2));

        }
        else
        {
            staminaSliderImage.color = Color.Lerp(myColors[1], myColors[2], (staminaSlider.value - 0.5f) * 2.4f);
            //Hot Panel
            hotPanelImage.color = Color.Lerp(myColors[3], myColors[4], (staminaSlider.value - 0.5f) * 2f);
        }
        if (!levelController.isGameActive)
        {
            if (timer <1)
            {
                timer += Time.deltaTime*3;

            }
            
            
            hotPanelImage.color = Color.Lerp(myColors[4], myColors[3], timer);
        }else
        {
            
            if (timer >0)
            {
                timer = 0;
            }
            
        }
    }

    private void LevelProgressSlider()
    {
        //LevelProgressBar
        float distance = finishLine.transform.position.z - cameraMove.transform.position.z;
        levelProgresBarr.value = 1 - (distance / maxDistance);

    }


}
