using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;

namespace LocalAutoUnion404
{
    [CalloutProperties("Local Car Chasing Person", "Valandria", "0.0.1")]
    public class LocalCarChasePed : Callout
    {
        Ped lccpvictim, lccpsuspect;
        Vehicle lccpvehicle;
        
        public LocalCarChasePed()
        {
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(Vector3Extension.Around(Game.PlayerPed.Position, 200f))));

            Random lccpcalloutscenario = new Random();
            int lccpcalloutdecision = lccpcalloutscenario.Next(1, 100 + 1);
            if (lccpcalloutdecision <)
            {
                ShortName = "Car Chasing Someone";
                CalloutDescription = "Someone in a vehicle is chasing someone on foot.";
                ResponseCode = 2;
                StartDistance = 200f;
            }
            if (lccpcalloutdecision <)
            {
                ShortName = "Car Chasing Animal";
                CalloutDescription = "Someone in a vehicle is chasing an animal.";
                ResponseCode = 2;
                StartDistance = 200f;
            }

        }

        public async override Task OnAccept()
        {

            InitBlip();
            UpdateData();
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);

            var cars = new[]
          {
               VehicleHash.Burrito,
               VehicleHash.Burrito2,
               VehicleHash.Burrito3,
               VehicleHash.Burrito4,
               VehicleHash.Burrito5,
               VehicleHash.GBurrito,
               VehicleHash.GBurrito2,
               VehicleHash.Bison,
               VehicleHash.Dubsta,
               VehicleHash.Dubsta2,
               VehicleHash.Dubsta3,
               VehicleHash.Taco
           };

            lccpsuspect = await SpawnPed(RandomUtils.GetRandomPed(), World.GetNextPositionOnStreet(Location.Around(10f)));
            lccpvictim = await SpawnPed(RandomUtils.GetRandomPed(), World.GetNextPositionOnStreet(Location.Around(10f)));
            lccpvehicle = await SpawnVehicle(cars[RandomUtils.Random.Next(cars.Length)], lccpsuspect.Position + 2);
            lccpsuspect.AlwaysKeepTask = true;
            lccpvictim.AlwaysKeepTask = true;
            lccpsuspect.BlockPermanentEvents = true;
            lccpvictim.BlockPermanentEvents = true;
            lccpsuspect.IsPersistent = true;
            lccpvictim.IsPersistent = true;

            API.Wait(500);

            lccpsuspect.SetIntoVehicle(lccpvehicle, VehicleSeat.Driver);
            lccpsuspect.Task.VehicleChase(lccpvictim);
            lccpvictim.Task.FleeFrom(lccpsuspect);

            Blip susblip = lccpsuspect.AttachBlip();
            susblip.Sprite = BlipSprite.Enemy;
            susblip.Color = BlipColor.Red;

            Blip lccpvictimblip = lccpvictim.AttachBlip();
            lccpvictimblip.Sprite = BlipSprite.Player;
            lccpvictimblip.Color = BlipColor.Blue;

            Blip carb = lccpvehicle.AttachBlip();
            carb.Sprite = BlipSprite.PersonalVehicleCar;
            carb.Color = BlipColor.Red;

            API.Wait(500);
        }
    }


}