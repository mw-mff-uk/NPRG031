using System;

namespace MainNamespace
{
  class Stopwatch
  {
    static long MILLISECONDS_IN_TICK = 10000;
    static long SECONDS_IN_TICK = Stopwatch.MILLISECONDS_IN_TICK * 1000;
    static long MINUTES_IN_TICK = Stopwatch.SECONDS_IN_TICK * 60;
    static long HOURS_IN_TICK = Stopwatch.MINUTES_IN_TICK * 60;
    static long DAYS_IN_TICK = Stopwatch.HOURS_IN_TICK * 24;
    private bool running;
    public bool Running { get => this.running; }
    private long ticks;
    public long Ticks { get => this.ticks + (this.running ? (DateTime.Now - this.start).Ticks : 0); }
    public double Milliseconds { get => (double)this.Ticks / (double)Stopwatch.MILLISECONDS_IN_TICK; }
    public double Seconds { get => (double)this.Ticks / (double)Stopwatch.SECONDS_IN_TICK; }
    public double Minutes { get => (double)this.Ticks / (double)Stopwatch.MINUTES_IN_TICK; }
    public double Hours { get => (double)this.Ticks / (double)Stopwatch.HOURS_IN_TICK; }
    public double Days { get => (double)this.Ticks / (double)Stopwatch.DAYS_IN_TICK; }

    private DateTime start;
    public Stopwatch Start()
    {
      if (!this.running)
      {
        this.running = true;
        this.start = DateTime.Now;
      }

      return this;
    }
    public Stopwatch Stop()
    {
      if (this.running)
      {
        this.running = false;
        this.ticks += (DateTime.Now - this.start).Ticks;
      }

      return this;
    }
    public Stopwatch Reset()
    {
      this.running = false;
      this.ticks = 0;

      return this;
    }
    public Stopwatch()
    {
      this.ticks = 0;
      this.running = false;
    }
  }
}
