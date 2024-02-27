namespace Projekt_PO;

class Program
    {
        static void Main()
        {
            Parking parking = new Parking(0,0); //Utworzenie bazowego parkingu o wymiarach 0x0
            Menu menu = new Menu(parking); // Utowrzenie nowego obiektu klasy Menu
            menu.DisplayMenu(); //Wywołanie metody wyświetlającej menu
        }
    }

