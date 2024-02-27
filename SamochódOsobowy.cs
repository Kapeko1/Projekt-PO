namespace Projekt_PO;

public class SamochodOsobowy : Pojazd
{
    public SamochodOsobowy(string licensePlate) : base(licensePlate) { }

    public override int SpaceRequired => 2;
}