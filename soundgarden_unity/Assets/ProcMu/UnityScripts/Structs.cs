using System;
using ProcMu.ScriptableObjects;
using UnityEngine;

namespace ProcMu.UnityScripts
{
    /// <summary> Data type for assigning distance float value to music configuration. </summary>
    public struct McDist
    {
        public McDist(MuConfig config, float distance, float rangeInner)
        {
            Mc = config;
            Dist = distance;
            RangeInner = rangeInner;
        }

        public MuConfig Mc { get; }
        public float Dist { get; }
        public float RangeInner { get; }
    }

    [Serializable]
    public struct RhythmParams
    {
        public int Hits;
        public int Rotation;
        public int StepLength;
    }

    [Serializable]
    public struct LsysRule
    {
        public char a;
        public string b;
    }
}