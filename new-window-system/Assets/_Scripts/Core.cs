using UnityEngine;

public class Core
{
    private IPlayerState _currentState;

    public void Update()
    {
        _currentState.Update();
    }

    public void RenderUI()
    {
        _currentState.RenderUI();
    }

    public void SetState(IPlayerState playerState)
    {
        _currentState = playerState;
    }
}

public interface IPlayerState
{
    public void Update();
    public void RenderUI();
}

public class MenuState : IPlayerState
{
    public void Update()
    {
    }

    public void RenderUI()
    {
    }
}

public class GameState : IPlayerState
{
    public void Update()
    {
    }

    public void RenderUI()
    {
    }
}