using SorceressSpell.LibrarIoh.Core;
using SorceressSpell.LibrarIoh.IO;
using SorceressSpell.LibrarIoh.Localization; 
using SorceressSpell.LibrarIoh.Unity.IO.Editor;
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace SorceressSpell.LibrarIoh.Unity.Localization.Editor
{
    [CustomEditor(typeof(LanguageFileScriptableObject))]
    public class LanguageFileScriptableObjectEditor : UnityEditor.Editor
    {
        private string _lastImportXmlMessage;
        private MessageType _lastImportXmlMessageType;
        private string _lastExportXmlMessage;
        private MessageType _lastExportXmlMessageType;
        private LanguageFileScriptableObject _editorTarget;
        private int _xmlObjectPickerId;

        #region Methods

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AppendImportControls();

            AppendExportControls();
        }

        private void AppendExportControls()
        {
            EditorGUILayout.LabelField(LanguageFileScriptableObjectEditorStrings.LabelHeaderExport, EditorStyles.boldLabel);

            if (GUILayout.Button(LanguageFileScriptableObjectEditorStrings.LabelButtonExportXml))
            {
                string finalPath = PathOperations.RemoveExtension(AssetPathOperations.GetAssetFullPath(_editorTarget)) + ".xml";

                Language language = new Language(_editorTarget);
                XmlDocument xmlDocument = language.Save();
                xmlDocument.Save(finalPath);

                _lastExportXmlMessage = string.Format(LanguageFileScriptableObjectEditorStrings.MessageInfoExportedToFile, DateTime.Now.GetHumanDateTimeString(), finalPath);
                _lastExportXmlMessageType = MessageType.Info;
            }

            if (!string.IsNullOrWhiteSpace(_lastExportXmlMessage))
            {
                EditorGUILayout.HelpBox(_lastExportXmlMessage, _lastExportXmlMessageType);
            }
        }

        private void AppendImportControls()
        {
            EditorGUILayout.LabelField(LanguageFileScriptableObjectEditorStrings.LabelHeaderImport, EditorStyles.boldLabel);

            if (GUILayout.Button(LanguageFileScriptableObjectEditorStrings.LabelButtonImportXml))
            {
                _xmlObjectPickerId = EditorGUIUtility.GetControlID(FocusType.Passive) + 1;
                EditorGUIUtility.ShowObjectPicker<TextAsset>(null, false, "", _xmlObjectPickerId);
            }

            string commandName = Event.current.commandName;
            if (commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == _xmlObjectPickerId)
            {
                //TextAsset currentLanguageFile = (TextAsset)EditorGUIUtility.GetObjectPickerObject();
                Repaint();
            }
            else if (commandName == "ObjectSelectorClosed" && EditorGUIUtility.GetObjectPickerControlID() == _xmlObjectPickerId)
            {
                TextAsset replacementLanguageFile = (TextAsset)EditorGUIUtility.GetObjectPickerObject();

                if (replacementLanguageFile != null &&
                    EditorUtility.DisplayDialog(
                        LanguageFileScriptableObjectEditorStrings.DisplayDialogueImportXmlTitle,
                        LanguageFileScriptableObjectEditorStrings.DisplayDialogueImportXmlMessage,
                        LanguageFileScriptableObjectEditorStrings.DisplayDialogueImportXmlOk,
                        LanguageFileScriptableObjectEditorStrings.DisplayDialogueImportXmlCancel))
                {
                    Undo.RecordObject(_editorTarget, LanguageFileScriptableObjectEditorStrings.ActionImportXml);

                    Dictionary<string, string> languageStrings = new Dictionary<string, string>();

                    Language language = new Language(new LanguageFileXml(replacementLanguageFile.text));
                    language.LoadStringsTo(languageStrings);

                    _editorTarget.Strings.Clear();

                    foreach (KeyValuePair<string, string> entry in languageStrings)
                    {
                        _editorTarget.AddString(entry.Key, entry.Value);
                    }

                    _lastImportXmlMessage = String.Format(LanguageFileScriptableObjectEditorStrings.MessageInfoImportedFromFile, DateTime.Now.GetHumanDateTimeString(), AssetDatabase.GetAssetPath(replacementLanguageFile));
                    _lastImportXmlMessageType = MessageType.Info;
                }

                _xmlObjectPickerId = -1;
            }

            if (!string.IsNullOrWhiteSpace(_lastImportXmlMessage))
            {
                EditorGUILayout.HelpBox(_lastImportXmlMessage, _lastImportXmlMessageType);
            }
        }

        private void OnEnable()
        {
            _editorTarget = (LanguageFileScriptableObject)target;

            _lastImportXmlMessage = "";
            _lastImportXmlMessageType = MessageType.Info;

            _lastExportXmlMessage = "";
            _lastExportXmlMessageType = MessageType.Info;

            _xmlObjectPickerId = -1;
        }
    }

    #endregion Methods
}
