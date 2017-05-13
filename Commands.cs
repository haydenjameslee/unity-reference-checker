using System;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace RefChecker
{
    public class Commands
    {
        [DidReloadScripts]
        private static void RunAfterCompilation() {
            if (Settings.GetCheckOnCompilation()) {
                CheckBuildScenes();
            }
        }

        public static void CheckBuildScenes() {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

            string previouslyOpenScenePath = SceneManager.GetActiveScene().path;

            EditorBuildSettingsScene[] buildSettingsScenes = EditorBuildSettings.scenes;
            for (int i = 0; i < buildSettingsScenes.Length; i++) {
                EditorBuildSettingsScene settingsScene = buildSettingsScenes[i];

                string scenePath = settingsScene.path;
                //Debug.Log("SettingsScene: path=" + scenePath + " enabled=" + settingsScene.enabled);

                EditorSceneManager.OpenScene(scenePath);
                Scene scene = SceneManager.GetSceneByPath(scenePath);
                CheckScene(scene);
            }

            EditorSceneManager.OpenScene(previouslyOpenScenePath);
        }

        public static void CheckOpenScene() {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

            var scene = SceneManager.GetActiveScene();
            CheckScene(scene);
        }

        private static void CheckScene(Scene scene) {
            //Debug.Log("Checking scene= " + scene.name);
            var roots = scene.GetRootGameObjects();

            for (int i = 0; i < roots.Length; i++) {
                CheckRootGameObject(roots[i]);
            }
        }

        private static void CheckRootGameObject(GameObject go) {
            //Debug.Log("Checking Root GameObject=" + go.name);
            var components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++) {
                CheckComponent(components[i]);
            }
        }

        private static void CheckComponent(Component c) {
            //Debug.Log("Component=" + c.GetType().Name);

            // Ignore non-MonoBehaviours like Transform, Camera etc
            bool isBehaviour = c as MonoBehaviour;
            if (!isBehaviour) {
                return;
            }

            Type compType = c.GetType();
            var fields = compType.GetFields();
            for (int i = 0; i < fields.Length; i++) {
                var info = fields[i];
                object value = info.GetValue(c);

                //Debug.Log("Field=" + info.Name + " " + Value=" + value);
                if (value == null) {
                    string log = BuildLog(c, info);
                    Debug.logger.LogFormat(Settings.GetLogSeverity(), log);
                }
            }
        }

        private static string BuildLog(Component c, System.Reflection.FieldInfo info) {
            var log = new ColorfulLogBuilder();
            bool useColor = Settings.GetColorfulLogs();
            log.SetColorful(useColor);
            log.Append("RefChecker: Component ");
            log.StartColor();
            log.Append(c.GetType().Name);
            log.EndColor();
            log.Append(" has a null reference for field ");
            log.StartColor();
            log.Append(info.Name);
            log.EndColor();
            log.Append(" on GameObject ");
            log.StartColor();
            log.Append(c.gameObject.name);
            log.EndColor();
            return log.ToString();
        }
    }
}