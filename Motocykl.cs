namespace Projekt_PO;

public class Motocykl : Pojazd
{
    public Motocykl(string licensePlate) : base(licensePlate) { }
    public override int SpaceRequired => 1;
}
