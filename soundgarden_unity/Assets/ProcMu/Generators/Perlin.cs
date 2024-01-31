using System.Collections.Generic;
using AudioHelm;
using ProcMu.ScriptableObjects;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace ProcMu.Generators
{
    public static class Perlin
    {
        public static List<Note> MakeNotes(List<Note> notesIn, MuConfig config, int index)
        {
            int bottomNote = config.instrument[index].oct0.x * 12;
            int topNote = config.instrument[index].oct0.y * 12 + 11;
            int currentNote;

            float seed = Random.Range(-2048f, 2048f);

            for (int i = 0; i < notesIn.Count; i++)
            {
                float offset = math.remap(0, config.barLength - 1, 0f, 4f, notesIn[i].start);
                //Evaluate noise based on note position in bar.
                float note = Mathf.PerlinNoise1D(seed + offset);

                //Remap note to a value between the min and max octave.
                note = math.remap(0f, 1f, bottomNote, topNote, note);

                //Find closest note in scale

                Debug.Log(note);
                notesIn[i].note = FindClosestNoteIndex((int)note, config.muScale.Scale);
            }


            return notesIn;
        }

        private static int FindClosestNoteIndex(int targetNote, bool[] scale)
        {
            // check from the target note upwards
            for (int up = targetNote; up < scale.Length; up++)
            {
                if (scale[up]) return up;
            }

            // check from the target note downwards
            for (int down = targetNote; down >= 0; down--)
            {
                if (scale[down]) return down;
            }

            // no active note found, return default value
            return 0;
        }
    }
}