using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IInitator
{
    private Core _core = new Core();
    private List<IInitable> _initables = new List<IInitable>();

    private void Start()
    {
        StartApp();
        _core.SetState(new MenuState());
        _core.Update();
    }

    private void StartApp()
    {
        foreach (IInitable initable in _initables)
        {
            initable.Init();
        }
    }

    public void AddInitable(IInitable initable)
    {
        if (!_initables.Contains(initable))
        {
            _initables.Add(initable);
        }
    }

    public void RemoveInitable(IInitable initable)
    {
        if (_initables.Contains(initable))
        {
            _initables.Remove(initable);
        }
    }
}

public interface IInitable
{
    public void Init();
}

public interface IInitator
{
    public void AddInitable(IInitable initable);
    public void RemoveInitable(IInitable initable);
}