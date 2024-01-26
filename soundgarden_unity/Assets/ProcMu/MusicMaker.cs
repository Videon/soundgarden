using System;
using System.Collections.Generic;
using AudioHelm;
using NaughtyAttributes;
using ProcMu.ScriptableObjects;
using ProcMu.UnityScripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicMaker : MonoBehaviour
{
    [SerializeField] private AudioHelmClock clock;
    [SerializeField] private HelmSequencer sequencerBass;
    [SerializeField] private HelmSequencer sequencerDrone;
    [SerializeField] private SampleSequencer sequencerPercussion;
    [SerializeField] private Sampler percussionSampler;


    [SerializeField] private List<Sequencer> sequencers;

    [SerializeField] private int rootNote = 60;

    [SerializeField] private MuConfig globalConfig;

    private void Awake()
    {
        //Then initialize everything
        Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeMusic();
    }

    private void Initialize()
    {
        sequencers.Clear();
        sequencers.Add(sequencerBass);
        sequencers.Add(sequencerDrone);
        sequencers.Add(sequencerPercussion);

        clock.bpm = globalConfig.bpm;

        //initialize sequencers
        for (int i = 0; i < sequencers.Count; i++)
        {
            sequencers[i].length = globalConfig.barLength * globalConfig.bars;
        }
    }

    [Button]
    public void MakeMusic()
    {
        //DRONE
        MakeNotes(sequencerDrone, 1, 0, 64, 3, 3);

        //BASS
        MakeNotes(sequencerBass, 2, 0, 4, 3, 4);

        //PERCUSSION
        MakePercussion();
    }

    public void MakeNotes(Sequencer sequencer, int notesAmt, int rotation, int stepLength, int minOct, int maxOct)
    {
        //Clear existing notes
        sequencer.Clear();

        List<Note> notes = new List<Note>();
        notes = Euclid.MakeRhythm(globalConfig.TotalSteps, notesAmt, rotation, stepLength);

        for (int i = 0; i < notes.Count; i++)
        {
            notes[i].note = Random.Range(minOct, maxOct) * 12 + Scales.GetScaleRandom(globalConfig.Scale);
        }

        for (int i = 0; i < notes.Count; i++)
        {
            sequencer.AddNote(notes[i].note, notes[i].start, notes[i].end, notes[i].velocity);
        }
    }

    private void MakePercussion()
    {
        //Clear existing notes
        sequencerPercussion.Clear();

        int root = 24;

        //Percussion uses one list per percussion sound, corresponding to one pitch in the sampler.
        List<List<Note>> notes = new List<List<Note>>(12);

        for (int i = 0; i < globalConfig.percussionParams.Length; i++)
        {
            percussionSampler.keyzones[i].audioClip = globalConfig.percussionSounds[i];
            notes.Add(Euclid.MakeRhythm(globalConfig.TotalSteps, globalConfig.percussionParams[i].Hits * globalConfig.bars, globalConfig.percussionParams[i].Rotation, 1));
        }

        for (int i = 0; i < globalConfig.percussionParams.Length; i++)
        {
            for (int j = 0; j < notes[i].Count; j++)
            {
                sequencerPercussion.AddNote(root + i, notes[i][j].start, notes[i][j].end, notes[i][j].velocity);
            }
        }
    }
}