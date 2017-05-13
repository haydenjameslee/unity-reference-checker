using UnityEngine;
using UnityEditor;
using System.Collections;

namespace RefChecker
{
    public class SettingsWindow : EditorWindow
    {
        private bool checkOnCompilation;
        private LogType logSeverity;
        private bool colorfulLogs;

        private const string checkAfterCompilationInfo = "Checks all build scenes whenever Unity finishes compiling.";

        [MenuItem("Window/RefChecker")]
        public static void ShowWindow() {
            GetWindow(typeof(SettingsWindow));
        }

        public void Awake() {
            checkOnCompilation = Settings.GetCheckOnCompilation();
            logSeverity = Settings.GetLogSeverity();
            colorfulLogs = Settings.GetColorfulLogs();
        }

        private void OnGUI() {
            GUILayout.Label("RefChecker", EditorStyles.boldLabel);
            DrawDocumentationButton();

            GUILayout.Label("Commands", EditorStyles.boldLabel);
            DrawCommandButtons();

            GUILayout.Label("Settings", EditorStyles.boldLabel);
            DrawSettings();
        }

        private static void DrawDocumentationButton() {
            if (GUILayout.Button("Documentation")) {
                Application.OpenURL("https://github.com/haydenjameslee/refchecker");
            }
        }

        private static void DrawCommandButtons() {
            if (GUILayout.Button("Check All Build Scenes")) {
                Commands.CheckBuildScenes();
            }
            if (GUILayout.Button("Check Open Scene")) {
                Commands.CheckOpenScene();
            }
        }

        private void DrawSettings() {
            DrawCheckOnCompilationToggle();
            DrawLogSeverityPopup();
            DrawColorfulLogsToggle();
            DrawClearSettingsButton();
        }

        private void DrawCheckOnCompilationToggle() {
            bool toggleValue = EditorGUILayout.Toggle("Check after compilation", checkOnCompilation);
            if (toggleValue) {
                EditorGUILayout.HelpBox(checkAfterCompilationInfo, MessageType.Info);
            }
            if (toggleValue != checkOnCompilation) {
                Settings.SetCheckOnCompilation(toggleValue);
                checkOnCompilation = toggleValue;
            }
        }

        private void DrawLogSeverityPopup() {
            LogType selectedLogSeverity = (LogType)EditorGUILayout.EnumPopup("Log type", logSeverity);
            if (selectedLogSeverity != logSeverity) {
                Settings.SetLogSeverity(selectedLogSeverity);
                logSeverity = selectedLogSeverity;
            }
        }

        private void DrawColorfulLogsToggle() {
            bool toggleValue = EditorGUILayout.Toggle("Print colorful logs", colorfulLogs);
            if (toggleValue != colorfulLogs) {
                Settings.SetColorfulLogs(toggleValue);
                colorfulLogs = toggleValue;
            }
        }

        private void DrawClearSettingsButton() {
            if (GUILayout.Button("Clear Settings")) {
                Settings.ClearSettings();
                Close();
            }
        }
    }
}
