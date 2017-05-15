using UnityEngine;
using RefCheckerExternal;

public class RefCheckerTestComponent : MonoBehaviour
{
    public int exampleInt;
    [IgnoreRefChecker] public MonoBehaviour exampleWithTag;
    public MonoBehaviour withoutReferenceWithoutTag; // Should print log
    public MonoBehaviour withoutReferenceWithoutTag2; // Should print log
    public MonoBehaviour withReference;

    private MonoBehaviour privateNonSerializable;
    [SerializeField] private MonoBehaviour privateSerializable; // Should print log

    [HideInInspector] public MonoBehaviour hiddenInInspector;
}
