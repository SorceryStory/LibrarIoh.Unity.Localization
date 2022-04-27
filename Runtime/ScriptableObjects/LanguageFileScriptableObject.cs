using SorceressSpell.LibrarIoh.Localization;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace SorceressSpell.LibrarIoh.Unity.Localization
{
    [CreateAssetMenu(fileName = "LanguageFileScriptableObject", menuName = "SorceressSpell.LibrarIoh.Unity.Localization/LanguageFileScriptableObject")]
    public class LanguageFileScriptableObject : ScriptableObject, ILanguageFile
    {
        #region Fields

        [Header("Basic Info")]
        public string Tag;
        public string Name;
        public string NativeName;

        [Header("Strings")]
        public List<LanguageStringEntry> Strings;

        #endregion Fields

        #region Methods

        public void AddString(string stringId, string stringValue)
        {
            Strings.Add(new LanguageStringEntry() { Id = stringId, String = stringValue });
        }

        public void LoadBasicInfo(ref string tag, ref string name, ref string nativeName)
        {
            tag = Tag;
            name = Name;
            nativeName = NativeName;
        }

        public void LoadStringsTo(Dictionary<string, string> strings)
        {
            foreach(LanguageStringEntry entry in Strings)
            {
                strings[entry.Id] = entry.String;
            }
        }

        public void SaveStringsXml(XmlDocument xmlDocument, XmlElement xmlElement)
        {
            foreach (LanguageStringEntry entry in Strings)
            {
                LanguageFileOperations.SaveStringXml(xmlDocument, xmlElement, entry.Id, entry.String);
            }
        }

        #endregion Methods
    }
}
