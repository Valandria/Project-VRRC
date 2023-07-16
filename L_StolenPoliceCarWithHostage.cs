using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

// To be merged with stolen police car (name change tbd).
namespace LocalAutoUnion404
{
    
    [CalloutProperties("Local Stolen Police Car (Hostage) Callout", "Valandria", "0.0.3")]
    public class StolenPoliceCarHostage : Callout
    {
        private Vehicle car;
        Ped driver, police;
        private string[] goodItemList = { "Open Soda Can", "Pack of Hotdogs", "Dog Food", "Empty Can", "Phone", "Cake", "Cup of Noodles", "Water Bottle", "Pack of Cards", "Outdated Insurance Card", "Pack of Pens", "Phone", "Tablet", "Computer", "Business Cards", "Taxi Business Card", "Textbooks", "Car Keys", "House Keys", "Keys", "Folder"};
        private string[] carList = { "cvpi", "pdimpala", "poldemonrb", "sahpmoto", "LSPD1", "LSPD2", "LSPD3", "LSPD4", "LSPD5", "LSPD6", "LSPD7", "LSPD8", "LSPD9", "LSPD10", "STBike", "umsrt", "umsrtb", "ford", "c3f150k9b", "pitbullbb", "policet" };
        public StolenPoliceCarHostage()
        {

            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Stolen Police Car (With Hostage)";
            CalloutDescription = "Someone stole a police car and took an officer hostage!";
            ResponseCode = 3;
            StartDistance = 250f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            driver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            police = await SpawnPed(PedHash.Hwaycop01SMY, Location + 1);
            Random randompd = new Random();
            string cartype = carList[randompd.Next(carList.Length)];
            VehicleHash Hash = (VehicleHash)API.GetHashKey(cartype);
            car = await SpawnVehicle(Hash, Location);
            API.SetVehicleLights(car.GetHashCode(), 2);
            API.SetVehicleLightsMode(car.GetHashCode(), 2);
            driver.SetIntoVehicle(car, VehicleSeat.Driver);
            police.SetIntoVehicle(car, VehicleSeat.Passenger);
            driver.Weapons.Give(WeaponHash.Pistol, 20, true, true);
            PedData data = new PedData();
            Random random3 = new Random();
            string name2 = goodItemList[random3.Next(goodItemList.Length)];
            List<Item> items = new List<Item>();
            Item goodItem = new Item {
                Name = name2,
                IsIllegal = false
            };
            Item Pistol = new Item {
                Name = "Pistol",
                IsIllegal = false
            };
            items.Add(Pistol);
            items.Add(goodItem);
            data.Items = items;
            PedData.Drugs[] drugs = data.UsedDrugs; // Look into this further.
            Utilities.SetPedData(driver.NetworkId,data);
            //Car Data
            VehicleData vehicleData = await Utilities.GetVehicleData(car.NetworkId);
            Utilities.SetVehicleData(car.NetworkId,vehicleData);
            Utilities.ExcludeVehicleFromTrafficStop(car.NetworkId,true);
            driver.AlwaysKeepTask = true;
            driver.BlockPermanentEvents = true;
            
            API.SetDriveTaskMaxCruiseSpeed(driver.GetHashCode(), 30f);
            API.SetDriveTaskDrivingStyle(driver.GetHashCode(), 524852);
            driver.Task.FleeFrom(player);
            car.AttachBlip();
            driver.AttachBlip();
            police.AttachBlip();
            police.Task.HandsUp(1000000);
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspect is fleeing!");
            PedData data1 = await Utilities.GetPedData(driver.NetworkId);
            string firstname = data1.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~Stay quiet and don't say anything!", 5000);
            PedData data2 = await Utilities.GetPedData(police.NetworkId);
            string firstname2 = data2.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~Will do.... are you high?", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~Shut up!", 5000);
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