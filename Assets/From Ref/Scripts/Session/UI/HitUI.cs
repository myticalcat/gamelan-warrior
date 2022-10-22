using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Session;

public class HitUI : MonoBehaviour {
    [Header("Configurations")]
    public float TransitionTime = 0.5f;
    public int NumberOfLoops = 10;
    
    [Header("References")]
    [SerializeField] private SessionManager _sessionManager;
    [SerializeField] private Sprite _missSprite;
    [SerializeField] private Sprite _goodSprite;
    [SerializeField] private Sprite _veryGoodSprite;
    private SpriteRenderer _hitRenderer;
    
    private IEnumerator _lastCoroutine;
    
    private void Awake() {
        _hitRenderer = transform.GetComponent<SpriteRenderer>();
        _sessionManager.OnHit += Hit;
    }
    
    private void Hit(NoteHitType hitType) {
        if (_lastCoroutine != null) StopCoroutine(_lastCoroutine);
        
        if (hitType == NoteHitType.Miss) {
            _hitRenderer.sprite = _missSprite;
        } else if (hitType == NoteHitType.Good) {
            _hitRenderer.sprite = _goodSprite;
        } else if (hitType == NoteHitType.VeryGood) {
            _hitRenderer.sprite = _veryGoodSprite;
        }
        
        _lastCoroutine = TransitionOff();
        StartCoroutine(_lastCoroutine);
    }
    
    private IEnumerator TransitionOff() {
        float delta = 1.0f / NumberOfLoops;
        float waitTime = TransitionTime / NumberOfLoops;
        
        Color temp;
        for (float i = 1; i >= 0; i -= delta) {
            temp = _hitRenderer.color;
            temp.a = i;
            _hitRenderer.color = temp;
            
            yield return new WaitForSeconds(waitTime);
        }
        
        temp = _hitRenderer.color;
        temp.a = 0;
        _hitRenderer.color = temp;
    }
}
