using System.Collections.Generic;
using UnityEngine;

public static class Markovka
{
    private static Dictionary<int, List<int>> markovChain;
    private static int currentRelativePitch = 0; // Start at root pitch

    static Markovka()
    {
        InitializeMarkovChain();
    }

    private static void InitializeMarkovChain()
    {
        markovChain = new Dictionary<int, List<int>>();

        // Define transitions for each relative pitch
        markovChain[0] = new List<int> { 0, 2, -2, 4 }; // Transitions from root
        markovChain[2] = new List<int> { 0, 3, -2, 5 }; // Transitions from +2 semitones
        markovChain[-2] = new List<int> { 0, 2, -3, -5 }; // Transitions from -2 semitones
        markovChain[4] = new List<int> { 0, 2, -2, 7 }; // Transitions from +4 semitones
        // Add more transitions as needed
    }

    public static int[] MakeMelody(int length)
    {
        int[] melody = new int[length];
        currentRelativePitch = 0; // Reset to root at the start of each melody generation

        for (int i = 0; i < melody.Length; i++)
        {
            melody[i] =  currentRelativePitch;
            currentRelativePitch = GetNextRelativePitch(currentRelativePitch);
        }
        return melody;
    }

    private static int GetNextRelativePitch(int currentPitch)
    {
        if (!markovChain.ContainsKey(currentPitch))
        {
            return 0; // Default to root if no defined transitions
        }

        var nextPitches = markovChain[currentPitch];
        return nextPitches[Random.Range(0, nextPitches.Count)];
    }
}