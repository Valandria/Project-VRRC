using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;


namespace LocalAutoUnion404
{
    
    [CalloutProperties("Local Reverse Vehicle", "Valandria", "0.0.1")]
    public class ReverseVehicle : Callout
    {
        private Vehicle lrcvehicle;
        Ped lrcdriver;
        private string[] lrcvehicleList = { "speedo", "speedo2", "stanier", "stinger", "stingergt", "stratum", "stretch", "taco", "tornado", "tornado2", "tornado3", "tornado4", "tourbus", "vader", "voodoo2", "dune5", "youga", "taxi", "tailgater", "sentinel2", "sentinel", "sandking2", "sandking", "ruffian", "rumpo", "rumpo2", "oracle2", "oracle", "ninef2", "ninef", "minivan", "gburrito", "emperor2", "emperor"};
        private string[] goodItemList = { "Open Soda Can", "Pack of Hotdogs", "Dog Food", "Empty Can", "Phone", "Cake", "Cup of Noodles", "Water Bottle", "Pack of Cards", "Outdated Insurance Card", "Pack of Pens", "Phone", "Tablet", "Computer", "Business Cards", "Taxi Business Card", "Textbooks", "Car Keys", "House Keys", "Keys", "Folder"};
        public ReverseVehicle()
        {

            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Car Driving in Reverse";
            CalloutDescription = "A car is driving in reverse.";
            ResponseCode = 3;
            StartDistance = 250f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            lrcdriver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            Random random = new Random();
            string lrcvehicletype = lrcvehicleList[random.Next(lrcvehicleList.Length)];
            VehicleHash Hash = (VehicleHash) API.GetHashKey(lrcvehicletype);
            lrcvehicle = await SpawnVehicle(Hash, Location);
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            VehicleData datalrcvehicle = await Utilities.GetVehicleData(lrcvehicle.NetworkId);
            string vehicleName = datalrcvehicle.Name;
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspects are driving a " + vehicleName + "!");
            //Driver Data
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.10;
            PedData.Drugs[] drugs = data.UsedDrugs; //TODO FIX THIS
            List<Item> items = new List<Item>();
            Random random3 = new Random();
            string name2 = goodItemList[random3.Next(goodItemList.Length)];
            Item goodItem = new Item {
                Name = name2,
                IsIllegal = false
            };
            items.Add(goodItem);
            data.Items = items;
            Utilities.SetPedData(lrcdriver.NetworkId,data);
            
            //Car Data
            VehicleData vehicleData = new VehicleData();
            vehicleData.Registration = false;
            Utilities.SetVehicleData(lrcvehicle.NetworkId,vehicleData);
            lrcdriver.AlwaysKeepTask = true;
            lrcdriver.BlockPermanentEvents = true;
            
            lrcdriver.Task.CruiseWithVehicle(lrcvehicle, 12f, 1923);
            lrcvehicle.AttachBlip();
            lrcdriver.AttachBlip();
            PedData data1 = await Utilities.GetPedData(lrcdriver.NetworkId);
            string firstname = data1.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~Why is everyone driving backwards?", 5000);
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