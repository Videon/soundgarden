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

        #region Drone parameters

        public GenAlgo droneAlgo;
        public Vector2Int drone_Oct0;
        public Vector2Int drone_Oct1;

        #endregion

        #region Bass parameters

        public GenAlgo bassAlgo;

        public int bass_minOct0;
        public int bass_maxOct0;
        public int bass_minOct1;
        public int bass_maxOct1;

        #endregion

        #region Percussion parameters

        //Euclidean rhythm parameter: total steps, hits, rotation
        public PercussionParams[] percussionParams = new PercussionParams[12];
        public AudioClip[] percussionSounds = new AudioClip[12];

        #endregion
    }
}