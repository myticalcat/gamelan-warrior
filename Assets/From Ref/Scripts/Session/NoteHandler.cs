using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Session;

namespace Game.Session {
    public class NoteHandler : MonoBehaviour, MouseInteractable {
        private SessionManager _sessionManager;
        private Note _noteObject;
        [SerializeField] private Transform _topPoint;
        [SerializeField] private Transform _bottomPoint;
        
        private bool _isInitialized = false;
        
        public void Initialize(SessionManager sessionHandler, Note noteObject, float defaultPositionX) {
            _sessionManager = sessionHandler;
            _noteObject = noteObject;
            
            transform.position = new Vector2(defaultPositionX, 100.0f);
            
            _isInitialized = true;
        }
        
        public void Deinitialize() {
            _isInitialized = false;
            
            _noteObject = null;
            _sessionManager = null;
        }
        
        private void Update() {
            if (!_isInitialized || !_sessionManager.GetIsStarted()) return;
            
            ChangePosition();
  
            CheckForMiss();
        }
        
        private void ChangePosition() {
            float positionX = transform.position.x;
            
            float timeFromStart = _noteObject.StartTime - _sessionManager.GetSessionTime();
            float positionY = _sessionManager.Line.position.y + timeFromStart / (_sessionManager.GetSecondsPerUnit() * 1000);
            
            transform.position = new Vector2(positionX, positionY);
        }
        
        /*
        private void CheckForHit() {
            if (Input.GetKeyDown(SessionUtils.KeyIndexToKeyCode(_noteObject.Key))) {
                if (_bottomPoint.position.y <= _sessionManager.Line.position.y
                        && _sessionManager.Line.position.y <= _topPoint.position.y) {       
                    float height = _topPoint.position.y - _bottomPoint.position.y;
                    float midPoint = _bottomPoint.position.y + height / 2;
                    
                    // Inner half of circle -> Perfect
                    if (4 * Mathf.Abs(midPoint - _sessionManager.Line.position.y) < height) {
                        _sessionManager.NoteHit(gameObject, NoteHitType.VeryGood);
                    } else {
                        _sessionManager.NoteHit(gameObject, NoteHitType.Good);
                    }
                }
            }
        }*/
        
        private void CheckForMiss() {
            if (_topPoint.position.y < _sessionManager.Line.position.y) {
                _sessionManager.NoteHit(gameObject, NoteHitType.Miss);
            }
        }

        public void interact()
        {
            if (_bottomPoint.position.y <= _sessionManager.Line.position.y
                        && _sessionManager.Line.position.y <= _topPoint.position.y)
            {
                float height = _topPoint.position.y - _bottomPoint.position.y;
                float midPoint = _bottomPoint.position.y + height / 2;

                // Inner half of circle -> Perfect
                if (4 * Mathf.Abs(midPoint - _sessionManager.Line.position.y) < height)
                {
                    _sessionManager.NoteHit(gameObject, NoteHitType.VeryGood);
                }
                else
                {
                    _sessionManager.NoteHit(gameObject, NoteHitType.Good);
                }
            }
        }
    }
}
