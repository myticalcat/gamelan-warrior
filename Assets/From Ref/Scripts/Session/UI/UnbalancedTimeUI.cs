using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Session;

namespace Game.Session.UI {
    public class UnbalancedTimeUI : MonoBehaviour {
        [SerializeField] private RectTransform _barRectTransform;
        [Space(16)]
        [SerializeField] SessionManager _sessionManager;
        
        private void Update() {
            bool isBalanced = _sessionManager.GetIsBalanced();
            float timeLeft = _sessionManager.GetUnbalancedTimeLeft();
            float maxTimeLeft = _sessionManager.UnbalancedFailTime;
            
            float barScaleX;
            if (isBalanced) barScaleX = 1.0f;
            else {
                barScaleX = 1.0f - (1.0f * timeLeft / maxTimeLeft);
            }
            
            Vector3 curScale = _barRectTransform.localScale;
            Vector3 newScale = new Vector3(barScaleX, curScale.y, curScale.z);
            
            _barRectTransform.localScale = newScale;
        }
    }
}

