using System;
using System.Collections.Generic;
using ProcMu.UnityScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProcMu.ScriptableObjects
{
    [CreateAssetMenu(fileName = "muc_", menuName = "ProcMu/Music configuration", order = 1)]
    public class MuConfig : ScriptableObject
    {
        public MelConfig[] instrument = new MelConfig[3];
        public RhythmParams[] rhythms = new RhythmParams[16];
        public ArpMode[] arpModes = new ArpMode[16];
        public Lsys[] lsystems = new Lsys[16];

        public PerConfig perConfig; //Configuration object for percussion

        public int TotalSteps => barLength * bars;

        #region Global parameters

        public float bpm = 120;
        public int bars = 4;
        public int barLength = 16;
        public MuScale MuScale;
        public ScaleType Scale;

        /// <summary> Contains active bar information for all instruments. 4 indices reserved per layer. </summary>
        public bool[] activeBars0 = new bool[64];

        public bool[] activeBars1 = new bool[64];

        #endregion

        #region Percussion parameters

        //Euclidean rhythm parameter: total steps, hits, rotation
        public RhythmParams[] percussionParams = new RhythmParams[12];
        public AudioClip[] percussionSounds = new AudioClip[12];

        #endregion
    }
}