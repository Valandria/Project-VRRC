using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;

namespace LocalAutoUnion404
{
    [CalloutProperties("Local Hit and Run", "Valandria", "0.0.1")]
    public class LocalHitAndRun : Callout
    {
        private Ped lhrdriver1, lhrdriver2;
        private Vehicle lhrvehicle1, lhrvehicle2;
        private string[] lhrvehicleList = { "adder", "carbonrs", "oracle", "oracle2", "phoenix", "vigero", "zentorno", "youga", "youga2", "sultan", "sultanrs", "sentinel", "sentinel2", "ruiner", "ruiner2", "ruiner3", "burrito", "burrio2", "burrito3", "gburrito", "bagger", "buffalo", "buffalo2", "comet2", "comet3", "felon", "stanier", "superd", "tailgater", "warrener", "stratum", "washington", "surge", "baller", "baller2", "baller4", "baller6", "bjxl", "calvacade", "calvacade2", "granger", "gresley", "huntley", "habanero", "mesa", "felon", "felon2", "zion", "zion2", "windsor", "windsor2", "buccaneer", "buccaneer2", "dominator", "faction", "faction2", "faction3", "gauntlet", "gauntlet2" };
        public LocalHitAndRun()
        {

            InitInfo(World.GetNextPositionOnStreet(Vector3Extension.Around(Game.PlayerPed.Position, 400)));
            ShortName = "L - Hit and Run";
            CalloutDescription = "A vehicle collision has occured, and one of the drivers is fleeing. Respond code 3. ";
            ResponseCode = 3;
            StartDistance = 200f;
        }

        public override async Task OnAccept()
        {
            InitBlip(25);
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);

            Random lhrvehicledecider1 = new Random();
            Random lhrvehicledecider2 = new Random();
            string lhrvehiclechoice1 = lhrvehicleList[lhrvehicledecider1.Next(lhrvehicleList.Length)];
            VehicleHash lhrvehicleHash1 = (VehicleHash)API.GetHashKey(lhrvehiclechoice1);
            string lhrvehiclechoice2 = lhrvehicleList[lhrvehicledecider2.Next(lhrvehicleList.Length)];
            VehicleHash lhrvehicleHash2 = (VehicleHash)API.GetHashKey(lhrvehiclechoice2);
            lhrvehicle1 = await SpawnVehicle(lhrvehicleHash1, Location, 180);
            lhrvehicle2 = await SpawnVehicle(lhrvehicleHash2, Location + 2);
            lhrvehicle1.Deform(Location, 10000, 100);

            lhrvehicle1.EngineHealth = 5;

            lhrvehicle1.BodyHealth = 1;
            lhrvehicle2.BodyHealth = 1;

            API.Wait(100);

