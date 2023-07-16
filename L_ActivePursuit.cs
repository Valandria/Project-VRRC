using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;


namespace LocalAutoUnion404
{

    [CalloutProperties("Local Active Pursuit of Armed Suspects", "Valandria", "0.0.1")]
    public class PursuitCallout : Callout
    {
        private Vehicle apasvehicle;
        Ped driver;
        Ped passenger;
        private string[] apasvehicleList = { "speedo", "speedo2", "stanier", "stinger", "stingergt", "stratum", "stretch", "taco", "tornado", "tornado2", "tornado3", "tornado4", "tourbus", "vader", "voodoo2", "dune5", "youga", "taxi", "tailgater", "sentinel2", "sentinel", "sandking2", "sandking", "ruffian", "rumpo", "rumpo2", "oracle2", "oracle", "ninef2", "ninef", "minivan", "gburrito", "emperor2", "emperor"};

        public PursuitCallout()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Pursuit of Armed Suspects";
            CalloutDescription = "Suspects just robbed a person with weapons. They are fleeing.";
            ResponseCode = 3;
            StartDistance = 150f;
        }
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            driver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            passenger = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            Random random = new Random();
            string apasvehicletype = apasvehicleList[random.Next(apasvehicleList.Length)];
            VehicleHash Hash = (VehicleHash) API.GetHashKey(apasvehicletype);
            apasvehicle = await SpawnVehicle(Hash, Location);
            driver.SetIntoVehicle(apasvehicle, VehicleSeat.Driver);
            passenger.SetIntoVehicle(apasvehicle, VehicleSeat.Passenger);
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            VehicleData dataapasvehicle = await Utilities.GetVehicleData(apasvehicle.NetworkId);
            string vehicleName = dataapasvehicle.Name;
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspects are driving a " + vehicleName + "!");
            
            //Driver Data
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.01;
            List<Item> items = new List<Item>();
            Item SMG = new Item {
                Name = "SMG",
                IsIllegal = true
            };
            items.Add(SMG);
            data.Items = items;
            Utilities.SetPedData(driver.NetworkId, data);
            
            //Passenger Data
            PedData data2 = new PedData();
            List<Item> items2 = data.Items;
            data2.BloodAlcoholLevel = 0.09;
            Item Pistol = new Item {
                Name = "Pistol",
                IsIllegal = true
            };
            items2.Add(Pistol);
            data2.Items = items2;
            Utilities.SetPedData(passenger.NetworkId, data2);
            
            //Car Data
            VehicleData vehicleData = new VehicleData();
            vehicleData.Registration = false;
            Utilities.SetVehicleData(apasvehicle.NetworkId,vehicleData);
            Utilities.ExcludeVehicleFromTrafficStop(apasvehicle.NetworkId,true);
            
            //Tasks
            driver.AlwaysKeepTask = true;
            driver.BlockPermanentEvents = true;
            passenger.AlwaysKeepTask = true;
            passenger.BlockPermanentEvents = true;
            
            passenger.Weapons.Give(WeaponHash.Pistol, 20, true, true);
            driver.Weapons.Give(WeaponHash.SMG, 30, true, true);
            API.SetDriveTaskMaxCruiseSpeed(driver.GetHashCode(), 35f);
            API.SetDriveTaskDrivingStyle(driver.GetHashCode(), 524852);
            driver.Task.FleeFrom(player);
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspects are fleeing!");
            apasvehicle.AttachBlip();
            driver.AttachBlip();
            passenger.AttachBlip();
            API.Wait(6000);
            passenger.Task.FightAgainst(player);
            PedData data3 = await Utilities.GetPedData(passenger.NetworkId);
            PedData data1 = await Utilities.GetPedData(driver.NetworkId);
            string firstname2 = data3.FirstName;
            string firstname = data1.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~I hate cops! Let me kill you!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~Do not shoot!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~To late!", 5000);
            Pursuit.RegisterPursuit(driver);
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