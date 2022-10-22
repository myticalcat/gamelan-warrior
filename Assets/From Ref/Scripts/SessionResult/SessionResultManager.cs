using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

using Game.Session;

public class SessionResultManager : MonoBehaviour {
    private static Result _result;
    
    public TMP_Text AlphabetScoreText;
    public TMP_Text ScoreText;
    public TMP_Text PerfectCount;
    public TMP_Text GoodCount;
    public TMP_Text MissCount;
    public SpriteRenderer BackgroundRenderer;
    public SFXManager _sfxManager;
    // public Image Background;
    
    private void Start() {
        if (_result == null) throw new Exception("Result is not set");
        
        AlphabetScoreText.text = _result.AlphabetScore.ToString();
        ScoreText.text = _result.Score.ToString();
        PerfectCount.text = "Perfect " + _result.VeryGoodCount.ToString() + "x";
        GoodCount.text = "Good " + _result.GoodCount.ToString() + "x";
        MissCount.text = "Miss " + _result.MissCount.ToString() + "x";
        BackgroundRenderer.sprite = _result.SelectedBeatmap.Background;
        
        if (_result.IsSuccess) _sfxManager.PlaySuccess();
        else _sfxManager.PlayFail();
    }
    
    public void CloseResult() {
        SceneManager.LoadScene("TitleScreen");
    }
    
    public static void SetResult(Result result) {
        _result = result;
    }
}
