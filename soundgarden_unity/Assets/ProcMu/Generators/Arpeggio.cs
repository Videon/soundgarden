using System;
using System.Collections;
using System.Collections.Generic;
using AudioHelm;
using ProcMu.ScriptableObjects;
using ProcMu.UnityScripts;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProcMu.Generators
{
    public static class Arpeggio
    {
        public static List<Note> MakeNotes(List<Note> notesIn, MuConfig config, int index)
        {
            List<Note> notesOut = notesIn;

            switch (config.arpModes[index])
            {
                default:
                case ArpMode.Up:
                    for (int i = 0; i < notesIn.Count; i++)
                    {
                        //notesOut[i].note = notesIn[i]
                            //determine start note
                            //find next note in scale
                            //find top note of set octaves
                            //go back to bottom note
                    }
                    break;
                case ArpMode.Down:
                    break;
                case ArpMode.UpAndDown:
                    break;
                case ArpMode.DownAndUp:
                    break;
                case ArpMode.Random:
                    break;
            }

            return notesOut;
        }
    }
}
