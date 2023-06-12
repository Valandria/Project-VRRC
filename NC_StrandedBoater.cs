using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace RangersoftheWildernessCallouts
{

    [CalloutProperties("NC Stranded Boater", "Valandria", "0.0.1")]
    public class NCStrandedBoater : Callout
    {
        private Vehicle ncsbboat;
        Ped ncsbdriver, ncsbpassenger1, ncsbpassenger2, ncsbpassenger3;
        private string[] ncsbboatList = { "tug", "Dinghy", "Jetmax", "Speeder", "Speeder2", "Squalo", "Submersible", "Submersible2", "Suntrap", "Toro", "Tropic", "Tropic2" };
        private Vector3[] boatcoordinates =
        {
            new Vector3(-1372.14f, 2650.83f, 0.82f),
            new Vector3(-870.12f, 2811.18f, 10.59f),
            new Vector3(-631.82f, 2928.98f, 14.2f),
            new Vector3(-639f, 2960.5f, 13.89f),
            new Vector3(-586.15f, 2932.08f, 13.83f),
            new Vector3(-545.15f, 2939.17f, 14.05f),
            new Vector3(-506.06f, 2890.33f, 14.22f),
            new Vector3(-440.65f, 2940.02f, 14.15f),
            new Vector3(-414.02f, 2988.9f, 14.11f),
            new Vector3(-349.51f, 3014.09f, 14.71f),
            new Vector3(-280.2f, 3026.21f, 19.3f),
            new Vector3(-280.13f, 2999.77f, 19.24f),
            new Vector3(-233.44f, 3011.68f, 19.15f),
            new Vector3(-177.96f, 3041.36f, 19.71f),
            new Vector3(-64.12f, 3119.4f, 26.19f),
            new Vector3(-47.66f, 3095.41f, 26.75f),
            new Vector3(-0.41f, 3109.29f, 26.6f),
            new Vector3(0.53f, 3127.32f, 26.69f),
            new Vector3(29.82f, 3141.46f, 26.45f),
            new Vector3(62.94f, 3166.08f, 26.46f),
            new Vector3(69.18f, 3216.93f, 26.59f),
            new Vector3(122.07f, 3316.47f, 30.86f),
            new Vector3(-254.72f, 4275.82f, 31.12f),
            new Vector3(-285.77f, 4416.03f, 31.21f),
            new Vector3(-445.23f, 4395.9f, 31.16f),
            new Vector3(-588.52f, 4402.82f, 15.86f),
            new Vector3(-679.22f, 4451.11f, 15.76f),
            new Vector3(-705.35f, 4429.16f, 15.68f),
            new Vector3(-759.02f, 4434.95f, 15.95f),
            new Vector3(-802.01f, 4459.64f, 15.82f),
            new Vector3(-873.5f, 4431.71f, 15.49f),
            new Vector3(-810.85f, 4431.6f, 15.88f),
            new Vector3(-800.38f, 4455.85f, 15.25f),
            new Vector3(-752.74f, 4435.21f, 15.87f),
            new Vector3(-674.09f, 4450.27f, 15.78f),
            new Vector3(-651.26f, 4414.08f, 15.48f),
            new Vector3(-588.41f, 4403.23f, 15.75f),
            new Vector3(-407.81f, 4468.01f, 31.07f),
            new Vector3(-283.55f, 4412.03f, 31f),
            new Vector3(-255.67f, 4276.57f, 31.29f),
            new Vector3(-194.05f, 4304.12f, 30.99f),
            new Vector3(-963.45f, 4361.88f, 10.48f),
            new Vector3(-1064.47f, 4385.03f, 11.04f),
            new Vector3(-1137.79f, 4413.12f, 10.73f),
            new Vector3(-1179.64f, 4375.7f, 5.5f),
            new Vector3(-1407.32f, 4317.75f, 1.27f),
            new Vector3(-1546.84f, 4345.83f, 0.86f),
            new Vector3(-1569.53f, 4403.2f, 0.94f),
            new Vector3(-1621.12f, 4417.94f, 0.95f),
            new Vector3(-1626.95f, 4420.81f, 1.87f),
            new Vector3(-1644.75f, 4490.33f, 1.15f),
            new Vector3(-1730.26f, 4469.46f, 1.29f),
            new Vector3(-1749.77f, 4552.45f, 1.14f),
            new Vector3(-1807.33f, 4613.7f, 1.55f),
        };

        public NCStrandedBoater()
        {
            InitInfo(boatcoordinates[RandomUtils.Random.Next(boatcoordinates.Length)]);
            ShortName = "NC - Stranded Boater";
            Random dispatchnotification = new Random();
            int dispatchpage = dispatchnotification.Next(1, 100 + 1);
            if (dispatchpage <= 33)
            {
                CalloutDescription = "A boater has become beached and is requesting assistance.";
            }
            else if (dispatchpage > 34 && dispatchpage <= 66)
            {
                CalloutDescription = "Reports of boaters becoming stranded on the river.";
            }
            else if (dispatchpage > 67)
            {
                CalloutDescription = "Reports of a boat running ashore along the river.";
            }
            ResponseCode = 2;
            StartDistance = 200f;
        }
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            Random strandedboattyperandom = new Random();
            string cartype = ncsbboatList[strandedboattyperandom.Next(ncsbboatList.Length)];
            VehicleHash Hash = (VehicleHash)API.GetHashKey(cartype);
            ncsbboat = await SpawnVehicle(Hash, Location);
            ncsbdriver.AlwaysKeepTask = true;
            ncsbdriver.BlockPermanentEvents = true;
            ncsbpassenger1.AlwaysKeepTask = true;
            ncsbpassenger1.BlockPermanentEvents = true;
            ncsbpassenger2.AlwaysKeepTask = true;
            ncsbpassenger2.BlockPermanentEvents = true;
            ncsbpassenger3.AlwaysKeepTask = true;
            ncsbpassenger3.BlockPermanentEvents = true;
            Random ncsbscenario = new Random();
            int ncsbtalktalk = ncsbscenario.Next(1, 100 + 1);
            if (ncsbtalktalk < 0)
            {
                Tick += fullsentitbro;
            }
            else if (ncsbtalktalk <= 0)
            {
                Tick += notmyboatdotjpeg;
            }
            else if (ncsbtalktalk <= 0)
            {
                Tick += huntingwithoutalicense;
            }
        }

        public async Task fullsentitbro()
        {
            ncsbdriver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            ncsbpassenger1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            ncsbpassenger2 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 4);
            ncsbpassenger3 = await SpawnPed(PedHash.Families01GFY, Location + 0);
            ncsbdriver.Kill();
            ncsbboat.AttachBlip();
            ncsbdriver.AttachBlip();
            ncsbpassenger1.AttachBlip();
            ncsbpassenger2.AttachBlip();
            ncsbpassenger3.AttachBlip();
            API.Wait(6000);
            PedData ncsbdriverdata = new PedData();
            PedData ncsbpassenger1data = new PedData();
            PedData ncsbpassenger2data = new PedData();
            PedData ncsbpassenger3data = new PedData();
            PedData ncsbdata4 = await Utilities.GetPedData(ncsbpassenger3.NetworkId);
            PedData ncsbdata3 = await Utilities.GetPedData(ncsbpassenger2.NetworkId);
            PedData ncsbdata2 = await Utilities.GetPedData(ncsbpassenger1.NetworkId);
            PedData ncsbdata1 = await Utilities.GetPedData(ncsbdriver.NetworkId);
            string driverfirstname = ncsbdata1.FirstName;
            string passenger1firstname = ncsbdata2.Firstname;
            string passenger2firstname = ncsbdata3.Firstname;
            ncsbdriverdata.Warrant = "FELONY: Boating while intoxicated.";
            ncsbdriverdata.BloodAlcoholLevel = 0.11;
            ncsbpassenger1data.BloodAlcoholLevel = 0.05;
            ncsbpassenger2data.BloodAlcoholLevel = 0.08;
            ncsbpassenger3data.BloodAlcoholLevel = 0.09;
            ncsbpassenger3data.FirstName = "Sonia";
            ncsbpassenger3data.LastName = "Charleston";
            VehicleData ncsbboatdata = new VehicleData();
            List<Item> drinkyonthewater = new List<Item>();
            Item BeerBottle = new Item
            {
                Name = "Pißwasser bottle, unopen",
                IsIllegal = false
            };
            Item sixpack = new Item
            {
                Name = "Six pack of Pißwasser, unopen",
                IsIllegal = false
            };
            Item sixpackopen = new Item
            {
                Name = "Six pack of Pißwasser, open",
                IsIllegal = true
            };
            Item Tallboy = new Item
            {
                Name = "Pißwasser Tallboy, unopen",
                IsIllegal = false
            };
            Item Openbottle = new Item
            {
                Name = "Pißwasser bottle, open",
            };
            Item Tallboyopen = new Item
            {
                Name = "Pißwasser Tallboy, open",
                IsIllegal = true
            };
            Item EmptyBottle = new Item
            {
                Name = "Empty Pißwasser bottle",
                IsIllegal = true
            };
            Item BeerCan = new Item
            {
                Name = "Pißwasser can, unopen",
                IsIllegal = false
            };
            Item OpenCan = new Item
            {
                Name = "Pißwasser can, open",
                IsIllegal = false
            };
            Item EmptyCan = new Item
            {
                Name = "Empty Pißwasser can",
                IsIllegal = false
            };
            Item Tallboyempty = new Item
            {
                Name = "Empty Pißwasser Tallboy",
                IsIllegal = false
            };

            Random ncsbitemlistingchance = new Random();
            int ncsbbeer = ncsbitemlistingchance.Next(1, 100 + 1);
            if (ncsbbeer < 10)
            {
                drinkyonthewater.Add(BeerBottle);
                drinkyonthewater.Add(EmptyBottle);
                drinkyonthewater.Add(EmptyBottle);
                drinkyonthewater.Add(EmptyBottle);
                drinkyonthewater.Add(Openbottle);
            }
            else if (ncsbbeer >= 10 && ncsbbeer < 20)
            {
                drinkyonthewater.Add(sixpackopen);
                drinkyonthewater.Add(Tallboyempty);
                drinkyonthewater.Add(Tallboyempty);
                drinkyonthewater.Add(Tallboy);
            }
            else if (ncsbbeer >= 20 && ncsbbeer < 30)
            {
                drinkyonthewater.Add(sixpack);
                drinkyonthewater.Add(OpenCan);
                drinkyonthewater.Add(EmptyCan);
                drinkyonthewater.Add(EmptyCan);
            }
            else if (ncsbbeer >= 30 && ncsbbeer < 40)
            {
                drinkyonthewater.Add(sixpackopen);
                drinkyonthewater.Add(BeerCan);
                drinkyonthewater.Add(EmptyCan);
            }
            else if (ncsbbeer >= 40 && ncsbbeer < 50)
            {
                drinkyonthewater.Add(Tallboyempty);
                drinkyonthewater.Add(Tallboyopen);
                drinkyonthewater.Add(Tallboyopen);
                drinkyonthewater.Add(Tallboy);
                drinkyonthewater.Add(BeerCan);
                drinkyonthewater.Add(BeerCan);
                drinkyonthewater.Add(OpenCan);
            }
            else if (ncsbbeer >= 50 && ncsbbeer < 60)
            {
                drinkyonthewater.Add(sixpackopen);
                drinkyonthewater.Add(EmptyCan);
                drinkyonthewater.Add(EmptyCan);
                drinkyonthewater.Add(EmptyCan);
            }
            else if (ncsbbeer >= 60 && ncsbbeer < 70)
            {
                drinkyonthewater.Add(Tallboy);
                drinkyonthewater.Add(Tallboyopen);
                drinkyonthewater.Add(EmptyCan);
                drinkyonthewater.Add(EmptyCan);
                drinkyonthewater.Add(OpenCan);
                drinkyonthewater.Add(BeerCan);
            }
            else if (ncsbbeer >= 70 && ncsbbeer < 80)
            {
                drinkyonthewater.Add(EmptyCan);
                drinkyonthewater.Add(EmptyCan);
                drinkyonthewater.Add(Tallboyempty);
                drinkyonthewater.Add(Tallboyopen);
                drinkyonthewater.Add(Tallboyempty);
            }
            else if (ncsbbeer >= 80 && ncsbbeer < 90)
            {
                drinkyonthewater.Add(sixpackopen);
                drinkyonthewater.Add(sixpack);
                drinkyonthewater.Add(EmptyCan);
            }
            else if (ncsbbeer >= 90)
            {
                drinkyonthewater.Add(sixpack);
                drinkyonthewater.Add(EmptyCan);
                drinkyonthewater.Add(EmptyCan);
                drinkyonthewater.Add(Tallboy);
            }
            ncsbboatdata.Items = drinkyonthewater;

            PedQuestion ncsbpass1q1 = new PedQuestion();
            ncsbpass1q1.Question = "What happened today?";
            ncsbpass1q1.Answers = new List<string>
            {
                "They just full sent it bro!",
                "Yo! Bro," + driverfirstname + "sent it bro!",
                "We just full sent it bro!"
                "We got some wicked air full sending it bro!"
            };
            PedQuestion ncsbpass1q2 = new PedQuestion();
            ncsbpass1q2.Question = "Could you explain what that is?";
            ncsbpass1q2.Answers = new List<string>
            {

            };
            PedQuestion ncsbpass1q3 = new PedQuestion();
            PedQuestion ncsbpass1q4 = new PedQuestion();
            PedQuestion ncsbpass1q5 = new PedQuestion();
            PedQuestion ncsbpass1q6 = new PedQuestion();
            PedQuestion ncsbpass1q7 = new PedQuestion();
            PedQuestion ncsbpass1q8 = new PedQuestion();
            PedQuestion ncsbpass1q9 = new PedQuestion();
            PedQuestion ncsbpass1q10 = new PedQuestion();
            PedQuestion ncsbpass1q11 = new PedQuestion();
            PedQuestion ncsbpass1q12 = new PedQuestion();
            PedQuestion ncsbpass1q13 = new PedQuestion();
            PedQuestion ncsbpass1q14 = new PedQuestion();
            PedQuestion ncsbpass1q15 = new PedQuestion();
            PedQuestion ncsbpass1q16 = new PedQuestion();
        }

        public async Task notmyboatdotjpeg()
        {

        }

        public async Task huntingwithoutalicense()
        {

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