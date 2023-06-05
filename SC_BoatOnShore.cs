using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace BeachCallouts
{

    [CalloutProperties("Southern Command Boat Ashore", "Valandria", "0.0.4")]
    public class SC_BoatOnShore : Callout
    {
        private Vehicle car;
        Ped driver, passenger;
        private string[] carList = { "tug", "Dinghy", "Jetmax", "Speeder", "Speeder2", "Squalo", "Submersible", "Submersible2", "Suntrap", "Toro", "Tropic", "Tropic2" };

        public SC_BoatOnShore()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if (x <= 40)
            {
                InitInfo(new Vector3(-1576.69f, -1221.67f, 1.46055f));
            }
            else if (x > 40 && x <= 65)
            {
                InitInfo(new Vector3(-1435.31f, -1554.3f, 1.5076f));
            }
            else
            {
                InitInfo(new Vector3(-1767.66f, -1014.86f, 1.93437f));
            }
            ShortName = "SC - Boat Ashore";
            CalloutDescription = "A boat has run aground.";
            ResponseCode = 2;
            StartDistance = 150f;
        }
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            driver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            passenger = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            Random random = new Random();
            string cartype = carList[random.Next(carList.Length)];
            VehicleHash Hash = (VehicleHash)API.GetHashKey(cartype);
            car = await SpawnVehicle(Hash, Location);
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~y~ Officer ~b~" + displayName + ",~y~ a " + cartype + " has washed ashore!");

            //Driver Data
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.09;
            Utilities.SetPedData(driver.NetworkId, data);
            PedQuestion question = new PedQuestion();
            question.Question = "What's going on today?";
            question.Answers = new List<string>
            {
                "1",
                "2"
            };
            AddPedQuestion(driver, question);
            PedQuestion question2 = new PedQuestion();
            question2.Question = "Are you hurt?";
            question2.Answers = new List<string>
            {
                "I'm fine, just a rough landing.",
                "I think I hit my head really hard but I can't remember.",
                "As long as they don't take blood."
            };
            AddPedQuestion(driver, question2);
            PedQuestion question3 = new PedQuestion();
            question3.Question = "Let's have you come onto shore and away from the boat.";
            question3.Answers = new List<string>
            {
                "Can you help me down?",
                "Sure, lead the way."
            };
            AddPedQuestion(driver, question3);
            PedQuestion question4 = new PedQuestion();
            {
                question4.Question = "-= Medical Section =-";
                question4.Answers = new List<string>
            {
                "This is a placeholder, dummy!"
            };
            AddPedQuestion(driver, question4);
            PedQuestion question5 = new PedQuestion();
            question5.Question = "Is there anything specific hurting at the moment?";
            question5.Answers = new List<string>
            {
                "I hit my head and it's hurting a little.",
                "I got jerked around when I hit the shore but I don't think I hit anything.",
                "My body feels really stiff."
            };
            AddPedQuestion(driver, question5);
            PedQuestion question6 = new PedQuestion();
            question6.Question = "Would you like to be taken to the hospital?";
            question6.Answers = new List<string>
            {
                "Yes, I'd like to go.",
                "I'd rather not if I don't need to, but if you insist.",
                "No I'll be fine."
            };
            AddPedQuestion(driver, question6);
            PedQuestion medtask1 = new PedQuestion();
            medtask1.Question = "- Blood pressure -";
            medtask1.Answers = new List<string>
            {
                "Blood Pressure 83/50",
                "Blood Pressure 132/101",
                "Blood Pressure 119/72"
            };
            AddPedQuestion(driver, medtask1);
            PedQuestion medtask2 = new PedQuestion();
            medtask2.Question = "- Physical Check -";
            medtask2.Answers = new List<string>
            {
                "Forehead bruise, signs of concussion from eyes.",
                "Forehead bruise welt, sign of concussion from eyes.",
                "Forehead buise, no sign of concussion from eyes.",
                "Forehead bruise welt, no sign of concussion from eyes.",
                "No bruising, sign of concussion from eyes.",
                "No bruising, no sign of concussion from eyes.",
                "Nose bleeding, possibly broken, sign of concussion.",
                "Nose bleeding, possibly broken, no sign of concussion."
            };
            AddPedQuestion(driver, medtask2);

            PedQuestion pquestion = new PedQuestion();
            pquestion.Question = "Are you okay?";
            pquestion.Answers = new List<string>
            {
                "I was thrown from the boat but I think I'm okay.",
                "My arm hurts a lot.",
                "I was thrown and I think I broke my arm, it hurts!",
                "My leg hurts  alot, I was thrown.",
                "Both of my legs hurt and so does my back.",
                "I don't know, I'm shaken not stirred.",
                "It hurts, I need an ambulance."
            };
            AddPedQuestion(passenger, pquestion);
            PedQuestion pquestion2 = new PedQuestion();
            pquestion2.Question = "I have an ambulance on the way.";
            pquestion.Answers = new List<string>
            {
                "*Sobs in pain*",
                "*Groans in pain*",
                "*Cries in pain*",
                "*Acknowledges your statement in pain*",
                "*Lays in pain in Spanish*",
                "My back hurts so much."
            };
            AddPedQuestion(passenger, pquestion2);
            PedQuestion pquestion3 = new PedQuestion();
            pquestion3.Question = "Have you and the driver been drinking today?";
            pquestion3.Answers = new List<string>
            {
                "We've had a few, but that's it.",
                "They had a lot more than me, like two or three times.",
                "I'm not going to rat them out.",
                "I can't say."
            };
            AddPedQuestion(passenger, pquestion3);
            PedQuestion pquestion4 = new PedQuestion();
            pquestion4.Question = "- Medical Section-";
            pquestion4.Answers = new List<string>
            {
                "This is a placeholder dummy!"
            };
            AddPedQuestion(passenger, pquestion4);
            PedQuestion pquestion5 = new PedQuestion();
            pquestion5.Question = "Where at on your arm is it hurting?";
            pquestion5.Answers = new List<string>
            {
                "All over.",
                "Around the elbow mostly.",
                "From my wrist to my elbow",
                "Mostly around my shoulder but it's shooting pain though the whole thing.",
                "It's around my wrist and above it."
            };
            AddPedQuestion(passenger, pquestion5);
            PedQuestion pquestion6 = new PedQuestion();
            pquestion6.Question = "Where at on your leg is it hurting?";
            pquestion6.Answers = new List<string>
            {
                "All over.",
                "Around the knee mostly.",
                "From my ankle to my knee.",
                "Mostly around my hip but it's shooting pain through the whole thing.",
                "Mostly around my tailbone but it's shooting pain through the whole thing.",
                "It's all around my ankle and above it."
            };
            AddPedQuestion(passenger, pquestion6);
            PedQuestion pquestion7 = new PedQuestion();
            pquestion7.Question = "Where at on your back is it hurting?";
            pquestion7.Answers = new List<string>
            {
                "All over.",
                "It's around the tailbone and lower back.",
                "It's my upper back around my shoulders.",
                "It's my upper back around my neck.",
                "Mostly up and down the middle"
            };
            AddPedQuestion(passenger, pquestion7);
            PedQuestion pmedtask1 = new PedQuestion();
            pmedtask1.Question = "- Physical Check; Arm";
            pmedtask1.Answers = new List<string>
            {
                "Bruising on arm, no visual broken bone(s).",
                "Bruising on arm, visually broken bone(s).",
                "No bruising on arm, no visual broken bone(s).",
                "No bruising on arm, visual broken bone(s).",
                "Bone visible through laceration in arm."
            };
            AddPedQuestion(passenger, pmedtask1);
            PedQuestion pmedtask2 = new PedQuestion();
            pmedtask2.Question = "- Physical Check; Leg";
            pmedtask2.Answers = new List<string>
            {
                "Bruising on leg, no visual broken bone(s).",
                "Bruising on leg, visually broken bone(s).",
                "No bruising on leg, no visual broken bone(s).",
                "No bruising on leg, visually broken bone(s).",
                "Bone visible through laceration in leg."
            };
            AddPedQuestion(passenger, pmedtask2);
                PedQuestion pmedtask3 = new PedQuestion();
                pmedtask3.Question = "- Physical Check; Back";
                pmedtask3.Answers = new List<string>
            {
                "Bruising on back, no visual broken bone(s).",
                "Bruising on back, visually broken bone(s).",
                "No bruising on back, no visual broken bone(s).",
                "No bruising on back, visually broken bone(s).",
                "Bone visible through laceration in back."
            };

                //Passenger Data
                PedData data2 = new PedData();
                List<Item> items = new List<Item>();
                data2.BloodAlcoholLevel = 0.06;
                Item Pistol = new Item {
                    Name = "Pistol",
                    IsIllegal = true
                };
                items.Add(Pistol);
                data2.Items = items;
                Utilities.SetPedData(passenger.NetworkId, data2);
                //Tasks
                driver.AlwaysKeepTask = true;
                driver.BlockPermanentEvents = true;
                passenger.AlwaysKeepTask = true;
                passenger.BlockPermanentEvents = true;
                passenger.Weapons.Give(WeaponHash.Pistol, 20, true, true);
                driver.Task.WanderAround();
                car.AttachBlip();
                driver.AttachBlip();
                passenger.AttachBlip();
                API.Wait(6000);
                PedData data4 = await Utilities.GetPedData(passenger.NetworkId);
                PedData data1 = await Utilities.GetPedData(driver.NetworkId);
                string firstname2 = data4.FirstName;
                string firstname = data1.FirstName;
                DrawSubtitle("~r~[" + firstname2 + "] ~s~I am sorry I can't be blamed for this!", 5000);
                passenger.Kill();
                API.Wait(2000);
                DrawSubtitle("~r~[" + firstname + "] ~s~NO WHY WOULD YOU DO THAT!", 5000);
                API.Wait(5000);
                DrawSubtitle("~r~[" + firstname + "] ~s~I need to get out of here....", 5000);
                driver.SetIntoVehicle(car, VehicleSeat.Driver);
                driver.Task.FleeFrom(player);
            };
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