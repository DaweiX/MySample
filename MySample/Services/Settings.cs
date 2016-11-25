using System;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace MySample.Services
{
    class SettingsService
    {
        private static SettingsService instance;
        public static SettingsService Instance
        {
            get
            {
                if (instance == null) 
                    instance=new SettingsService();
                return instance;
            }
        }
        const string _isNight = "toast-on-app-events";
        IPropertySet settings = ApplicationData.Current.RoamingSettings.Values;
        public bool IsNight
        {
            get
            {
                object setting;
                if (settings.TryGetValue(_isNight, out setting))
                    return (bool)setting;
                return true;
            }
            set
            {
                settings[_isNight] = value;
            }
        }
    }
}