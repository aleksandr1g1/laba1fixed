using System;
using System.Collections.Generic;

public interface ITransportable
{
    void Load(Unit unit);
    void Unload(Unit unit);
}

public abstract class Unit
{
    public int Health { get; protected set; }
    public string Name { get; protected set; }
    public int Speed { get; protected set; }

    protected Unit(string name, int health, int speed)
    {
        Name = name;
        Health = health;
        Speed = speed;
    }

    public abstract void Attack();
}

public class Infantry : Unit, ITransportable
{
    public Infantry() : base("Infantry", 100, 10)
    {
    }

    public override void Attack()
    {
        Console.WriteLine("Infantry attacks!");
    }

    public void Load(Unit unit)
    {
        Console.WriteLine("Infantry is loading onto transport.");
    }

    public void Unload(Unit unit)
    {
        Console.WriteLine($"{Name} is unloading from transport.");
    }
}

public class Tank : Unit
{
    public int Fuel { get; protected set; }
    public string MainWeapon { get; protected set; }
    public string AdditionalWeapon { get; protected set; }

    public Tank() : base("Tank", 500, 20)
    {
        Fuel = 500;
        MainWeapon = "Cannon";
        AdditionalWeapon = "Machine Gun";
    }

    public override void Attack()
    {
        Console.WriteLine("Tank fires main weapon!");
    }
}

public abstract class FlyingUnit : Unit
{
    public int DetectionRange { get; protected set; }
    public int Fuel { get; protected set; }

    protected FlyingUnit(string name, int health, int speed, int detectionRange, int fuel)
        : base(name, health, speed)
    {
        DetectionRange = detectionRange;
        Fuel = fuel;
    }
}

public class Airplane : FlyingUnit
{
    public Airplane() : base("Airplane", 400, 50, 100, 500)
    {
    }

    public override void Attack()
    {
        Console.WriteLine("Airplane drops bombs!");
    }
}

public class Helicopter : FlyingUnit
{
    public Helicopter() : base("Helicopter", 300, 40, 80, 450)
    {
    }

    public override void Attack()
    {
        Console.WriteLine("Helicopter fires rockets!");
    }
}

public class Artillery : Unit
{
    public int FiringRange { get; protected set; }
    public int Fuel { get; protected set; }

    public Artillery() : base("Artillery", 350, 15)
    {
        FiringRange = 200;
        Fuel = 300;
    }

    public override void Attack()
    {
        Console.WriteLine("Artillery fires shells!");
    }
}

public class Transport : Unit, ITransportable
{
    private List<Unit> _units = new List<Unit>();

    public Transport() : base("Transport", 150, 5)
    {
    }

    public override void Attack()
    {
        Console.WriteLine("Transport is not combat capable.");
    }

    public void Load(Unit unit)
    {
        if (unit is ITransportable && !_units.Contains(unit))
        {
            _units.Add(unit);
            Console.WriteLine($"{unit.Name} is loaded onto {Name}.");
        }
        else
        {
            Console.WriteLine("Unit cannot be loaded.");
        }
    }

    public void Unload(Unit unit)
    {
        if (_units.Contains(unit))
        {
            _units.Remove(unit);
            Console.WriteLine($"{unit.Name} is unloading from {Name}.");
        }
        else
        {
            Console.WriteLine("Unit is not loaded on this transport.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Infantry infantry = new Infantry();
        Tank tank = new Tank();
        Transport transport = new Transport();

        transport.Load(infantry);
        transport.Load(tank);

        transport.Unload(infantry);
        transport.Unload(tank);

        tank.Attack();
        infantry.Attack();
    }
}