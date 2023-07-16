using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;

namespace LocalAutoUnion404
{

    [CalloutProperties("Local Hit and Run Bicycle", "Valandria", "0.0.1")]
    public class LocalHitAndRunBike : Callout
    {
        private Ped lhrbbiker, lhrbdriver;
        private Vehicle lhrbbike, lhrbvehicle;
        private string[] lhrbbikerpedsList = { "cyclist01", "cyclist01amy" };
        private string[] lhrbbikervehicleList = { "tribike", "tribike2", "tribike3" };
        private string[] lhrbvehicleList = { "adder", "carbonrs", "oracle", "oracle2", "phoenix", "vigero", "zentorno", "youga", "youga2", "sultan", "sultanrs", "sentinel", "sentinel2", "ruiner", "ruiner2", "ruiner3", "burrito", "burrio2", "burrito3", "gburrito", "bagger", "buffalo", "buffalo2", "comet2", "comet3", "felon", "stanier", "superd", "tailgater", "warrener", "stratum", "washington", "surge", "baller", "baller2", "baller4", "baller6", "bjxl", "calvacade", "calvacade2", "granger", "gresley", "huntley", "habanero", "mesa", "felon", "felon2", "zion", "zion2", "windsor", "windsor2", "buccaneer", "buccaneer2", "dominator", "faction", "faction2", "faction3", "gauntlet", "gauntlet2" };

        public LocalHitAndRunBike()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if (x <= 50)
            {
                InitInfo(World.GetNextPositionOnStreet(Vector3Extension.Around(Game.PlayerPed.Position, 400)));
            }
            else
            {
                InitInfo(World.GetNextPositionOnSidewalk(Vector3Extension.Around(Game.PlayerPed.Position, 400)));
            }


            ShortName = "L - Hit and Run Bicycle";
            CalloutDescription = "A Car has struck a bicycle and is fleeing. Respond code 3.";
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

            Random lhrbpeddecider = new Random();
            Random lhrbbikedecider = new Random();
            Random lhrbvehicledecider = new Random();
            string lhrbpedchoice = lhrbbikerpedsList[lhrbpeddecider.Next(lhrbbikerpedsList.Length)];
            string lhrbbikechoice = lhrbbikervehicleList[lhrbbikedecider.Next(lhrbbikervehicleList.Length)];
            string lhrbvehiclechoice = lhrbvehicleList[lhrbvehicledecider.Next(lhrbvehicleList.Length)];
            PedHash lhrbpedHash = (PedHash)API.GetHashKey(lhrbpedchoice);
            VehicleHash lhrbbikeHash = (VehicleHash)API.GetHashKey(lhrbbikechoice);
            VehicleHash lhrbvehicleHash = (VehicleHash)API.GetHashKey(lhrbvehiclechoice);

            lhrbbike = await SpawnVehicle(lhrbbikeHash, Location, 180);
            lhrbvehicle = await SpawnVehicle(lhrbvehicleHash, Location + 2);
            lhrbbike.Deform(Location, 10000, 100);

            lhrbbike.EngineHealth = 5;

            lhrbbike.BodyHealth = 1;
            lhrbvehicle.BodyHealth = 2;

            API.Wait(2);

            lhrbbiker = await SpawnPed(lhrbpedHash, Location + 5);
            lhrbdriver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 6, 180);

            lhrbbiker.AlwaysKeepTask = true;
            lhrbbiker.BlockPermanentEvents = true;

            lhrbdriver.AlwaysKeepTask = true;
            lhrbdriver.BlockPermanentEvents = true;

            lhrbdriver.SetIntoVehicle(lhrbvehicle, VehicleSeat.Driver);

            Utilities.ExcludeVehicleFromTrafficStop(lhrbbike.NetworkId, true);
            Utilities.ExcludeVehicleFromTrafficStop(lhrbvehicle.NetworkId, true);

            PlayerData playerData = Utilities.GetPlayerData();
            VehicleData datacar = await Utilities.GetVehicleData(lhrbvehicle.NetworkId);
            string CallSign = playerData.Callsign;
            string vehicleName = datacar.Name;
            string carColor = datacar.Color;
            ShowNetworkedNotification("~b~" + CallSign + ",~y~ the suspect is driving a " + carColor + " " + vehicleName + ".", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "Pursuit", 15f);

            lhrbbike.Deform(Location, 10000, 100);
            lhrbvehicle.Deform(Location, 10000, 100);
            lhrbbiker.AttachBlip();
            lhrbbike.AttachBlip();

            var pursuit = Pursuit.RegisterPursuit(lhrbdriver);
            lhrbdriver.Task.FleeFrom(lhrbbiker);
            lhrbdriver.DrivingSpeed = 250;
            lhrbdriver.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;
            lhrbvehicle.MaxSpeed = 250;
            lhrbvehicle.EngineTorqueMultiplier = 2;
        }
    }
}