using System.Collections;
using System.Collections.Generic;
using ProcMu.UnityScripts;
using UnityEngine;

namespace ProcMu.ScriptableObjects
{
    [CreateAssetMenu(fileName = "lsys_", menuName = "ProcMu/L-system configuration", order = 1)]
    public class Lsys : ScriptableObject
    {
        public string axiom;
        public List<LsysRule> ruleset;

        private string sentence = "";

        private string IterateSentence(string oldsentence)
        {
            if (string.IsNullOrEmpty(sentence)) sentence = axiom;
            string s = "";

            for (int i = 0; i < oldsentence.Length; i++)
            {
                foreach (var rule in ruleset)
                {
                    if (oldsentence[i] != rule.a) continue;
                    s += ruleset[i].b;
                }
            }

            Debug.Log(s);
            return s;
        }
    }
}