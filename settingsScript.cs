using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsScript : MonoBehaviour
{
    public static float musicVal = 1f;
    public static float SFX = 1f;
    public Slider musicSlider;
    public Slider SpecialFX;
    
    void Start()
    {
        SpecialFX.value = SFX;
        musicSlider.value = musicVal;
    }
    
    void Update()
    {
        musicVal = musicSlider.value;
        SFX = SpecialFX.value;
    }
}
