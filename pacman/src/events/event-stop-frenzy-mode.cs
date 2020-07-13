namespace MainNamespace
{
  class StopFrenzyModeEvent : GameClockEvent
  {
    private Pacman pacman;
    public override void Execute()
    {
      this.pacman.StopFrenzyMode();
    }
    public StopFrenzyModeEvent(Pacman pacman)
    {
      this.pacman = pacman;
    }
  }
}
