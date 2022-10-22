using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Cutscene.Statements;

namespace Game.Cutscene {
    public class CutsceneUtils {
        public static CutsceneStatement[] ParseStatements(string input) {
            string[] lines = input.Split(new char[]{'\n'}, StringSplitOptions.RemoveEmptyEntries);
            CutsceneStatement[] ret = new CutsceneStatement[lines.Length];
            
            for (int i = 0; i < lines.Length; i++) {
                string[] splittedLine = lines[i].Split('|');
                
                if (splittedLine[0].Equals("dialog")) {
                    ret[i] = new Dialog(splittedLine[1]);
                } else if (splittedLine[0].Equals("setbg")) {
                    ret[i] = new SetBG(Int32.Parse(splittedLine[1]));
                } else if (splittedLine[0].Equals("delay")) {
                    float duration = float.Parse(splittedLine[1]);
                    ret[i] = new Delay(duration);
                } else {
                    throw new Exception(String.Format("Line {0} is invalid", i + 1));
                }
            }
            
            return ret;
        }
    }
}

