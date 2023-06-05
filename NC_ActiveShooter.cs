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
    
    [CalloutProperties("Northern Command Active Shooter", "Valandria", "0.1.0")]
    public class NC_ActiveShooter : Callout
    {
        private Ped suspect, vic1, vic2, vic3, vic4, vic5;
        public NC_ActiveShooter()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if (x <= 10)
                InitInfo(new Vector3(1990.03f, 3047.53f, 47.22f)); // Yellow Jack
            if (x > 10 && x <= 20)
                InitInfo(new Vector3(78.86f, 3707.71f, 40.99f)); // Stab City
            if (x > 20 && x <= 30)
                InitInfo(new Vector3(13981.82f, 4371.48f, 43.18f)); // 2008
            if (x > 30 && x <= 40)
                InitInfo(new Vector3(2828.69f, 4567.24f, 46.44f)); // 1051
            if (x > 40 && x <= 50)
                InitInfo(new Vector3(2781.97f, 3461.02f, 55.43f)); // U-Tool
            if (x > 50 && x <= 60)
                InitInfo(new Vector3(68.92f, 6407.78f, 31.23f)); // Cluckin' Bell Farms
            if (x > 60 && x <= 70)
                InitInfo(new Vector3(-272.65f, 6633.92f, 7.41f)); // Pier at 3018 
            if (x > 70 && x <= 80)
                InitInfo(new Vector3(-1106.94f, 2708.34f, 19.11f)); // Clothes shop at 1005
            if (x > 80 && x <= 90)
                InitInfo(new Vector3(-1585.43f, 5202.49f, 4.01f)); // Pier at 3001
            if (x > 90)
                InitInfo(new Vector3(1980.42f, 3710.13f, 32.09f)); // Across from 24/7 Sandy
            ShortName = "NC - Active Shooter";
            CalloutDescription = "Reports of an active shooter in the county!";
            ResponseCode = 3;
            StartDistance = 300f;
        }
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~y~Officer ~b~" + displayName + ",~y~ several reports of an active shooter have come in!");
            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            vic1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            vic2 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            vic3 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 4);
            vic4 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 5);
            vic5 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 1);
            //Suspect 1
            PedData data = new PedData();
            List<Item> items = new List<Item>();
            data.BloodAlcoholLevel = 0.08;
            Item Rifle = new Item {
                Name = "Rifle",
                IsIllegal = true
            };
            items.Add(Rifle);
            data.Items = items;
            Utilities.SetPedData(suspect.NetworkId,data);
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            suspect.AttachBlip();
            suspect.Weapons.Give(WeaponHash.MarksmanRifle, 1000, true, true);
            suspect.Accuracy = 50;
            suspect.RelationshipGroup = 0xCE133D78;
            suspect.Task.FightAgainstHatedTargets(this.StartDistance);
            PedQuestion question = new PedQuestion();
            question.Question = "What're you shoot at people for?";
            question.Answers = new List<string>
            {
                "Go to hell pig!",
                "Fuck you!",
                "You suck at aiming!",
                "Why didn't you kill me?",
                "*Silence*",
                "*Silence in Spanish*",
                "*Glares*",
                "*Stares*"
            };
            PedQuestion question2 = new PedQuestion();
            question2.Question = "What the hell is wrong with you?";
            question2.Answers = new List<string>
            {
                "Go to hell pig!",
                "Fuck you!",
                "You suck at aiming!",
                "Why didn't you kill me?",
                "*Silence*",
                "*Silence in Spanish*",
                "*Glares*",
                "*Stares*"
            };
            PedQuestion question3 = new PedQuestion();
            question3.Question = "Why did you do this?";
            question3.Answers = new List<string>
            {
                "Go to hell pig!",
                "Fuck you!",
                "You suck at aiming!",
                "Why didn't you kill me?",
                "*Silence*",
                "*Silence in Spanish*",
                "*Glares*",
                "*Stares*"
            };
            vic1.Kill();
            vic2.Kill();
            vic3.Kill();
            vic4.Kill();
            vic5.Kill();
            vic1.AttachBlip();
            vic2.AttachBlip();
            vic3.AttachBlip();
            vic4.AttachBlip();
            vic5.AttachBlip();
            PedData data1 = await Utilities.GetPedData(suspect.NetworkId);
            string firstname = data1.FirstName;
            DrawSubtitle("~r~[" + firstname + "] ~s~I knew this was coming... DIE!", 5000);
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