using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Session {
    [CreateAssetMenu(fileName = "NewBeatmap", menuName = "Session/Beatmap", order = 1)]
    public class Beatmap : ScriptableObject {
        public string Name = "New Beatmap";
        public float Difficulty = 1.0f;
        public float Speed = 1.0f;
        public AudioClip Audio;
        public Sprite Background;
        public int KeyCount = 4;
        [Multiline(40)] public string Code =
        "tap-1000-dark-0\n" +
        "tap-1000-light-1\n" +
        "tap-1000-light-0\n" +
        "tap-1000-dark-3";
        
        public float GetDuration() {
            return Audio.length * 1000;
        }
        
        public float GetSecondsPerUnit() {
            return 1 / Speed;
        }
    }
}
