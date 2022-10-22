using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Session {
    public class MissBarUI : MonoBehaviour {
        [SerializeField] private RectTransform _barRectTransform;
        [Space(16)]
        [SerializeField] SessionManager _sessionManager;
        
        private void Update() {
            float barScaleX = _sessionManager.GetMissRatio();
            
            Vector3 curScale = _barRectTransform.localScale;
            Vector3 newScale = new Vector3(barScaleX, curScale.y, curScale.z);
            
            _barRectTransform.localScale = newScale;
        }
    }
}


