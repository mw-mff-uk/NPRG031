using System;
using System.Collections.Generic;
using System.Text;

namespace simulace
{
  public enum TypUdalosti
  {
    Start,
    Trpelivost,
    Obslouzen
  }

  public class Udalost
  {
    public int kdy;
    public Proces kdo;
    public TypUdalosti co;
    public Udalost(int kdy, Proces kdo, TypUdalosti co)
    {
      this.kdy = kdy;
      this.kdo = kdo;
      this.co = co;
    }
  }
  public class Kalendar
  {
    private List<Udalost> seznam;
    public Kalendar()
    {
      this.seznam = new List<Udalost>();
    }
    public void Pridej(int kdy, Proces kdo, TypUdalosti co)
    {
      //Console.WriteLine("PLAN: {0} {1} {2}", kdy, kdo.ID, co);
      // pro hledani chyby:
      foreach (Udalost ud in this.seznam)
        if (ud.kdo == kdo)
          Console.WriteLine("");


      this.seznam.Add(new Udalost(kdy, kdo, co));
    }
    public void Odeber(Proces kdo, TypUdalosti co)
    {
      foreach (Udalost ud in this.seznam)
      {
        if ((ud.kdo == kdo) && (ud.co == co))
        {
          this.seznam.Remove(ud);
          return; // odebiram jen jeden vyskyt!
        }
      }
    }
    public Udalost Prvni()
    {
      Udalost prvni = null;
      foreach (Udalost ud in seznam)
        if ((prvni == null) || (ud.kdy < prvni.kdy))
          prvni = ud;
      seznam.Remove(prvni);
      return prvni;
    }
    public Udalost Vyber()
    {
      return Prvni();
    }
    public void PrintInfo()
    {
      foreach (Udalost ud in this.seznam)
        Console.WriteLine("PLAN: {0} {1} {2}", ud.kdy, ud.kdo.ID, ud.co);
    }
  }

  public abstract class Proces
  {
    public static char[] mezery = { ' ' };
    public int patro;
    public string ID;
    public abstract void Zpracuj(Udalost ud);
    public void log(string zprava)
    {
      //if (ID == "Dana")
      //if (ID == "elefant")
      //if (this is Zakaznik)
      // Console.WriteLine("{0}/{3} {1}: {2}",
      //     model.Cas, ID, zprava, patro);
    }
    protected Model model;
  }

  public class Oddeleni : Proces
  {
    private int rychlost;
    public int Rychlost { get { return this.rychlost; } }
    private List<Zakaznik> fronta;
    private bool obsluhuje;

    public Oddeleni(Model model, string popis)
    {
      this.model = model;

      string[] popisy = popis.Split(Proces.mezery, StringSplitOptions.RemoveEmptyEntries);

      this.ID = popisy[0];

      this.patro = int.Parse(popisy[1]);
      if (this.patro > model.MaxPatro)
        model.MaxPatro = this.patro;

      this.rychlost = int.Parse(popisy[2]);

      this.obsluhuje = false;
      this.fronta = new List<Zakaznik>();

      this.model.VsechnaOddeleni.Add(this);

      // Console.WriteLine("Init Oddelenie: {0}", ID);
    }
    public void ZaradDoFronty(Zakaznik zak)
    {
      fronta.Add(zak);
      log("do fronty " + zak.ID);

      // Nerob nič, ak obsluhuje
      if (!obsluhuje)
      {
        obsluhuje = true;
        model.Naplanuj(model.Cas, this, TypUdalosti.Start);
      }
    }
    public void VyradZFronty(Zakaznik koho)
    {
      fronta.Remove(koho);
    }
    public override void Zpracuj(Udalost ud)
    {
      switch (ud.co)
      {
        case TypUdalosti.Start:
          if (fronta.Count == 0)
            obsluhuje = false; // a dal neni naplanovana a probudi se tim, ze se nekdo zaradi do fronty
          else
          {
            Zakaznik zak = fronta[0];
            fronta.RemoveAt(0);
            model.Odplanuj(zak, TypUdalosti.Trpelivost);
            model.Naplanuj(model.Cas + rychlost, zak, TypUdalosti.Obslouzen);
            model.Naplanuj(model.Cas + rychlost, this, TypUdalosti.Start);
          }
          break;
      }
    }
  }
  public enum SmeryJizdy
  {
    Nahoru,
    Dolu,
    Stoji
  }
  public class Vytah : Proces
  {
    private int kapacita;
    private int dobaNastupu;
    private int dobaVystupu;
    private int dobaPatro2Patro;
    static int[] ismery = { +1, -1, 0 }; // prevod (int) SmeryJizdy na smer

