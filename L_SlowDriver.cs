using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;


namespace LocalAutoUnion404
{
    
    [CalloutProperties("Local Slow Driver", "Valandria", "0.0.1")]
    public class SlowDriver : Callout
    {
        private Vehicle lsdvehicle;
        Ped driver;
        private string[] goodItemList = { "Open Soda Can", "Pack of Hotdogs", "Dog Food", "Empty Can", "Phone", "Cake", "Cup of Noodles", "Water Bottle", "Pack of Cards", "Outdated Insurance Card", "Pack of Pens", "Phone", "Tablet", "Computer", "Business Cards", "Taxi Business Card", "Textbooks", "Car Keys", "House Keys", "Keys", "Folder"};
        private string[] lsdvehicleList = { "speedo", "speedo2", "stanier", "stinger", "stingergt", "stratum", "stretch", "taco", "tornado", "tornado2", "tornado3", "tornado4", "tourbus", "vader", "voodoo2", "dune5", "youga", "taxi", "tailgater", "sentinel2", "sentinel", "sandking2", "sandking", "ruffian", "rumpo", "rumpo2", "oracle2", "oracle", "ninef2", "ninef", "minivan", "gburrito", "emperor2", "emperor"};
        public SlowDriver()
        {

            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Slow Driver";
            CalloutDescription = "A vehicle is driving at very slow speeds causing traffic jams.";
            ResponseCode = 3;
            StartDistance = 250f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            Random random = new Random();
            string lsdvehicletype = lsdvehicleList[random.Next(lsdvehicleList.Length)];
            VehicleHash Hash = (VehicleHash) API.GetHashKey(lsdvehicletype);
            lsdvehicle = await SpawnVehicle(Hash, Location);
            driver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            driver.SetIntoVehicle(lsdvehicle, VehicleSeat.Driver);

            //Driver Data
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.05;
            List<Item> items = new List<Item>();
            Item Meth = new Item {
                Name = "Bag of Meth",
                IsIllegal = true
            };
            items.Add(Meth);
            Random random3 = new Random();
            string name2 = goodItemList[random3.Next(goodItemList.Length)];
            Item goodItem = new Item {
                Name = name2,
                IsIllegal = false
            };
            items.Add(goodItem);
            data.Items = items;
            Utilities.SetPedData(driver.NetworkId,data);
            
            driver.AlwaysKeepTask = true;
            driver.BlockPermanentEvents = true;
            
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            VehicleData datalsdvehicle = await Utilities.GetVehicleData(lsdvehicle.NetworkId);
            string vehicleName = datalsdvehicle.Name;
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspects are driving a " + vehicleName + "!");
            
            driver.Task.CruiseWithVehicle(lsdvehicle, 2f, 387);
            lsdvehicle.AttachBlip();
            driver.AttachBlip();
            API.AddBlipForEntity(lsdvehicle.GetHashCode());
            API.AddBlipForEntity(driver.GetHashCode());
            PedData data1 = await Utilities.GetPedData(driver.NetworkId);
            string firstname = data1.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~Is that a bird? Wait... I think it's a car...", 5000);
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