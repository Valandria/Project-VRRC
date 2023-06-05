using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;




namespace StoreRobberySCCallout
{
    [CalloutProperties("SC Store Robbery", "Valandria", "0.0.2")]
    public class StoreRobberySC : Callout
    {
        Ped driver, suspect2, suspect3;
        private Vehicle getaway;

        private Vector3[] coordinates = {
            new Vector3(-622.39f,-230.8f,38.06f), // Vangelico Store - 697 
            new Vector3(26.34f,-1346.9f,29.5f), // 24/7 - 125
            new Vector3(-3039.61f,586.13f,7.91f), // 24/7 - 804
            new Vector3(-3242.65f,1001.58f,12.83f), // 24/7 - 905
            new Vector3(2557.24f,382.62f,108.62f), // 24/7 - 402
            new Vector3(374.67f,325.74f,103.57f), // 24/7 - 574
            new Vector3(-48.58f, -1756.77f, 29.42f), // LTD - 120
            new Vector3(1136.52f,-982.67f,46.42f), // Liquor Store - 449
            new Vector3(-1224.5f,-906.47f,12.33f), // Liquor Store - 333
            new Vector3(-1487.29f,-380.71f,40.16f), // Liquor Store - 635
            new Vector3(-2968.88f,389.93f,15.04f), // Liquor Store - 815
            new Vector3(77.47f,-1389.8f,29.38f), // Clothing Store - 134
            new Vector3(-711.71f,-155.57f,37.42f), // Clothing Store - 696
            new Vector3(-160.82f,-302.01f,39.73f), // Clothing Store - 539
            new Vector3(422.57f,-808.51f,29.49f), // Clothing Store - 208
            new Vector3(-819.06f,-1073.59f,11.33f), // Clothing Store - 354
            new Vector3(-1452.26f,-237.08f,49.81f), // Clothing Store - 644
            new Vector3(126.33f,-220.67f,54.56f), // Clothing Store - 583
            new Vector3(-1193f,772f,17.32f), // Clothing Store - 342
            new Vector3(-3170.34f,1047.28f,20.86f), // Clothing Store - 908
        };
        
        public StoreRobberySC()
        {
            Vector3 location = coordinates.OrderBy(x => World.GetDistance(x, Game.PlayerPed.Position)).Skip(1).First();

            InitInfo(location);
            ShortName = "SC - Store Robbery";
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
            if (Gang <= 12) // Ballas Male Heavy
            {
                driver = await SpawnPed(PedHash.BallaEast01GMY, Location);
                suspect2 = await SpawnPed(PedHash.BallaOrig01GMY, Location);
                suspect3 = await SpawnPed(PedHash.BallaSout01GMY, Location);
            }
            if (Gang >= 13 && Gang <= 24) // Ballas Female Heavy
            {
                driver = await SpawnPed(PedHash.Ballas01GFY, Location);
                suspect2 = await SpawnPed(PedHash.Ballas01GFY, Location);
                suspect3 = await SpawnPed(PedHash.BallaSout01GMY, Location);
            }
            if (Gang >= 25 && Gang <= 36) // Families Male Heavy
            {
                driver = await SpawnPed(PedHash.Famdnf01GMY, Location);
                suspect2 = await SpawnPed(PedHash.Families01GFY, Location);
                suspect3 = await SpawnPed(PedHash.Famfor01GMY, Location);
            }
            if (Gang >= 37 && Gang <= 48) // Families Female Heavy
            {
                driver = await SpawnPed(PedHash.Families01GFY, Location);
                suspect2 = await SpawnPed(PedHash.Famca01GMY, Location);
                suspect3 = await SpawnPed(PedHash.Families01GFY, Location);
            }
            if (Gang >= 49 && Gang <= 60) // Vagos Male Heavy
            {
                driver = await SpawnPed(PedHash.Vagos01GFY, Location);
                suspect2 = await SpawnPed(PedHash.SalvaGoon01GMY, Location);
                suspect3 = await SpawnPed(PedHash.SalvaGoon03GMY, Location);
            }
            if (Gang >= 61 && Gang <= 72) // Vagos Female Heavy
            {
                driver = await SpawnPed(PedHash.Vagos01GFY, Location);
                suspect2 = await SpawnPed(PedHash.MexGoon01GMY, Location);
                suspect3 = await SpawnPed(PedHash.Vagos01GFY, Location);
            }
            if (Gang >= 73 && Gang <= 84) // Ambient Male
            {
                driver = await SpawnPed(PedHash.Soucent01AMM, Location);
                suspect2 = await SpawnPed(PedHash.Dhill01AMY, Location);
                suspect3 = await SpawnPed(PedHash.Stlat02AMM, Location);
            }
            if (Gang >= 85) // Ambient Female
            {
                driver = await SpawnPed(PedHash.Hipster02AFY, Location);
                suspect2 = await SpawnPed(PedHash.Hipster04AFY, Location);
                suspect3 = await SpawnPed(PedHash.Indian01AFY, Location);
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



