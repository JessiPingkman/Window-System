using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(GraphicRaycaster))]
public abstract class Window : MonoBehaviour
{
    public UnityEvent onRefresh;
    public UnityEvent onEnable;
    public UnityEvent onDisable;
    public UnityEvent onOpen;
    public UnityEvent onClose;

    [SerializeField] private bool _over;
    [SerializeField] private bool _canBeClosedByGesture;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GraphicRaycaster _raycaster;

    public bool Opened { get; private set; }

    public bool Over => _over;
    public bool CanBeClosedByGesture => _canBeClosedByGesture;

    private Canvas Canvas
    {
        get
        {
            if (_canvas == null)
            {
                _canvas = GetComponent<Canvas>();
                if (_canvas == null)
                {
                    Debug.LogError($"{name} с компонентом окна не имеет компонента Canvas");
                    _canvas = gameObject.AddComponent<Canvas>();
                }
            }

            return _canvas;
        }
    }

    private GraphicRaycaster Raycaster
    {
        get
        {
            if (_raycaster == null)
            {
                _raycaster = GetComponent<GraphicRaycaster>();
                if (_raycaster == null)
                {
                    Debug.LogError($"{name} с компонентом окна не имеет компонента GraphicRaycaster");
                    _raycaster = gameObject.AddComponent<GraphicRaycaster>();
                }
            }

            return _raycaster;
        }
    }

    private void Enable()
    {
        Canvas.enabled = true;
        Raycaster.enabled = true;
        onEnable?.Invoke();
    }

    private void Disable()
    {
        Canvas.enabled = false;
        Raycaster.enabled = false;
        onDisable?.Invoke();
    }

    protected abstract void Build();

    public void OpenWindow()
    {
        if (!WindowController.CanBeOpened(this)) return;
        Build();
        Enable();
        Opened = true;
        onOpen?.Invoke();
        WindowController.AddWindow(this);
        RefreshWindow();
    }

    public void CloseWindow()
    {
        Disable();
        Opened = false;
        onClose?.Invoke();
        WindowController.RemoveWindow(this);
    }

    private void RefreshWindow()
    {
        onRefresh?.Invoke();
    }
}