using SorceressSpell.LibrarIoh.Localization;
using System.Collections.Generic;
using UnityEngine;

namespace SorceressSpell.LibrarIoh.Unity.Localization
{
    [CreateAssetMenu(fileName = "LanguageListScriptableObject", menuName = "SorceressSpell.LibrarIoh.Unity.Localization/LanguageListScriptableObject")]
    public class LanguageListScriptableObject : ScriptableObject
    {
        #region Fields

        public List<TextAsset> XmlLanguages;
        public List<LanguageFileScriptableObject> ScriptableObjectLanguages;

        #endregion Fields


    }
}