    private class Pasazer
    {
      public Proces kdo;
      public int kamJede;
      public Pasazer(Proces kdo, int kamJede)
      {
        this.kdo = kdo;
        this.kamJede = kamJede;
      }
    }

    private List<Pasazer>[,] cekatele; // [patro,smer]
    private List<Pasazer> naklad;   // pasazeri ve vytahu
    private SmeryJizdy smer;
    private int kdyJsemMenilSmer;

    public void PridejDoFronty(int odkud, int kam, Proces kdo)
    {
      Pasazer pas = new Pasazer(kdo, kam);
      if (kam > odkud)
        cekatele[odkud, (int)SmeryJizdy.Nahoru].Add(pas);
      else
        cekatele[odkud, (int)SmeryJizdy.Dolu].Add(pas);

      // pripadne rozjet stojici vytah:
      if (smer == SmeryJizdy.Stoji)
      {
        model.Odplanuj(model.vytah, TypUdalosti.Start); // kdyby nahodou uz byl naplanovany
        model.Naplanuj(model.Cas, this, TypUdalosti.Start);
      }
    }
    public bool CekaNekdoVPatrechVeSmeruJizdy()
    {
      int ismer = ismery[(int)smer];
      for (int pat = patro + ismer; (pat > 0) && (pat <= model.MaxPatro); pat += ismer)
        if ((cekatele[pat, (int)SmeryJizdy.Nahoru].Count > 0) || (cekatele[pat, (int)SmeryJizdy.Dolu].Count > 0))
        {
          if (cekatele[pat, (int)SmeryJizdy.Nahoru].Count > 0)
            log("Nahoru ceka " + cekatele[pat, (int)SmeryJizdy.Nahoru][0].kdo.ID
                + " v patre " + pat + "/" + cekatele[pat, (int)SmeryJizdy.Nahoru][0].kdo.patro);
          if (cekatele[pat, (int)SmeryJizdy.Dolu].Count > 0)
            log("Dolu ceka " + cekatele[pat, (int)SmeryJizdy.Dolu][0].kdo.ID
                + " v patre " + pat + "/" + cekatele[pat, (int)SmeryJizdy.Dolu][0].kdo.patro);

          //log(" x "+cekatele[pat, (int)SmeryJizdy.Nahoru].Count+" x "+cekatele[pat, (int)SmeryJizdy.Dolu].Count);
          return true;
        }
      return false;
    }

