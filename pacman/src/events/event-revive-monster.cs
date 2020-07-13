namespace MainNamespace
{
  class ReviveMonsterEvent : GameClockEvent
  {
    private Monster monster;
    public override void Execute()
    {
      this.monster.Revive();
    }
    public ReviveMonsterEvent(Monster monster)
    {
      this.monster = monster;
    }
  }
}
