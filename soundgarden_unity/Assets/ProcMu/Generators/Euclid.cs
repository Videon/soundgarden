using System.Collections.Generic;
using AudioHelm;

public static class Euclid
{
    private static Note note;

    public static List<Note> MakeRhythm(int steps, int hits, int rotation, int stepLength = 1)
    {
        List<Note> notes = new List<Note>();
        for (int i = 0; i < steps; i++)
        {
            if (((i + rotation) * hits) % steps < hits)
            {
                Note currentNote = new Note();
                currentNote.start = i;
                currentNote.end = i + stepLength;
                currentNote.velocity = 1;
                notes.Add(currentNote);
            }
        }

        return notes;
    }
}