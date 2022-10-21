using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game.Session {
    public class SessionManager : MonoBehaviour {
        // Configurations and References
        [Header("Configurations")]
        public float StartDelayTime = 2.0f;
        public float UnbalancedFailTime = 10.0f;
        
        [Header("References")]
        public Transform NotesParent;
        public Transform Line;
        public AudioSource AudioSourceComponent;
        public GameObject NoteLightPrefab; // TODO: make it one prefab
        public GameObject NoteDarkPrefab;
        public LoadingUI LoadingScript;
        // public BackgroundUI BackgroundScript;
        public SpriteRenderer _backgroundRenderer;
        public SFXManager _sfxManager;
        
        // Attributes
        private bool _isLoaded = false;
        private bool _isStarted = false;
        private float _startTime;
        private Beatmap _beatmap;
        private Note[] _notes;
        private float _duration;
        
        // Play Attributes
        private int _currentScore = 0;
        private int _balanceScale = 0;
        private float _unbalancedStartTime = 0;
        private int _veryGoodCount = 0;
        private int _goodCount = 0;
        private int _missCount = 0;
        
        // Events
        public delegate void BalanceScaleChangeHandler(int currentBalanceScale);
        public event BalanceScaleChangeHandler OnBalanceScaleChange;
        
        public delegate void HitHandler(NoteHitType hitType);
        public event HitHandler OnHit;
        
        // Frame-Based Methods
        private void Awake() {
            GameObject dataTransferObj = GameObject.Find("SessionDataTransfer");
            SessionDataTransferScript dataTransferComp = dataTransferObj.GetComponent<SessionDataTransferScript>();
            Beatmap curBeatmap = dataTransferComp.SelectedBeatmap;
            Destroy(dataTransferObj);
            
            LoadSession(curBeatmap);
            StartSession();
        }
        
        private void Update() {
            if (GetSessionTime() >= 0.0f && !AudioSourceComponent.isPlaying) AudioSourceComponent.Play();
            if (GetSessionTime() > _duration) {
                EndSession(false);
            }
            CheckBalanceScaleFail();
            CheckMissFail();
        }
        
        // Session Status Methods
        public void LoadSession(Beatmap beatmap) {
            if (_isLoaded) throw new Exception("Session is already loaded");
            if (beatmap == null) throw new Exception("Beatmap is empty");
            
            _beatmap = beatmap;
            AudioSourceComponent.clip = _beatmap.Audio;
            _notes = SessionUtils.BeatmapDecoder(_beatmap);
            _backgroundRenderer.sprite = _beatmap.Background;
            // BackgroundScript.SetBackground(_beatmap.Background);
            
            _duration = Mathf.Min(_notes[_notes.Length - 1].StartTime + 2000f, _beatmap.GetDuration());
            
            // TODO: implement object pooling
            for (int i = 0; i < _notes.Length; i++) {
                GameObject currentNote;
                if (_notes[i].Affinity == NoteAffinity.Light)
                    currentNote = Instantiate(NoteLightPrefab, NotesParent);
                else
                    currentNote = Instantiate(NoteDarkPrefab, NotesParent);
                
                NoteHandler noteHandler = currentNote.GetComponent<NoteHandler>();
                noteHandler.Initialize(this, _notes[i], Line.position.x + SessionUtils.KeyIndexToHorizontalPosition(_notes[i].Key));
            }
            
            LoadingScript.Finish();
            _isLoaded = true;
        }
        
        public void StartSession() {
            if (!_isLoaded) throw new Exception("Session is not loaded");
            
            _startTime = Time.time;
            // AudioSourceComponent.PlayDelayed(StartDelayTime);
            _isStarted = true;
        }
        
        public void EndSession(bool isUnbalanced) {
            // isFail, Score, Combo
            // TODO: calculate alphabet score + save result
            string alphabetScore;
            bool isSuccess;
            (isSuccess, alphabetScore) = SessionUtils.CalculateAlphabetScore(_notes.Length, isUnbalanced, _veryGoodCount, _goodCount, _missCount);
            Result result = new Result(_beatmap, isSuccess, _currentScore, alphabetScore, _veryGoodCount, _goodCount, _missCount);
            SessionResultManager.SetResult(result);
            SceneManager.LoadScene("SessionResult");
        }
        
        // In Session Methods
        public void NoteHit(GameObject note, NoteHitType hitType) {
            bool isDark = note.tag == "Dark";
            Destroy(note);
            
            if (hitType == NoteHitType.Miss) {
                _sfxManager.PlayMiss();
                SetBalanceScale(isDark);
                _missCount++;
            } else if (hitType == NoteHitType.Good) {
                _sfxManager.PlayHit();
                _currentScore += 100;
                _goodCount++;
            } else if (hitType == NoteHitType.VeryGood) {
                _sfxManager.PlayHit();
                _currentScore += 300;
                _veryGoodCount++;
            }
            
            if (OnHit != null) OnHit(hitType);
        }
        
        // Balance System
        private void SetBalanceScale(bool isDark) {
            bool isBalancedBefore = _balanceScale == 0;
            _balanceScale += isDark ? 1 : -1;
            
            if (isBalancedBefore) _unbalancedStartTime = Time.time;
            
            if (OnBalanceScaleChange != null)
                OnBalanceScaleChange(_balanceScale);
        }
        
        private void CheckBalanceScaleFail() {
            if (Time.time - _unbalancedStartTime > UnbalancedFailTime && _balanceScale != 0) {
                EndSession(true);
            }
        }
        
        private void CheckMissFail() {
            if (5 * _missCount > _notes.Length) {
                EndSession(true);
            }
        }
        
        // Getters
        public bool GetIsStarted() {
            return _isStarted;
        }
        
        public float GetSessionTime() {
            if (!_isStarted) throw new Exception("Session is not started");
            return (Time.time - _startTime - StartDelayTime) * 1000;
        }
        
        public int GetCurrentScore() {
            return _currentScore;
        }
        
        public (int, int) GetBalanceScale() { // Dark, Light
            return (_balanceScale, -_balanceScale);
        }
        
        public float GetSecondsPerUnit() {
            return _beatmap.GetSecondsPerUnit();
        }
        
        public int GetKeyCount() {
            return _beatmap.KeyCount;
        }
        
        // Negative if balanced
        public bool GetIsBalanced() {
            return _balanceScale == 0;
        }
        
        public float GetUnbalancedTimeLeft() {
            return Time.time - _unbalancedStartTime;
        }
        
        public float GetMissRatio() {
            return _missCount / (1.0f * _notes.Length * (20.0f/100.0f));
        }
    }
}
