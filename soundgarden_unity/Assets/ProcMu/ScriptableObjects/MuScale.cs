using ProcMu.UnityScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProcMu.ScriptableObjects
{
    [CreateAssetMenu(fileName = "mus_", menuName = "ProcMu/Scale", order = 1)]
    public class MuScale : ScriptableObject
    {
        [SerializeField] public bool[] Scale = new bool[128];
        [HideInInspector] public int[] ScaleNotes = new int[128];
        [FormerlySerializedAs("tonic")] [SerializeField] public RootNote rootNote;
    }
}