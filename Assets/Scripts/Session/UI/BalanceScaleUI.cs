using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Session;

namespace Game.Session.UI {
    public class BalanceScaleUI : MonoBehaviour {
        [SerializeField] private int _maxVisibleDifference = 5;
        [SerializeField] private RectTransform _barRectTransform;
        [Space(16)]
        [SerializeField] SessionManager _sessionManager;
        
        private void Awake() {
            _sessionManager.OnBalanceScaleChange += UpdateBalanceScale;
        }
        
        private void UpdateBalanceScale(int currentBalanceScale) {
            currentBalanceScale = Mathf.Clamp(currentBalanceScale, -_maxVisibleDifference, _maxVisibleDifference);
            float barScaleX = 1.0f * (currentBalanceScale + _maxVisibleDifference) / (2 * _maxVisibleDifference);
            
            Vector3 curScale = _barRectTransform.localScale;
            Vector3 newScale = new Vector3(barScaleX, curScale.y, curScale.z);
            
            _barRectTransform.localScale = newScale;
        }
    }
}
