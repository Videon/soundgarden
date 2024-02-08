using System;
using System.Collections.Generic;
using ProcMu.ScriptableObjects;
using ProcMu.UnityScripts;
using ProcMu.UnityScripts.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GardenInterpolator : MonoBehaviour
{
    [SerializeField] private MusicMaker musicMaker;
    [SerializeField] private MuConfig outputConfig;

    [SerializeField] private LayerMask layerMask;

    [SerializeField, Tooltip("Player object transform for position tracking.")]
    public Transform trackedTransform;

    [SerializeField]
    [Tooltip("How music speed is determined. Global: Uses global bpm value. Local: Uses interpolated bpm value.")]
    private BpmMode bpmMode;

    [SerializeField, Tooltip("The maximum distance of a music zone center away from the player to be considered.")]
    private float maxDistance;

    [SerializeField, Tooltip("The maximum number of music zones to consider for interpolation.")]
    private int maxZones;

    [SerializeField, Tooltip("Checks per second: How often music configuration is evaluated.")]
    private float cps = 2;

    #region private variables

    private readonly Collider[] _colliders = new Collider[16]; //Pre-allocated memory for found music zone colliders

    private Vector3 _playerPos;


    [SerializeField]
    private McDist[] _mcs = new McDist[16]; //List of music configurations with attached distance value.

    [SerializeField] private float[] distances;

    private float elapsedTime = 0f;

    /// <summary> Cumulated distance factors for all music zones. </summary>
    private float distanceSum = 0f;

    #endregion

    private void Awake()
    {
        musicMaker.globalConfig = outputConfig;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < 1f / cps) return; //Only proceed if interval has passed.

        elapsedTime = 0f;

        int top = FindAndSortMusicZones();

        //Generate distances array from sorted McDist objects in list
        distances = new float[top];
        for (int i = 0; i < top; i++)
        {
            if (_mcs[i].Mc == null) break;

            distances[i] = _mcs[i].Dist;
        }

        int inner = CheckInner(top);

        //If check position is within inner radius of a zone, use its config, otherwise interpolate.
        //if (inner > -1) ProcMuUtils.CopyMuConfig(_mcs[inner].Mc, musicMaker);
        //else InterpolateAll(distances, top);
    }

    /// <summary> Checks whether player is in inner zone of any music zone in reach and returns its index. </summary>
    /// <returns> Index of zone whose inner zone the player occupies. -1 if not in any inner zone. </returns>
    private int CheckInner(int top)
    {
        for (int i = 0; i < top; i++)
        {
            if (_mcs[i].Dist <= _mcs[i].RangeInner) return i;
        }

        return -1;
    }

    /// <summary> Finds all music zones within range and puts them in McDist array. </summary>
    /// <returns> Number of found music zones. </returns>
    private int FindAndSortMusicZones()
    {
        Array.Clear(_mcs, 0, _mcs.Length); //Clear list before filling it again with data.

        int found = Physics.OverlapSphereNonAlloc(trackedTransform.position, maxDistance, _colliders, layerMask);

        //Use number of colliders as top if there are fewer colliders than space in the array.
        int top = found < _colliders.Length ? found : _colliders.Length;

        for (int i = 0; i < top; i++)
        {
            float dist = Vector3.Distance(trackedTransform.position, _colliders[i].transform.position);

            MusicZone mz = _colliders[i].GetComponent<MusicZone>();

            _mcs[i] = new McDist(mz.Config, dist, mz.RadiusInner);
        }

        Array.Sort(_mcs, ProcMuUtils.CompareMcDists); //Sort objects according to distance.

        distanceSum = 0f; //Reset cumulated distances to 0

        return top;
    }


    private void InterpolateAll()
    {
        float totalWeight = 0f;
        float[] weights = new float[distances.Length];
        // Calculate weights based on inverse distance
        for (int i = 0; i < _mcs.Length; i++)
        {
            weights[i] = 1f / _mcs[i].Dist;
            totalWeight += weights[i];
        }


        for (int i = 0; i < _mcs.Length; i++)
        {
            //For every parameter in each MuConfig, find the interpolated value
            for (int instrIndex = 0; instrIndex < outputConfig.instrument.Length; instrIndex++)
            {
                List<MelConfig> instruments = new List<MelConfig>();
                for (int k = 0; k < _mcs.Length; k++)
                {
                    instruments.Add(_mcs[i].Mc.instrument[instrIndex]);
                }

                outputConfig.instrument[i] = GetClosest(instruments, _mcs);
            }

            //todo use weights and totalWeight for calculating weighted values!
/*
            for (int i = 0; i < _mcs[i].Mc.instrument.Length; i++)
            {
                for (int j = 0; j < _mcs[i].Mc.instrument[i].parameters.Length; j++)
                {
                    outputConfig.instrument[i].parameters[j].value =
                        InterpolateParameter(_mcs, distances, weights, totalWeight, i, j);
                }
            }
            */
        }

        // Set the output configuration to the music maker
        musicMaker.globalConfig = outputConfig;
    }


    private static T GetClosest<T>(IList<T> values, McDist[] mcs)
    {
        //Find index of value at closest distance
        int index = -1;
        float closest = 99999999f;
        for (int i = 0; i < mcs.Length; i++)
        {
            if (mcs[i].Dist >= closest) continue;

            index = i;
            closest = mcs[i].Dist;
        }

        return values[index];
    }
}