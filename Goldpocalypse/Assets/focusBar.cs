using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class focusBar : MonoBehaviour
{
    public Slider focusSlider;

    public void setMaxFocus(float focus)
    {
        focusSlider.maxValue = focus;
        focusSlider.value = focus;
    }

    public void setFocus(float focus)
    {
        focusSlider.value = focus;
    }

}
