using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cutscene.Statements {
    public class CutsceneStatement {}
    
    public class Dialog : CutsceneStatement {
        private string _dialog;
        
        public Dialog(string dialog) {
            _dialog = dialog;
        }
        
        public string GetDialog() {
            return _dialog;
        }
    }
    
    public class SetBG : CutsceneStatement {
        private int _index;
        
        public SetBG(int index) {
            _index = index;
        }
        
        public int GetIndex() {
            return _index;
        }
    }
    
    public class Delay : CutsceneStatement {
        private float _duration;
        
        public Delay(float duration) {
            _duration = duration;
        }
        
        public float GetDuration() {
            return _duration;
        }
    }
}