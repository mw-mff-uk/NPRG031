namespace MainNamespace
{
  class StopInvincibilityEvent : GameClockEvent
  {
    private Pacman pacman;
    public override void Execute()
    {
      this.pacman.StopInvincibility();
    }
    public StopInvincibilityEvent(Pacman pacman)
    {
      this.pacman = pacman;
    }
  }
}
