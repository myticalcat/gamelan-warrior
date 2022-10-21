using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Game.Cutscene.UI;

namespace Game.Cutscene {
    public class CutsceneManager : MonoBehaviour {
        [SerializeField] private DialogBox _dialogBox;
        [SerializeField] private SpriteRenderer _background;
        private Cutscene _cutscene;
        private Statements.CutsceneStatement[] _statements;
        private int _currentStatementIndex = 0;
        private bool _ready = true;
        
        private void Awake() {
            GameObject dataTransferObj = GameObject.Find("CutsceneDataTransfer");
            CutsceneDataTransferScript dataTransferComp = dataTransferObj.GetComponent<CutsceneDataTransferScript>();
            _cutscene = dataTransferComp.SelectedCutscene;
            Destroy(dataTransferObj);
            
            _statements = CutsceneUtils.ParseStatements(_cutscene.Statements);
            
            RegisterToDialog();
        }
        
        private void Update() {
            UpdateDelay();
            
            if (!_ready) {
                bool isReady = CheckDelay() && CheckDialog();
                _ready = isReady;
            } else {
                if (_currentStatementIndex >= _statements.Length) {
                    // TODO: handle back to menu + unlock gameplay
                    SceneManager.LoadScene("TitleScreen");
                    return;
                }
                
                Statements.CutsceneStatement curStatement = _statements[_currentStatementIndex];
                
                if (curStatement is Statements.Dialog) {
                    Statements.Dialog curDialog = (Statements.Dialog) curStatement;
                    StartDialog(curDialog.GetDialog());
                } else if (curStatement is Statements.SetBG) {
                    Statements.SetBG curSetBG = (Statements.SetBG) curStatement;
                    Sprite curBG = _cutscene.Backgrounds[curSetBG.GetIndex()];
                    _background.sprite = curBG;
                } else if (curStatement is Statements.Delay) {
                    Statements.Delay curDelay = (Statements.Delay) curStatement;
                    StartDelay(curDelay.GetDuration());
                }
                
                _currentStatementIndex++;
            }
        }
        
        /* Statements */
        // SetBG
        private void SetBackground(Sprite background) {
            
        }
        
        // Dialog
        private bool _isNotInDialog = true;
        
        private void RegisterToDialog() {
            _dialogBox.OnInput += EndDialog;
        }
        
        private void EndDialog() {
            if (!(_currentStatementIndex < _statements.Length
                    && _statements[_currentStatementIndex] is Statements.Dialog)) { // _currentStatement is the unrunned statement
                _dialogBox.Close();
            }
            
            _isNotInDialog = true;
        }
        
        private void StartDialog(string text) {
            _dialogBox.SetText(text);
            _isNotInDialog = false;
            _ready = false;
        }
        
        private bool CheckDialog() {
            return _isNotInDialog;
        }
        
        // Delay
        private float _delayLeft = 0f;
        
        private void StartDelay(float delayTime) {
            _delayLeft = delayTime;
            _ready = false;
        }
        
        private void UpdateDelay() {
            if (_delayLeft > 0f) {
                _delayLeft -= Time.deltaTime;
            }
        }
        
        private bool CheckDelay() {
            return _delayLeft.CompareTo(0f) <= 0;
        }
    }
}


