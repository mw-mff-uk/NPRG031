using System;

namespace MainNamespace
{
  class GameClock
  {
    private const int TICK_DURATION_HISTORY_LENGTH = 30;
    private LinkedList<GameClockEvent> events;
    private bool isActive = false;
    public bool IsActive { get => this.isActive; }
    private Stopwatch stopwatch;
    public double Elapsed { get => this.stopwatch.Milliseconds; }
    private double lastTick = 0;
    private double lastTickDuration = 0;
    public double LastTickDuration { get => this.lastTickDuration; }
    private LinkedList<double> ticksHistory;
    private double[] tickDurationHistory;
    private int tickDurationHistoryCursor = 0;
    public double TickDuration
    {
      get
      {
        double sum = 0.0;

        for (int i = 0; i < this.tickDurationHistory.Length; i++)
          sum += this.tickDurationHistory[i];

        return sum / this.tickDurationHistory.Length;
      }
    }
    public double Tick()
    {
      double elapsed = this.Elapsed;
      this.lastTickDuration = elapsed - this.lastTick;
      this.lastTick = elapsed;

      this.tickDurationHistory[this.tickDurationHistoryCursor] = this.lastTickDuration;
      this.tickDurationHistoryCursor = (this.tickDurationHistoryCursor + 1) % this.tickDurationHistory.Length;

      return this.lastTickDuration;
    }
    public int PlanEvent(GameClockEvent e, double executeAfter)
    {
      e.ExecutionTimestamp = this.Elapsed + executeAfter;
      this.events.InsertLast(e);

      return this.events.Length;
    }
    public int ExecuteEvents()
    {
      int executed = 0;

      var iterator = this.events.Iterator();
      while (!iterator.Done)
      {
        LinkedListItem<GameClockEvent> item = iterator.Next();
        GameClockEvent e = item.Value;

        if (e.ExecutionTimestamp <= this.Elapsed)
        {
          e.Execute();
          executed++;
          this.events.RemoveItem(item);
        }
      }

      return executed;
    }
    public void Reset()
    {
      this.lastTick = 0;
      this.lastTickDuration = 0;
      this.stopwatch.Start().Stop().Reset();

      for (int i = 0; i < this.tickDurationHistory.Length; i++)
        this.tickDurationHistory[i] = 0.0;

      this.events.Wipe();
    }
    public void Stop()
    {
      this.stopwatch.Stop();
    }
    public void Start()
    {
      this.stopwatch.Start();
    }
    public GameClock()
    {
      this.stopwatch = new Stopwatch();
      this.tickDurationHistory = new double[TICK_DURATION_HISTORY_LENGTH];
      this.events = new LinkedList<GameClockEvent>();

      this.Reset();
    }
  }
}
