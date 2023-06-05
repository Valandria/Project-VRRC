using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;




namespace StoreRobberyNCCallout
{
    [CalloutProperties("NC Store Robbery", "Valandria", "0.0.1")]
    public class StoreRobberyNC : Callout
    {
        Ped driver, suspect2, suspect3;
        private Vehicle getaway;

        private Vector3[] coordinates = {
            new Vector3(1729.41f,6414.47f,35.04f), // 24/7 - 3030
            new Vector3(1698.54f,4925.11f,42.06f), // 24/7 - 2013
            new Vector3(1961.84f,3741.17f,32.34f), // 24/7 - 1036
            new Vector3(547.27f,2671.36f,42.16f), // 24/7 - 928
            new Vector3(2679f,3281.06f,55.24f), // 24/7 - 957
            new Vector3(1167.33f,2708.33f,38.16f), // Liquor Store - 940
            new Vector3(1391.97f,3604.27f,34.98f), // Liquor Store - 1016
            new Vector3(2.02f,6512.52f,31.88f), // Clothing Store - 3020
            new Vector3(1692.24f,4820.38f,42.06f), // Clothing Store - 2014
            new Vector3(615.65f,2759.4f,42.09f), // Clothing Store - 930
            new Vector3(1199.15f,2708.73f,38.22f), // Clothing Store - 940
            new Vector3(-1098.61f,2711.06f,19.11f), // Clothing Store - 1005
        };
        
        public StoreRobberyNC()
        {
            Vector3 location = coordinates.OrderBy(x => World.GetDistance(x, Game.PlayerPed.Position)).Skip(1).First();

            InitInfo(location);
            ShortName = "NC - Store Robbery";
            CalloutDescription = "Three suspects with weapons are robbing a store. Respond in Code 3.";
            ResponseCode = 3;
            StartDistance = 60f;
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
           };
            Random GangType = new Random();
            int Gang = GangType.Next(1, 100 + 1);
            if (Gang <= 25) // Lost Male Heavy
            {
                driver = await SpawnPed(PedHash.Lost01GMY, Location);
                suspect2 = await SpawnPed(PedHash.Lost02GMY, Location);
                suspect3 = await SpawnPed(PedHash.Lost03GMY, Location);
            }
            if (Gang >= 26 && Gang <= 50 ) // Lost Female Heavy
            {
                driver = await SpawnPed(PedHash.Lost01GFY, Location);
                suspect2 = await SpawnPed(PedHash.Vagos01GFY, Location);
                suspect3 = await SpawnPed(PedHash.Hippie01AFY, Location);
            }
            if (Gang >= 51 && Gang <= 75) // Meth Heads
            {
                driver = await SpawnPed(PedHash.Rurmeth01AFY, Location);
                suspect2 = await SpawnPed(PedHash.Methhead01AMY, Location);
                suspect3 = await SpawnPed(PedHash.Rurmeth01AMM, Location);
            }
            if (Gang >= 76 ) // Locals
            {
                driver = await SpawnPed(PedHash.Hillbilly01AMM, Location);
                suspect2 = await SpawnPed(PedHash.Hillbilly02AMM, Location);
                suspect3 = await SpawnPed(PedHash.Hiker01AFY, Location);
            }

            getaway = await SpawnVehicle(cars[RandomUtils.Random.Next(cars.Length)], World.GetNextPositionOnStreet(Location));
            driver.AlwaysKeepTask = false;
            driver.BlockPermanentEvents = true;
            suspect2.AlwaysKeepTask = false;
            suspect2.BlockPermanentEvents = true;
            suspect3.AlwaysKeepTask = false;
            suspect3.BlockPermanentEvents = true;

            driver.AttachBlip();
            suspect2.AttachBlip();
            suspect3.AttachBlip();
            getaway.AttachBlip();
            driver.Accuracy = 2;
            suspect2.Accuracy = 2;
            suspect3.Accuracy = 2;
            suspect2.ShootRate = 500;
            suspect3.ShootRate = 1000;
            driver.Armor = 6000;
            suspect2.Armor = 6000;
            suspect3.Armor = 6969;

            suspect2.Weapons.Give(WeaponHash.APPistol, 9999, true, true);
            suspect3.Weapons.Give(WeaponHash.PistolMk2, 9999, true, true);

            API.Wait(5000);
            driver.Task.EnterVehicle(getaway, VehicleSeat.Driver);
            suspect2.Task.EnterVehicle(getaway, VehicleSeat.Any);
            API.Wait(2000);
            suspect3.Task.EnterVehicle(getaway, VehicleSeat.Any);
            var sus3incar = suspect3.CurrentVehicle;
            Tick += DriveAway;

        }
        public override void OnCancelBefore()
        {
            Tick -= DriveAway;
            base.OnCancelBefore();
        }
        private async Task DriveAway()
        {
            if (suspect3.IsInVehicle()) {
                await BaseScript.Delay(2000);
                driver.Task.FleeFrom(Game.PlayerPed);
                suspect2.Task.ShootAt(Game.PlayerPed);
                suspect3.Task.ShootAt(Game.PlayerPed);
            }
            else
            {
                driver.Task.EnterVehicle(getaway, VehicleSeat.Driver);
                suspect2.Task.EnterVehicle(getaway, VehicleSeat.Driver);
                BaseScript.Delay(2000);
                suspect3.Task.EnterVehicle(getaway, VehicleSeat.Driver);
                return;
            }
        }





    }
    }



