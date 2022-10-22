using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Session {
    // Notes
    public class Note {
        public int StartTime;
        public NoteAffinity Affinity;
        public int Key;
        
        public override string ToString() {
            return "" + StartTime + " " + Affinity.ToString() + " " + Key;
        }
    }
    
    public class TapNote : Note { }
    
    public class HoldNote : Note {
        public int EndTime;
    }
    
    // Result
    // TODO: Result save and load
    public class Result {
        public Beatmap SelectedBeatmap;
        
        public int Score;
        public string AlphabetScore;
        public bool IsSuccess;
        
        public int VeryGoodCount;
        public int GoodCount;
        public int MissCount;
        
        public Result(Beatmap beatmap, bool isSuccess, int score, string alphabetScore, int veryGoodCount, int goodCount, int missCount) {
            SelectedBeatmap = beatmap;
            Score = score;
            AlphabetScore = alphabetScore;
            IsSuccess = isSuccess; 
            VeryGoodCount = veryGoodCount;
            GoodCount = goodCount;
            MissCount = missCount;
        }
    }
    
    // Enums
    public enum NoteAffinity {
        Light,
        Dark,
    }
    
    public enum NoteHitType {
        Miss,
        Good,
        VeryGood,
    }
}
