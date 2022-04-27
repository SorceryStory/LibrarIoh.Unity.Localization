using SorceressSpell.LibrarIoh.Localization;
using UnityEngine;

namespace SorceressSpell.LibrarIoh.Unity.Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        #region Fields

        public LanguageListScriptableObject LanguageList;
        private LocalizationController _languageController;

        #endregion Fields

        #region Properties

        public Language CurrentLanguage
        {
            get { return _languageController.CurrentLanguage; }
        }

        #endregion Properties

        #region Delegates

        public delegate void OnLanguageChangedEvent();

        #endregion Delegates

        #region Events

        public event OnLanguageChangedEvent OnLanguageChanged;

        #endregion Events

        #region Methods

        public void ChangeLanguage(string languageTag)
        {
            _languageController.SetLanguage(languageTag);
        }

        public string GetGenderedString(string stringId, Gender gender)
        {
            return _languageController.GetGenderedString(stringId, gender, stringId);
        }

        public string GetGenderedString(string stringId, Gender gender, string defaultValue)
        {
            return _languageController.GetGenderedString(stringId, gender, defaultValue);
        }

        public string GetString(string stringId)
        {
            return _languageController.GetString(stringId, stringId);
        }

        public string GetString(string stringId, string defaultValue)
        {
            return _languageController.GetString(stringId, defaultValue);
        }

        private void Awake()
        {
            _languageController = new LocalizationController();

            foreach (TextAsset xmlLanguage in LanguageList.XmlLanguages)
            {
                _languageController.AddLanguage(new LanguageFileXml(xmlLanguage.text));
            }

            foreach (LanguageFileScriptableObject scriptableObjectLanguage in LanguageList.ScriptableObjectLanguages)
            {
                _languageController.AddLanguage(scriptableObjectLanguage);
            }

            _languageController.OnLanguageChanged += OnControllerLanguageChanged;

            _languageController.SetLanguage("en-GB");
        }

        private void OnControllerLanguageChanged(LanguageChangedEventArgs lcea)
        {
            OnLanguageChanged?.Invoke();
        }

        #endregion Methods
    }
}
