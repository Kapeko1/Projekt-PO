namespace Projekt_PO;

public abstract class Pojazd
{
    public string LicensePlate { get; private set; } // Pole przechowujące klucz, tzn nr rejestracyjny
    public abstract int SpaceRequired { get; } //Pole przechowujące informajcję o ilości zajmowanego miejsca
    public DateTime AddedTime { get; } // Pole przechowujące czas dodania pojazdu
    public DateTime? DepartureTime { get; set; } // opcjonalne pole przechowujące informacje o wyjeździe pojazdu


    protected Pojazd(string licensePlate)
    {
        LicensePlate = licensePlate;
        AddedTime = DateTime.Now; // Podczas tworzenia nowego pojazdu automatycznie ustawiamy datę dodania jako aktualna
    }
}
