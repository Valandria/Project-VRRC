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
    
    [CalloutProperties("Southern Command Active Shooter", "Valandria", "0.1.0")]
    public class SC_ActiveShooter : Callout
    {
        private Ped suspect, vic1, vic2, vic3, vic4, vic5;
        public SC_ActiveShooter()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if (x <= 10)
                InitInfo(new Vector3(-931.35f, -724.4f, 19.92f));
            if (x > 10 && x <= 20)
                InitInfo(new Vector3(-277.91f, -2071f, 27.76f));
            if (x > 20 && x <= 30)
                InitInfo(new Vector3(-1688.4f, -1059.91f, 13.0558f));
            if (x > 30 && x <= 40)
                InitInfo(new Vector3(-1036.38f, -2732.19f, 13.76f));
            if (x > 40 && x <= 50)
                InitInfo(new Vector3(-1073.27f, -2727.32f, 0.81f));
            if (x > 50 && x <= 60)
                InitInfo(new Vector3(-818.08f, -130.96f, 28.18f));
            if (x > 60 && x <= 70)
                InitInfo(new Vector3(-637.35f, -241.33f, 38.1f));
            if (x > 70 && x <= 80)
                InitInfo(new Vector3(-1497.57f, -431.17f, 35.59f));
            if (x > 80 && x <= 90)
                InitInfo(new Vector3(191.34f, -915.49f, 30.69f));
            if (x > 90)
                InitInfo(new Vector3(-600.74f, -115.34f, 41.74f));
            ShortName = "SC - Active Shooter";
            CalloutDescription = "Reports of an active shooter in the city!";
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