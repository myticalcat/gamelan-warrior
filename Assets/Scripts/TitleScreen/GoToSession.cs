using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Session;

public class GoToSession : MonoBehaviour {
    [SerializeField] private Beatmap _beatmap;
    [SerializeField] private GameObject _dataTransferPrefab;
    
    public void StartSession() {
        GameObject dataTransferObj = Instantiate(_dataTransferPrefab);
        dataTransferObj.name = "SessionDataTransfer";
        SessionDataTransferScript dataTransferComp = dataTransferObj.GetComponent<SessionDataTransferScript>();
        dataTransferComp.SelectedBeatmap = _beatmap;
        DontDestroyOnLoad(dataTransferComp);
        
        SceneManager.LoadScene("Session");
    }
}
