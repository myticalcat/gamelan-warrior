using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Session;

public class BeatmapDecoderTestScript : MonoBehaviour {
    public Beatmap SelectedBeatmap;
    
    private void Start() {
        Note[] noteList = SessionUtils.BeatmapDecoder(SelectedBeatmap);
        for (int i = 0; i < noteList.Length; i++) {
            Debug.Log(noteList[i]);
        }
    }
}
