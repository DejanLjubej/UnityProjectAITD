using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SliderController : MonoBehaviour {
    
    
    public void OnValueChanged(float value)
    {
        Debug.Log("New value" + value);
    }
}
