using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game.Session;

namespace Game.Session.UI {
    public class ScoreUI : MonoBehaviour {
        public TMP_Text text;
        public SessionManager _sessionManager;
        
        private void Update() {
            text.text = GetScore().ToString();
        }
        
        private int GetScore() {
            if (!_sessionManager.GetIsStarted()) {
                return 0;
            } else {
                return _sessionManager.GetCurrentScore();
            }
        }
    }
}


