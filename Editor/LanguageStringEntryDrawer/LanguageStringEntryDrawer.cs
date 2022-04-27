using UnityEditor;
using UnityEngine;

namespace SorceressSpell.LibrarIoh.Unity.Localization.Editor
{
    [CustomPropertyDrawer(typeof(LanguageStringEntry))]
    public class LanguageStringEntryDrawer : PropertyDrawer
    {
        #region Methods

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            float IdWidthSpacing = LanguageStringEntryDrawerConstants.IdWidth + LanguageStringEntryDrawerConstants.Spacing;

            Rect idRect = new Rect(position.x, position.y, LanguageStringEntryDrawerConstants.IdWidth, position.height);
            Rect stringRect = new Rect(position.x + IdWidthSpacing, position.y, position.width - IdWidthSpacing, position.height);

            EditorGUI.PropertyField(idRect, property.FindPropertyRelative("Id"), GUIContent.none);
            EditorGUI.PropertyField(stringRect, property.FindPropertyRelative("String"), GUIContent.none);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        #endregion Methods
    }
}
