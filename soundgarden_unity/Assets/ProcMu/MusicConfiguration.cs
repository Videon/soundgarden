using UnityEngine;

[CreateAssetMenu(fileName = "MC_", menuName = "Music/MusicConfiguration", order = 1)]
public class MusicConfiguration : ScriptableObject
{
    public ScaleType scale;
    public int rootNote;
}