using System;
using System.Collections.Generic;
using UnityEngine;

namespace RefChecker
{
    public class Settings
    {
        public static bool GetCheckOnCompilation() {
            return GetPlayerPrefsBool(Keys.checkOnCompilation, false);
        }

        public static void SetCheckOnCompilation(bool check) {
            SetPlayerPrefsBool(Keys.checkOnCompilation, check);
        }

        public static LogType GetLogSeverity() {
            string savedStr = PlayerPrefs.GetString(Keys.logSeverity, "Error");
            switch (savedStr) {
                case "Error":
                    return LogType.Error;
                case "Assert":
                    return LogType.Assert;
                case "Warning":
                    return LogType.Warning;
                case "Log":
                    return LogType.Log;
                case "Exception":
                    return LogType.Exception;
                default:
                    ClearSettings();
                    throw new IndexOutOfRangeException();
            }
        }

        public static void SetLogSeverity(LogType type) {
            string typeStr = type.ToString();
            PlayerPrefs.SetString(Keys.logSeverity, typeStr);
        }

        public static bool GetColorfulLogs() {
            return GetPlayerPrefsBool(Keys.colorfulLogs, true);
        }

        public static void SetColorfulLogs(bool colorful) {
            SetPlayerPrefsBool(Keys.colorfulLogs, colorful);
        }

        public static void ClearSettings() {
            PlayerPrefs.DeleteKey(Keys.checkOnCompilation);
            PlayerPrefs.DeleteKey(Keys.logSeverity);
            PlayerPrefs.DeleteKey(Keys.colorfulLogs);
        }

        private static bool GetPlayerPrefsBool(string key, bool defaultValue) {
            int defaultValueInt = defaultValue ? 1 : 0;
            return PlayerPrefs.GetInt(key, defaultValueInt) == 1;
        }

        private static void SetPlayerPrefsBool(string key, bool value) {
            int valueInt = value ? 1 : 0;
            PlayerPrefs.SetInt(key, valueInt);
        }

        private static class Keys
        {
            private static string refcheckerPretext = "RefChecker:";
            public static string checkOnCompilation = refcheckerPretext + "checkOnCompilation";
            public static string logSeverity = refcheckerPretext + "logSeverity";
            public static string colorfulLogs = refcheckerPretext + "colorfulLogs";
        }
    }
}
