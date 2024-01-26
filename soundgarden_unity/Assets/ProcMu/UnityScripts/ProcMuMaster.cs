using System.Collections;
using ProcMu.ScriptableObjects;
using ProcMu.UnityScripts.Utilities;
using UnityEngine;

namespace ProcMu.UnityScripts
{
    
    public class ProcMuMaster : MonoBehaviour
    {
        //[SerializeField] private CsoundUnity csoundUnity;
        private bool _isInitialized;

        private double[] comm = new double[4]; //Allocate space for communication information with Csound

        private AudioClip[] audioClips;
        private MuSampleDb sampleDb;

        [SerializeField, Range(0, 1000)] private double bpm = 120;
        [SerializeField, Range(0f, 1f)] private float intensity = 1;

        public MuConfig mc;

        private void Awake()
        {

        }

        public void SetBpm(double bpm)
        {
            this.bpm = bpm;
        }

        public void SetIntensity(float intensity)
        {
            this.intensity = intensity;
        }
    }
}