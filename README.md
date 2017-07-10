# UnityRefChecker

Unassigned reference warnings at compile time, across scenes.

UnityRefChecker helps you avoid null references in MonoBehaviours by looking through all MonoBehaviour references in a scene and warning you in Unity's console if a reference has not been assigned.

Fields that cause a log:
- Unassigned public MonoBehaviour fields that do not have [IgnoreRefChecker] or [HideInInspector]
- Unassigned private MonoBehaviour fields that do not have [IgnoreRefChecker] and do have [SerializeField]


## Example

Here are some example logs:

![Example logs](http://i.imgur.com/qMypSw9.png "Example logs")


## Getting Started

1. Open your Unity project

2. Clone this project into the `Assets/` folder

3. Add the `[IgnoreRefChecker]` attribute in front of any members that you wish to keep unassigned

4. In Unity, go to `Window -> UnityRefChecker` to run commands and configure settings


## Commands 

- `Check All Build Scenes` - Checks all MonoBehaviour references in all scenes listed in the Unity Build Settings

- `Check Open Scene` - Checks all MonoBehaviour references in the currently active scene


## Attributes

- `IgnoreRefChecker` - Add this attribute to fields that you wish to keep unassigned. UnityRefChecker will not warn you about these fields


## Settings 

| Property | Description | Default Value |
| -------- | ----------- | ------------- |
| Check after compilation   | Runs the `Check All Build Scenes` command every time Unity finishes compiling | false |
| Log type  | The severity of the log using Unity's LogType (Error, Log, Warning) | Error |
| Colorful logs  | Adds color to the Unity console logs to highlight important info | true |


## TODO

- Add log saying "all clear" when you click run and there are no errors, don't include for on compile run?
- Make video tutorial showing the problem this solves
- Include prefabs in checks
- Add to Unity Asset Store


## Testing

To test UnityRefChecker create a new Unity project, clone UnityRefChecker and set up a scene like this:

![Test scene](http://i.imgur.com/8TxyP84.png "Example test scene structure")

Then open the UnityRefChecker Window and run commands. The RefCheckerTestComponent has the expected results as comments.
