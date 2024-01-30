using System.Collections;
using System.Collections.Generic;
using ProcMu.UnityScripts;
using UnityEditor;
using UnityEngine;

namespace ProcMu.ScriptableObjects.Editor
{
    [CustomEditor(typeof(Lsys))]
    public class LsysEditor : UnityEditor.Editor
    {
        private Lsys _lsys;

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            _lsys = (Lsys)target;

            Draw();

            if (EditorGUI.EndChangeCheck()) EditorUtility.SetDirty(_lsys);
        }

        private void Draw()
        {
            EditorGUILayout.BeginVertical();


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Axiom", GUILayout.Width(80f));
            _lsys.axiom = EditorGUILayout.TextField(_lsys.axiom, GUILayout.Width(100f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            if (GUILayout.Button("Add rule", GUILayout.Width(100f))) AddRule();

            EditorGUILayout.Space();

            for (int i = 0; i < _lsys.ruleset.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Rule " + i, GUILayout.Width(80f));
                _lsys.ruleset[i] = new LsysRule()
                {
                    a = EditorGUILayout.TextField("", _lsys.ruleset[i].a.ToString(), GUILayout.Width(140f))[0],
                    b = EditorGUILayout.TextField("", _lsys.ruleset[i].b, GUILayout.Width(140f))
                };
                if (GUILayout.Button("x", GUILayout.Width(20f))) RemoveRule(_lsys.ruleset[i]);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        private void AddRule()
        {
            _lsys.ruleset.Add(new LsysRule()
            {
                a = ("" + _lsys.ruleset.Count)[0],
                b = ""
            });
        }

        private void RemoveRule(LsysRule rule)
        {
            _lsys.ruleset.Remove(rule);
        }
    }
}