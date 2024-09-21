using APIRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Utils
{
    public class DataStore
    {
        public static List<Fleet> Fleets { get; set; } = new List<Fleet>();
        public static List<Truck> Trucks { get; set; } = new List<Truck>();
        private enum UbicationsBarcelona
        {
            
        };
        public static void InitializeData()
        {
            
            Fleets.Add(new Fleet { id = "F0", company = "FleetPro Transport" });
            Fleets.Add(new Fleet { id = "F1", company = "TruckLine Logistics" });
            Fleets.Add(new Fleet { id = "F2", company = "CargoFleet Solutions" });

            List<String> UbicationsBarcelona = new List<String> {"RondaLitoral", "RondaDeDalt", "PuertoDeBarcelona", "ZonaFranca", "GranViaDeLesCortsCatalanes", "AvenidaDiagonal", "AutopistaAP7", "CarreteraDeSants", "AutopistaC32", "AvenidaMeridiana"};
            for (int i = 0; i < 10; i++)
            {
                var truck = new Truck
                {
                    id = $"T{i}",
                    ubication = UbicationsBarcelona[i],
                    state = i % 2 == 0 ? "Active" : "Inactive",
                    fleetId = i < 3 ? "F0" : i < 6 ? "F1" : "F2",
                };

                Trucks.Add(truck);
            }
        }

    }

}
