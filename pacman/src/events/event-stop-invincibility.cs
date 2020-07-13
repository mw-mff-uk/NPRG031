namespace MainNamespace
{
  class StopInvincibilityEvent : GameClockEvent
  {
    private Pacman pacman;
    public override void Execute()
    {
      this.pacman.IsInvincible = false;
    }
    public StopInvincibilityEvent(Pacman pacman)
    {
      this.pacman = pacman;
    }
  }
}
