using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Session;

public class SessionTester : MonoBehaviour {
    [SerializeField] private SessionManager _sessionManager;
    [SerializeField] private Beatmap _beatmap;
    
    private void Start() {
        _sessionManager.LoadSession(_beatmap);
        _sessionManager.StartSession();
    }
}
