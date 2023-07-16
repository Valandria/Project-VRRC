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
    
    [CalloutProperties("Local Drunk Driver Pursuit", "Valandria", "0.0.1")]
    public class DrunkDriver : Callout
    {

        private Vehicle lddvehicle;
        private Ped ldddriver;
        private string[] lddvehicleList = { "speedo", "speedo2", "stanier", "stinger", "stingergt", "stratum", "stretch", "taco", "tornado", "tornado2", "tornado3", "tornado4", "tourbus", "vader", "voodoo2", "dune5", "youga", "taxi", "tailgater", "sentinel2", "sentinel", "sandking2", "sandking", "ruffian", "rumpo", "rumpo2", "oracle2", "oracle", "ninef2", "ninef", "minivan", "gburrito", "emperor2", "emperor"};

        public DrunkDriver() {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Drunk Driver Pursuit";
            Random dispatch = new Random();
            int dispatchnote = dispatch.Next(1, 100 + 1);
            if (dispatchnote <= 25)
            {
                CalloutDescription = "A driver has been called in for reckless driving.";
            }
            else if (dispatchnote > 25 && dispatchnote <= 50)
            {
                CalloutDescription = "Reports of a possible DUI in progress in your area.";
            }
            else if (dispatchnote > 50  && dispatchnote <= 75)
            {
                CalloutDescription = "A driver has been reported for consuming what appears to be alcohol while driving.";
            }
            else if (dispatchnote > 75 && dispatchnote <= 99)
            {
                CalloutDescription = "Reports of a possible intoxicated driver.";
            }
            else if (dispatchnote >= 100)
            {
                CalloutDescription = "A driver has been called in for being unable to handle their liquor.";
            }
            ResponseCode = 3;
            StartDistance = 200f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            ldddriver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            Random random = new Random();
            string cartype = lddvehicleList[random.Next(lddvehicleList.Length)];
            VehicleHash Hash = (VehicleHash) API.GetHashKey(cartype);
            lddvehicle = await SpawnVehicle(Hash, Location);
            ldddriver.SetIntoVehicle(lddvehicle, VehicleSeat.Driver);
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~r~[DrunkCallouts] ~y~Officer ~b~" + displayName + ",~y~ the suspect is driving a " + cartype + "!");
            
            //Driver Data
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.18;
            List<Item> items = new List<Item>();
            Item BeerBottle = new Item {
                Name = "Pißwasser bottle, unopen",
                IsIllegal = false
            };
            Item DogCollar = new Item {
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
            Utilities.SetPedData(ldddriver.NetworkId,data);
            Utilities.ExcludeVehicleFromTrafficStop(lddvehicle.NetworkId,true);

            VehicleData vehdata = new VehicleData();
            List<Item> vehitems = new List<Item>();

            Random vehitemz = new Random();
            int vehcont = vehitemz.Next(1, 100 + 1);
            if (vehcont <= 10)
            {
                vehitems.Add(sixpack);
            }
            else if (vehcont > 10 && vehcont <= 20)
            {
                vehitems.Add(sixpackopen);
                vehitems.Add(EmptyCan);
                vehitems.Add(EmptyCan);
            }
            else if (vehcont > 20 && vehcont <= 30)
            {
                vehitems.Add(Tallboy);
                vehitems.Add(Tallboy);
                vehitems.Add(Tallboyopen);
                vehitems.Add(Tallboyempty);
            }
            else if (vehcont > 30 && vehcont <= 40)
            {
                vehitems.Add(Tallboy);
                vehitems.Add(Tallboyopen);
                vehitems.Add(sixpack);
            }
            else if (vehcont > 40 && vehcont <= 50)
            {
                vehitems.Add(Tallboy);
                vehitems.Add(Tallboyopen);
                vehitems.Add(Openbottle);
                vehitems.Add(EmptyBottle);
            }
            else if (vehcont > 50 && vehcont <= 60)
            {
                vehitems.Add(EmptyCan);
                vehitems.Add(BeerCan);
                vehitems.Add(sixpackopen);
                vehitems.Add(OpenCan);
            }
            else if (vehcont > 60 && vehcont <= 70)
            {
                vehitems.Add(EmptyBottle);
                vehitems.Add(EmptyBottle);
                vehitems.Add(EmptyBottle);
                vehitems.Add(EmptyBottle);
                vehitems.Add(EmptyCan);
                vehitems.Add(BeerCan);
            }
            else if (vehcont > 70 && vehcont <= 80)
            {
                vehitems.Add(BeerBottle);
                vehitems.Add(BeerBottle);
                vehitems.Add(EmptyBottle);
                vehitems.Add(EmptyBottle);
                vehitems.Add(EmptyBottle);
                vehitems.Add(Openbottle);
            }
            else if (vehcont > 80 && vehcont <= 90)
            {
                vehitems.Add(Tallboy);
                vehitems.Add(Tallboy);
                vehitems.Add(Tallboyopen);
                vehitems.Add(sixpackopen);
                vehitems.Add(EmptyCan);
                vehitems.Add(EmptyCan);
                vehitems.Add(EmptyCan);
                vehitems.Add(EmptyCan);
            }
            else if (vehcont > 90 && vehcont <= 100)
            {
                vehitems.Add(Tallboy);
                vehitems.Add(Tallboyopen);
                vehitems.Add(Openbottle);
                vehitems.Add(EmptyBottle);
                vehitems.Add(EmptyCan);
                vehitems.Add(EmptyCan);
                vehitems.Add(EmptyCan);
                vehitems.Add(Tallboyempty);
                vehitems.Add(Tallboyempty);
            }

                //Tasks
            ldddriver.AlwaysKeepTask = true;
            ldddriver.BlockPermanentEvents = true;
            API.SetPedIsDrunk(ldddriver.GetHashCode(), true);
            API.SetDriveTaskMaxCruiseSpeed(ldddriver.GetHashCode(), 35f);
            API.SetDriveTaskDrivingStyle(ldddriver.GetHashCode(), 524852);
            ldddriver.Task.FleeFrom(player);
            Notify("~o~Officer ~b~" + displayName + ",~o~ the driver is fleeing!");
            lddvehicle.AttachBlip();
            ldddriver.AttachBlip();
            PedData data1 = await Utilities.GetPedData(ldddriver.NetworkId);
            string firstname = data1.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~Is that a bird?", 5000);
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