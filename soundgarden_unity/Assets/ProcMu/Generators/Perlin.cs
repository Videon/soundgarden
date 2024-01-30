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
            List<Note> notesOut = notesIn;

            float seed = Random.Range(-2048f, 2048f);

            for (int i = 0; i < notesIn.Count; i++)
            {
                float offset = math.remap(0, config.barLength - 1, 0f, 4f, notesIn[i].start);
                //Evaluate noise based on note position in bar.
                float note = Mathf.PerlinNoise1D(seed + offset);

                //Remap note to a value between the min and max octave.
                note = math.remap(0f, 1f, 
                        config.instrument[index].oct0.x * 12, config.instrument[index].oct0.y * 12, note);

                Debug.Log(note);
                notesOut[i].note = (int)note;
            }


            return notesOut;
        }
    }
}