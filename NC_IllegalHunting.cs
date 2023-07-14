using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;

namespace RangersoftheWilderness.net
{
    [CalloutProperties("Northern Command Illegal Hunting", "Valandria", "0.0.1")]
    public class NC_IllegalHunting : Callout
    {
        private Ped ncihped1, ncihped2, ncihped3, ncihped4, ncihanimal;
        private Vehicle ncihvehicle;
        private string[] vehicleList = { "bison", "bison2", "bison3", "bobcatxl", "bodhi2", "dune", "duneloader", "mesa", "mesa2", "mesa3", "bifta" };
        private string[] methvehicleList = { "camper", "journey" };
        private string[] hunterpedlist = { "hunter", "huntercutscene", "taphillbilly", "hillbilly01amm", "hillbilly02amm" };
        private string[] suspiciouspedlist = { "methhead01amy", "methfemale01", "rurmeth01amm", "rurmeth01afy", "methmale01", "methhead01amy" };
        private string[] weaponlist = { "pumpshotgun", "assaultrifle", "carbinerifle", "tacticalrifle", "marksmanrifle" };
        private Vector3[] coordinates =
        {
            new Vector3(-498.13f, 2972.35f, 26.19f),
            new Vector3(-293.37f, 2956.16f, 29.17f),
            new Vector3(-156.96f, 2956.74f, 32.08f),
            new Vector3(-121.45f, 2734.77f, 59.59f),
            new Vector3(-254f, 3099.04f, 35.31f),
            new Vector3(-114.95f, 3187.59f, 37.49f),
            new Vector3(52.9f, 3332.51f, 36.96f),
            new Vector3(-215.73f, 3706.62f, 50.92f),
            new Vector3(-255.51f, 3940.47f, 43.02f),
            new Vector3(-616.09f, 3991.99f, 120.96f),
            new Vector3(-613.11f, 3994.81f, 121.23f),
            new Vector3(-1007.62f, 4232.86f, 109.43f),
            new Vector3(-1044.34f, 4267.75f, 111.3f),
            new Vector3(-1193.22f, 4302.6f, 77f),
            new Vector3(-1532.11f, 4200.3f, 71.52f),
            new Vector3(-1550.55f, 4232.18f, 68.55f),
            new Vector3(-1663.2f, 4239.88f, 81.38f),
            new Vector3(-1828.82f, 4419.14f, 47.68f),
            new Vector3(-1858.43f, 4428.88f, 48.65f),
            new Vector3(-1956.85f, 4442.65f, 35.73f),
            new Vector3(-1880.99f, 4510.86f, 25.85f),
        };

        public NC_IllegalHunting()
        {
            InitInfo(coordinates[RandomUtils.Random.Next(coordinates.Length)]);
            ShortName = "NC - Reports of Illegal Hunting";
            Random ncihscenario = new Random();
            int ncihscenariochoice = ncihscenario.Next(1, 100 + 1);
            if (ncihscenariochoice <= 26)
            {
                Tick += HuntersOnTheProwl;
                CalloutDescription = "Reports coming in of hunters near the roadway actively hunting, respond code 3.";
                ResponseCode = 3;
            };
            if (ncihscenariochoice >= 27 && ncihscenariochoice <= 51)
            {
                Tick += ComingUpEmpty;
                CalloutDescription = "Reports coming in of hunters near the roadway, respond code 2.";
                ResponseCode = 2;
            };
            if (ncihscenariochoice >= 52 && ncihscenariochoice <= 75)
            {
                Tick += SomethingSmellsMethy;
                CalloutDescription = "Reports of suspicious hunters near the roadway, respond code 2.";
                ResponseCode = 2;
            };
            if (ncihscenariochoice >= 76)
            {
                Tick += AbandonedVehicle;
                CalloutDescription = "Reports of a vehicle abandoned by hunters near the roadway, respond code 1";
                ResponseCode = 1;
            };
            StartDistance = 200f;
        }

        public override async Task OnAccept()
        {
            InitBlip(50);
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);

            API.Wait(1000);
            Random ncihvehicledecider = new Random();
            string ncihvehiclechoice = vehicleList[ncihvehicledecider.Next(vehicleList.Length)];
            VehicleHash ncihvehicleHash = (VehicleHash)API.GetHashKey(ncihvehiclechoice);
            ncihvehicle = await SpawnVehicle(ncihvehicleHash, Location, 0);
            ncihvehicle.IsPersistent = true;
        }

