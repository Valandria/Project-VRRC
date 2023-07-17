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
        private Vehicle ncihvehicle, ncihmethvehicle;
        private string[] vehicleList = { "bison", "bison2", "bison3", "bobcatxl", "bodhi2", "dune", "duneloader", "mesa", "mesa2", "mesa3", "bifta" };
        private string[] methvehicleList = { "camper", "journey" };
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
                CalloutDescription = "Reports coming in of hunters near the roadway actively hunting, respond code 2.";
                ResponseCode = 2;
            };
            if (ncihscenariochoice >= 27 && ncihscenariochoice <= 51)
            {
                CalloutDescription = "Reports coming in of hunters near the roadway, respond code 2.";
                ResponseCode = 2;
            };
            if (ncihscenariochoice >= 52 && ncihscenariochoice <= 75)
            {
                CalloutDescription = "Reports of suspicious hunters near the roadway, respond code 2.";
                ResponseCode = 2;
            };
            if (ncihscenariochoice >= 76)
            {
                CalloutDescription = "Reports of a vehicle possibly abandoned by hunters near the roadway, respond code 2";
                ResponseCode = 2;
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

            Random ncihvehicledecider = new Random();
            string ncihvehiclechoice = vehicleList[ncihvehicledecider.Next(vehicleList.Length)];
            VehicleHash ncihvehicleHash = (VehicleHash)API.GetHashKey(ncihvehiclechoice);
            ncihvehicle = await SpawnVehicle(ncihvehicleHash, Location, 0);
            ncihvehicle.IsPersistent = true;

            API.Wait(250);

            Random NCIHscenario = new Random();
            int NCIHscenariodecision = NCIHscenario.Next(1, 100 + 1);
            if (NCIHscenariodecision < 40)
            {
                Tick += HuntersOnTheProwl;
            }
            if (NCIHscenariodecision >= 40 && NCIHscenariodecision < 80)
            {
                Tick += ComingUpEmpty;
            }
            if (NCIHscenariodecision >= 80 && NCIHscenariodecision < 90)
            {
                Tick += SomethingSmellsMethy;
            }
            if (NCIHscenariodecision >= 90)
            {
                Tick += AbandonedVehicle;
            }
        }

        public async Task HuntersOnTheProwl()
        {
            Tick -= HuntersOnTheProwl;

            Random hotphunter1decider = new Random();
            API.Wait(250);
            Random hotphunter2decider = new Random();
            API.Wait(250);
            Random hotphunter3decider = new Random();
            API.Wait(250);
            Random hotphunter4decider = new Random();
            int hotphunter1choice = hotphunter1decider.Next(1, 50);
            int hotphunter2choice = hotphunter2decider.Next(1, 50);
            int hotphunter3choice = hotphunter3decider.Next(1, 50);
            int hotphunter4choice = hotphunter4decider.Next(1, 50);
            if (hotphunter1choice <= 10)
            {
                ncihped1 = await SpawnPed(PedHash.Hunter, Location + 3, 0);
            }
            if (hotphunter1choice > 10 && hotphunter1choice <= 20)
            {
                ncihped1 = await SpawnPed(PedHash.HunterCutscene, Location + 3, 0);
            }
            if (hotphunter1choice > 20 && hotphunter1choice <= 30)
            {
                ncihped1 = await SpawnPed(PedHash.Taphillbilly, Location + 3, 0);
            }
            if (hotphunter1choice > 30 && hotphunter1choice <= 40)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly01AMM, Location + 3, 0);
            }
            if (hotphunter1choice > 40 && hotphunter1choice <= 50)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly02AMM, Location + 3, 0);
            }
            if (hotphunter2choice <= 10)
            {
                ncihped1 = await SpawnPed(PedHash.Hunter, Location + 5, 0);
            }
            if (hotphunter2choice > 10 && hotphunter1choice <= 20)
            {
                ncihped1 = await SpawnPed(PedHash.HunterCutscene, Location + 5, 0);
            }
            if (hotphunter2choice > 20 && hotphunter1choice <= 30)
            {
                ncihped1 = await SpawnPed(PedHash.Taphillbilly, Location + 5, 0);
            }
            if (hotphunter2choice > 30 && hotphunter1choice <= 40)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly01AMM, Location + 5, 0);
            }
            if (hotphunter2choice > 40 && hotphunter1choice <= 50)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly02AMM, Location + 5, 0);
            }
            if (hotphunter3choice <= 10)
            {
                ncihped1 = await SpawnPed(PedHash.Hunter, Location + 2, 0);
            }
            if (hotphunter3choice > 10 && hotphunter1choice <= 20)
            {
                ncihped1 = await SpawnPed(PedHash.HunterCutscene, Location + 2, 0);
            }
            if (hotphunter3choice > 20 && hotphunter1choice <= 30)
            {
                ncihped1 = await SpawnPed(PedHash.Taphillbilly, Location + 2, 0);
            }
            if (hotphunter3choice > 30 && hotphunter1choice <= 40)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly01AMM, Location + 2, 0);
            }
            if (hotphunter3choice > 40 && hotphunter1choice <= 50)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly02AMM, Location + 2, 0);
            }
            if (hotphunter4choice <= 10)
            {
                ncihped1 = await SpawnPed(PedHash.Hunter, Location + 3, 0);
            }
            if (hotphunter4choice > 10 && hotphunter1choice <= 20)
            {
                ncihped1 = await SpawnPed(PedHash.HunterCutscene, Location + 3, 0);
            }
            if (hotphunter4choice > 20 && hotphunter1choice <= 30)
            {
                ncihped1 = await SpawnPed(PedHash.Taphillbilly, Location + 3, 0);
            }
            if (hotphunter4choice > 30 && hotphunter1choice <= 40)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly01AMM, Location + 3, 0);
            }
            if (hotphunter4choice > 40 && hotphunter1choice <= 50)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly02AMM, Location + 3, 0);
            }

            API.Wait(250);

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
                //Tick += hotpmountainlionquestions;
            }
            if (hotpanimaldecider > 26 && hotpanimaldecider <= 51)
            {
                ncihanimal = await SpawnPed(PedHash.Boar, Location);
                //Tick += hotpboarquestions;
            }
            if (hotpanimaldecider > 51 && hotpanimaldecider <= 76)
            {
                ncihanimal = await SpawnPed(PedHash.Coyote, Location);
                //Tick += hotpcoyotequestions;
            }
            if (hotpanimaldecider > 76)
            {
                ncihanimal = await SpawnPed(PedHash.Deer, Location);
                //Tick += hotpdeerquestions;
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

            float potpdrinkingsmellnotification = Game.PlayerPed.Position.DistanceTo(ncihvehicle.Position);
            Random hotpdrinkingprobability = new Random();
            int hotpdrinkingchance = hotpdrinkingprobability.Next(1, 100 + 1);
            if (hotpdrinkingchance <= 15)
            {
                //Tick += hotpdrinkingbuddies;
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

                if (potpdrinkingsmellnotification < 15f)
                {
                    ShowDialog("Heavy smell of alcohol from the vehicle", 10000, 10f);
                }
            }
            if (hotpdrinkingchance > 15 && hotpdrinkingchance <= 30)
            {
                //Tick += hotpdrinkingbuddies;
                hotphunter1data.BloodAlcoholLevel = 0.09;
                hotphunter2data.BloodAlcoholLevel = 0.04;
                hotphunter3data.BloodAlcoholLevel = 0.08;
                hotphunter4data.BloodAlcoholLevel = 0.02;

                hotpdrinkoptions.Add(hotptallboyempty);
                hotpdrinkoptions.Add(hotptallboyopen);
                hotpdrinkoptions.Add(hotptallboyopen);
                hotpdrinkoptions.Add(hotptallboyopen);
                hotpdrinkoptions.Add(hotpsixpack);

                if (potpdrinkingsmellnotification < 10f)
                {
                    ShowDialog("Heavy smell of alcohol from the vehicle", 10000, 10f);
                }
            }

            ncihped1.SetData(hotphunter1data);
            ncihped2.SetData(hotphunter2data);
            ncihped3.SetData(hotphunter3data);
            ncihped4.SetData(hotphunter4data);
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

            Random cuehunter1decider = new Random();
            API.Wait(250);
            Random cuehunter2decider = new Random();
            API.Wait(250);
            Random cuehunter3decider = new Random();
            API.Wait(250);
            Random cuehunter4decider = new Random();
            int cuehunter1choice = cuehunter1decider.Next(1, 50);
            int cuehunter2choice = cuehunter2decider.Next(1, 50);
            int cuehunter3choice = cuehunter3decider.Next(1, 50);
            int cuehunter4choice = cuehunter4decider.Next(1, 50);
            if (cuehunter1choice <= 10)
            {
                ncihped1 = await SpawnPed(PedHash.Hunter, Location + 3, 0);
            }
            if (cuehunter1choice > 10 && cuehunter1choice <= 20)
            {
                ncihped1 = await SpawnPed(PedHash.HunterCutscene, Location + 3, 0);
            }
            if (cuehunter1choice > 20 && cuehunter1choice <= 30)
            {
                ncihped1 = await SpawnPed(PedHash.Taphillbilly, Location + 3, 0);
            }
            if (cuehunter1choice > 30 && cuehunter1choice <= 40)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly01AMM, Location + 3, 0);
            }
            if (cuehunter1choice > 40 && cuehunter1choice <= 50)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly02AMM, Location + 3, 0);
            }
            if (cuehunter2choice <= 10)
            {
                ncihped2 = await SpawnPed(PedHash.Hunter, Location + 5, 0);
            }
            if (cuehunter2choice > 10 && cuehunter2choice <= 20)
            {
                ncihped2 = await SpawnPed(PedHash.HunterCutscene, Location + 5, 0);
            }
            if (cuehunter2choice > 20 && cuehunter2choice <= 30)
            {
                ncihped2 = await SpawnPed(PedHash.Taphillbilly, Location + 5, 0);
            }
            if (cuehunter2choice > 30 && cuehunter2choice <= 40)
            {
                ncihped2 = await SpawnPed(PedHash.Hillbilly01AMM, Location + 5, 0);
            }
            if (cuehunter2choice > 40 && cuehunter2choice <= 50)
            {
                ncihped2 = await SpawnPed(PedHash.Hillbilly02AMM, Location + 5, 0);
            }
            if (cuehunter3choice <= 10)
            {
                ncihped3 = await SpawnPed(PedHash.Hunter, Location + 2, 0);
            }
            if (cuehunter3choice > 10 && cuehunter3choice <= 20)
            {
                ncihped3 = await SpawnPed(PedHash.HunterCutscene, Location + 2, 0);
            }
            if (cuehunter3choice > 20 && cuehunter3choice <= 30)
            {
                ncihped3 = await SpawnPed(PedHash.Taphillbilly, Location + 2, 0);
            }
            if (cuehunter3choice > 30 && cuehunter3choice <= 40)
            {
                ncihped3 = await SpawnPed(PedHash.Hillbilly01AMM, Location + 2, 0);
            }
            if (cuehunter3choice > 40 && cuehunter3choice <= 50)
            {
                ncihped3 = await SpawnPed(PedHash.Hillbilly02AMM, Location + 2, 0);
            }
            if (cuehunter4choice <= 10)
            {
                ncihped4 = await SpawnPed(PedHash.Hunter, Location + 3, 0);
            }
            if (cuehunter4choice > 10 && cuehunter4choice <= 20)
            {
                ncihped4 = await SpawnPed(PedHash.HunterCutscene, Location + 3, 0);
            }
            if (cuehunter4choice > 20 && cuehunter4choice <= 30)
            {
                ncihped4 = await SpawnPed(PedHash.Taphillbilly, Location + 3, 0);
            }
            if (cuehunter4choice > 30 && cuehunter4choice <= 40)
            {
                ncihped4 = await SpawnPed(PedHash.Hillbilly01AMM, Location + 3, 0);
            }
            if (cuehunter4choice > 40 && cuehunter4choice <= 50)
            {
                ncihped4 = await SpawnPed(PedHash.Hillbilly02AMM, Location + 3, 0);
            }

            ncihped1.AlwaysKeepTask = true;
            ncihped2.AlwaysKeepTask = true;
            ncihped3.AlwaysKeepTask = true;
            ncihped4.AlwaysKeepTask = true;
            ncihped1.BlockPermanentEvents = true;
            ncihped2.BlockPermanentEvents = true;
            ncihped3.BlockPermanentEvents = true;
            ncihped4.BlockPermanentEvents = true;
            ncihped1.IsPersistent = true;
            ncihped2.IsPersistent = true;
            ncihped3.IsPersistent = true;
            ncihped4.IsPersistent = true;

            API.Wait(500);

            Random cuehunter1weapondecider = new Random();
            Random cuehunter2weapondecider = new Random();
            Random cuehunter3weapondecider = new Random();
            Random cuehunter4weapondecider = new Random();
            string cuehunter1weaponchoice = weaponlist[cuehunter1weapondecider.Next(weaponlist.Length)];
            string cuehunter2weaponchoice = weaponlist[cuehunter2weapondecider.Next(weaponlist.Length)];
            string cuehunter3weaponchoice = weaponlist[cuehunter3weapondecider.Next(weaponlist.Length)];
            string cuehunter4weaponchoice = weaponlist[cuehunter4weapondecider.Next(weaponlist.Length)];
            WeaponHash cuehunter1weaponHash = (WeaponHash)API.GetHashKey(cuehunter1weaponchoice);
            WeaponHash cuehunter2weaponHash = (WeaponHash)API.GetHashKey(cuehunter2weaponchoice);
            WeaponHash cuehunter3weaponHash = (WeaponHash)API.GetHashKey(cuehunter3weaponchoice);
            WeaponHash cuehunter4weaponHash = (WeaponHash)API.GetHashKey(cuehunter4weaponchoice);
            ncihped1.Weapons.Give(cuehunter1weaponHash, 9999, true, true);
            ncihped2.Weapons.Give(cuehunter2weaponHash, 9999, true, true);
            ncihped3.Weapons.Give(cuehunter3weaponHash, 9999, true, true);
            ncihped4.Weapons.Give(cuehunter4weaponHash, 9999, true, true);

            ncihped1.AttachBlip();
            ncihped2.AttachBlip();
            ncihped3.AttachBlip();
            ncihped4.AttachBlip();

            API.Wait(500);

            PedData cuehunter1data = await Utilities.GetPedData(ncihped1.NetworkId);
            PedData cuehunter2data = await Utilities.GetPedData(ncihped2.NetworkId);
            PedData cuehunter3data = await Utilities.GetPedData(ncihped3.NetworkId);
            PedData cuehunter4data = await Utilities.GetPedData(ncihped4.NetworkId);
            VehicleData cuevehicledata = await Utilities.GetVehicleData(ncihvehicle.NetworkId);

            string cuehunter1firstname = cuehunter1data.FirstName;
            string cuehunter2firstname = cuehunter2data.FirstName;
            string cuehunter3firstname = cuehunter3data.FirstName;
            string cuehunter4firstname = cuehunter4data.FirstName;

            API.Wait(500);

            List<Item> cuedrinkoptions = new List<Item>();
            Item cuebeerbottle = new Item
            {
                Name = "Pißwasser bottle, unopen",
                IsIllegal = false
            };
            Item cuesixpack = new Item
            {
                Name = "Six pack of Pißwasser, unopen",
                IsIllegal = false
            };
            Item cuesixpackopen = new Item
            {
                Name = "Six pack of Pißwasser, open",
                IsIllegal = true
            };
            Item cuetallboy = new Item
            {
                Name = "Pißwasser Tallboy, unopen",
                IsIllegal = false
            };
            Item cueopenbottle = new Item
            {
                Name = "Pißwasser bottle, open",
                IsIllegal = true
            };
            Item cuetallboyopen = new Item
            {
                Name = "Pißwasser Tallboy, open",
                IsIllegal = true
            };
            Item cueemptybottle = new Item
            {
                Name = "Empty Pißwasser bottle",
                IsIllegal = true
            };
            Item cuebeercan = new Item
            {
                Name = "Pißwasser can, unopen",
                IsIllegal = false
            };
            Item cueopencan = new Item
            {
                Name = "Pißwasser can, open",
                IsIllegal = true
            };
            Item cueemptycan = new Item
            {
                Name = "Empty Pißwasser can",
                IsIllegal = true
            };
            Item cuetallboyempty = new Item
            {
                Name = "Empty Pißwasser Tallboy",
                IsIllegal = true
            };

            float cuedrinkingsmellnotification = Game.PlayerPed.Position.DistanceTo(ncihvehicle.Position);
            Random cuedrinkingprobability = new Random();
            int cuedrinkingchance = cuedrinkingprobability.Next(1, 100 + 1);
            if (cuedrinkingchance <= 15)
            {
                //Tick += cuedrinkingbuddies;
                cuehunter1data.BloodAlcoholLevel = 0.13;
                cuehunter2data.BloodAlcoholLevel = 0.08;
                cuehunter3data.BloodAlcoholLevel = 0.11;
                cuehunter4data.BloodAlcoholLevel = 0.05;

                cuedrinkoptions.Add(cuesixpackopen);
                cuedrinkoptions.Add(cuesixpack);
                cuedrinkoptions.Add(cueemptycan);
                cuedrinkoptions.Add(cueemptycan);
                cuedrinkoptions.Add(cueemptycan);
                cuedrinkoptions.Add(cueopencan);
                cuedrinkoptions.Add(cueopencan);
            }
            if (cuedrinkingsmellnotification < 15f && cuedrinkingchance <= 15)
            {
                ShowDialog("Heavy smell of alcohol from the vehicle", 10000, 10f);
            }
            if (cuedrinkingchance > 15 && cuedrinkingchance <= 30)
            {
                //Tick += cuedrinkingbuddies;
                cuehunter1data.BloodAlcoholLevel = 0.09;
                cuehunter2data.BloodAlcoholLevel = 0.04;
                cuehunter3data.BloodAlcoholLevel = 0.08;
                cuehunter4data.BloodAlcoholLevel = 0.02;

                cuedrinkoptions.Add(cuetallboyempty);
                cuedrinkoptions.Add(cuetallboyopen);
                cuedrinkoptions.Add(cuetallboyopen);
                cuedrinkoptions.Add(cuetallboyopen);
                cuedrinkoptions.Add(cuesixpack);
            }
            if (cuedrinkingsmellnotification < 15f && cuedrinkingchance > 15 && cuedrinkingchance <= 30)
            {
                ShowDialog("Heavy smell of alcohol from the vehicle", 10000, 10f);
            }

            ncihped1.SetData(cuehunter1data);
            ncihped2.SetData(cuehunter2data);
            ncihped3.SetData(cuehunter3data);
            ncihped4.SetData(cuehunter4data);

            cuevehicledata.Items = cuedrinkoptions;
            Utilities.SetVehicleData(ncihvehicle.NetworkId, cuevehicledata);
        }

        public async Task SomethingSmellsMethy()
        {
            Tick -= SomethingSmellsMethy;

            Random methvehicledecider = new Random();
            string methvehiclechoice = methvehicleList[methvehicledecider.Next(methvehicleList.Length)];
            VehicleHash ncihmethvehicleHash = (VehicleHash)API.GetHashKey(methvehiclechoice);
            ncihmethvehicle = await SpawnVehicle(ncihmethvehicleHash, Location + 5, 0);
            ncihmethvehicle.IsPersistent = true;

            API.Wait(500);

            Random ssmhunter1decider = new Random();
            API.Wait(250);
            Random ssmhunter2decider = new Random();
            API.Wait(250);
            Random ssmmeth1decider = new Random();
            API.Wait(250);
            Random ssmmeth2decider = new Random();
            int ssmhunter1choice = ssmhunter1decider.Next(1, 50);
            int ssmhunter2choice = ssmhunter2decider.Next(1, 50);
            int ssmmeth1choice = ssmmeth1decider.Next(1, 50);
            int ssmmeth2choice = ssmmeth2decider.Next(1, 50);
            if (ssmhunter1choice <= 10)
            {
                ncihped1 = await SpawnPed(PedHash.Hunter, Location + 3, 0);
            }
            if (ssmhunter1choice > 10 && ssmhunter1choice <= 20)
            {
                ncihped1 = await SpawnPed(PedHash.HunterCutscene, Location + 3, 0);
            }
            if (ssmhunter1choice > 20 && ssmhunter1choice <= 30)
            {
                ncihped1 = await SpawnPed(PedHash.Taphillbilly, Location + 3, 0);
            }
            if (ssmhunter1choice > 30 && ssmhunter1choice <= 40)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly01AMM, Location + 3, 0);
            }
            if (ssmhunter1choice > 40 && ssmhunter1choice <= 50)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly02AMM, Location + 3, 0);
            }
            if (ssmhunter2choice <= 10)
            {
                ncihped1 = await SpawnPed(PedHash.Hunter, Location + 5, 0);
            }
            if (ssmhunter2choice > 10 && ssmhunter2choice <= 20)
            {
                ncihped1 = await SpawnPed(PedHash.HunterCutscene, Location + 5, 0);
            }
            if (ssmhunter2choice > 20 && ssmhunter2choice <= 30)
            {
                ncihped1 = await SpawnPed(PedHash.Taphillbilly, Location + 5, 0);
            }
            if (ssmhunter2choice > 30 && ssmhunter2choice <= 40)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly01AMM, Location + 5, 0);
            }
            if (ssmhunter2choice > 40 && ssmhunter2choice <= 50)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly02AMM, Location + 5, 0);
            }
            if (ssmmeth1choice <= 10)
            {
                ncihped1 = await SpawnPed(PedHash.Rurmeth01AFY, Location + 2, 0);
            }
            if (ssmmeth1choice > 10 && ssmmeth1choice <= 20)
            {
                ncihped1 = await SpawnPed(PedHash.Rurmeth01AMM, Location + 2, 0);
            }
            if (ssmmeth1choice > 20 && ssmmeth1choice <= 30)
            {
                ncihped1 = await SpawnPed(PedHash.MethFemale01, Location + 2, 0);
            }
            if (ssmmeth1choice > 30 && ssmmeth1choice <= 40)
            {
                ncihped1 = await SpawnPed(PedHash.Methhead01AMY, Location + 2, 0);
            }
            if (ssmmeth1choice > 40 && ssmmeth1choice <= 50)
            {
                ncihped1 = await SpawnPed(PedHash.MethMale01, Location + 2, 0);
            }
            if (ssmmeth2choice <= 10)
            {
                ncihped1 = await SpawnPed(PedHash.Hunter, Location + 3, 0);
            }
            if (ssmmeth2choice > 10 && ssmmeth2choice <= 20)
            {
                ncihped1 = await SpawnPed(PedHash.HunterCutscene, Location + 3, 0);
            }
            if (ssmmeth2choice > 20 && ssmmeth2choice <= 30)
            {
                ncihped1 = await SpawnPed(PedHash.Taphillbilly, Location + 3, 0);
            }
            if (ssmmeth2choice > 30 && ssmmeth2choice <= 40)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly01AMM, Location + 3, 0);
            }
            if (ssmmeth2choice > 40 && ssmmeth2choice <= 50)
            {
                ncihped1 = await SpawnPed(PedHash.Hillbilly02AMM, Location + 3, 0);
            }

            API.Wait(250);

            ncihped1.AlwaysKeepTask = true;
            ncihped2.AlwaysKeepTask = true;
            ncihped3.AlwaysKeepTask = true;
            ncihped4.AlwaysKeepTask = true;
            ncihped1.BlockPermanentEvents = true;
            ncihped2.BlockPermanentEvents = true;
            ncihped3.BlockPermanentEvents = true;
            ncihped4.BlockPermanentEvents = true;
            ncihped1.IsPersistent = true;
            ncihped2.IsPersistent = true;
            ncihped3.IsPersistent = true;
            ncihped4.IsPersistent = true;

            API.Wait(500);

            Random ssmpursuitprobability = new Random();
            int ssmpursuitagogobaby = ssmpursuitprobability.Next(1, 100 + 1);
            if (ssmpursuitagogobaby >= 37 && ssmpursuitagogobaby <= 52)
            {
                Tick += SomethingSmellsMethyPursuitBothFlee;
            }
            else
            {
                Tick += SomethingSmellsMethyPursuitMethFlee;
            }

            API.Wait(500);

            PedData ssmhunter1data = await Utilities.GetPedData(ncihped1.NetworkId);
            PedData ssmhunter2data = await Utilities.GetPedData(ncihped2.NetworkId);
            PedData ssmmeth1data = await Utilities.GetPedData(ncihped3.NetworkId);
            PedData ssmmeth2data = await Utilities.GetPedData(ncihped4.NetworkId);
            VehicleData ssmvehicledata = await Utilities.GetVehicleData(ncihvehicle.NetworkId);
            VehicleData ssmmethvehicledata = await Utilities.GetVehicleData(ncihmethvehicle.NetworkId);

            string ssmhunter1firstname = ssmhunter1data.FirstName;
            string ssmhunter2firstname = ssmhunter2data.FirstName;
            string ssmmeth1firstname = ssmmeth1data.FirstName;
            string ssmmeth2firstname = ssmmeth2data.FirstName;

            List<Item> methitemsinvehicle = new List<Item>();
            List<Item> methitemsinmethvehicle = new List<Item>();
            Item ssmmethpipe = new Item
            {
                Name = "Glass meth pipe",
                IsIllegal = true,
            };
            Item ssmcrushedmethpoor = new Item
            {
                Name = "Bag of yellowish white crushed up meth",
                IsIllegal = true,
            };
            Item ssmcrushedmethstandard = new Item
            {
                Name = "Bag of white crushed up meth",
                IsIllegal = true,
            };
            Item ssmcrushedmethblue = new Item
            {
                Name = "Bag of blueish white crushed up meth",
                IsIllegal = true,
            };
            Item ssmpowdermethpoor = new Item
            {
                Name = "Small bag of yellowish white powder",
                IsIllegal = true,
            };
            Item ssmpowdermethstandard = new Item
            {
                Name = "Small bag of white powder",
                IsIllegal = true,
            };
            Item ssmpowdermethblue = new Item
            {
                Name = "Small bag of blueish white powder",
                IsIllegal = true,
            };
            Item ssmcrystalmethpoor = new Item
            {
                Name = "Bag of yellowish white crystals",
                IsIllegal = true,
            };
            Item ssmcrystalmethstandard = new Item
            {
                Name = "Bag of white crystals",
                IsIllegal = true,
            };
            Item ssmcrystalmethblue = new Item
            {
                Name = "Bag of blueish white crystals",
                IsIllegal = true,
            };
            Item ssmroadflares = new Item
            {
                Name = "Several road flares that have been hollowed out",
                IsIllegal = false,
            };
            Item ssmantifreeze = new Item
            {
                Name = "Empty can of antifreeze",
                IsIllegal = false,
            };
            Item ssmpropaintank = new Item
            {
                Name = "Propain tank",
                IsIllegal = false,
            };

            methitemsinmethvehicle.Add(ssmpropaintank);
            methitemsinmethvehicle.Add(ssmantifreeze);
            methitemsinmethvehicle.Add(ssmroadflares);
            methitemsinmethvehicle.Add(ssmmethpipe);
            methitemsinvehicle.Add(ssmmethpipe);

            API.Wait(250);

            Random ssmmethquality = new Random();
            int ssmmethqualitychecker = ssmmethquality.Next(1, 100 + 1);
            if (ssmmethqualitychecker <= 50)
            {
                methitemsinvehicle.Add(ssmcrushedmethpoor);
                methitemsinmethvehicle.Add(ssmcrystalmethpoor);
                methitemsinmethvehicle.Add(ssmpowdermethpoor);
                //Tick += SomethingSmellsMethyPoorQualityQuestions;
            }
            if (ssmmethqualitychecker > 51 && ssmmethqualitychecker <= 75)
            {
                methitemsinvehicle.Add(ssmcrushedmethstandard);
                methitemsinmethvehicle.Add(ssmcrystalmethstandard);
                methitemsinmethvehicle.Add(ssmpowdermethstandard);
                //Tick += SomethingSmellsMethyStandardQualityQuestions;
            }
            if (ssmmethqualitychecker > 76)
            {
                methitemsinvehicle.Add(ssmcrushedmethblue);
                methitemsinmethvehicle.Add(ssmcrystalmethblue);
                methitemsinmethvehicle.Add(ssmpowdermethblue);
                //Tick += SomethingSmellsMethyBlueQualityQuestions;
            }

            ssmmeth1data.Warrant = "Production of a controlled substance.";
            ssmmeth2data.Warrant = "Distribution of a controlled substance.";

            API.Wait(500);

            float ssmmethsmellnotification = Game.PlayerPed.Position.DistanceTo(ncihmethvehicle.Position);
            if (ssmmethsmellnotification < 10f)
            {
                ShowDialog("Heavy smell of amonia (urine)", 10000, 10f);
            }

            Utilities.SetPedData(ncihped1.NetworkId, ssmhunter1data);
            Utilities.SetPedData(ncihped2.NetworkId, ssmhunter2data);
            Utilities.SetPedData(ncihped3.NetworkId, ssmmeth1data);
            Utilities.SetPedData(ncihped4.NetworkId, ssmmeth2data);

            ssmmethvehicledata.Items = methitemsinvehicle;
            ssmvehicledata.Items = methitemsinvehicle;
            Utilities.SetVehicleData(ncihvehicle.NetworkId, ssmvehicledata);
            Utilities.SetVehicleData(ncihmethvehicle.NetworkId, ssmmethvehicledata);
        }

        public async Task SomethingSmellsMethyPursuitBothFlee()
        {
            Tick -= SomethingSmellsMethyPursuitBothFlee;

            ncihped1.Task.EnterVehicle(ncihvehicle, VehicleSeat.Driver);
            ncihped2.Task.EnterVehicle(ncihvehicle, VehicleSeat.Passenger);

            if (ncihped2.IsInVehicle(ncihvehicle) && ncihped1.IsInVehicle(ncihvehicle))
            {
                ncihped1.Task.FleeFrom(Game.PlayerPed);
                Pursuit.RegisterPursuit(ncihped1);
                ncihped1.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;
                ncihvehicle.Speed = 250;
            }

            ncihped3.Task.EnterVehicle(ncihmethvehicle, VehicleSeat.Driver);
            ncihped4.Task.EnterVehicle(ncihmethvehicle, VehicleSeat.Any);

            if (ncihped4.IsInVehicle(ncihmethvehicle) && ncihped3.IsInVehicle(ncihmethvehicle))
            {
                ncihped3.Task.FleeFrom(Game.PlayerPed);
                Pursuit.RegisterPursuit(ncihped3);
                ncihped1.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;
                ncihvehicle.Speed = 250;
            }
        }

        public async Task SomethingSmellsMethyPursuitMethFlee()
        {
            Tick -= SomethingSmellsMethyPursuitMethFlee;

            ncihped3.Task.EnterVehicle(ncihmethvehicle, VehicleSeat.Driver);
            ncihped4.Task.EnterVehicle(ncihmethvehicle, VehicleSeat.Any);

            if (ncihped4.IsInVehicle(ncihmethvehicle) && ncihped3.IsInVehicle(ncihmethvehicle))
            {
                ncihped3.Task.FleeFrom(Game.PlayerPed);
                Pursuit.RegisterPursuit(ncihped3);
                ncihped1.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;
                ncihvehicle.Speed = 250;
            }
        }

        public async Task SomethingSmellsMethyPoorQualityQuestions()
        {

        }

        public async Task SomethingSmellsMethyStandardQualityQuestions()
        {

        }

        public async Task SomethingSmellsMethyBlueQualityQuestions()
        {

        }

        public async Task AbandonedVehicle()
        {
            Tick -= AbandonedVehicle;

            API.Wait(500);

            Random AVncihvehiclefindingrandomizer = new Random();
            int AVncihvehiclefindings = AVncihvehiclefindingrandomizer.Next(1, 80 + 1);
            if (AVncihvehiclefindings <= 20)
            {
                Tick += AbandonedVehicleDrinking;
            }

            if (AVncihvehiclefindings > 20 && AVncihvehiclefindings <= 40)
            {
                Tick += AbandonedVehicleBloodyInterior;
            }

            if (AVncihvehiclefindings > 40 && AVncihvehiclefindings <= 60)
            {
                Tick += AbandonedVehicleDrug;
            }

            if (AVncihvehiclefindings > 60)
            {
                Tick += AbandonedVehicleEmpty;
            }
        }

        public async Task AbandonedVehicleDrinking()
        {
            Tick -= AbandonedVehicleDrinking;
            VehicleData AVDrinkncihvehicledata = await Utilities.GetVehicleData(ncihvehicle.NetworkId);
            List<Item> AVDrinkncihvehicleitemlist = new List<Item>();
            float AVDrinkncihvehicleobservation = Game.PlayerPed.Position.DistanceTo(ncihmethvehicle.Position);

            Random AVDrinkdeadbodyequalstrue = new Random();
            int AVDrinkdeadbodyreallyequalstrue = AVDrinkdeadbodyequalstrue.Next(1, 100 + 1);
            if (AVDrinkdeadbodyreallyequalstrue < 10)
            {
                Tick += AbandonedVehicleBodyNearbyDrink;
            }

            if (AVDrinkncihvehicleobservation < 10f)
            {
                ShowDialog("Heavy smell of alcohol eminating from the vehicle", 10000, 10f);
            }

            Item AVDrinkbeerbottle = new Item
            {
                Name = "Pißwasser bottle, unopen",
                IsIllegal = false
            };
            Item AVDrinksixpack = new Item
            {
                Name = "Six pack of Pißwasser, unopen",
                IsIllegal = false
            };
            Item AVDrinksixpackopen = new Item
            {
                Name = "Six pack of Pißwasser, open",
                IsIllegal = true
            };
            Item AVDrinktallboy = new Item
            {
                Name = "Pißwasser Tallboy, unopen",
                IsIllegal = false
            };
            Item AVDrinkopenbottle = new Item
            {
                Name = "Pißwasser bottle, open",
                IsIllegal = true
            };
            Item AVDrinktallboyopen = new Item
            {
                Name = "Pißwasser Tallboy, open",
                IsIllegal = true
            };
            Item AVDrinkemptybottle = new Item
            {
                Name = "Empty Pißwasser bottle",
                IsIllegal = true
            };
            Item AVDrinkbeercan = new Item
            {
                Name = "Pißwasser can, unopen",
                IsIllegal = false
            };
            Item AVDrinkopencan = new Item
            {
                Name = "Pißwasser can, open",
                IsIllegal = true
            };
            Item AVDrinkemptycan = new Item
            {
                Name = "Empty Pißwasser can",
                IsIllegal = true
            };
            Item AVDrinktallboyempty = new Item
            {
                Name = "Empty Pißwasser Tallboy",
                IsIllegal = true
            };

            Random AVDrinkvehicleinventorydecider = new Random();
            int AVDrinkvehicleinventorydecision = AVDrinkvehicleinventorydecider.Next(1, 100 + 1);
            if (AVDrinkvehicleinventorydecision <= 33)
            {
                AVDrinkncihvehicleitemlist.Add(AVDrinksixpackopen);
                AVDrinkncihvehicleitemlist.Add(AVDrinktallboyempty);
                AVDrinkncihvehicleitemlist.Add(AVDrinktallboyopen);
                AVDrinkncihvehicleitemlist.Add(AVDrinktallboy);
                AVDrinkncihvehicleitemlist.Add(AVDrinktallboyopen);
                AVDrinkncihvehicledata.Items = AVDrinkncihvehicleitemlist;
            }
            if (AVDrinkvehicleinventorydecision > 33 && AVDrinkvehicleinventorydecision <= 66)
            {
                AVDrinkncihvehicleitemlist.Add(AVDrinksixpackopen);
                AVDrinkncihvehicleitemlist.Add(AVDrinkemptybottle);
                AVDrinkncihvehicleitemlist.Add(AVDrinkbeerbottle);
                AVDrinkncihvehicleitemlist.Add(AVDrinkopenbottle);
                AVDrinkncihvehicledata.Items = AVDrinkncihvehicleitemlist;
            }
            if (AVDrinkvehicleinventorydecision > 66)
            {
                AVDrinkncihvehicleitemlist.Add(AVDrinksixpack);
                AVDrinkncihvehicleitemlist.Add(AVDrinkopencan);
                AVDrinkncihvehicleitemlist.Add(AVDrinkopencan);
                AVDrinkncihvehicleitemlist.Add(AVDrinkbeercan);
                AVDrinkncihvehicleitemlist.Add(AVDrinkemptycan);
                AVDrinkncihvehicleitemlist.Add(AVDrinkemptycan);
                AVDrinkncihvehicledata.Items = AVDrinkncihvehicleitemlist;
            }

            API.Wait(500);

            ncihvehicle.SetData(AVDrinkncihvehicledata);
        }

        public async Task AbandonedVehicleBloodyInterior()
        {
            Tick -= AbandonedVehicleBloodyInterior;
            VehicleData AVBIncihvehicledata = await Utilities.GetVehicleData(ncihvehicle.NetworkId);
            List<Item> AVBIncihvehicleitemlist = new List<Item>();
            float AVBIncihvehicleobservation = Game.PlayerPed.Position.DistanceTo(ncihmethvehicle.Position);

            Random AVBIdeadbodyequalstrue = new Random();
            int AVBIdeadbodyreallyequalstrue = AVBIdeadbodyequalstrue.Next(1, 100 + 1);
            if (AVBIdeadbodyreallyequalstrue < 10)
            {
                Tick += AbandonedVehicleBodyNearbyBI;
            }

            if (AVBIncihvehicleobservation < 10f)
            {
                ShowDialog("Several bulletholes in the vehicle, excessive blood in and around the driver compartment.", 10000, 10f);
            }

            Item AVBIvehobservation1 = new Item
            {
                Name = "Bullets entered from the rear driver side of the vehicle",
            };
            Item AVBIvehobservation2 = new Item
            {
                Name = "Driver seat soaked in blood",
                IsIllegal = true,
            };
            Item AVBIvehobservation3 = new Item
            {
                Name = "Bullet holes into console do not match number of holes on exterior"
            };

            AVBIncihvehicleitemlist.Add(AVBIvehobservation1);
            AVBIncihvehicleitemlist.Add(AVBIvehobservation2);
            AVBIncihvehicleitemlist.Add(AVBIvehobservation3);

            AVBIncihvehicledata.Items = AVBIncihvehicleitemlist;

            ncihvehicle.SetData(AVBIncihvehicledata);
        }

        public async Task AbandonedVehicleDrug()
        {
            Tick -= AbandonedVehicleDrug;
            VehicleData AVDrugncihvehicledata = await Utilities.GetVehicleData(ncihvehicle.NetworkId);
            List<Item> AVDrugncihvehicleitemlist = new List<Item>();
            float AVDrugncihvehicleobservation = Game.PlayerPed.Position.DistanceTo(ncihmethvehicle.Position);

            Random AVDrugdeadbodyequalstrue = new Random();
            int AVDrugdeadbodyreallyequalstrue = AVDrugdeadbodyequalstrue.Next(1, 100 + 1);
            if (AVDrugdeadbodyreallyequalstrue < 10)
            {
                Tick += AbandonedVehicleBodyNearbyDrug;
            }

            Item AVBvehicleclue1 = new Item
            {
                Name = "Hidden compartments broken open",
                IsIllegal = true,
            };
            Item AVBvehicleclue2 = new Item
            {
                Name = "Open ice chest",
                IsIllegal = false,
            };
            Item AVBvehicleclue3 = new Item
            {
                Name = "Empty bag from Up-n-Atom",
                IsIllegal = false,
            };

            AVDrugncihvehicleitemlist.Add(AVBvehicleclue1);
            AVDrugncihvehicleitemlist.Add(AVBvehicleclue2);
            AVDrugncihvehicleitemlist.Add(AVBvehicleclue3);

            AVDrugncihvehicledata.Items = AVDrugncihvehicleitemlist;

            ncihvehicle.SetData(AVDrugncihvehicledata);

            if (AVDrugncihvehicleobservation < 10f)
            {
                ShowDialog("The vehicle appears to have been ransacked", 10000, 10f);
            }
        }

        public async Task AbandonedVehicleEmpty()
        {
            Tick -= AbandonedVehicleEmpty;

            VehicleData AVEncihvehicledata = await Utilities.GetVehicleData(ncihvehicle.NetworkId);
            List<Item> AVEncihvehicleitemlist = new List<Item>();
            float AVEncihvehicleobservation = Game.PlayerPed.Position.DistanceTo(ncihmethvehicle.Position);

            Item AVEdashstripped = new Item
            {
                Name = "The dash has been stripped down",
            };
            Item AVEbustedcolumn = new Item
            {
                Name = "Steering wheel column has been busted open",
                IsIllegal = true,
            };

            if (AVEncihvehicleobservation < 10f)
            {
                ShowDialog("Vehicle seems to be strangely empty and attempted to be stripped down for parts", 10000, 10f);
            }
        }

        public async Task AbandonedVehicleBodyNearbyDrink()
        {
            Tick -= AbandonedVehicleBodyNearbyDrink;

            PedData AVBNDrinkncihbodydata = await Utilities.GetPedData(ncihped1.NetworkId);
            List<Item> AVBNDrinkncihpeditemlist = new List<Item>();
            float AVBNDrinkncihbodyobservation = Game.PlayerPed.Position.DistanceTo(ncihped1.Position);

            Random AVBNDrinkncihbodyplacementdecider = new Random();
            int AVBNDrinkncihbodyplacementdecision = AVBNDrinkncihbodyplacementdecider.Next(1, 100 + 1);
            if (AVBNDrinkncihbodyplacementdecision <= 33)
            {
                ncihped1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2, 0);
                ncihped1.SetIntoVehicle(ncihvehicle, VehicleSeat.Driver);
                ncihped1.Kill();
            }
            if (AVBNDrinkncihbodyplacementdecision > 33 && AVBNDrinkncihbodyplacementdecision <= 66)
            {
                ncihped1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 50, 0);
                ncihped1.Kill();
            }
            if (AVBNDrinkncihbodyplacementdecision > 66)
            {
                ncihped1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 15, 0);
                ncihped1.Kill();
            }

            if (AVBNDrinkncihbodyobservation < 10f)
            {
                ShowDialog("Heavy smell of alcohol.", 10000, 10f);
            }

            AVBNDrinkncihbodydata.BloodAlcoholLevel = 0.22;
            ncihped1.SetData(AVBNDrinkncihbodydata);

            if (ncihped1.IsAlive)
            {
                ncihped1.Kill();
                return;
            }
        }

        public async Task AbandonedVehicleBodyNearbyBI()
        {
            Tick -= AbandonedVehicleBodyNearbyBI;

            PedData AVBNBIncihbodydata = await Utilities.GetPedData(ncihped1.NetworkId);
            List<Item> AVBNBIncihpeditemlist = new List<Item>();
            float AVBNBIncihbodyobservation = Game.PlayerPed.Position.DistanceTo(ncihped1.Position);

            Random AVBNBIncihbodyplacementdecider = new Random();
            int AVBNBIncihbodyplacementdecision = AVBNBIncihbodyplacementdecider.Next(1, 100 + 1);
            if (AVBNBIncihbodyplacementdecision <= 33)
            {
                ncihped1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 30, 0);
                ncihped1.Kill();
            }
            if (AVBNBIncihbodyplacementdecision > 33 && AVBNBIncihbodyplacementdecision <= 66)
            {
                ncihped1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 50, 0);
                ncihped1.Kill();
            }
            if (AVBNBIncihbodyplacementdecision > 66)
            {
                ncihped1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 15, 0);
                ncihped1.Kill();
            }

            Item AVBNBIbulletwound1 = new Item
            {
                Name = "Bullet entry and exit wound through left arm",
                IsIllegal = true,
            };
            Item AVBNBIbulletwound2 = new Item
            {
                Name = "Bullet entry wound through lower back",
                IsIllegal = true,
            };
            Item AVBNBIbulletwound3 = new Item
            {
                Name = "Bullet entry wound through center of back",
                IsIllegal = true,
            };
            Item AVBNBIbulletwound4 = new Item
            {
                Name = "Bullet entry and exit wound through left upper back",
                IsIllegal = true,
            };

            AVBNBIncihpeditemlist.Add(AVBNBIbulletwound1);
            AVBNBIncihpeditemlist.Add(AVBNBIbulletwound2);
            AVBNBIncihpeditemlist.Add(AVBNBIbulletwound3);
            AVBNBIncihpeditemlist.Add(AVBNBIbulletwound4);

            AVBNBIncihbodydata.Items = AVBNBIncihpeditemlist;

            ncihped1.SetData(AVBNBIncihbodydata);

            if (ncihped1.IsAlive)
            {
                ncihped1.Kill();
                return;
            }
        }

        public async Task AbandonedVehicleBodyNearbyDrug()
        {
            Tick -= AbandonedVehicleBodyNearbyDrug;

            PedData AVBNDrugncihbodydata = await Utilities.GetPedData(ncihped1.NetworkId);
            List<Item> AVBNDrugncihpeditemlist = new List<Item>();
            float AVBNDrugncihbodyobservation = Game.PlayerPed.Position.DistanceTo(ncihped1.Position);

            Random AVBNDrugncihbodyplacementdecider = new Random();
            int AVBNDrugncihbodyplacementdecision = AVBNDrugncihbodyplacementdecider.Next(1, 100 + 1);
            if (AVBNDrugncihbodyplacementdecision <= 33)
            {
                ncihped1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2, 0);
                ncihped1.SetIntoVehicle(ncihvehicle, VehicleSeat.Driver);
                ncihped1.Kill();
            }
            if (AVBNDrugncihbodyplacementdecision > 33 && AVBNDrugncihbodyplacementdecision <= 66)
            {
                ncihped1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 50, 0);
                ncihped1.Kill();
            }
            if (AVBNDrugncihbodyplacementdecision > 66)
            {
                ncihped1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 3, 0);
                ncihped1.Kill();
            }

            Item AVBNDrugexecutionstyle = new Item
            {
                Name = "Shot in the back of the head",
                IsIllegal = true,
            };
            Item AVBNDrugpockets = new Item
            {
                Name = "Pockets turned inside out and empty",
                IsIllegal = false,
            };

            AVBNDrugncihpeditemlist.Add(AVBNDrugexecutionstyle);
            AVBNDrugncihpeditemlist.Add(AVBNDrugpockets);

            AVBNDrugncihbodydata.Items = AVBNDrugncihpeditemlist;
            AVBNDrugncihbodydata.FirstName = "Unknown";
            AVBNDrugncihbodydata.LastName = "Unknown";
            AVBNDrugncihbodydata.DateOfBirth = "Unknown";

            ncihped1.SetData(AVBNDrugncihbodydata);

            if (ncihped1.IsAlive)
            {
                ncihped1.Kill();
                return;
            }
        }
    }
}
