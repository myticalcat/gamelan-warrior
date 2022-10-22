using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Cutscene;

public class GoToCutscene : MonoBehaviour {
    [SerializeField] private Cutscene _cutscene;
    [SerializeField] private GameObject _dataTransferPrefab;
    
    public void StartCutscene() {
        GameObject dataTransferObj = Instantiate(_dataTransferPrefab);
        dataTransferObj.name = "CutsceneDataTransfer";
        CutsceneDataTransferScript dataTransferComp = dataTransferObj.GetComponent<CutsceneDataTransferScript>();
        dataTransferComp.SelectedCutscene = _cutscene;
        DontDestroyOnLoad(dataTransferComp);
        
        SceneManager.LoadScene("Cutscene");
    }
}
