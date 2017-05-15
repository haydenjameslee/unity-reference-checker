# UnityRefChecker

Have you ever added a reference to a MonoBehaviour in one scene but forgotten to add it in another?

UnityRefChecker is a Unity3D plugin that helps you avoid null references in MonoBehaviours by looking through all MonoBehaviour references in a scene and warning you in Unity's console if a reference has not been assigned.

Fields that cause a log:
- Unassigned public MonoBehaviour fields that do not have [IgnoreRefChecker] or [HideInInspector]
- Unassigned private MonoBehaviour fields that do not have [IgnoreRefChecker] and do have [SerializeField]


## Getting Started

1. Open your Unity project

2. Clone this project into the `Assets/` folder

3. Add the `[IgnoreRefChecker]` attribute in front of any members that you wish to keep unassigned

4. In Unity, go to `Window -> RefChecker` to run commands and configure settings


## Commands 

- `Check All Build Scenes` - Checks all MonoBehaviour references in all scenes listed in the Unity Build Settings

- `Check Open Scene` - Checks all MonoBehaviour references in the currently active scene


## Attributes

- `IgnoreRefChecker` - Add this attribute to fields that you wish to keep unassigned. RefChecker will not warn you about these fields


## Settings 

| Property | Description | Default Value |
| -------- | ----------- | ------------- |
| Check after compilation   | Runs the `Check All Build Scenes` command every time Unity finishes compiling | false |
| Log type  | The severity of the log using Unity's LogType (Error, Log, Warning) | Error |
| Colorful logs  | Adds color to the Unity console logs to highlight important info | false |


## TODO

- Include prefabs in checks


## Testing

To test RefChecker create a new Unity project, clone RefChecker and set up a scene like this:

![Example test scene](http://i.imgur.com/8TxyP84.png "Example test scene structure")

Then open the RefChecker Window and run commands. The RefCheckerTestComponent has the expected results as comments.