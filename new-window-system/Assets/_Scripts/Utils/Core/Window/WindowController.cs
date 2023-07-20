using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour, IInitable
{
    [SerializeField] private GameManager _gameManager;
    private static WindowController _instance;
    private Dictionary<string, Window> _windows = new Dictionary<string, Window>();
    private const string START_WINDOW = "MainWindow";

    private static WindowController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WindowController>();

                if (_instance == null)
                {
                    GameObject controllerObj = new GameObject("WindowController");
                    _instance = controllerObj.AddComponent<WindowController>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        Build();
        _gameManager.AddInitable(this);
    }

    public static bool CanBeOpened(Window window)
    {
        return true;
    }

    public static void AddWindow(Window window)
    {
        if (!Instance._windows.ContainsKey(window.name))
        {
            Instance._windows.Add(window.name, window);
        }
    }

    public static void RemoveWindow(Window window)
    {
        if (Instance._windows.ContainsKey(window.name))
        {
            Instance._windows.Remove(window.name);
        }
    }

    public static Window GetWindow(string windowName)
    {
        if (Instance._windows.TryGetValue(windowName, out var window))
        {
            return window;
        }

        Debug.LogWarning($"Window '{windowName}' not found.");
        return null;
    }

    public static void OpenWindowByKey(string windowKey)
    {
        Window window = GetWindow(windowKey);

        if (window != null)
        {
            window.OpenWindow();
        }
    }

    public void Init()
    {
        OpenWindowByKey(START_WINDOW);
    }

    private void Build()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _windows.Add(transform.GetChild(i).name, transform.GetChild(i).GetComponent<Window>());
        }
    }

    public IEnumerable<Window> GetWindows()
    {
        return new List<Window>(_windows.Values);
    }
}