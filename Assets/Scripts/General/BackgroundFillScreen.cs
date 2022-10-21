using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFillScreen : MonoBehaviour {
    // [SerializeField] private int _defaultWidth = 5000;
    // [SerializeField] private int _defaultHeight = 5000;
    private Camera _camera;
    
    private void Start() {
        _camera = Camera.main;
    }
    
    private void Update() {
        SetScale();
        SetPosition();
    }
    
    // Assuming the background is a square
    private void SetScale() {
        // float scaling = 1.0f * _defaultHeight / Screen.height;
        float width = Screen.width;
        float height = Screen.height;
        
        float pixelPerUnit = height / (_camera.orthographicSize * 2);
        
        float sizeInUnit = Mathf.Max(width, height) / pixelPerUnit;
        
        Vector2 newScale = new Vector2(sizeInUnit, sizeInUnit);
        transform.localScale = newScale;
    }
    
    private void SetPosition() {
        Vector2 camPosition = _camera.transform.position;
        Vector2 newPosition = new Vector2(camPosition.x, camPosition.y);
        
        transform.position = newPosition;
    }
}
