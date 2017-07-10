using UnityEngine;
using UnityEditor;

namespace UnityRefChecker
{
    public class SettingsWindow : EditorWindow
    {
        private bool checkOnCompilation;
        private LogType logSeverity;
        private bool colorfulLogs;

        private const string checkAfterCompilationInfo = "Checks all build scenes whenever Unity finishes compiling.";

        [MenuItem("Window/UnityRefChecker")]
        public static void ShowWindow() {
            GetWindow(typeof(SettingsWindow));
        }

        public void Awake() {
            checkOnCompilation = Settings.GetCheckOnCompilation();
            logSeverity = Settings.GetLogSeverity();
            colorfulLogs = Settings.GetColorfulLogs();
        }

        private void OnGUI() {
            GUILayout.Label("UnityRefChecker", EditorStyles.boldLabel);
            DrawDocumentationButton();

            GUILayout.Label("Commands", EditorStyles.boldLabel);
            DrawCommandButtons();

            GUILayout.Label("Settings", EditorStyles.boldLabel);
            DrawSettings();
        }

        private static void DrawDocumentationButton() {
            if (GUILayout.Button("Documentation")) {
                Application.OpenURL("https://github.com/haydenjameslee/unityrefchecker");
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
            DrawResetSettingsButton();
            DrawClearConsoleButton();
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
            bool toggleValue = EditorGUILayout.Toggle("Colorful logs", colorfulLogs);
            if (toggleValue != colorfulLogs) {
                Settings.SetColorfulLogs(toggleValue);
                colorfulLogs = toggleValue;
            }
        }

        private void DrawResetSettingsButton() {
            if (GUILayout.Button("Reset Settings")) {
                Settings.ClearSettings();
                Close();
            }
        }

        private void DrawClearConsoleButton() {
            if (GUILayout.Button("Clear Console")) {
                Commands.ClearConsole();
            }
        }
    }
}
