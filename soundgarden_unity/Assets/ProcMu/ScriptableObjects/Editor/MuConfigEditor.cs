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
            _muConfig = (MuConfig)target;

            DrawGeneralSettings();
            DrawActiveBars();
            DrawDrone();
            //DrawChords();
            //DrawSnhMel();
            DrawBass();

            DrawPercussion();

            EditorUtility.SetDirty(_muConfig); //TODO SET ONLY DIRTY WHEN A CHANGE WAS MADE
        }

        private void DrawGeneralSettings()
        {
            GUI.backgroundColor = defaultColor;
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("General settings");
            _muConfig.bpm = EditorGUILayout.FloatField("BPM", _muConfig.bpm);
            _muConfig.Scale = (ScaleType)EditorGUILayout.EnumPopup(_muConfig.Scale);
            _muConfig.barLength = EditorGUILayout.IntField("Bar length", _muConfig.barLength);
            _muConfig.bars = EditorGUILayout.IntField("Bars", _muConfig.bars);
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
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("Intensity = 0 settings", GUILayout.Width(160f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("Intensity = 1 settings", GUILayout.Width(160f));
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
                    _muConfig.activeBars0[index + i] =
                        EditorGUILayout.Toggle("", _muConfig.activeBars0[index + i], GUILayout.Width(20f));
                }

                EditorGUILayout.LabelField("", GUILayout.Width(80f));

                //Draw active bars for intensity = 1
                for (int i = 0; i < 4; i++)
                {
                    _muConfig.activeBars1[index + i] =
                        EditorGUILayout.Toggle("", _muConfig.activeBars1[index + i], GUILayout.Width(20f));
                }

                EditorGUILayout.EndHorizontal();
                GUI.color = defaultColor;
            }

            EditorGUILayout.EndVertical();
        }

        #region LEGACY

        /*
        private void DrawEucRth()
        {
            GUI.contentColor = instrColors[0];
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(20f);
            EditorGUILayout.LabelField("Euclidean Rhythm settings");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(80f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("Intensity = 0 settings", GUILayout.Width(160f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("Intensity = 1 settings", GUILayout.Width(160f));
            EditorGUILayout.EndHorizontal();

            //Draw labels
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Sample/trigger", GUILayout.Width(100f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("min pulses", GUILayout.Width(80f));
            EditorGUILayout.LabelField("max pulses", GUILayout.Width(80f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("min pulses", GUILayout.Width(80f));
            EditorGUILayout.LabelField("max pulses", GUILayout.Width(80f));
            EditorGUILayout.EndHorizontal();

            //Draw rhythm layers
            for (int i = 0; i < 4; i++)
            {
                EditorGUILayout.BeginHorizontal();
                _muConfig.eucrth_sampleSelection[i] =
                    EditorGUILayout.Popup(_muConfig.eucrth_sampleSelection[i], _sampledb.audioClipNames,
                        GUILayout.Width(100f));
                EditorGUILayout.LabelField("", GUILayout.Width(10f));

                _muConfig.eucrth_minImpulses0[i] =
                    EditorGUILayout.IntField(_muConfig.eucrth_minImpulses0[i], GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(40f));
                _muConfig.eucrth_maxImpulses0[i] =
                    EditorGUILayout.IntField(_muConfig.eucrth_maxImpulses0[i], GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(10f));
                _muConfig.eucrth_minImpulses1[i] =
                    EditorGUILayout.IntField(_muConfig.eucrth_minImpulses1[i], GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(40f));
                _muConfig.eucrth_maxImpulses1[i] =
                    EditorGUILayout.IntField(_muConfig.eucrth_maxImpulses1[i], GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(40f));


                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            GUI.contentColor = defaultColor;
        }

        private void DrawChords()
        {
            GUI.contentColor = instrColors[1];
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(20f);

            EditorGUILayout.LabelField("Chords Settings");

            #region Chords dynamic settings

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(100f));
            EditorGUILayout.LabelField("Intensity = 0 settings", GUILayout.Width(160f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("Intensity = 1 settings", GUILayout.Width(160f));
            EditorGUILayout.EndHorizontal();

            //Draw labels
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(100f));
            EditorGUILayout.LabelField("min", GUILayout.Width(80f));
            EditorGUILayout.LabelField("max", GUILayout.Width(80f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("min", GUILayout.Width(80f));
            EditorGUILayout.LabelField("max", GUILayout.Width(80f));
            EditorGUILayout.EndHorizontal();

            #region trigger settings

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("TRIGGER", GUILayout.Width(100f));
            _muConfig.chords_minImpulses0 =
                EditorGUILayout.IntField(_muConfig.chords_minImpulses0, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            _muConfig.chords_maxImpulses0 =
                EditorGUILayout.IntField(_muConfig.chords_maxImpulses0, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            _muConfig.chords_minImpulses1 =
                EditorGUILayout.IntField(_muConfig.chords_minImpulses1, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            _muConfig.chords_maxImpulses1 =
                EditorGUILayout.IntField(_muConfig.chords_maxImpulses1, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            EditorGUILayout.EndHorizontal();

            #endregion

            #region octave settings

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("OCTAVE", GUILayout.Width(100f));
            _muConfig.chords_minOct0 = EditorGUILayout.IntField(_muConfig.chords_minOct0, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            _muConfig.chords_maxOct0 = EditorGUILayout.IntField(_muConfig.chords_maxOct0, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            _muConfig.chords_minOct1 = EditorGUILayout.IntField(_muConfig.chords_minOct1, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            _muConfig.chords_maxOct1 = EditorGUILayout.IntField(_muConfig.chords_maxOct1, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            EditorGUILayout.EndHorizontal();

            #endregion

            #endregion

            EditorGUILayout.Space(20f);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Chord Mode", GUILayout.Width(100f));
            _muConfig.chordMode = (ChordMode) EditorGUILayout.EnumPopup(_muConfig.chordMode, GUILayout.Width(120f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20f);
            _muConfig.chords_synthconfig = (GSynthConfig) EditorGUILayout.ObjectField("Synth config",
                _muConfig.chords_synthconfig, typeof(GSynthConfig), false);
            EditorGUILayout.EndVertical();
            GUI.contentColor = defaultColor;
        }

        private void DrawSnhMel()
        {
            GUI.contentColor = instrColors[2];
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(20f);
            EditorGUILayout.LabelField("Sample And Hold Melody Settings");

            #region Snhmel dynamic settings

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(100f));
            EditorGUILayout.LabelField("Intensity = 0 settings", GUILayout.Width(160f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("Intensity = 1 settings", GUILayout.Width(160f));
            EditorGUILayout.EndHorizontal();

            //Draw labels
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(100f));
            EditorGUILayout.LabelField("min", GUILayout.Width(80f));
            EditorGUILayout.LabelField("max", GUILayout.Width(80f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("min", GUILayout.Width(80f));
            EditorGUILayout.LabelField("max", GUILayout.Width(80f));
            EditorGUILayout.EndHorizontal();

            #region trigger settings

            if (_muConfig.snhmel_melodymode == MelodyMode.Retriggered)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("TRIGGER", GUILayout.Width(100f));
                _muConfig.snhmel_minImpulses0 =
                    EditorGUILayout.IntField(_muConfig.snhmel_minImpulses0, GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(40f));
                _muConfig.snhmel_maxImpulses0 =
                    EditorGUILayout.IntField(_muConfig.snhmel_maxImpulses0, GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(10f));
                _muConfig.snhmel_minImpulses1 =
                    EditorGUILayout.IntField(_muConfig.snhmel_minImpulses1, GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(40f));
                _muConfig.snhmel_maxImpulses1 =
                    EditorGUILayout.IntField(_muConfig.snhmel_maxImpulses1, GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(40f));
                EditorGUILayout.EndHorizontal();
            }

            #endregion

            #region occurence settings

            if (_muConfig.snhmel_melodymode == MelodyMode.Continuous)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("OCCURENCE", GUILayout.Width(100f));
                _muConfig.snhmel_minOccurence0 =
                    EditorGUILayout.FloatField(_muConfig.snhmel_minOccurence0, GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(40f));
                _muConfig.snhmel_maxOccurence0 =
                    EditorGUILayout.FloatField(_muConfig.snhmel_maxOccurence0, GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(10f));
                _muConfig.snhmel_minOccurence1 =
                    EditorGUILayout.FloatField(_muConfig.snhmel_minOccurence1, GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(40f));
                _muConfig.snhmel_maxOccurence1 =
                    EditorGUILayout.FloatField(_muConfig.snhmel_maxOccurence1, GUILayout.Width(40f));
                EditorGUILayout.LabelField("", GUILayout.Width(40f));
                EditorGUILayout.EndHorizontal();
            }

            #endregion

            #region octave settings

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("OCTAVE", GUILayout.Width(100f));
            _muConfig.snhmel_minOct0 = EditorGUILayout.IntField(_muConfig.snhmel_minOct0, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            _muConfig.snhmel_maxOct0 = EditorGUILayout.IntField(_muConfig.snhmel_maxOct0, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            _muConfig.snhmel_minOct1 = EditorGUILayout.IntField(_muConfig.snhmel_minOct1, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            _muConfig.snhmel_maxOct1 = EditorGUILayout.IntField(_muConfig.snhmel_maxOct1, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            EditorGUILayout.EndHorizontal();

            #endregion

            #endregion

            EditorGUILayout.Space(20f);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Melody curve", GUILayout.Width(100f));
            _muConfig.snhmel_melodycurve =
                (MelodyCurve) EditorGUILayout.EnumPopup(_muConfig.snhmel_melodycurve, GUILayout.Width(120f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Melody mode", GUILayout.Width(100f));
            _muConfig.snhmel_melodymode =
                (MelodyMode) EditorGUILayout.EnumPopup(_muConfig.snhmel_melodymode, GUILayout.Width(120f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20f);
            _muConfig.snhmel_synthconfig = (GSynthConfig) EditorGUILayout.ObjectField("Synth config",
                _muConfig.snhmel_synthconfig, typeof(GSynthConfig), false);

            EditorGUILayout.EndVertical();
            GUI.contentColor = defaultColor;
        }
*/

        #endregion

        private void DrawDrone()
        {
            GUI.contentColor = instrColors[0];
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(20f);
            EditorGUILayout.LabelField("DRONE SETTINGS");

            #region Generative settings

            EditorGUILayout.BeginHorizontal();
            _muConfig.droneAlgo = (GenAlgo)EditorGUILayout.EnumPopup(_muConfig.droneAlgo);
            EditorGUILayout.EndHorizontal();

            #endregion

            #region Intensity labels

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(100f));
            EditorGUILayout.LabelField("Intensity = 0 settings", GUILayout.Width(160f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("Intensity = 1 settings", GUILayout.Width(160f));
            EditorGUILayout.EndHorizontal();

            //Draw labels
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(100f));
            EditorGUILayout.LabelField("min", GUILayout.Width(80f));
            EditorGUILayout.LabelField("max", GUILayout.Width(80f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("min", GUILayout.Width(80f));
            EditorGUILayout.LabelField("max", GUILayout.Width(80f));
            EditorGUILayout.EndHorizontal();

            #endregion

            #region octave settings

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("OCTAVE", GUILayout.Width(100f));
            _muConfig.drone_Oct0.x = EditorGUILayout.IntField(_muConfig.drone_Oct0.x, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            _muConfig.drone_Oct0.y = EditorGUILayout.IntField(_muConfig.drone_Oct0.y, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            _muConfig.drone_Oct1.x = EditorGUILayout.IntField(_muConfig.drone_Oct1.x, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            _muConfig.drone_Oct1.y = EditorGUILayout.IntField(_muConfig.drone_Oct0.y, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            EditorGUILayout.EndHorizontal();

            #endregion

            EditorGUILayout.EndVertical();
            GUI.contentColor = defaultColor;
        }

        private void DrawBass()
        {
            GUI.contentColor = instrColors[3];
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(20f);
            EditorGUILayout.LabelField("BASS SETTINGS");

            #region Snhmel dynamic settings

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(100f));
            EditorGUILayout.LabelField("Intensity = 0 settings", GUILayout.Width(160f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("Intensity = 1 settings", GUILayout.Width(160f));
            EditorGUILayout.EndHorizontal();

            //Draw labels
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(100f));
            EditorGUILayout.LabelField("min", GUILayout.Width(80f));
            EditorGUILayout.LabelField("max", GUILayout.Width(80f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            EditorGUILayout.LabelField("min", GUILayout.Width(80f));
            EditorGUILayout.LabelField("max", GUILayout.Width(80f));
            EditorGUILayout.EndHorizontal();

            #region octave settings

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("OCTAVE", GUILayout.Width(100f));
            _muConfig.bass_minOct0 = EditorGUILayout.IntField(_muConfig.bass_minOct0, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            _muConfig.bass_maxOct0 = EditorGUILayout.IntField(_muConfig.bass_maxOct0, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(10f));
            _muConfig.bass_minOct1 = EditorGUILayout.IntField(_muConfig.bass_minOct1, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            _muConfig.bass_maxOct1 = EditorGUILayout.IntField(_muConfig.bass_maxOct1, GUILayout.Width(40f));
            EditorGUILayout.LabelField("", GUILayout.Width(40f));
            EditorGUILayout.EndHorizontal();

            #endregion

            #endregion

            EditorGUILayout.Space(20f);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Melody curve", GUILayout.Width(100f));
            //_muConfig.bass_melodycurve = (MelodyCurve) EditorGUILayout.EnumPopup(_muConfig.bass_melodycurve, GUILayout.Width(120f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Melody mode", GUILayout.Width(100f));
            //_muConfig.bass_melodymode =(MelodyMode) EditorGUILayout.EnumPopup(_muConfig.bass_melodymode, GUILayout.Width(120f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20f);
            //_muConfig.bass_synthconfig = (GSynthConfig) EditorGUILayout.ObjectField("Synth config",_muConfig.bass_synthconfig, typeof(GSynthConfig), false);

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
                    (AudioClip)EditorGUILayout.ObjectField(_muConfig.percussionSounds[i], typeof(AudioClip), false,GUILayout.Width(100f));
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
    }
}