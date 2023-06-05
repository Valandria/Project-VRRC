using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;



namespace Code1000
{
    [CalloutProperties("Northern Command Stolen Plane", "Valandria", "0.1.0")]
    public class StolenPlane : Callout
    {
        Ped pilot;
        Vehicle plane;
        private Vector3[] coordinates = {

        new Vector3(-1829.458f, 2976.475f, 32.80999f), 
        new Vector3(-2146.112f, 3244.762f, 32.39441f), 
        new Vector3(-2136.841f, 3093.844f, 32.39064f), 
        new Vector3(-2255.839f, 3180.582f, 32.39298f), 
        new Vector3(-2607.074f, 3300.446f, 32.3767f), 
        new Vector3(-1977.54f, 2839.883f, 32.393f), 
        new Vector3(1750.61f, 3261.696f, 40.90128f), 
        new Vector3(1728.01f, 3315.628f, 40.57636f), 
        new Vector3(1394.2f, 3100.044f, 39.69923f), 
        new Vector3(1080.925f, 3017.436f, 40.54663f), 
        new Vector3(2092.452f, 4792.613f, 41.06519f), 
        new Vector3(2129.059f, 4793.998f, 41.12133f), 

        };

        public StolenPlane()
        {
            Vector3 location = coordinates.OrderBy(x => World.GetDistance(x, Game.PlayerPed.Position)).Skip(2).First();

            InitInfo(location);
            ShortName = "NC - Stolen Plane";
            CalloutDescription = "A Plane has been stolen. Recover it without injuring anybody. Respond Code 2.";
            ResponseCode = 2;
            StartDistance = 100f;
        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
            var carlist = new[]
            {
                VehicleHash.Cuban800,
                VehicleHash.Luxor,
                VehicleHash.Luxor2,
                VehicleHash.Dodo,
                VehicleHash.Duster

            };
            plane = await SpawnVehicle(carlist[RandomUtils.Random.Next(carlist.Length)], Location);
            pilot = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            pilot.AlwaysKeepTask = true;
            pilot.BlockPermanentEvents = true;
            plane.AttachBlip();
            pilot.SetIntoVehicle(plane, VehicleSeat.Driver);



        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);
            pilot.AttachBlip();
            pilot.Task.DriveTo(plane, new Vector3(1000f, 1000f, 1000f), 100, 200, ((int)DrivingStyle.Rushed));

        }
    }


}
