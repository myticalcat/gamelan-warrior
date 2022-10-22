using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cutscene {
    [CreateAssetMenu(fileName = "NewCutscene", menuName = "Cutscene/Cutscene", order = 1)]
    public class Cutscene : ScriptableObject {
        public string Name = "New Cutscene";
        public Sprite[] Backgrounds;
        [Multiline(40)] public string Statements =
        "setbg|0\n" +
        "dialog|So dark..\n" +
        "dialog|Teleport to village\n" +
        "setbg|1\n" +
        "dialog|Okay, now, everything is good.";
    }
}
