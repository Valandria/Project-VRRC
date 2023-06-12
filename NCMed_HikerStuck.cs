using System;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace RangersoftheWildernessCallouts
{
    
    [CalloutProperties("NC Med Hiker Stuck", "Valandria", "0.1.1")]
    public class MedHikerStuck : Callout
    {
        private Ped vic;
        private Vector3[] coordinates = {
            new Vector3(-765.125f, 4342.06f, 146.31f),
            new Vector3(-789.051f, 4546.31f, 114.618f),
            new Vector3(-765.125f, 4342.06f, 146.31f),
            new Vector3(-664.3f, 4481.98f, 70.34f),
            new Vector3(-567.69f, 4438.48f, 24.45f),
            new Vector3(-591.45f, 4454.55f, 16.85f),
            new Vector3(-681.19f, 4478.9f, 55.42f),
            new Vector3(-909.6f, 4438.49f, 35.26f),
            new Vector3(-1935.35f, 486.18f, 18.9f),
            new Vector3(-1604.06f, 4270.34f, 105.09f),
            new Vector3(-625.27f, 4334.72f, 114.21f),
            new Vector3(-552.6f, 4440.08f, 33.05f),
            new Vector3(-537.33f, 4401.33f, 34.38f),
        };
        
        public MedHikerStuck()
        {
            InitInfo(coordinates[RandomUtils.Random.Next(coordinates.Length)]);
            Random random = new Random();
            ShortName = "NC Med - Hiker is stuck and requesting assistance.";
            CalloutDescription = "A hiker has become stuck and is requesting assistance.";
            ResponseCode = 2;
            StartDistance = 200f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            vic = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.07;
            Utilities.SetPedData(vic.NetworkId, data);
            vic.AlwaysKeepTask = true;
            vic.BlockPermanentEvents = true;
            PedData data1 = await Utilities.GetPedData(vic.NetworkId);
            string firstname = data1.FirstName;
            vic.AttachBlip();
            PedQuestion question1 = new PedQuestion();
            question1.Question = "Are you okay?";
            question1.Answers = new System.Collections.Generic.List<string>
            {
                "Yeah I'm fine.",
                "Yes, just a bit shaken.",
                "Fuck to the fuck no.",
                "I just wanna go home. *sobs*",
                "No."
            };
            PedQuestion question2 = new PedQuestion();
            question2.Question = "Do you have any injuries?";
            question2.Answers = new System.Collections.Generic.List<string>
            {
                "I-I'm not sure.",
                "Just some cuts and scrapes.",
                "I'm really thirsty.",
                "No just hungry.",
                "It's really hot.",
            };
            PedQuestion question3 = new PedQuestion();
            question3.Question = "How did you get here?";
            question3.Answers = new System.Collections.Generic.List<string>
            {
                "I was with a guide but I saw my opportunity with BlueGoat.",
                "I was part of a tour group but I saw my opportunity with BlueGoat.",
                "I drank some BlueGoat, it gives you goat-like reflexes!",
                "I was wanting to see the view and I saw my opportunity with BlueGoat.",
                "I'm a LifeInvader influencer for BlueGoat, you wouldn't get it boomer.",
            };
            PedQuestion question4 = new PedQuestion();
            question4.Question = "What is BlueGoat?";
            question4.Answers = new System.Collections.Generic.List<string>
            {
                "BlueGoat is the only energy drink to give you reflexes like a goat, to climb Mt. Chiliad!",
                "BlueGoat is the only energy drink to give you reflexes like a goat, to parkour away from the police!",
                "BlueGoat is the only energy drink to give you reflexes like a goat, to jump at vehicles head-on!",
                "BlueGoat is the only energy drink to give you reflexes like a goat, to belt beautiful ballads of justice.",
                "BlueGoat is the only energy drink to give you reflexes like a goat, to not be like Goat Man and dish out vigilante justice.",
            };
            PedQuestion question5 = new PedQuestion();
            question5.Question = "*Medical Observations (1)*";
            question5.Answers = new System.Collections.Generic.List<string>
            {
                "Patient has dry and pale skin, breathing rapidly, heart beating rapidly.",
                "Patient is breathing rapidly, heart beating rapidly.",
                "Patient has dry and pale skin, heart beating rapidly.",
                "Patient has red skin and heavily perspiring",
            };
            PedQuestion question6 = new PedQuestion();
            question6.Question = "*Medical Observations (2)*";
            question6.Answers = new System.Collections.Generic.List<string>
            {
                "Patient has no visible physical abnormalities.",
                "Patient has a few scratches, no active bleeding.",
                "Patient has a few scratches, actively bleeding.",
                "Patient has hives forming across body.",
            };
            AddPedQuestion(vic, question1);
            AddPedQuestion(vic, question2);
            AddPedQuestion(vic, question3);
            AddPedQuestion(vic, question4);
            AddPedQuestion(vic, question5);
            AddPedQuestion(vic, question6);
        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }
    }
}