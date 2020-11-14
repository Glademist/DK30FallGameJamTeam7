using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMixing
{
    public static List<string> GetMovementSounds(string name)
    {
        switch (name)
        {
            case "Squire":
                return new List<string>(new string[] { "Squire_Move1", "Squire_Move2", "Squire_Move3" });
            case "Knight":
                return new List<string>(new string[] { "Knight_Move1", "Knight_Move2", "Knight_Move3", "Knight_Move4" });
            case "Slime":
                return new List<string>(new string[] { "Slime_Move1", "Slime_Move2" });
            case "Goblin":
            case "Imp":
                return new List<string>(new string[] { "Small_Move1", "Small_Move2", "Small_Move3" });
            case "Mimic":
                return new List<string>(new string[] { "Mimic_Move1", "Mimic_Move2", "Mimic_Move3" });
            case "GoblinKing":
                return new List<string>(new string[] { "GoblinKing_Move4", "GoblinKing_Move5" });
        }
        return new List<string>();
    }
}
