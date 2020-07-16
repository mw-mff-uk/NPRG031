class Settings
{
  private int monsters = 4;
  public int Monsters { get => this.monsters; }
  private double playerSpeed = 0.175;
  public double PlayerSpeed { get => this.playerSpeed; }
  private double monsterSpeed = 0.17;
  public double MonsterSpeed { get => this.monsterSpeed; }
  private int tickPeriod = 16;
  public int TickPeriod { get => this.tickPeriod; }
  private bool friendlyMode = false;
  public bool FriendlyMode { get => this.friendlyMode; }
  private double invincibilityDuration = 5000.0;
  public double InvincibilityDuration { get => this.invincibilityDuration; }
  private double frenzyModeDuration = 10000.0;
  public double FrenzyModeDuration { get => this.frenzyModeDuration; }
}
