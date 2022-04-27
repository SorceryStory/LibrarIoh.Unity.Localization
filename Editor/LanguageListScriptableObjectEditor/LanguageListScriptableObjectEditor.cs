using SorceressSpell.LibrarIoh.Localization;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SorceressSpell.LibrarIoh.Unity.Localization.Editor
{
    [CustomEditor(typeof(LanguageListScriptableObject))]
    public class LanguageListScriptableObjectEditor : UnityEditor.Editor
    {
        private LanguageListScriptableObject _editorTarget;

        #region Methods

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CheckForRepeatedTags();
        }

        private void CheckForRepeatedTags()
        {
            List<string> xmlTags = new List<string>();
            List<string> scriptableObjectTags = new List<string>();

            Language currentLanguage;

            foreach (TextAsset languageTextAsset in _editorTarget.XmlLanguages)
            {
                currentLanguage = new Language(new LanguageFileXml(languageTextAsset.text));

                xmlTags.Add(currentLanguage.Tag);
            }

            foreach (LanguageFileScriptableObject languageScriptableObject in _editorTarget.ScriptableObjectLanguages)
            {
                scriptableObjectTags.Add(languageScriptableObject.Tag);
            }

            bool hasRepeatedTags = false;
            string repeatedTag = "";

            // Xml Files
            for (int i = 0; !hasRepeatedTags && i < xmlTags.Count - 1; ++i)
            {
                for (int j = 0; !hasRepeatedTags && j < xmlTags.Count; ++j)
                {
                    if (xmlTags[i] == xmlTags[j])
                    {
                        hasRepeatedTags = true;
                        repeatedTag = xmlTags[i];
                    }
                }
            }

            // ScriptableObjects
            for (int i = 0; !hasRepeatedTags && i < scriptableObjectTags.Count - 1; ++i)
            {
                for (int j = 0; !hasRepeatedTags && j < scriptableObjectTags.Count; ++j)
                {
                    if (scriptableObjectTags[i] == scriptableObjectTags[j])
                    {
                        hasRepeatedTags = true;
                        repeatedTag = scriptableObjectTags[i];
                    }
                }
            }

            // Xml Files <> ScriptableObjects
            for (int i = 0; !hasRepeatedTags && i < xmlTags.Count; ++i)
            {
                for (int j = 0; !hasRepeatedTags && j < scriptableObjectTags.Count; ++j)
                {
                    if (xmlTags[i] == scriptableObjectTags[j])
                    {
                        hasRepeatedTags = true;
                        repeatedTag = xmlTags[i];
                    }
                }
            }

            if (hasRepeatedTags)
            {
                EditorGUILayout.HelpBox(string.Format(LanguageListScriptableObjectEditorStrings.MessageWarningMultipleFilesSameTag, repeatedTag), MessageType.Warning);
            }
        }

        private void OnEnable()
        {
            _editorTarget = (LanguageListScriptableObject)target;
        }
    }

    #endregion Methods
}
