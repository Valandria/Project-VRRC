using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;


namespace LocalAutoUnion404
{
    
    [CalloutProperties("Local Reckless Driver", "Valandria", "0.0.1")]
    public class RecklessDriver : Callout
    {
        private Vehicle lrdvehicle;
        Ped driver;
        private string[] lrdvehicleList = { "speedo", "speedo2", "stanier", "stinger", "stingergt", "stratum", "stretch", "taco", "tornado", "tornado2", "tornado3", "tornado4", "tourbus", "vader", "voodoo2", "dune5", "youga", "taxi", "tailgater", "sentinel2", "sentinel", "sandking2", "sandking", "ruffian", "rumpo", "rumpo2", "oracle2", "oracle", "ninef2", "ninef", "minivan", "gburrito", "emperor2", "emperor"};
        private string[] badItemList = { "Beer Bottle", "Open Beer Can", "Wine Bottle", "Random Pills", "Needles"};
        private string[] goodItemList = { "Open Soda Can", "Pack of Hotdogs", "Dog Food", "Empty Can", "Phone", "Cake", "Cup of Noodles", "Water Bottle", "Pack of Cards", "Outdated Insurance Card", "Pack of Pens", "Phone", "Tablet", "Computer", "Business Cards", "Taxi Business Card", "Textbooks", "Car Keys", "House Keys", "Keys", "Folder"};

        public RecklessDriver()
        {

            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Reckless Driver";
            CalloutDescription = "A car is driving recklessly.";
            ResponseCode = 3;
            StartDistance = 200f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            Random random = new Random();
            string lrdvehicletype = lrdvehicleList[random.Next(lrdvehicleList.Length)];
            VehicleHash selectedHash = (VehicleHash) API.GetHashKey(lrdvehicletype);
            lrdvehicle = await SpawnVehicle(selectedHash, Location);
            driver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            driver.SetIntoVehicle(lrdvehicle, VehicleSeat.Driver);
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            VehicleData datalrdvehicle = await Utilities.GetVehicleData(lrdvehicle.NetworkId);
            string vehicleName = datalrdvehicle.Name;
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspects are driving a " + vehicleName + "!");
            
            //Driver Data
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.15;
            Random random2 = new Random();
            List<Item> items = new List<Item>();
            string name = badItemList[random2.Next(badItemList.Length)];
            Item badItem = new Item {
                Name = name,
                IsIllegal = true
            };
            Random random3 = new Random();
            string name2 = goodItemList[random3.Next(goodItemList.Length)];
            Item goodItem = new Item {
                Name = name2,
                IsIllegal = false
            };
            items.Add(badItem);
            items.Add(goodItem);
            data.Items = items;
            Utilities.SetPedData(driver.NetworkId,data);
            //Car Data
            VehicleData vehicleData = new VehicleData();
            vehicleData.Insurance = false;
            Utilities.SetVehicleData(lrdvehicle.NetworkId,vehicleData);
            driver.AlwaysKeepTask = true;
            driver.BlockPermanentEvents = true;
            driver.Task.CruiseWithVehicle(lrdvehicle, 25f, 525116);
            lrdvehicle.AttachBlip();
            driver.AttachBlip();
            PedData data1 = await Utilities.GetPedData(driver.NetworkId);
            string firstname = data1.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~Lets go! Full speed ahead!", 5000);
        }
        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }
        private void Notify(string message)
        {
            API.BeginTextCommandThefeedPost("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandThefeedPostTicker(false, true);
        }
        private void DrawSubtitle(string message, int duration)
        {
            API.BeginTextCommandPrint("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandPrint(duration, false);
        }
    }
}