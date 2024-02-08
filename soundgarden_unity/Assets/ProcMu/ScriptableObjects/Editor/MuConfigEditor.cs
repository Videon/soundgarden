using System;
using ProcMu.UnityScripts;
using UnityEditor;
using UnityEngine;

namespace ProcMu.ScriptableObjects.Editor
{
    [CustomEditor(typeof(MuConfig))]
    public class MuConfigEditor : UnityEditor.Editor
    {
        private MuConfig _muConfig;

        #region GUI styles

        private Color defaultColor;

        private Color[] instrColors;

        //TODO ADD DISPLAY FOR REAL-TIME INFORMATION
        private GUIStyle gsActive;

        private GUIStyle gsInactive;

        #endregion

        private void OnEnable()
        {
            defaultColor = GUI.color;
            instrColors = new Color[]
            {
                new Color32(144, 241, 239, 255), //EUCRTH
                new Color32(255, 214, 224, 255), //CHORDS
                new Color32(255, 239, 159, 255), //SNHMEL
                new Color32(193, 251, 164, 255) //BASS
            };

            gsActive = new GUIStyle(GUIStyle.none)
            {
                normal = { textColor = Color.white }
            };

            gsInactive = new GUIStyle(GUIStyle.none)
            {
                normal = { textColor = Color.black }
            };
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            _muConfig = (MuConfig)target;

            DrawGeneralSettings();
            DrawActiveBars();
            for (int i = 0; i < _muConfig.instrument.Length; i++)
            {
                DrawInstrument(i);
            }

            DrawPercussion();

            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(_muConfig);
        }

