using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Session {
    public class SessionUtils {
        public static Note[] BeatmapDecoder(Beatmap beatmap) {
            string[] lines = beatmap.Code.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            Note[] ret = new Note[lines.Length];
            
            for (int i = 0; i < lines.Length; i++) {
                string[] splittedLine = lines[i].Split(new char[]{'-'});
                try {
                    if (splittedLine[0].Equals("tap")) {
                        ret[i] = new Note();
                        ret[i].StartTime = Int32.Parse(splittedLine[1]);
                        ret[i].Affinity = splittedLine[2].Equals("dark") ? NoteAffinity.Dark : NoteAffinity.Light;
                        int key = Int32.Parse(splittedLine[3]);
                        if (key < 0 || key >= beatmap.KeyCount) throw new Exception("Invalid key count");
                        ret[i].Key = key;
                    } else if (splittedLine.Equals("hold")) {
                        throw new Exception("Invalid note type");
                    } else {
                        throw new Exception("Invalid note type");
                    }
                } catch(Exception e) {
                    throw new Exception("Beatmap code is not valid: " + e.Message);
                }
            }
            
            return ret;
        }
        
        public static KeyCode KeyIndexToKeyCode(int keyIndex) {
            KeyCode[] keyIndexToKeyCode = new KeyCode[]{KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K};
            return keyIndexToKeyCode[keyIndex];
        }
        
        public static float KeyIndexToHorizontalPosition(int keyIndex) {
            return keyIndex - 2.5f;
        }
        
        // isSuccess, Alphabet Score
        public static (bool, string) CalculateAlphabetScore(int totalNotes, bool isUnbalanced, int veryGoodCount, int goodCount, int missCount) {
            if (!isUnbalanced && missCount == 0 && goodCount == 0) return (true, "SS");
            else if (!isUnbalanced && missCount == 0 && 20 * goodCount <= totalNotes) return (true, "S");
            else if (!isUnbalanced && 20 * missCount <= totalNotes && 10 * goodCount <= totalNotes) return (true, "A");
            else if (!isUnbalanced && 10 * missCount <= totalNotes && 5 * goodCount <= 2 * totalNotes) return (false, "B");
            else if (!isUnbalanced && 5 * missCount <= totalNotes) return (false, "C");
            else return (false, "D");
        }
    }
}