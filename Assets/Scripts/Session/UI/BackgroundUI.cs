using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundUI : MonoBehaviour {
    public Image BackgroundImage;
    
    public void SetBackground(Sprite sprite) {
        BackgroundImage.sprite = sprite;
    }
}
