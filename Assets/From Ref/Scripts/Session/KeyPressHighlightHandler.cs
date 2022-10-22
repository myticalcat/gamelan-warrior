using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Session;

public class KeyPressHighlightHandler : MonoBehaviour {
    [SerializeField] private SessionManager _sessionManager;
    [SerializeField] private GameObject[] _keyPressHighlights;
    
    private void Update() {
        for (int i = 0; i < 6; i++) {
            if (Input.GetKey(SessionUtils.KeyIndexToKeyCode(i))) {
                if (!_keyPressHighlights[i].activeSelf) _keyPressHighlights[i].SetActive(true); 
            } else {
                if (_keyPressHighlights[i].activeSelf) _keyPressHighlights[i].SetActive(false);
            }
        }
    }
}
