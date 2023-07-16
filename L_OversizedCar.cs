using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;

namespace LocalAutoUnion404
{
    
    [CalloutProperties("Local Oversized Vehicle", "Valandria", "0.0.1")]
    public class OversizedCar : Callout
    {
        private Vehicle lovvehicle;
        Ped driver;
        private string[] goodItemList = { "Open Soda Can", "Pack of Hotdogs", "Dog Food", "Empty Can", "Phone", "Cake", "Cup of Noodles", "Water Bottle", "Pack of Cards", "Outdated Insurance Card", "Pack of Pens", "Phone", "Tablet", "Computer", "Business Cards", "Taxi Business Card", "Textbooks", "Car Keys", "House Keys", "Keys", "Folder"};

        public OversizedCar()
        {

            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Oversized Vehicle";
            CalloutDescription = "A oversized vehicle is causing issues.";
            ResponseCode = 2;
            StartDistance = 250f;
        }
        public override async void OnStart(Ped player)
        {
            base.OnStart(player);
            driver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            lovvehicle = await SpawnVehicle(VehicleHash.Dump, Location,12);
            driver.SetIntoVehicle(lovvehicle, VehicleSeat.Driver);

            //Driver Data
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.13;
            List<Item> items = new List<Item>();
            Random random3 = new Random();
            string name2 = goodItemList[random3.Next(goodItemList.Length)];
            Item goodItem = new Item {
                Name = name2,
                IsIllegal = false
            };
            items.Add(goodItem);
            data.Items = items;
            Utilities.SetPedData(driver.NetworkId, data);
            
            //Car Data
            VehicleData vehicleData = new VehicleData();
            vehicleData.Insurance = false;
            vehicleData.Registration = false;
            Utilities.SetVehicleData(lovvehicle.NetworkId,vehicleData);
            driver.AlwaysKeepTask = true;
            driver.BlockPermanentEvents = true;
            driver.Task.CruiseWithVehicle(lovvehicle,15f, 525116);
            lovvehicle.AttachBlip();
            driver.AttachBlip();
        }
        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }
    }
}
