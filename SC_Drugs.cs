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
    
    [CalloutProperties("Southern Command Drug Deal", "Valandria", "0.0.2")]
    public class SC_Drugs : Callout
    {
        private Ped suspect, suspect2;

        public SC_Drugs()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if(x <= 40)
            { 
                InitInfo(new Vector3(-1591.88f, -927.26f, 8.98211f));
            }
            else if(x > 40 && x <= 65)
            {
                InitInfo(new Vector3(-1224.92f, -1803.93f, 2.36156f));
            }
            else
            {
                InitInfo(new Vector3(-1579.6f, -1021.78f, 7.64913f));
            }
            ShortName = "SC - Drug Deal in Progress";
            CalloutDescription = "Caller reports a possible drug deal in progress.";
            ResponseCode = 2;
            StartDistance = 200f;
        }
        
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect2 = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            
            //Suspect Data
            PedData data = new PedData();
            List<Item> items = new List<Item>();
            data.BloodAlcoholLevel = 0.25;
            Item DrugBags = new Item 
            {
                Name = "Drug Bag(s)",
                IsIllegal = true
            };
            items.Add(DrugBags);
            data.Items = items;
            Item Cash = new Item
            {
                Name = "$375 in mostly small bills",
                IsIllegal = false
            };
            items.Add(Cash);
            data.Items = items;
            Utilities.SetPedData(suspect.NetworkId,data);

            //Suspect2 Data
            PedData data2 = new PedData();
            List<Item> items2 = new List<Item>();
            data.BloodAlcoholLevel = 0.18;
            Item Drugs = new Item {
                Name = "Drugs",
                IsIllegal = true
            };
            items.Add(Drugs);
            data.Items = items2;
            Utilities.SetPedData(suspect2.NetworkId,data2);
            suspect2.Accuracy = 40;
            
            //Tasks
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            suspect2.AlwaysKeepTask = true;
            suspect2.BlockPermanentEvents = true;
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspects have been reported to have");
            Notify("~y~been exchanging baggies!");
            suspect.AttachBlip();
            suspect2.AttachBlip();
            PedData data1 = await Utilities.GetPedData(suspect.NetworkId);
            string firstname = data1.FirstName;
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if(x <= 40)
            { 
                DrawSubtitle("~r~[" + firstname + "] ~s~SHOOT COPS ARE HERE!", 5000);
                suspect.Task.ReactAndFlee(player);
                var pursuit = Pursuit.RegisterPursuit(suspect);
                suspect2.Task.ShootAt(player);
                var pursuit2 = Pursuit.RegisterPursuit(suspect2);
            }
            else if(x > 40 && x <= 65)
            {
                suspect.Task.ReactAndFlee(player);
                var pursuit = Pursuit.RegisterPursuit(suspect);
                suspect2.Task.ReactAndFlee(player);
                var pursuit2 = Pursuit.RegisterPursuit(suspect2);
                DrawSubtitle("~r~[" + firstname + "] ~s~BAIL! RUN!", 5000);
            }
            else
            {
                DrawSubtitle("~r~[" + firstname + "] ~s~Shoot, I give up!", 5000);
                suspect.Task.HandsUp(100000); 
                suspect2.Task.ReactAndFlee(player);
                var pursuit = Pursuit.RegisterPursuit(suspect2);
            }

            PedQuestion s1question1 = new PedQuestion();
            s1question1.Question = "What do you have on you?";
            s1question1.Answers = new List<string>
            {
                "Fuck you.",
                "Just got a few bags.",
                "Just got a few dime bags.",
                "I just got a little bit on me.",
                "Just a few eight balls.",
                "Just whatever's in my pocket."
            };
            AddPedQuestion(suspect, s1question1);
            PedQuestion s1question2 = new PedQuestion();
            s1question2.Question = "Are you willing to give up the person you buy from?";
            s1question2.Answers = new List<string>
            {
                "Nope.",
                "Just take me to jail bro.",
                "Fuck you.",
                "*Silence*",
                "I know my rights."
            };
            AddPedQuestion(suspect, s1question2);
            PedQuestion s1question3 = new PedQuestion();
            s1question3.Question = "Are you the local dealer around here?";
            s1question3.Answers = new List<string>
            {
                "Maybe.",
                "Depends, who's asking?",
                "Depends, you looking to buy?",
                "Nah, I buy from a guy named None-Ya",
                "I run these streets.",
                "*Silence*"
            };
            AddPedQuestion(suspect, s1question3);
            PedQuestion s2question1 = new PedQuestion();
            s2question1.Question = "What're you doing out here?";
            s2question1.Answers = new List<string>
            {
                "I'm just hanging around the area.",
                "Just hanging out with my friends, that a crime?",
                "I was just passing through and this guy tried to sell me drugs, I swear!",
                "This guy was robbing me!",
                "He just shoved drugs into my friends' jeans pockets, which I am wearing currently.",
                "They're not my drugs, I swear!",
                "*Silence*"
            };
            AddPedQuestion(suspect2, s2question1);
            PedQuestion s2question2 = new PedQuestion();
            s2question2.Question = "I never mentioned drugs.";
            s2question2.Answers = new List<string>
            { 
                "*Silence*",
                "This is entrapment!",
                "I mean, you sorta implied it.",
                "I heard you talking to the other guy.",
                "I heard you talking to my dealer though."
            };
            AddPedQuestion(suspect2, s2question2);
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