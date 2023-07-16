public interface IState
{
    void Tick();

    void OnEnterState();

    void OnExitState();

    void OnReset();
}