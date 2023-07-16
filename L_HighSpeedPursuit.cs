using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;



namespace LocalAutoUnion404
{
    [CalloutProperties("Local High Speed Pursuit", "Valandria", "0.0.1")]
    public class HighSpeedPursuit : Callout
    {
        Ped lhspsuspect;
        Vehicle lhspvehicle;

        public HighSpeedPursuit()
        {
            Random random = new Random();
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(Vector3Extension.Around(Game.PlayerPed.Position, 200f))));
            ShortName = "High Speed Pursuit";
            CalloutDescription = "The lhspsuspect was spotted by traffic cameras after eluding police. Re-engage in the pursuit. Respond in Code 3.";
            ResponseCode = 3;
            StartDistance = 80f;
        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
            
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);

            var carlist = new[]
            {
                VehicleHash.Adder,
                VehicleHash.Coquette,
                VehicleHash.Coquette2,
                VehicleHash.Coquette3,
                VehicleHash.Cheetah,
                VehicleHash.Cheetah2,
                VehicleHash.Comet2,
                VehicleHash.Comet3
            };
            lhspvehicle = await SpawnVehicle(carlist[RandomUtils.Random.Next(carlist.Length)], Location);
            lhspsuspect = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            lhspsuspect.AlwaysKeepTask = true;
            lhspsuspect.BlockPermanentEvents = true;


            lhspsuspect.AttachBlip();
            Utilities.ExcludeVehicleFromTrafficStop(lhspvehicle.NetworkId, true);
            lhspsuspect.SetIntoVehicle(lhspvehicle, VehicleSeat.Driver);
            Pursuit.RegisterPursuit(lhspsuspect);
            lhspsuspect.Task.FleeFrom(Game.PlayerPed);
            Utilities.RequestBackup(Utilities.Backups.Code3);
            ShowNetworkedNotification("Pursuit engaged. Code 3 Backup requested.", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "Pursuit", 15f);
            lhspsuspect.DrivingSpeed = 230;
            lhspsuspect.DrivingStyle = DrivingStyle.Rushed;
            lhspvehicle.MaxSpeed = 330;

        }
    }


}
