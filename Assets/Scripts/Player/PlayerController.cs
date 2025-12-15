using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Player))]
public abstract class PlayerController<T> : PlayerComponent
{
    [BoxGroup("Settings"), SerializeField] protected bool isActiveOnStart = true;

    protected PlayerInputActions playerInputActions;

    [SerializeField, BoxGroup("Reference")] protected T controlledObject;

    protected override void Awake()
    {
        base.Awake();
        playerInputActions = new PlayerInputActions();
        SetControllerEnabled(isActiveOnStart);
    }

    private void Reset()
    {
        entity = GetComponent<Player>();
    }

    private void Update()
    {
        if (!playerInputActions.asset.enabled) return;

        ControlUpdate();
    }

    private void FixedUpdate()
    {
        if (!playerInputActions.asset.enabled) return;

        ControlFixedUpdate();
    }

    public virtual void ControlUpdate() { }

    public virtual void ControlFixedUpdate() { }

    [Button("Setup_SetControllerEnabled")]
    public void SetControllerEnabled(bool value)
    {
        if (value)
            playerInputActions.Enable();
        else
            playerInputActions.Disable();
    }
}
