using System;
using UnityEngine;
using ProcMu.UnityScripts;

[Serializable]
public class MelConfig
{
    public String name;
    public GenAlgo algo;
    public RootNote rootNote;
    public Vector2Int oct0;
    public Vector2Int oct1;
}