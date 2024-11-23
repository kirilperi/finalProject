using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider expSlider;
    public float maxExp = 100f;
    public float exp;
    float lerpSpeed = 0.05f;
    void Start()
    {
        exp = 0;
    }


    void Update()
    {
        if (expSlider.value != exp)
        {
            expSlider.value = Mathf.Lerp(expSlider.value, exp, lerpSpeed);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetExp(10);
        }


        
    }
    void GetExp(float expDrop)
    {
        exp += expDrop;
    }
}
