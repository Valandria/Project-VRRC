using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;


namespace LocalAutoUnion404
{

    [CalloutProperties("Local Small Vehicle", "Valandria", "0.0.4")]
    public class SmallCar : Callout
    {
        private Vehicle tugtug;
        Ped lsvcped, lsvctoothy;
        private string[] goodItemList = { "Open Soda Can", "Pack of Hotdogs", "Dog Food", "Empty Can", "Phone", "Cake", "Cup of Noodles", "Water Bottle", "Pack of Cards", "Outdated Insurance Card", "Pack of Pens", "Phone", "Tablet", "Computer", "Business Cards", "Taxi Business Card", "Textbooks", "Car Keys", "House Keys", "Keys", "Folder" };

        public SmallCar()
        {

            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Very Small Vehicle";
            CalloutDescription = "A very small vehicle is causing traffic issues.  Respond code 2.";
            ResponseCode = 2;
            StartDistance = 250f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            // To be expanded - ticks for drug types / scenarios
            Random thatsapaddling = new Random();
            int toothycomehome = thatsapaddling.Next(1, 100 + 1);
//            if (toothycomehome <= 1)
//            {
//                Tick += Toothytime;
//            };
            if (toothycomehome > 1)
            {
                Tick += Methmademedoit;
            }

            //Vehicle Data
            VehicleData pockettugger = new VehicleData();
            pockettugger.Flag = "Stolen";
            pockettugger.LicensePlate = "LSIA";
            Utilities.SetVehicleData(tugtug.NetworkId, pockettugger);

            //To add - Random chance of pursuit, with smaller chance of shooting during said pursuit.
        }
        public async Task Methmademedoit()
        {
            Tick -= Methmademedoit;
            lsvcped = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            tugtug = await SpawnVehicle(VehicleHash.Airtug, Location, 12);
            lsvcped.SetIntoVehicle(tugtug, VehicleSeat.Driver);

            // Ped Data
            PedData lsvcpeddata = new PedData();
            lsvcpeddata.BloodAlcoholLevel = 0.09;
            lsvcpeddata.Warrant = "Felony Drug Possession";
            Utilities.SetPedData(lsvcped.NetworkId, lsvcpeddata);
            lsvcped.AlwaysKeepTask = true;
            lsvcped.BlockPermanentEvents = true;
            lsvcped.Task.CruiseWithVehicle(tugtug, 5f, 524675);
            tugtug.AttachBlip();
            lsvcped.AttachBlip();
            List<Item> items = new List<Item>();
            Random random3 = new Random();
            string name2 = goodItemList[random3.Next(goodItemList.Length)];
            Item goodItem = new Item
            {
                Name = name2,
                IsIllegal = false
            };
            Item meth = new Item
            {
                Name = "Small bag of meth",
                IsIllegal = true
            };
            Item methpipe = new Item
            {
                Name = "Used meth pipe",
                IsIllegal = true
            };
            Item screwdriver = new Item
            {
                Name = "Screwdriver",
                IsIllegal = false
            };
            Item bustedsteercolumn = new Item
            {
                Name = "Busted console",
                IsIllegal = false
            };
            Item blackbag = new Item
            {
                Name = "Black bag with meth and paraphernalia",
                IsIllegal = true
            };
            lsvcpeddata.Items = items;
            items.Add(goodItem);
            Random lotsofdrugs = new Random();
            int methmademedoitofficer = lotsofdrugs.Next(1, 100 + 1);
            if (methmademedoitofficer <= 50)
            {
                items.Add(meth);
                items.Add(methpipe);
                items.Add(screwdriver);
            }
            if (methmademedoitofficer >= 51)
            {
                items.Add(blackbag);
                items.Add(screwdriver);
            }

            //Additional ticks for question sets - Ideas; 'tweaked out', 'livestreamer', 'rich person?'
            Random MethmademedoitQuestions = new Random();
            int MethmademedoitQuestionType = MethmademedoitQuestions.Next(1, 100 + 1);
            if (MethmademedoitQuestionType > 1)
            {
                Tick += TheLordsMeth;
            }
            //else if ( > 30 &&  <= 60)
            //{
            //    Tick += ;
            //}
            //else if ( > 60)
            //{
            //    Tick += ;
            //}
        }

        //public async Task ()
        //{
            //Tick -= ;
        //}

        //public async Task ()
        //{
            //Tick -= ;
        //};

