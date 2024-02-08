using UnityEngine;

public static class Scales
{
    public static readonly int[] Chromatic = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
    public static readonly int[] Major = { 0, 2, 4, 5, 7, 9, 11 };
    public static readonly int[] NaturalMinor = { 0, 2, 3, 5, 7, 8, 10 };
    public static readonly int[] HarmonicMinor = { 0, 2, 3, 5, 7, 8, 11 };
    public static readonly int[] MelodicMinorAscending = { 0, 2, 3, 5, 7, 9, 11 };
    public static readonly int[] PentatonicMajor = { 0, 2, 4, 7, 9 };
    public static readonly int[] PentatonicMinor = { 0, 3, 5, 7, 10 };
    public static readonly int[] Blues = { 0, 3, 5, 6, 7, 10 };
    public static readonly int[] WholeTone = { 0, 2, 4, 6, 8, 10 };
    public static readonly int[] Dorian = { 0, 2, 3, 5, 7, 9, 10 };
    public static readonly int[] Phrygian = { 0, 1, 3, 5, 7, 8, 10 };
    public static readonly int[] Lydian = { 0, 2, 4, 6, 7, 9, 11 };
    public static readonly int[] Mixolydian = { 0, 2, 4, 5, 7, 9, 10 };
    public static readonly int[] Aeolian = { 0, 2, 3, 5, 7, 8, 10 };
    public static readonly int[] Locrian = { 0, 1, 3, 5, 6, 8, 10 };

    public static int[] GetScale(ScaleType type)
    {
        switch (type)
        {
            case ScaleType.Chromatic:
                return Chromatic;
            case ScaleType.Major:
                return Major;
            case ScaleType.NaturalMinor:
                return NaturalMinor;
            case ScaleType.HarmonicMinor:
                return HarmonicMinor;
            case ScaleType.MelodicMinorAscending:
                return MelodicMinorAscending;
            case ScaleType.PentatonicMajor:
                return PentatonicMajor;
            case ScaleType.PentatonicMinor:
                return PentatonicMinor;
            case ScaleType.Blues:
                return Blues;
            case ScaleType.WholeTone:
                return WholeTone;
            case ScaleType.Dorian:
                return Dorian;
            case ScaleType.Phrygian:
                return Phrygian;
            case ScaleType.Lydian:
                return Lydian;
            case ScaleType.Mixolydian:
                return Mixolydian;
            case ScaleType.Aeolian:
                return Aeolian;
            case ScaleType.Locrian:
                return Locrian;
            default:
                return Chromatic;
        }
    }

    /// <summary> Returns a random note within a scale. </summary>
    /// <returns></returns>
    public static int GetScaleRandom(ScaleType type)
    {
        int[] scaleNotes = GetScale(type);
        return scaleNotes[Random.Range(0, scaleNotes.Length)];
    }
}
