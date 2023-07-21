public interface IState
{
    void Tick();

    void OnEnterState();

    void OnExitState();

    float TimeDelayToExit();

    void ConditionCheck();

    void OnReset();
}