        public async Task TheLordsMeth()
        {
            Tick -= TheLordsMeth;

            PedQuestion lsvcqtlmmr = new PedQuestion();
            lsvcqtlmmr.Question = "*Reads Miranda Rights*";
            lsvcqtlmmr.Answers = new List<string>
            {
                "I don't need your rights, I have the LORD!",
                "LORD!  SMITE MY ENEMIES!",
                "LORD have mercy on you heathens!",
                "I will never speak against my LORD!",
                "This is discrimination!  YOU HATE THE LORD!"
            };
            PedQuestion lsvcqtlm1 = new PedQuestion();
            lsvcqtlm1.Question = "What are you doing?";
            lsvcqtlm1.Answers = new List<string>
            {
                "What do you mean?  I'm busy for the LORD!",
                "I'm driving for the LORD!",
                "I'm not doing anything wrong, I am blessed by the LORD!",
                "I'm trying to go praise the LORD!"
            };
            AddPedQuestion(lsvcped, lsvcqtlm1);
            PedQuestion lsvcqtlm2 = new PedQuestion();
            lsvcqtlm2.Question = "Where did you get the vehicle?";
            lsvcqtlm2.Answers = new List<string>
            {
                "It was given to be by the LORD!",
                "I borrowed it in the name of the LORD!",
                "It's mine, a gift from the LORD!",
                "Buddy is letting me borrow it so I can run errands for the LORD!"
            };
            AddPedQuestion(lsvcped, lsvcqtlm2);
            PedQuestion lsvcqtlm3 = new PedQuestion();
            lsvcqtlm3.Question = "Are you currently on any sort of substance?";
            lsvcqtlm3.Answers = new List<string>
            {
                "I swear to the- I SWEAR TO THE LORD I AM NOT.  PRAISE THE LORD!",
                "Nope. Nope. Nope. Nope nope.  Nope.  Swear to the LORD!",
                "I would never disgrace the LORD!",
                "The LORD would never let me!  NEVER LET ME!"
            };
            AddPedQuestion(lsvcped, lsvcqtlm3);
            PedQuestion lsvcqtlm4 = new PedQuestion();
            lsvcqtlm4.Question = "Have you talked to the Lord lately?";
            lsvcqtlm4.Answers = new List<string>
            {
                "The LORD speaks to me all the time, unbeliever!  UNBELIEVER!",
                "The LORD speaks to me all the time, heathen!  HEATHEN!",
                "I was just going to see the LORD!  But I got stopped by a PIG!  OINK!",
                "I am always talking to the LORD!  He is disappointed by you."
            };
            AddPedQuestion(lsvcped, lsvcqtlm4);
            PedQuestion lsvcqtlm5 = new PedQuestion();
            lsvcqtlm5.Question = "Did he ask you to harm anyone?";
            lsvcqtlm5.Answers = new List<string>
            {
                "The LORD never asks to harm anyone, never.",
                "I will harm EVERYONE FOR THE LORD!  But he never asked.",
                "Only the person who tried to stop me from driving.",
                "The LORD shall not be questioned!"
            };
            AddPedQuestion(lsvcped, lsvcqtlm5);
            PedQuestion lsvcqtlm6 = new PedQuestion();
            lsvcqtlm6.Question = "Where are you heading to?";
            lsvcqtlm6.Answers = new List<string>
            {
                "Find the LORD!",
                "Where the LORD asked me to go.",
                "Beyond the great horizon for the LORD!",
            };
            AddPedQuestion(lsvcped, lsvcqtlm6);
            PedQuestion lsvcqtlm7 = new PedQuestion();
            lsvcqtlm7.Question = "You're under arrest for... <charges>";
            lsvcqtlm7.Answers = new List<string>
            {
                "You and your family are going to HELL!",
                "You can't do this, I follow the LORD!",
                "LORD HELP ME!  PLEASE LORD SMITE THIS PIG!  SAVE ME LORD!",
                "LORD SAVE ME!  HELP!  HELP!  I'M BEING OPPRESSED!"
            };
            AddPedQuestion(lsvcped, lsvcqtlm7);
        }

        public async Task Toothytime()
        //Coming soon - Chesterfield T. Toothy's whacky adventures with an air tug looking for street corner love.
        {
           Tick -= Toothytime;

           lsvctoothy = await SpawnPed(PedHash.OldMan2, Location + 2);
           tugtug = await SpawnVehicle(VehicleHash.Airtug, Location, 12);
           lsvctoothy.SetIntoVehicle(tugtug, VehicleSeat.Driver);

            //Ped Data
            PedData lsvctoothydata = new PedData();
        }
        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }
    }
}