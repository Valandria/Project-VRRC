using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;


namespace LocalAutoUnion404
{
    
    [CalloutProperties("Local Stolen Police Car", "Valandria", "0.0.3")]
    public class StolenPoliceCar : Callout
    {
        private Vehicle stolenleo;
        Ped driver;
        private string[] goodItemList = { "Open Soda Can", "Pack of Hotdogs", "Dog Food", "Empty Can", "Phone", "Cake", "Cup of Noodles", "Water Bottle", "Pack of Cards", "Outdated Insurance Card", "Pack of Pens", "Phone", "Tablet", "Computer", "Business Cards", "Taxi Business Card", "Textbooks", "Car Keys", "House Keys", "Keys", "Folder"};
        private string[] stolenleoList = { "cvpi", "pdimpala", "poldemonrb", "sahpmoto", "LSPD1", "LSPD2", "LSPD3", "LSPD4", "LSPD5", "LSPD6", "LSPD7", "LSPD8", "LSPD9", "LSPD10", "STBike", "umsrt", "umsrtb", "ford", "c3f150k9b", "pitbullbb", "policet" };
        public StolenPoliceCar()
        {

            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Stolen Police Car";
            CalloutDescription = "Someone stole a police car!";
            ResponseCode = 3;
            StartDistance = 250f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            driver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            Random randompd = new Random();
            string stolenleotype = stolenleoList[randompd.Next(stolenleoList.Length)];
            VehicleHash Hash = (VehicleHash)API.GetHashKey(stolenleotype);
            stolenleo = await SpawnVehicle(Hash, Location);
            API.SetVehicleLights(stolenleo.GetHashCode(), 2);
            API.SetVehicleLightsMode(stolenleo.GetHashCode(), 2);
            driver.SetIntoVehicle(stolenleo, VehicleSeat.Driver);
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
            Utilities.SetPedData(driver.NetworkId,data);
            //Car Data
            VehicleData vehicleData = await Utilities.GetVehicleData(stolenleo.NetworkId);
            Utilities.SetVehicleData(stolenleo.NetworkId,vehicleData);
            Utilities.ExcludeVehicleFromTrafficStop(stolenleo.NetworkId,true);
            driver.AlwaysKeepTask = true;
            driver.BlockPermanentEvents = true;
            
            API.SetDriveTaskMaxCruiseSpeed(driver.GetHashCode(), 30f);
            API.SetDriveTaskDrivingStyle(driver.GetHashCode(), 524852);
            driver.Task.FleeFrom(player);
            stolenleo.AttachBlip();
            driver.AttachBlip();
            dynamic playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspect is fleeing!");
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
    }
}