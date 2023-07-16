using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;


namespace LocalAutoUnion404
{
    
    [CalloutProperties("Lcoal Stolen FD Vehicle", "Valandria", "0.0.3")]
    public class StolenFD : Callout
    {
        private Vehicle stolenfd;
        Ped fdthief;
        private string[] goodItemList = { "Open Soda Can", "Pack of Hotdogs", "Dog Food", "Empty Can", "Phone", "Cake", "Cup of Noodles", "Water Bottle", "Pack of Cards", "Outdated Insurance Card", "Pack of Pens", "Phone", "Tablet", "Computer", "Business Cards", "Taxi Business Card", "Textbooks", "Car Keys", "House Keys", "Keys", "Folder"};
        private string[] stolenfdList = { "17silv", "18fire", "2020expedition", "ftau", "3500hdambo", "ambulance", "f450ambo", "freightliner", "emsgator", "rescue", "rescue1", "rescue11", "sar250", "f550csquad", "f550swr", "f150bat", "arff5", "enforcer", "firetruk", "enforcerta", "mctanker", "bf350", "brush", "bulldog", "freightlinerwrecker", "isgtow" };
        public StolenFD()
        {

            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Stolen FD Vehicle";
            CalloutDescription = "Someone stole a vehicle from the Fire Department!";
            ResponseCode = 3;
            StartDistance = 250f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            fdthief = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            Random randomfd = new Random();
            string stolenfdtype = stolenfdList[randomfd.Next(stolenfdList.Length)];
            VehicleHash Hash = (VehicleHash)API.GetHashKey(stolenfdtype);
            stolenfd = await SpawnVehicle(Hash, Location);
            API.SetVehicleLights(stolenfd.GetHashCode(), 2);
            API.SetVehicleLightsMode(stolenfd.GetHashCode(), 2);
            fdthief.SetIntoVehicle(stolenfd, VehicleSeat.Driver);
            PedData data = new PedData();
            Random random3 = new Random();
            string name2 = goodItemList[random3.Next(goodItemList.Length)];
            List<Item> items = new List<Item>();
            Item goodItem = new Item {
                Name = name2,
                IsIllegal = false
            };
            items.Add(goodItem);
            data.Items = items;
            Utilities.SetPedData(fdthief.NetworkId,data);
            //Car Data
            VehicleData vehicleData = await Utilities.GetVehicleData(stolenfd.NetworkId);
            Utilities.SetVehicleData(stolenfd.NetworkId,vehicleData);
            Utilities.ExcludeVehicleFromTrafficStop(stolenfd.NetworkId,true);
            fdthief.AlwaysKeepTask = true;
            fdthief.BlockPermanentEvents = true;
            
            API.SetDriveTaskMaxCruiseSpeed(fdthief.GetHashCode(), 30f);
            API.SetDriveTaskDrivingStyle(fdthief.GetHashCode(), 524852);
            fdthief.Task.FleeFrom(player);
            stolenfd.AttachBlip();
            fdthief.AttachBlip();
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspect is fleeing!");
            Pursuit.RegisterPursuit(fdthief);
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
    }
}