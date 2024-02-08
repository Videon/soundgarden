namespace ProcMu.UnityScripts
{
    public enum RootNote
    {
        C,
        CSharp,
        D,
        DSharp,
        E,
        F,
        FSharp,
        G,
        GSharp,
        A,
        ASharp,
        B
    }

    public enum Scale
    {
        Lydian,
        Ionian_Major,
        Mixolydian,
        Dorian,
        Aeolian_Minor,
        Phrygian,
        Locrian
    }

    public enum ChordMode
    {
        Random357,
        Random357Oct,
        RandomRoot,
        RandomRootOct,
        Triads,
        TriadsOct
    }

    public enum Wavetable
    {
        Sine,
        Square,
        Saw,
        Pulse
    }

    public enum Instrument
    {
        EucRth,
        Chords,
        SnhMel,
        Bass
    }

    public enum MelodyCurve
    {
        Sine,
        Square,
        Saw,
        Pulse
    }

    public enum MelodyMode
    {
        Continuous,
        Retriggered
    }

    public enum InterpolationMode
    {
        DistanceWeighted, //Generates new value from weighted average of all input values
        Closest //Sets new value according to closest music zone configuration
    }

    public enum BpmMode
    {
        Global, //Uses value defined in ProcMu master
        Local //Uses interpolated value
    }

    public enum GenAlgo
    {
        Random,
        Arpeggio,
        Lsystem,
        Perlin
    }

    public enum ClimbMode
    {
        Up,
        Down,
        UpAndDown,
        DownAndUp,
        Random
    }

    public enum InstrumentType
    {
        Drone,
        Bass,
        Lead,
        Percussion
    }
}