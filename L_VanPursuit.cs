using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace LocalVanPursuitCallout
{

    [CalloutProperties("Local Pursuit of Armed Suspects (Van)", "Valandria", "0.0.2")]
    public class VanPursuit : Callout
    {
        private Vehicle car;
        Ped driver, passenger, passenger2;
        
        public VanPursuit()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Pursuit of Armed Suspects (Van)";
            CalloutDescription = "Suspects just robbed a store with weapons. They are fleeing.";
            ResponseCode = 3;
            StartDistance = 150f;
        }
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            driver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            passenger = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            passenger2 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            car = await SpawnVehicle(VehicleHash.Speedo, Location);
            driver.SetIntoVehicle(car, VehicleSeat.Driver);
            passenger2.SetIntoVehicle(car, VehicleSeat.RightRear);
            passenger.SetIntoVehicle(car, VehicleSeat.LeftRear);
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspects are armed and dangerous!");
            //Driver Data

            PedData data = new PedData();
            List<Item> items = new List<Item>();

            data.BloodAlcoholLevel = 0.01;
            Item Pistol = new Item {
                Name = "Pistol",
                IsIllegal = false
            };
            items.Add(Pistol);
            data.Items = items;
            Utilities.SetPedData(driver.NetworkId,data);
            
            //Passenger Data 2
            PedData data2 = new PedData();
            data2.BloodAlcoholLevel = 0.01;
            Item SMG = new Item {
                Name = "SMG",
                IsIllegal = false
            };
            items.Add(SMG);
            data2.Items = items;
            Utilities.SetPedData(passenger.NetworkId,data2);
            
            //Passenger Data
            PedData data3 = new PedData();
            data3.BloodAlcoholLevel = 0.09;
            Item SMG2 = new Item {
                Name = "SMG",
                IsIllegal = false
            };
            items.Add(SMG2);
            data3.Items = items;
            Utilities.SetPedData(passenger2.NetworkId,data3);
            
            //Car Data
            VehicleData vehicleData = await Utilities.GetVehicleData(car.NetworkId);
            vehicleData.Insurance = false;
            Utilities.SetVehicleData(car.NetworkId,vehicleData);
            Utilities.ExcludeVehicleFromTrafficStop(car.NetworkId,true);
            
            //Tasks
            driver.AlwaysKeepTask = true;
            driver.BlockPermanentEvents = true;
            passenger.AlwaysKeepTask = true;
            passenger.BlockPermanentEvents = true;
            passenger2.AlwaysKeepTask = true;
            passenger2.BlockPermanentEvents = true;
            
            driver.Weapons.Give(WeaponHash.Pistol, 20, true, true);
            passenger.Weapons.Give(WeaponHash.SMG, 150, true, true);
            passenger2.Weapons.Give(WeaponHash.SMG, 150, true, true);
            API.SetDriveTaskMaxCruiseSpeed(driver.GetHashCode(), 35f);
            API.SetDriveTaskDrivingStyle(driver.GetHashCode(), 524852);
            driver.Task.FleeFrom(player);
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspects are fleeing!");
            car.AttachBlip();
            driver.AttachBlip();
            passenger.AttachBlip();
            passenger2.AttachBlip();
            API.Wait(6000);
            passenger.Task.FightAgainst(player);
            passenger2.Task.FightAgainst(player);
            PedData data1 = await Utilities.GetPedData(driver.NetworkId);
            string firstname = data1.FirstName;
            PedData data4 = await Utilities.GetPedData(passenger.NetworkId);
            string firstname2 = data4.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~I hate cops! Let me kill you!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~FIRE!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~DIE!", 5000);
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