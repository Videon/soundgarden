using System;
using System.Collections.Generic;
using AudioHelm;
using NaughtyAttributes;
using ProcMu.Generators;
using ProcMu.ScriptableObjects;
using ProcMu.UnityScripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicMaker : MonoBehaviour
{
    [SerializeField] private AudioHelmClock clock;
    [SerializeField] private Sampler percussionSampler;


    [SerializeField] private List<Sequencer> sequencers;

    [SerializeField] private int rootNote = 60;

    [SerializeField] private MuConfig globalConfig;

    [SerializeField] private int currentStep;

    private void Awake()
    {
        //Then initialize everything
        Initialize();
    }

    void Start()
    {
    }

    private void Initialize()
    {
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
        MakeNotes(sequencers[0], globalConfig, 0); //DRONE
        MakeNotes(sequencers[1], globalConfig, 1); //BASS
        MakeNotes(sequencers[2], globalConfig, 2); //MELODY
        MakePercussion(); //PERCUSSION
    }

    public void MakeNotes(Sequencer sequencer, MuConfig config, int index)
    {
        //Clear existing notes
        sequencer.Clear();

        List<Note> notes = new List<Note>();

        //Generate rhythm
        notes = Euclid.MakeRhythm(globalConfig.TotalSteps, config.rhythms[index].Hits, config.rhythms[index].Rotation,
            config.rhythms[index].StepLength);

        //Make melody
        switch (config.instrument[index].algo)
        {
            default:
            case GenAlgo.Random:
                for (int i = 0; i < notes.Count; i++)
                {
                    notes[i].note =
                        Random.Range(config.instrument[index].oct0.x, config.instrument[index].oct0.y) * 12 +
                        Scales.GetScaleRandom(globalConfig.scale);
                }

                break;
            case GenAlgo.Arpeggio:
                notes = Climber.MakeNotes(notes, config, index);
                break;
            case GenAlgo.Lsystem:
                break;
            case GenAlgo.Perlin:
                notes = Perlin.MakeNotes(notes, config, index);
                break;
        }


        for (int i = 0; i < notes.Count; i++)
        {
            sequencer.AddNote(notes[i].note, notes[i].start, notes[i].end, notes[i].velocity);
        }
    }

    private void MakePercussion()
    {
        //Clear existing notes
        sequencers[3].Clear();

        int root = 24;

        //Percussion uses one list per percussion sound, corresponding to one pitch in the sampler.
        List<List<Note>> notes = new List<List<Note>>(12);

        for (int i = 0; i < globalConfig.percussionParams.Length; i++)
        {
            percussionSampler.keyzones[i].audioClip = globalConfig.percussionSounds[i];
            notes.Add(Euclid.MakeRhythm(globalConfig.TotalSteps,
                globalConfig.percussionParams[i].Hits * globalConfig.bars, globalConfig.percussionParams[i].Rotation,
                1));
        }

        for (int i = 0; i < globalConfig.percussionParams.Length; i++)
        {
            for (int j = 0; j < notes[i].Count; j++)
            {
                sequencers[3].AddNote(root + i, notes[i][j].start, notes[i][j].end, notes[i][j].velocity);
            }
        }
    }

    public void CountStep()
    {
        currentStep = (currentStep + 1) % globalConfig.TotalSteps;
        if (currentStep == globalConfig.TotalSteps - 1) MakeMusic();
    }
}