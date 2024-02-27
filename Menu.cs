namespace Projekt_PO;
using System;

public class Menu
{
    private Parking parking;

    public Menu(Parking parking)
    {
        this.parking = parking;
    }

    public void DisplayMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("System Zarządzania Parkingiem");
            Console.WriteLine("1. Wyświetl stan parkingu");
            Console.WriteLine("2. Dodaj pojazd");
            Console.WriteLine("3. Usuń pojazd");
            Console.WriteLine("4. Wyświetl informacje o pojazdach");
            //Console.WriteLine("5. Wyjście")
            /*doszedłem do wniosku, że program ma działać 24/7, i po jednokrotnym uruchomieniu nie ma powodu,
             żeby ktoś go wyłączał. Jednak samą funkcje zachowałem*/
            Console.Write("Wybierz opcję: ");

            switch (Console.ReadLine())
            {
                case "1":
                    parking.DisplayParking();
                    WaitForKeyPress();
                    break;
                case "2":
                    AddVehicleMenu();
                    break;
                case "3":
                    RemoveVehicleMenu();
                    break;
                case "4":
                    DisplayVehicleLocationsMenu();
                    break;
                /*case "5":
                    return; // Wyjście z pętli i zakończenie programu*/ //jw.
                default:
                    Console.WriteLine("Nieprawidłowy wybór, spróbuj ponownie.");
                    WaitForKeyPress();
                    break;
            }
        }
    }

    private void AddVehicleMenu()
    {
        Console.Write("Podaj numer rejestracyjny pojazdu: ");
        string licensePlate = Console.ReadLine();
        
        if (licensePlate == "0000000000")
        {
            ServiceMode();
            return;
        }
        
        Console.Write("Podaj typ pojazdu (M - Motocykl, S - Samochód Osobowy, A - Autobus): ");
        char type = Console.ReadKey().KeyChar;
        Console.WriteLine();
        

        Pojazd vehicle = null;
        switch (type)
        {
            case 'M':
                vehicle = new Motocykl(licensePlate);
                break;
            case 'S':
                vehicle = new SamochodOsobowy(licensePlate);
                break;
            case 'A':
                vehicle = new Autobus(licensePlate);
                break;
        }

        if (parking.AddVehicle(vehicle, type))
        {
            Console.WriteLine("Pojazd został dodany.");
            var coordinates = parking.GetVehicleCoordinates(licensePlate);
            Console.WriteLine($"Dodano pojazd na współrzędnych: ({coordinates.Item1}, {coordinates.Item2})");
        }
        else
        {
            Console.WriteLine("Nie udało się dodać pojazdu. Brak wolnych miejsc.");
        }
        WaitForKeyPress();
    }

    private void RemoveVehicleMenu()
    {
        Console.Write("Podaj numer rejestracyjny pojazdu: ");
        string licensePlate = Console.ReadLine();

        parking.RemoveVehicle(licensePlate);
        Console.WriteLine("Pojazd został usunięty.");
        WaitForKeyPress();
    }

    private void WaitForKeyPress()
    {
        Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }
    
    private void ServiceMode()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Tryb serwisowy:");
            Console.WriteLine("1. Eksportuj informacje o parkingu do pliku");
            Console.WriteLine("2. Utwórz nowy parking");
            Console.WriteLine("3. Powrót do menu głównego");
            Console.Write("Wybierz opcję: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Podaj nazwę pliku: ");
                    string fileName = Console.ReadLine();
                    parking.SaveToFile(fileName);
                    Console.WriteLine($"Informacje o pojazdach zostały zapisane do pliku {fileName}.");
                    WaitForKeyPress();
                    break;
                case "2":
                    Console.WriteLine("Tworzenie nowego parkingu.");
                    Console.Write("Podaj szerokość parkingu: ");
                    int width = int.Parse(Console.ReadLine());
                    Console.Write("Podaj wysokość parkingu: ");
                    int height = int.Parse(Console.ReadLine());
                    // Po podaniu wymiarów tworzony jest nowy parking.
                    this.parking = new Parking(width, height);
                    Console.WriteLine($"Nowy parking o wymiarach {width}x{height} został utworzony.");
                    WaitForKeyPress();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    WaitForKeyPress();
                    break;
            }
        }
    }
        
    private void DisplayVehicleLocationsMenu()
    {
        Console.Clear();
        Console.WriteLine("Informacje o pojazdach na parkingu:");
        parking.DisplayVehicleInformation();
        WaitForKeyPress();
    }
}
