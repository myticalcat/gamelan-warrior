using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour {
    public float TransitionTime = 0.5f;
    public int NumberOfLoops = 10;
    
    public Image BackgroundImage;
    public TMP_Text Text;
    
    private IEnumerator TransitionOff() {
        float delta = 1.0f / NumberOfLoops;
        float waitTime = TransitionTime / NumberOfLoops;
        
        for (float i = 1; i >= 0; i -= delta) {
            Color temp;
            
            temp = BackgroundImage.color;
            temp.a = i;
            BackgroundImage.color = temp;
            
            temp = Text.color;
            temp.a = i;
            Text.color = temp;
            
            yield return new WaitForSeconds(waitTime);
        }
        
        gameObject.SetActive(false);
    }
    
    public void Finish() {
        StartCoroutine(TransitionOff());
    }
}
