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
    public static class Climber
    {
        public static List<Note> MakeNotes(List<Note> notesIn, MuConfig config, int index)
        {
            MuScale scale = config.muScale;

            int bottomNote = config.instrument[index].oct0.x * 12;
            int topNote = config.instrument[index].oct0.y * 12 + 11;
            int currentNote;

            switch (config.arpModes[index])
            {
                default:
                case ClimbMode.Up:
                    currentNote = bottomNote;
                    for (int i = 0; i < notesIn.Count; i++)
                    {
                        // find next note in scale
                        while (true)
                        {
                            if (currentNote > topNote) currentNote = bottomNote;

                            if (scale.Scale[currentNote])
                            {
                                notesIn[i].note = currentNote;
                                currentNote++;
                                break;
                            }

                            currentNote++;
                        }
                    }

                    break;
                case ClimbMode.Down:
                    currentNote = topNote;
                    for (int i = 0; i < notesIn.Count; i++)
                    {
                        // find next note in scale
                        while (true)
                        {
                            if (currentNote < bottomNote) currentNote = topNote;

                            if (scale.Scale[currentNote])
                            {
                                notesIn[i].note = currentNote;
                                currentNote--;
                                break;
                            }

                            currentNote--;
                        }
                    }

                    break;
                case ClimbMode.UpAndDown:
                    break;
                case ClimbMode.DownAndUp:
                    break;
                case ClimbMode.Random:
                    break;
            }

            return notesIn;
        }
    }
}