    public Vytah(Model model, string popis)
    {
      this.model = model;

      string[] popisy = popis.Split(Proces.mezery, StringSplitOptions.RemoveEmptyEntries);

      this.ID = popisy[0];
      this.kapacita = int.Parse(popisy[1]);
      this.dobaNastupu = int.Parse(popisy[2]);
      this.dobaVystupu = int.Parse(popisy[3]);
      this.dobaPatro2Patro = int.Parse(popisy[4]);

      this.patro = 0;
      this.smer = SmeryJizdy.Stoji;
      this.kdyJsemMenilSmer = -1;

      // Matrix: one row for each floor
      //         two columns: going up, going down
      this.cekatele = new List<Pasazer>[model.MaxPatro + 1, 2];
      for (int i = 0; i < model.MaxPatro + 1; i++)
      {
        for (int j = 0; j < 2; j++)
        {
          this.cekatele[i, j] = new List<Pasazer>();
        }

      }

      // People in the elevator right now
      naklad = new List<Pasazer>();

      // Console.WriteLine("Init Vytah: {0}", this.kapacita);
    }
    public override void Zpracuj(Udalost ud)
    {
      switch (ud.co)
      {
        case TypUdalosti.Start:

          // HACK pro cerstve probuzeny vytah:
          if (smer == SmeryJizdy.Stoji)
            // stoji, tedy nikoho neveze a nekdo ho prave probudil => nastavim jakykoliv smer a najde ho:
            smer = SmeryJizdy.Nahoru;

          // chce nekdo vystoupit?
          foreach (Pasazer pas in naklad)
            if (pas.kamJede == patro)
            // bude vystupovat:
            {
              naklad.Remove(pas);

              pas.kdo.patro = patro;
              model.Naplanuj(model.Cas + dobaVystupu, pas.kdo, TypUdalosti.Start);
              log("vystupuje " + pas.kdo.ID);

              model.Naplanuj(model.Cas + dobaVystupu, this, TypUdalosti.Start);

              return; // to je pro tuhle chvili vsechno
            }

          // muze a chce nekdo nastoupit?
          if (naklad.Count == kapacita)
          // i kdyby chtel nekdo nastupovat, nemuze; veze lidi => pokracuje:
          {
            // popojet:
            int ismer = ismery[(int)smer];
            patro = patro + ismer;

            string spas = "";
            foreach (Pasazer pas in naklad)
              spas += " " + pas.kdo.ID;
            log("odjizdim");
            model.Naplanuj(model.Cas + dobaPatro2Patro, this, TypUdalosti.Start);
            return; // to je pro tuhle chvili vsechno
          }
          else
          // neni uplne plny
          {
            // chce nastoupit nekdo VE SMERU jizdy?
            if (cekatele[patro, (int)smer].Count > 0)
            {
              log("nastupuje " + cekatele[patro, (int)smer][0].kdo.ID);
              naklad.Add(cekatele[patro, (int)smer][0]);
              cekatele[patro, (int)smer].RemoveAt(0);
              model.Naplanuj(model.Cas + dobaNastupu, this, TypUdalosti.Start);

              return; // to je pro tuhle chvili vsechno
            }

            // ve smeru jizdy nikdo nenastupuje:
            if (naklad.Count > 0)
            // nikdo nenastupuje, vezu pasazery => pokracuju v jizde:
            {
              // popojet:
              int ismer = ismery[(int)smer];
              patro = patro + ismer;

              string spas = "";
              foreach (Pasazer pas in naklad)
                spas += " " + pas.kdo.ID;
              //log("nekoho vezu");
              log("odjizdim: " + spas);

              model.Naplanuj(model.Cas + dobaPatro2Patro, this, TypUdalosti.Start);
              return; // to je pro tuhle chvili vsechno
            }

            // vytah je prazdny, pokud v dalsich patrech ve smeru jizdy uz nikdo neceka, muze zmenit smer nebo se zastavit:
            if (CekaNekdoVPatrechVeSmeruJizdy() == true)
            // pokracuje v jizde:
            {
              // popojet:
              int ismer = ismery[(int)smer];
              patro = patro + ismer;

              //log("nekdo ceka");
              log("odjizdim");
              model.Naplanuj(model.Cas + dobaPatro2Patro, this, TypUdalosti.Start);
              return; // to je pro tuhle chvili vsechno
            }

            // ve smeru jizdy uz nikdo neceka => zmenit smer nebo zastavit:
            if (smer == SmeryJizdy.Nahoru)
              smer = SmeryJizdy.Dolu;
            else
              smer = SmeryJizdy.Nahoru;

            log("zmena smeru");

            //chce nekdo nastoupit prave tady?
            if (kdyJsemMenilSmer != model.Cas)
            {
              kdyJsemMenilSmer = model.Cas;
              // podivat se, jestli nekdo nechce nastoupit opacnym smerem:
              model.Naplanuj(model.Cas, this, TypUdalosti.Start);
              return;
            }

            // uz jsem jednou smer menil a zase nikdo nenastoupil a nechce => zastavit
            log("zastavuje");
            smer = SmeryJizdy.Stoji;
            return; // to je pro tuhle chvili vsechno
          }
      }
    }
  }
  public class Zakaznik : Proces
  {
    private bool isInside;
    private int trpelivost;
    public int Trpelivost { get { return this.trpelivost; } }
    private int prichod;
    private List<string> Nakupy;
    public Zakaznik(Model model, string popis)
    {
      this.model = model;

      string[] popisy = popis.Split(Proces.mezery, StringSplitOptions.RemoveEmptyEntries);

      this.ID = popisy[0];
      this.prichod = int.Parse(popisy[1]);
      this.trpelivost = int.Parse(popisy[2]);

      this.Nakupy = new List<string>();
      for (int i = 3; i < popisy.Length; i++)
        this.Nakupy.Add(popisy[i]);

      this.patro = 0;
      this.isInside = false;

      this.model.Naplanuj(prichod, this, TypUdalosti.Start);

      // Console.WriteLine("Init Zakaznik: {0}", ID);
    }
    public override void Zpracuj(Udalost ud)
    {
      switch (ud.co)
      {
        case TypUdalosti.Start:
          bool isInside = this.isInside;

          // Ak je človek vonku
          if (!isInside)
          {
            if (this.model.Capacity == 0 || this.model.Capacity > this.model.PeopleInside)
            {
              this.isInside = true;
              this.model.PeopleInside += 1;
              // Console.WriteLine(this.model.PeopleInside);

              isInside = true;
            }
            else
            {
              // Console.WriteLine("Enqueue");
              this.model.ShopQueue.Enqueue(this);
            }
          }

          if (isInside)
          {
            if (Nakupy.Count == 0)
            // ma nakoupeno
            {
              if (patro == 0)
              {
                this.model.TimeSpent += this.model.Cas;
                this.isInside = false;
                this.model.PeopleInside -= 1;

                if (this.model.ShopQueue.Count > 0)
                {
                  // Pusti človeka dnu
                  this.model.Naplanuj(this.model.Cas, this.model.ShopQueue.Dequeue(), TypUdalosti.Start);
                }

                log("-------------- odchází"); // nic, konci
              }
              else
                this.model.vytah.PridejDoFronty(patro, 0, this);
            }
            else
            {
              Oddeleni odd = OddeleniPodleJmena(Nakupy[0]);
              int pat = odd.patro;
              if (pat == patro) // to oddeleni je v patre, kde prave jsem
              {
                if (Nakupy.Count > 1)
                  model.Naplanuj(model.Cas + trpelivost, this, TypUdalosti.Trpelivost);
                odd.ZaradDoFronty(this);
              }
              else
                model.vytah.PridejDoFronty(patro, pat, this);
            }
          }
          break;
        case TypUdalosti.Obslouzen:
          log("Nakoupeno: " + Nakupy[0]);
          Nakupy.RemoveAt(0);
          // ...a budu hledat dalsi nakup -->> Start
          model.Naplanuj(model.Cas, this, TypUdalosti.Start);
          break;
        case TypUdalosti.Trpelivost:
          log("!!! Trpelivost: " + Nakupy[0]);
          // vyradit z fronty:
          {
            Oddeleni odd = OddeleniPodleJmena(Nakupy[0]);
            odd.VyradZFronty(this);
          }

          // prehodit tenhle nakup na konec:
          string nesplneny = Nakupy[0];
          Nakupy.RemoveAt(0);
          Nakupy.Add(nesplneny);

          // ...a budu hledat dalsi nakup -->> Start
          model.Naplanuj(model.Cas, this, TypUdalosti.Start);
          break;
      }
    }

