using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace LocalAutoUnion404
{
    
    [CalloutProperties("Local Drunk Biker", "Valandria", "0.0.1")]
    public class DrunkBiker : Callout
    {

        private Vehicle ldbbike;
        private Ped ldbbiker;

        public DrunkBiker() {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Drunk Biker";
            CalloutDescription = "A person is operating a bike while drunk.";
            ResponseCode = 3;
            StartDistance = 150f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            ldbbiker = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            ldbbike = await SpawnVehicle(VehicleHash.TriBike, Location);
            ldbbiker.SetIntoVehicle(ldbbike, VehicleSeat.Driver);
                
            //Driver Data
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.10;
            List<Item> items = new List<Item>();
            Item BeerBottle = new Item
            {
                Name = "Pißwasser bottle, unopen",
                IsIllegal = false
            };
            Item DogCollar = new Item
            {
                Name = "Dog Collar",
                IsIllegal = false
            };
            Item sixpack = new Item
            {
                Name = "Six pack of Pißwasser, unopen",
                IsIllegal = false
            };
            Item sixpackopen = new Item
            {
                Name = "Six pack of Pißwasser, open",
                IsIllegal = true
            };
            Item Tallboy = new Item
            {
                Name = "Pißwasser Tallboy, unopen",
                IsIllegal = false
            };
            Item Openbottle = new Item
            {
                Name = "Pißwasser bottle, open",
            };
            Item Tallboyopen = new Item
            {
                Name = "Pißwasser Tallboy, open",
                IsIllegal = true
            };
            Item EmptyBottle = new Item
            {
                Name = "Empty Pißwasser bottle",
                IsIllegal = true
            };
            Item BeerCan = new Item
            {
                Name = "Pißwasser can, unopen",
                IsIllegal = false
            };
            Item OpenCan = new Item
            {
                Name = "Pißwasser can, open",
                IsIllegal = false
            };
            Item EmptyCan = new Item
            {
                Name = "Empty Pißwasser can",
                IsIllegal = false
            };
            Item Tallboyempty = new Item
            {
                Name = "Empty Pißwasser Tallboy",
                IsIllegal = false
            };

            Random itemz = new Random();
            int pis = itemz.Next(1, 100 + 1);
            if (pis <= 50)
            {
                items.Add(Tallboy);
                items.Add(Tallboy);
                items.Add(Tallboyopen);
                items.Add(Tallboyempty);
            }
            else if (pis > 50)
            {
                items.Add(BeerBottle);
                items.Add(Openbottle);
                items.Add(EmptyBottle);
                items.Add(DogCollar);
            }
            data.Items = items;
            Utilities.SetPedData(ldbbiker.NetworkId, data);
            Utilities.ExcludeVehicleFromTrafficStop(ldbbike.NetworkId,true);

            //Tasks
            ldbbiker.AlwaysKeepTask = true;
            ldbbiker.BlockPermanentEvents = true;
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            API.SetPedIsDrunk(ldbbiker.GetHashCode(), true);
            API.SetDriveTaskMaxCruiseSpeed(ldbbiker.GetHashCode(), 35f);
            API.SetDriveTaskDrivingStyle(ldbbiker.GetHashCode(), 524852);
            ldbbiker.Task.FleeFrom(player);
            Notify("~o~Officer ~b~" + displayName + ",~o~ the biker is fleeing!");
            ldbbike.AttachBlip();
            ldbbiker.AttachBlip();
            PedData data1 = await Utilities.GetPedData(ldbbiker.NetworkId);
            string firstname = data1.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~Are those police lights?", 5000);
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