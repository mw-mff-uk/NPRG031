namespace MainNamespace
{
  abstract class GameClockEvent
  {
    public double ExecutionTimestamp;
    public abstract void Execute();
  }
}
