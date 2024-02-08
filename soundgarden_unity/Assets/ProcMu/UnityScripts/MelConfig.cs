using System;
using UnityEngine;
using ProcMu.UnityScripts;

[Serializable]
public class MelConfig
{
    public String name;
    public InstrumentType type;
    public GenAlgo algo;
    public Vector2Int oct0;
    public AudioHelm.HelmPatch patch;
    //public Vector2Int oct1;
}