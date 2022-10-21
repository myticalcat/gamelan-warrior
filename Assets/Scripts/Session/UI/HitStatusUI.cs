using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game.Session;

public class HitStatusUI : MonoBehaviour {
    [Header("Configurations")]
    public float TransitionTime = 0.5f;
    public int NumberOfLoops = 10;
    
    [Header("References")]
    [SerializeField] private SessionManager _sessionManager;
    [SerializeField] private TMP_Text _text;
    private IEnumerator _lastCoroutine;
    
    private void Awake() {
        _sessionManager.OnHit += Hit;
    }
    
    private void Hit(NoteHitType hitType) {
        if (_lastCoroutine != null) StopCoroutine(_lastCoroutine);
        
        _text.text = hitType.ToString();
        
        _lastCoroutine = TransitionOff();
        StartCoroutine(_lastCoroutine);
    }
    
    private IEnumerator TransitionOff() {
        float delta = 1.0f / NumberOfLoops;
        float waitTime = TransitionTime / NumberOfLoops;
        
        Color temp;
        for (float i = 1; i >= 0; i -= delta) {
            temp = _text.color;
            temp.a = i;
            _text.color = temp;
            
            yield return new WaitForSeconds(waitTime);
        }
        
        temp = _text.color;
        temp.a = 0;
        _text.color = temp;
    }
}