            lhrdriver1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 5);
            lhrdriver1.IsPersistent = true;
            lhrdriver2 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 6, 180);
            lhrdriver2.IsPersistent = true;

            lhrdriver1.AlwaysKeepTask = true;
            lhrdriver1.BlockPermanentEvents = true;

            lhrdriver2.AlwaysKeepTask = true;
            lhrdriver2.BlockPermanentEvents = true;

            lhrdriver2.SetIntoVehicle(lhrvehicle2, VehicleSeat.Driver);
            Random lhrdriver1pos = new Random();
            int lhrdriver1seat = lhrdriver1pos.Next(1, 100 + 1);
            if (lhrdriver1seat <= 50)
            {
                lhrdriver1.SetIntoVehicle(lhrvehicle1, VehicleSeat.Driver);
            };
            Random lhrdriver1obituary = new Random();
            int lhrdriver1grimreaperstyle = lhrdriver1obituary.Next(1, 100 + 1);
            if (lhrdriver1grimreaperstyle < 26)
            {
                lhrdriver1.Kill();
                Tick += WreckTrauma;
            }
            if (lhrdriver1grimreaperstyle >= 90)
            {
                lhrdriver1.Kill();
                lhrdriver2.Weapons.Give(WeaponHash.Pistol, 9999, true, true);
                lhrdriver2.Accuracy = 30;
                lhrdriver2.ShootRate = 500;
                lhrdriver2.Task.ShootAt(player, -1, FiringPattern.SingleShot);
                Tick += RoadRage;
            }

            Utilities.ExcludeVehicleFromTrafficStop(lhrvehicle1.NetworkId, true);
            Utilities.ExcludeVehicleFromTrafficStop(lhrvehicle2.NetworkId, true);

            PlayerData playerData = Utilities.GetPlayerData();
            PedData lhrdriver1data = await Utilities.GetPedData(lhrdriver1.NetworkId);
            VehicleData lhrvehicle1data = await Utilities.GetVehicleData(lhrvehicle1.NetworkId);
            VehicleData lhrvehicle2data = await Utilities.GetVehicleData(lhrvehicle2.NetworkId);
            string CallSign = playerData.Callsign;
            string vehicleName = lhrvehicle2data.Name;
            string carColor = lhrvehicle2data.Color;
            ShowNetworkedNotification("~b~" + CallSign + ",~y~ the suspect is driving a " + carColor + " " + vehicleName + ".", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "Pursuit", 50f);

            lhrvehicle1.Deform(Location, 10000, 100);
            lhrvehicle2.Deform(Location, 10000, 100);
            lhrdriver1.AttachBlip();
            lhrvehicle1.AttachBlip();
            lhrvehicle1.IsPersistent = true;
            lhrvehicle2.IsPersistent = true;

            var pursuit = Pursuit.RegisterPursuit(lhrdriver2);
            lhrdriver2.Task.FleeFrom(lhrdriver1);
            lhrdriver2.DrivingSpeed = 250;
            lhrdriver2.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;
            lhrvehicle2.MaxSpeed = 250;
            lhrvehicle2.EngineTorqueMultiplier = 2;
        }

        public async Task WreckTrauma()
        {
            Tick -= WreckTrauma;
            PedData lhrdriver1data = await Utilities.GetPedData(lhrdriver1.NetworkId);
            List<Item> lhrdriver1medinjuries = lhrdriver1data.Items;
            Item lhrheadbruise = new Item
            {
                Name = "Large bruise on forehead",
                IsIllegal = false
            };
            Item lhrseatbeltbruise = new Item
            {
                Name = "Bruise across chest from seatbelt",
                IsIllegal = false
            };
            Item lhrwristbruise = new Item
            {
                Name = "Bruise on wrist",
                IsIllegal = false
            };
            Item lhrearbleeding = new Item
            {
                Name = "Bleeding from ear",
                IsIllegal = true
            };

            Random lhrtypeoftrauma = new Random();
            int lhrtrauma = lhrtypeoftrauma.Next(1, 100 + 1);
            if (lhrtrauma >= 10)
            {
                lhrdriver1medinjuries.Add(lhrheadbruise);
            }
            if (lhrtrauma > 10 && lhrtrauma < 50)
            {
                lhrdriver1medinjuries.Add(lhrwristbruise);
            }
            if (lhrtrauma > 40 && lhrtrauma < 90)
            {
                lhrdriver1medinjuries.Add(lhrseatbeltbruise);
            }
            if (lhrtrauma >= 60 && lhrtrauma <= 80)
            {
                lhrdriver1medinjuries.Add(lhrearbleeding);
            }
        }
        public async Task RoadRage()
        {
            Tick -= RoadRage;
            PedData lhrdriver1data = await Utilities.GetPedData(lhrdriver1.NetworkId);
            List<Item> lhrdriver1medinjuries = lhrdriver1data.Items;
            Item lhrgunshotwoundchest = new Item
            {
                Name = "Gunshot wound to the chest",
                IsIllegal = true
            };
            Item lhrbleedingchest = new Item
            {
                Name = "Heavy bleeding from chest",
                IsIllegal = true
            };
            Item lhrgunshotwoundhead = new Item
            {
                Name = "Gunshot wound to the head",
                IsIllegal = true
            };
            Item lhrbleedinghead = new Item
            {
                Name = "Heavy bleeding from head",
                IsIllegal = true
            };

            float lhrbulletholeidentification = Game.PlayerPed.Position.DistanceTo(lhrvehicle1.Position);
            if (lhrbulletholeidentification < 15f)
            {
                ShowDialog("There appears to be several small caliber bullet holes in the vehicle.", 10000, 15f);
            }
        }
    }
}