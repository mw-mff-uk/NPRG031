using System;
using System.Text;


namespace MainNamespace
{
  class Animal
  {
    public virtual void WhoAmI()
    {
      Console.WriteLine("I am an animal");
    }
  }
  class Dog : Animal
  {
    public new void WhoAmI()
    {
      Console.WriteLine("I am a dog");
    }
  }
  class Catfish : Animal
  {
    public override void WhoAmI()
    {
      Console.WriteLine("I am a catfish");
    }
  }
  class MainClass
  {
    static int Main(string[] args)
    {
      Dog a = new Dog();
      Animal b = new Dog();
      Animal c = new Catfish();
      Animal d = new Animal();

      a.WhoAmI();
      b.WhoAmI();
      c.WhoAmI();
      d.WhoAmI();

      return 0;
    }
  }
}