        private void DrawGeneralSettings()
        {
            GUI.backgroundColor = defaultColor;
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("General settings");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("BPM", GUILayout.Width(100f));
            _muConfig.bpm = EditorGUILayout.FloatField(_muConfig.bpm, GUILayout.Width(100f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("steps/bar", GUILayout.Width(100f));
            _muConfig.barLength = EditorGUILayout.IntField(_muConfig.barLength, GUILayout.Width(100f));
            EditorGUILayout.LabelField("bars", GUILayout.Width(100f));
            _muConfig.bars = EditorGUILayout.IntField(_muConfig.bars, GUILayout.Width(100f));
            EditorGUILayout.LabelField("total steps: " + _muConfig.TotalSteps, GUILayout.Width(120f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("Scale", GUILayout.Width(100f));
            //_muConfig.scale = (ScaleType)EditorGUILayout.EnumPopup(_muConfig.scale, GUILayout.Width(100f));

            EditorGUILayout.LabelField("Scale", GUILayout.Width(100f));
            _muConfig.muScale =
                (MuScale)EditorGUILayout.ObjectField(_muConfig.muScale, typeof(MuScale), GUILayout.Width(100f));

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
            GUI.backgroundColor = defaultColor;
        }

        private void DrawActiveBars()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("Active bars");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Instrument", GUILayout.Width(80f));
            EditorGUILayout.EndHorizontal();

            for (int instr = 0; instr < 4; instr++)
            {
                int index = instr * 4;
                GUI.color = instrColors[instr];
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(((Instrument)instr).ToString(), GUILayout.Width(80f));
                EditorGUILayout.LabelField("", GUILayout.Width(10f));

                //Draw active bars for intensity = 0
                for (int i = 0; i < 4; i++)
                {
                    _muConfig.activeBars[index + i] =
                        EditorGUILayout.Toggle("", _muConfig.activeBars[index + i], GUILayout.Width(20f));
                }

                EditorGUILayout.EndHorizontal();
                GUI.color = defaultColor;
            }

            EditorGUILayout.EndVertical();
        }

        private void DrawInstrument(int index)
        {
            GUI.contentColor = instrColors[0];
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(20f);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Instrument", GUILayout.Width(100f));
            _muConfig.instrument[index].name =
                EditorGUILayout.TextField(_muConfig.instrument[index].name, GUILayout.Width(120f));
            _muConfig.instrument[index].type =
                (InstrumentType)EditorGUILayout.EnumPopup(_muConfig.instrument[index].type, GUILayout.Width(120f));
            EditorGUILayout.EndHorizontal();

            #region Generative settings

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Algorithm", GUILayout.Width(100f));
            _muConfig.instrument[index].algo =
                (GenAlgo)EditorGUILayout.EnumPopup(_muConfig.instrument[index].algo, GUILayout.Width(120f));
            EditorGUILayout.EndHorizontal();

            switch (_muConfig.instrument[index].algo)
            {
                case GenAlgo.Arpeggio:
                    DrawArpeggio(index);
                    break;
                case GenAlgo.Lsystem:
                    DrawLSystem(index);
                    break;
                case GenAlgo.Random:
                    break;
                case GenAlgo.Perlin:
                    break;
            }

            #endregion

            DrawRhythmSettings(index);

            #region Intensity labels

            //Draw labels
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(100f));
            EditorGUILayout.LabelField("min", GUILayout.Width(80f));
            EditorGUILayout.LabelField("max", GUILayout.Width(80f));
            EditorGUILayout.EndHorizontal();

            #endregion

            #region octave settings

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("OCTAVE", GUILayout.Width(100f));
            _muConfig.instrument[index].oct0.x =
                EditorGUILayout.IntField(_muConfig.instrument[index].oct0.x, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            _muConfig.instrument[index].oct0.y =
                EditorGUILayout.IntField(_muConfig.instrument[index].oct0.y, GUILayout.Width(40f));
            EditorGUILayout.EndHorizontal();

            #endregion

            EditorGUILayout.EndVertical();
            GUI.contentColor = defaultColor;
        }

        private void DrawPercussion()
        {
            GUI.contentColor = instrColors[0];

            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(20f);
            EditorGUILayout.LabelField("PERCUSSION SETTINGS");

            for (int i = 0; i < _muConfig.percussionParams.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Track " + (i + 1), GUILayout.Width(60f));
                EditorGUILayout.LabelField("Sample:", GUILayout.Width(60f));
                _muConfig.percussionSounds[i] =
                    (AudioClip)EditorGUILayout.ObjectField(_muConfig.percussionSounds[i], typeof(AudioClip), false,
                        GUILayout.Width(100f));
                EditorGUILayout.LabelField("Hits", GUILayout.Width(60f));
                _muConfig.percussionParams[i].Hits =
                    EditorGUILayout.IntField(_muConfig.percussionParams[i].Hits, GUILayout.Width(40f));
                EditorGUILayout.LabelField("Rotation", GUILayout.Width(60f));
                _muConfig.percussionParams[i].Rotation =
                    EditorGUILayout.IntField(_muConfig.percussionParams[i].Rotation, GUILayout.Width(40f));
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();

            GUI.contentColor = defaultColor;
        }

        private void DrawRandom(int index)
        {
            EditorGUILayout.BeginVertical();


            EditorGUILayout.EndVertical();
        }

        private void DrawArpeggio(int index)
        {
            EditorGUILayout.BeginVertical();
            _muConfig.arpModes[index] =
                (ClimbMode)EditorGUILayout.EnumPopup(_muConfig.arpModes[index], GUILayout.Width(100f));
            EditorGUILayout.EndVertical();
        }

        private void DrawLSystem(int index)
        {
            EditorGUILayout.BeginVertical();

            _muConfig.lsystems[index] =
                (Lsys)EditorGUILayout.ObjectField(_muConfig.lsystems[index],
                    typeof(Lsys), false, GUILayout.Width(100f));

            EditorGUILayout.EndVertical();
        }

        private void DrawPerlin(int index)
        {
        }


        private void DrawRhythmSettings(int index)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("RHYTHM SETTINGS");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Hits", GUILayout.Width(80f));
            _muConfig.rhythms[index].Hits =
                EditorGUILayout.IntField(_muConfig.rhythms[index].Hits, GUILayout.Width(40f));
            EditorGUILayout.LabelField("Rotation", GUILayout.Width(80f));
            _muConfig.rhythms[index].Rotation =
                EditorGUILayout.IntField(_muConfig.rhythms[index].Rotation, GUILayout.Width(40f));
            EditorGUILayout.LabelField("Step length", GUILayout.Width(80f));
            _muConfig.rhythms[index].StepLength =
                EditorGUILayout.IntField(_muConfig.rhythms[index].StepLength, GUILayout.Width(40f));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }
}