        public async Task HuntersOnTheProwl()
        {
            Tick -= HuntersOnTheProwl;
            
            Random hotphunter1decider = new Random();
            Random hotphunter2decider = new Random();
            Random hotphunter3decider = new Random();
            Random hotphunter4decider = new Random();
            string hotphunter1choice = hunterpedlist[hotphunter1decider.Next(hunterpedlist.Length)];
            string hotphunter2choice = hunterpedlist[hotphunter2decider.Next(hunterpedlist.Length)];
            string hotphunter3choice = hunterpedlist[hotphunter3decider.Next(hunterpedlist.Length)];
            string hotphunter4choice = hunterpedlist[hotphunter4decider.Next(hunterpedlist.Length)];
            PedHash hotphunter1pedHash = (PedHash)API.GetHashKey(hotphunter1choice);
            PedHash hotphunter2pedHash = (PedHash)API.GetHashKey(hotphunter2choice);
            PedHash hotphunter3pedHash = (PedHash)API.GetHashKey(hotphunter3choice);
            PedHash hotphunter4pedHash = (PedHash)API.GetHashKey(hotphunter4choice);
            ncihped1 = await SpawnPed(hotphunter1pedHash, Location + 3, 0);
            ncihped2 = await SpawnPed(hotphunter2pedHash, Location + 5, 0);
            ncihped3 = await SpawnPed(hotphunter3pedHash, Location + 2, 0);
            ncihped4 = await SpawnPed(hotphunter4pedHash, Location + 3, 0);

            ncihped1.AlwaysKeepTask = true;
            ncihped2.AlwaysKeepTask = true;
            ncihped3.AlwaysKeepTask = true;
            ncihped4.AlwaysKeepTask = true;
            ncihped1.BlockPermanentEvents = true;
            ncihped2.BlockPermanentEvents = true;
            ncihped3.BlockPermanentEvents = true;
            ncihped4.BlockPermanentEvents = true;

            API.Wait(500);

            Random hotpanimalnumber = new Random();
            int hotpanimaldecider = hotpanimalnumber.Next(1, 100 + 1);
            if (hotpanimaldecider <= 26)
            {
                ncihanimal = await SpawnPed(PedHash.MountainLion, Location);
                Tick += hotpmountainlionquestions;
            }
            if (hotpanimaldecider > 26 && hotpanimaldecider <= 51)
            {
                ncihanimal = await SpawnPed(PedHash.Boar, Location);
                Tick += hotpboarquestions;
            }
            if (hotpanimaldecider > 51 && hotpanimaldecider <= 76)
            {
                ncihanimal = await SpawnPed(PedHash.Coyote, Location);
                Tick += hotpcoyotequestions;
            }
            if (hotpanimaldecider > 76)
            {
                ncihanimal = await SpawnPed(PedHash.Deer, Location);
                Tick += hotpdeerquestions;
            }
            ncihanimal.Kill();

            API.Wait(500);

            Random hotphunter1weapondecider = new Random();
            Random hotphunter2weapondecider = new Random();
            Random hotphunter3weapondecider = new Random();
            Random hotphunter4weapondecider = new Random();
            string hotphunter1weaponchoice = weaponlist[hotphunter1weapondecider.Next(weaponlist.Length)];
            string hotphunter2weaponchoice = weaponlist[hotphunter2weapondecider.Next(weaponlist.Length)];
            string hotphunter3weaponchoice = weaponlist[hotphunter3weapondecider.Next(weaponlist.Length)];
            string hotphunter4weaponchoice = weaponlist[hotphunter4weapondecider.Next(weaponlist.Length)];
            WeaponHash hotphunter1weaponHash = (WeaponHash)API.GetHashKey(hotphunter1weaponchoice);
            WeaponHash hotphunter2weaponHash = (WeaponHash)API.GetHashKey(hotphunter2weaponchoice);
            WeaponHash hotphunter3weaponHash = (WeaponHash)API.GetHashKey(hotphunter3weaponchoice);
            WeaponHash hotphunter4weaponHash = (WeaponHash)API.GetHashKey(hotphunter4weaponchoice);
            ncihped1.Weapons.Give(hotphunter1weaponHash, 9999, true, true);
            ncihped2.Weapons.Give(hotphunter2weaponHash, 9999, true, true);
            ncihped3.Weapons.Give(hotphunter3weaponHash, 9999, true, true);
            ncihped4.Weapons.Give(hotphunter4weaponHash, 9999, true, true);

            ncihped1.AttachBlip();
            ncihped2.AttachBlip();
            ncihped3.AttachBlip();
            ncihped4.AttachBlip();

            API.Wait(500);

            PedData hotphunter1data = await Utilities.GetPedData(ncihped1.NetworkId);
            PedData hotphunter2data = await Utilities.GetPedData(ncihped2.NetworkId);
            PedData hotphunter3data = await Utilities.GetPedData(ncihped3.NetworkId);
            PedData hotphunter4data = await Utilities.GetPedData(ncihped4.NetworkId);
            PedData hotpanimaldata = await Utilities.GetPedData(ncihanimal.NetworkId);
            VehicleData hotpvehicledata = await Utilities.GetVehicleData(ncihvehicle.NetworkId);

            string hotphunter1firstname = hotphunter1data.FirstName;
            string hotphunter2firstname = hotphunter2data.FirstName;
            string hotphunter3firstname = hotphunter3data.FirstName;
            string hotphunter4firstname = hotphunter4data.FirstName;

            PedQuestion hotphunter1q1 = new PedQuestion();
            hotphunter1q1.Question = "";
            hotphunter1q1.Answers = new List<string>
            {

            };

            API.Wait(500);

            List<Item> hotpdrinkoptions = new List<Item>();
            Item hotpbeerbottle = new Item
            {
                Name = "Pißwasser bottle, unopen",
                IsIllegal = false
            };
            Item hotpsixpack = new Item
            {
                Name = "Six pack of Pißwasser, unopen",
                IsIllegal = false
            };
            Item hotpsixpackopen = new Item
            {
                Name = "Six pack of Pißwasser, open",
                IsIllegal = true
            };
            Item hotptallboy = new Item
            {
                Name = "Pißwasser Tallboy, unopen",
                IsIllegal = false
            };
            Item hotpopenbottle = new Item
            {
                Name = "Pißwasser bottle, open",
                IsIllegal = true
            };
            Item hotptallboyopen = new Item
            {
                Name = "Pißwasser Tallboy, open",
                IsIllegal = true
            };
            Item hotpemptybottle = new Item
            {
                Name = "Empty Pißwasser bottle",
                IsIllegal = true
            };
            Item hotpbeercan = new Item
            {
                Name = "Pißwasser can, unopen",
                IsIllegal = false
            };
            Item hotpopencan = new Item
            {
                Name = "Pißwasser can, open",
                IsIllegal = true
            };
            Item hotpemptycan = new Item
            {
                Name = "Empty Pißwasser can",
                IsIllegal = true
            };
            Item hotptallboyempty = new Item
            {
                Name = "Empty Pißwasser Tallboy",
                IsIllegal = true
            };

            Random hotpdrinkingprobability = new Random();
            int hotpdrinkingchance = hotpdrinkingprobability.Next(1, 100 + 1);
            if (hotpdrinkingchance <= 15)
            {
                Tick += hotpdrinkingbuddies;
                hotphunter1data.BloodAlcoholLevel = 0.13;
                hotphunter2data.BloodAlcoholLevel = 0.08;
                hotphunter3data.BloodAlcoholLevel = 0.11;
                hotphunter4data.BloodAlcoholLevel = 0.05;

                hotpdrinkoptions.Add(hotpsixpackopen);
                hotpdrinkoptions.Add(hotpsixpack);
                hotpdrinkoptions.Add(hotpemptycan);
                hotpdrinkoptions.Add(hotpemptycan);
                hotpdrinkoptions.Add(hotpemptycan);
                hotpdrinkoptions.Add(hotpopencan);
                hotpdrinkoptions.Add(hotpopencan);
            }
            if (hotpdrinkingchance > 15 && hotpdrinkingchance <= 30)
            {
                Tick += hotpdrinkingbuddies;
                hotphunter1data.BloodAlcoholLevel = 0.09;
                hotphunter2data.BloodAlcoholLevel = 0.04;
                hotphunter3data.BloodAlcoholLevel = 0.08;
                hotphunter4data.BloodAlcoholLevel = 0.02;

                hotpdrinkoptions.Add(hotptallboyempty);
                hotpdrinkoptions.Add(hotptallboyopen);
                hotpdrinkoptions.Add(hotptallboyopen);
                hotpdrinkoptions.Add(hotptallboyopen);
                hotpdrinkoptions.Add(hotpsixpack);
            }

            hotpvehicledata.Items = hotpdrinkoptions;
            Utilities.SetVehicleData(ncihvehicle.NetworkId, hotpvehicledata);
        }

        public async Task hotpmountainlionquestions()
        {
            Tick -= hotpmountainlionquestions;

        }

        public async Task hotpboarquestions()
        {
            Tick -= hotpboarquestions;

        }

        public async Task hotpcoyotequestions()
        {
            Tick -= hotpcoyotequestions;

        }

        public async Task hotpdeerquestions()
        {
            Tick -= hotpdeerquestions;

        }

        public async Task hotpdrinkingbuddies()
        {
            Tick -= hotpdrinkingbuddies;
            
        }

        public async Task ComingUpEmpty()
        {
            Tick -= ComingUpEmpty;

        }

        public async Task SomethingSmellsMethy()
        {
            Tick -= SomethingSmellsMethy;

        }

        public async Task AbandonedVehicle()
        {
            Tick -= AbandonedVehicle;

        }

        public async Task AbandonedVehicleMurderMystery()
        {
            Tick -= AbandonedVehicleMurderMystery;

        }
    }
}
