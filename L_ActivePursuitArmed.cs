using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;


namespace LocalAutoUnion404
{

    [CalloutProperties("Local Active Pursuit of Armed Suspects", "Valandria", "0.0.1")]
    public class ActivePursuitArmedCallout : Callout
    {
        private Vehicle apasvehicle;
        Ped apasdriver;
        Ped apaspassenger;
        private string[] apasvehicleList = { "speedo", "speedo2", "stanier", "stinger", "stingergt", "stratum", "stretch", "taco", "tornado", "tornado2", "tornado3", "tornado4", "tourbus", "vader", "voodoo2", "dune5", "youga", "taxi", "tailgater", "sentinel2", "sentinel", "sandking2", "sandking", "ruffian", "rumpo", "rumpo2", "oracle2", "oracle", "ninef2", "ninef", "minivan", "gburrito", "emperor2", "emperor"};

        public ActivePursuitArmedCallout()
        {
            Random apaslocation = new Random();
            float apasoffsetX = apaslocation.Next(100, 700);
            float apasoffsetY = apaslocation.Next(100, 700);
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(apasoffsetX, apasoffsetY, 0))));
            ShortName = "L - Pursuit of Armed Suspects";
            CalloutDescription = "Reports of a fleeing robbery suspect nearby, confirmed armed, intercept code 3!";
            ResponseCode = 3;
            StartDistance = 150f;
        }
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            apasdriver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            apaspassenger = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            Random random = new Random();
            string apasvehicletype = apasvehicleList[random.Next(apasvehicleList.Length)];
            VehicleHash Hash = (VehicleHash) API.GetHashKey(apasvehicletype);
            apasvehicle = await SpawnVehicle(Hash, Location);
            apasdriver.SetIntoVehicle(apasvehicle, VehicleSeat.Driver);
            apaspassenger.SetIntoVehicle(apasvehicle, VehicleSeat.Passenger);
            VehicleData dataapasvehicle = await Utilities.GetVehicleData(apasvehicle.NetworkId);
            string apasvehicleName = dataapasvehicle.Name;
            Notify("Suspects have been reported to be fleeing in a " + apasvehicleName + "!");
            
            //Car Data
            VehicleData apasvehicleData = new VehicleData();
            apasvehicleData.Registration = false;
            Utilities.SetVehicleData(apasvehicle.NetworkId,apasvehicleData);
            Utilities.ExcludeVehicleFromTrafficStop(apasvehicle.NetworkId,true);
            apasvehicle.IsPersistent = true;
            List<Item> apasitems3 = new List<Item>();

            //Tasks
            apasdriver.AlwaysKeepTask = true;
            apasdriver.BlockPermanentEvents = true;
            apaspassenger.AlwaysKeepTask = true;
            apaspassenger.BlockPermanentEvents = true;
            apasdriver.IsPersistent = true;
            apaspassenger.IsPersistent = true;
            
            apaspassenger.Weapons.Give(WeaponHash.Pistol, 20, true, true);
            apasdriver.Weapons.Give(WeaponHash.SMG, 30, true, true);
            apasdriver.DrivingSpeed = 200;
            apasdriver.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;
            apasvehicle.EnginePowerMultiplier = 2;
            apasdriver.Task.FleeFrom(player);
            Notify("Suspects are actively shooting at law enforcement!");
            apasvehicle.AttachBlip();
            apasdriver.AttachBlip();
            apaspassenger.AttachBlip();
            API.Wait(6000);
            apaspassenger.Task.FightAgainst(player);
            PedData data3 = await Utilities.GetPedData(apaspassenger.NetworkId);
            PedData data1 = await Utilities.GetPedData(apasdriver.NetworkId);
            string firstname2 = data3.FirstName;
            string firstname = data1.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~I hate cops! Let me kill you!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~Do not shoot!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~To late!", 5000);
            Pursuit.RegisterPursuit(apasdriver);

            //Driver Data
            PedData apasdriverdata = new PedData();
            apasdriverdata.BloodAlcoholLevel = 0.01;
            List<Item> apasitems = new List<Item>();
            Item SMG = new Item
            {
                Name = "SMG",
                IsIllegal = true
            };
            apasitems.Add(SMG);
            Utilities.SetPedData(apasdriver.NetworkId, apasdriverdata);

            //Passenger Data
            PedData apaspassengerdata = new PedData();
            List<Item> apasitems2 = apaspassengerdata.Items;
            apaspassengerdata.BloodAlcoholLevel = 0.09;
            Item Pistol = new Item
            {
                Name = "Pistol",
                IsIllegal = true
            };
            apasitems2.Add(Pistol);
            Utilities.SetPedData(apaspassenger.NetworkId, apaspassengerdata);

            Random apasextraillegalthingsdecider = new Random();
            int apasextraillegalthingsdecision = apasextraillegalthingsdecider.Next(1, 20);
            if (apasextraillegalthingsdecision <= 5)
            {
                //Drugs
                Item apascrackpipedriverleftpocket = new Item
                {
                    Name = "Crack pipe",
                    IsIllegal = true
                };
                Item apaslighterinconsole = new Item
                {
                    Name = "Lighter inside console",
                    IsIllegal = false
                };
                Item apascrackfoilunderdriverseat = new Item
                {
                    Name = "Crack rock inside foil under driver seat",
                    IsIllegal = true
                };

                apasitems.Add(apascrackpipedriverleftpocket);
                apasitems3.Add(apaslighterinconsole);
                apasitems3.Add(apascrackfoilunderdriverseat);
                apasdriverdata.UsedDrugs[0] = new PedData.Drugs();
                apaspassengerdata.UsedDrugs[0] = new PedData.Drugs();

            };
            if (apasextraillegalthingsdecision > 5 && apasextraillegalthingsdecision <= 10)
            {
                //Alcohol
                Item apasemptybeercansonfloorboard = new Item
                {
                    Name = "Several cans of empty beer on floorboard",
                    IsIllegal = true,
                };
                Item apasopenbeercanincupholder = new Item
                {
                    Name = "Open can of beer in cup holder",
                    IsIllegal = true,
                };

                apasitems3.Add(apasemptybeercansonfloorboard);
                apasitems3.Add(apasopenbeercanincupholder);
                apasdriverdata.BloodAlcoholLevel = 0.11;
                apaspassengerdata.BloodAlcoholLevel = 0.09;
            };
            if (apasextraillegalthingsdecision > 10 && apasextraillegalthingsdecision <= 15)
            {
                //Weapons
                Item apascacheoflongrifles = new Item
                {
                    Name = "Several rifles with serial numbers scratched off",
                    IsIllegal = true,
                };
                Item apasstockpileofammo = new Item
                {
                    Name = "Unmarked metal crate filled with ammo",
                    IsIllegal = false,
                };

                apasitems3.Add(apascacheoflongrifles);
                apasitems3.Add(apasstockpileofammo);
            };
            if (apasextraillegalthingsdecision > 15)
            {
                //Warrant
            };

            apasdriverdata.Items = apasitems;
            apaspassengerdata.Items = apasitems2;
            apasvehicleData.Items = apasitems3;
            Utilities.SetPedData(apasdriver.NetworkId, apasdriverdata);
            Utilities.SetPedData(apaspassenger.NetworkId, apaspassengerdata);
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