namespace Projekt_PO;

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


public class Parking
{
    private int width;
    private int height;
    private char[,] layout;
    private Dictionary<string, (int, int, char, DateTime, DateTime)> vehicles = new Dictionary<string, (int, int, char, DateTime, DateTime)>();

    public Parking(int width, int height)
    {
        this.width = width;
        this.height = height;
        layout = new char[height, width];
        InitLayout();
    }

     private void InitLayout()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                layout[i, j] = (i % 3 != 2) ? 'V' : ' '; // Zmiana: 'V' dla miejsc parkingowych, ' ' dla przejazdów
            }
        }
    }

    public bool AddVehicle(Pojazd vehicle, char vehicleType)
    {
        for (int i = 0; i < height; i += (i % 3 == 2 ? 2 : 1)) // Ustalenie czestotliwosci przejazdów
        {
            for (int j = 0; j <= width - vehicle.SpaceRequired; j++)
            {
                bool canFit = true;
                for (int k = 0; k < vehicle.SpaceRequired; k++)
                {
                    if (layout[i, j + k] != 'V')
                    {
                        canFit = false;
                        break;
                    }
                }

                if (canFit)
                {
                    for (int k = 0; k < vehicle.SpaceRequired; k++)
                    {
                        layout[i, j + k] = vehicleType; // Oznaczam miejsce pojazdu odpowiednim symbolem
                    }
                    // Aktualizacja słownika pojazdów
                    vehicles[vehicle.LicensePlate] = (i, j, vehicleType, vehicle.AddedTime, DateTime.MinValue);
                    return true;
                }
            }
        }
        return false;
    }


    /*public bool RemoveVehicle(string licensePlate)
    {
        if (vehicles.ContainsKey(licensePlate))
        {
            
            var (x, y, type, addedTime, _) = vehicles[licensePlate];
            vehicles[licensePlate] = (x, y, type, addedTime, DateTime.Now); // Ustawienie daty odjazdu
            vehicles.Remove(licensePlate); // Usuwamy wpis związanego z numerem rejestracyjnym
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (layout[i, j] == type)
                    {
                        layout[i, j] = 'V'; // Zaznaczamy miejsce jako wolne
                    }
                }
            }
            return true;
        }
        return false;
    }*/
    /*
    public bool RemoveVehicle(string licensePlate)
    {
        if (vehicles.ContainsKey(licensePlate))
        {
            var (x, y, type, addedTime, departureTime) = vehicles[licensePlate];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (layout[i, j] == type)
                    {
                        layout[i, j] = 'V'; // Usuwamy graficzne przedstawienie pojazdu z parkingu
                    }
                }
            }
            // Aktualizujemy datę odjazdu pojazdu w słowniku
            vehicles[licensePlate] = (x, y, type, addedTime, DateTime.Now);
            return true;
        }
        return false;
    }
*/
    
    public bool RemoveVehicle(string licensePlate)
    {

        if (vehicles.ContainsKey(licensePlate))
        {
            var (x, y, vehicleType, addedTime, _) = vehicles[licensePlate];
            vehicles[licensePlate] = (x, y, vehicleType, addedTime, DateTime.Now);
    
            // Usuwanie pojazdu z graficznego układu
            int spaceRequired = 0;
            if (vehicleType == 'A')
            {
                spaceRequired = 4;
            }
            else if (vehicleType == 'S')
            {
                spaceRequired = 2;
            }
            else if (vehicleType == 'M')
            {
                spaceRequired = 1;
            }
            for (int i = 0; i < spaceRequired; i++)
            {
                if (y + i < layout.GetLength(1)) // Upewnienie się, że nie wyjdziemy poza granice tablicy
                {
                    layout[x, y + i] = 'V'; // Ustawienie na wolne miejsce
                }
            }

            return true;
        }
        else
        {
            return false; // w przypadku ze pojazd nie istnieje
        }
    }
    

    public void DisplayParking()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < height; i++)
        {
            sb.Append("|"); //Rozpoczynamy wiersz |
            for (int j = 0; j < width; j++)
            {
                char current = layout[i, j];
                char next = j < width - 1 ? layout[i, j + 1] : ' ';
                sb.Append(current);
                // Dodajemy | tylko jeśli aktualne miejsce jest 'V' lub gdy następne miejsce różni się od aktualnego
                if (current == 'V' || next != current)
                {
                    sb.Append("|");
                }
            }
            sb.AppendLine(); // nowy wiersz
        }
        Console.WriteLine(sb.ToString());
    }



    
    public (int, int) GetVehicleCoordinates(string licensePlate)
    {
        if (vehicles.ContainsKey(licensePlate))
        {
            return (vehicles[licensePlate].Item1, vehicles[licensePlate].Item2);
        }
        else
        {
            // Jeśli nie znaleziono pojazdu o podanym numerze rejestracyjnym, zwracamy (-1, -1 bo musi zwrócić int)
            return (-1, -1);
        }
    }
    public void DisplayVehicleInformation()
    {
        foreach (var vehicle in vehicles)
        {
            if (vehicle.Value.Item5 == DateTime.MinValue) // Sprawdzenie czy pojazd juz wyjechal
            {
                Console.WriteLine($"Numer rejestracyjny: {vehicle.Key}");
                Console.WriteLine($"Typ pojazdu: {vehicle.Value.Item3}");
                Console.WriteLine($"Współrzędne: ({vehicle.Value.Item1}, {vehicle.Value.Item2})");
                Console.WriteLine($"Czas przyjazdu: {vehicle.Value.Item4}");
                Console.WriteLine();
            }
        }
    }

    public void SaveToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var vehicle in vehicles)
            {
                writer.WriteLine($"{vehicle.Key},{vehicle.Value.Item3},{vehicle.Value.Item4},{vehicle.Value.Item5}");
            }
        }
    }
}