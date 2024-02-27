namespace Projekt_PO;

public class Autobus : Pojazd
{
    public Autobus(string licensePlate) : base(licensePlate) { }

    public override int SpaceRequired => 4;
}