    private Oddeleni OddeleniPodleJmena(string kamChci)
    {
      foreach (Oddeleni odd in model.VsechnaOddeleni)
        if (odd.ID == kamChci)
          return odd;
      return null;
    }
  }


  public class Model
  {
    public int Cas;
    public int PeopleInside;
    public int Capacity;
    public Vytah vytah;
    public List<Oddeleni> VsechnaOddeleni = new List<Oddeleni>();
    public Queue<Zakaznik> ShopQueue = new Queue<Zakaznik>();
    public int MaxPatro;
    public int nCustomers;
    public int TimeSpent;
    private Kalendar kalendar;
    public void Naplanuj(int kdy, Proces kdo, TypUdalosti co)
    {
      kalendar.Pridej(kdy, kdo, co);
    }
    public void Odplanuj(Proces kdo, TypUdalosti co)
    {
      kalendar.Odeber(kdo, co);
    }
    public void VytvorProcesy()
    {
      System.IO.StreamReader soubor = new System.IO.StreamReader("obchod_data.txt", Encoding.GetEncoding("utf-8"));
      while (!soubor.EndOfStream)
      {
        string s = soubor.ReadLine();
        if (s != "")
        {
          switch (s[0])
          {
            case 'O':
              new Oddeleni(this, s.Substring(1));
              break;
            // case 'Z':
            //   new Zakaznik(this, s.Substring(1));
            //   break;
            case 'V':
              this.vytah = new Vytah(this, s.Substring(1));
              break;
          }
        }
      }

      int maxStores = this.VsechnaOddeleni.Count;
      for (int i = 0; i < this.nCustomers; i++)
      {
        string stores = "";
        int arrival = Program.rnd.Next(0, 601);
        int patience = Program.rnd.Next(3, 51);
        int nStores = Program.rnd.Next(1, 21);
        for (int j = 0; j < nStores; j++)
          stores += (j == 0 ? "" : " ") + this.VsechnaOddeleni[Program.rnd.Next(0, maxStores)].ID;

        this.TimeSpent -= arrival;

        string args = String.Format("{0} {1} {2} {3}", i, arrival, patience, stores);
        // Console.WriteLine(args);
        new Zakaznik(this, args);
      }

      soubor.Close();
    }
    public void PrintInfo()
    {
      Console.WriteLine("ID,floor,speed");
      foreach (Oddeleni oddeleni in this.VsechnaOddeleni)
        Console.WriteLine("{0},{1},{2}", oddeleni.ID, oddeleni.patro, oddeleni.Rychlost);
    }
    public int Vypocet()
    {
      this.PeopleInside = 0;
      this.TimeSpent = 0;
      this.Cas = 0;
      this.kalendar = new Kalendar();
      this.VytvorProcesy();

      // this.kalendar.PrintInfo();
      Udalost ud;
      while ((ud = kalendar.Vyber()) != null)
      {
        // Console.WriteLine("{0} {1} {2}", ud.kdy, ud.kdo.ID, ud.co);
        Cas = ud.kdy;
        ud.kdo.Zpracuj(ud);
      }

      return Cas;
    }

    public Model(int nCustomers, int capacity)
    {
      this.nCustomers = nCustomers;
      this.Capacity = capacity;
    }
  }

  class Program
  {
    public static Random rnd = new Random(12345);
    static void Main(string[] args)
    {
      int k = Int32.Parse(args[1]);

      Model model;

      Program.rnd = new Random(12345);

      for (int nCustomers = 1; nCustomers <= 501; nCustomers += 10)
      {
        int sum = 0;
        for (int j = 0; j < 10; j++)
        {
          model = new Model(nCustomers, k);
          model.Vypocet();
          sum += model.TimeSpent;
          // Console.WriteLine("[{0}]: {1}", j, model.TimeSpent);
        }
        Console.WriteLine("{0},{1},{2}", nCustomers, k, (double)sum / (10.0 * (double)nCustomers));
        // System.Environment.Exit(0);
      }

      // Console.WriteLine("{0} KONEC --------------------------------", model.Vypocet());
      // Console.ReadLine();
    }
  }
}