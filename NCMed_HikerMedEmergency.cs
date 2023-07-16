using System;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace RangersoftheWildernessCallouts
{
    
    [CalloutProperties("NC Med - Hiker Medical Emergency", "Valandria", "0.1.0")]
    public class NCMedWilderness : Callout
    {
        private Ped vicmedwild;
        private Vector3[] coordinates = {
            new Vector3(-1114.65f, 4837.19f, 207.55f),
            new Vector3(-1028.83f, 4712.69f, 238.54f),
            new Vector3(-860.12f, 4831.19f, 297.18f),
            new Vector3(-603.74f, 4761.92f, 210.81f),
            new Vector3(-498.34f, 4727.65f, 242.08f),
            new Vector3(-283.07f, 4673.92f, 240.91f),
            new Vector3(-64.24f, 4986.12f, 401.99f),
            new Vector3(39.44f, 5029.08f, 461.12f),
            new Vector3(123.54f, 5143.53f, 519.49f),
            new Vector3(131.58f, 5191.9f, 549.43f),
            new Vector3(226.34f, 5298.39f, 620f),
            new Vector3(551.42f, 5512.73f, 773.97f),
            new Vector3(519.76f, 5547.06f, 777.58f),
            new Vector3(432.64f, 5617.49f, 765.91f),
            new Vector3(1399.93f, 5539.27f, 464.3f),
            new Vector3(1909.21f, 5828.85f, 290.39f),
            new Vector3(2262.96f, 5772.79f, 151.95f),
            new Vector3(2158.94f, 5382.31f, 165.3f),
            new Vector3(2356.76f, 5339.32f, 116.81f),
            new Vector3(-260.51f, 4376.55f, 39.58f),
            new Vector3(-450.79f, 4559.07f, 106.43f),
            new Vector3(-656.16f, 4515.06f, 86.54f),
            new Vector3(-998.85f, 4556.48f, 128.35f),
            new Vector3(1159.66f, 4566.36f, 142.69f),
            new Vector3(-1207.49f, 4621f, 140.01f),
            new Vector3(-1253.68f, 4610.91f, 131.2f),
            new Vector3(-1379.52f, 4723.68f, 44.9f),
            new Vector3(-1495.73f, 4685.46f, 35.98f),
            new Vector3(-1616.06f, 4735.64f, 52.72f),
            new Vector3(-1534.67f, 4924.95f, 57.34f),
            new Vector3(-1150.16f, 5030.39f, 157.88f),
            new Vector3(-1018.91f, 4990.65f, 182.62f),
            new Vector3(-744.78f, 5079.53f, 134.46f),
            new Vector3(-373.54f, 4939.76f, 198.01f),
        };
        
        public NCMedWilderness()
        {
            InitInfo(coordinates[RandomUtils.Random.Next(coordinates.Length)]);
            ShortName = "NC Med - Medical Emergency in the Wilderness";
            CalloutDescription = "An individual is having a medical emergency in the wilderness, GPS location is being sent now.";
            ResponseCode = 3;
            StartDistance = 200f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            vicmedwild = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            PedData data = new PedData();
            Utilities.SetPedData(vicmedwild.NetworkId, data);
            vicmedwild.AlwaysKeepTask = true;
            vicmedwild.BlockPermanentEvents = true;
            PedData vicmedwilddata = await Utilities.GetPedData(vicmedwild.NetworkId);
            string firstname = vicmedwilddata.FirstName;
            vicmedwild.AttachBlip();

            Random random = new Random();
            int dedped = random.Next(1, 100 + 1);
            if (dedped > 80)
            {
                vicmedwild.Kill();
            }
            Random victypepedspawn = new Random();
            int medissue = victypepedspawn.Next(1, 100 + 1);
            if (medissue <= 20)
            {
                await BaseScript.Delay(3000);
                Tick += MedicationRelated;
            }
            if (medissue > 20 && medissue <= 40)
            {
                await BaseScript.Delay(3000);
                Tick += AsthmaRelated;
            }
            if (medissue > 40 && medissue <= 50)
            {
                await BaseScript.Delay(3000);
                Tick += AnimalRelated;
            }
            if (medissue > 50 && medissue <= 70)
            {
                await BaseScript.Delay(3000);
                Tick += CardiacRelated;
            }
            if (medissue > 70 && medissue <= 90)
            {
                await BaseScript.Delay(3000);
                Tick += HeatRelated;
            }
            if (medissue > 90)
            {
                await BaseScript.Delay(3000);
                Tick += GSWRelated;
            };
        }

        public async Task MedicationRelated()
        {
            Random mrtype = new Random();
            int mrtypedecider = mrtype.Next(1, 100 + 1);
            if (mrtypedecider <= 33)
            {
                await BaseScript.Delay(3000);
                Tick += MRHeart;
            }
            if (mrtypedecider > 33 && mrtypedecider <= 66)
            {
                await BaseScript.Delay(3000);
                Tick += MRSeizure;
            }
            if (mrtypedecider > 66)
            {
                await BaseScript.Delay(3000);
                Tick += MROrgan;
            }
        }

        public async Task AsthmaRelated()
        {
            PedData ARdata = new PedData();

            Item ARitem1 = new Item
            {
                Name = "Scrapes on left arm",
            };
            Item ARitem2 = new Item
            {
                Name = "Scrapes on right arm",
            };
            Item ARitem3 = new Item
            {
                Name = "Scrapes on left knee",
            };
            Item ARitem4 = new Item
            {
                Name = "Scrapes on right knee",
            };
            Item ARitem5 = new Item
            {
                Name = "Bruising around left eye",
            };
            Item ARitem6 = new Item
            {
                Name = "Bruising around right eye",
            };
            Item ARitem7 = new Item
            {
                Name = "Swelling on left forehead",
            };
            Item ARitem8 = new Item
            {
                Name = "Swelling on right forehead",
            };
            Item ARitem9 = new Item
            {
                Name = "Swelling on left rear of head",
            };
            Item ARitem10 = new Item
            {
                Name = "Swelling on right rear of head",
            };

            PedQuestion ARmenu1 = new PedQuestion();
            ARmenu1.Question = "-= Medical Questions =-";

            PedQuestion ARquestion1 = new PedQuestion();
            ARquestion1.Question = "What's going on today?";
            ARquestion1.Answers = new System.Collections.Generic.List<string>
            {
                "I didn't bring my inhaler and I'm short on breath.",
                "I didn't bring my inhaler and my chest hurts.",
                "I lost my inhaler on the hike.",
                "I don't know, I just felt weak and lost my balance.",
                "I didn't bring my inhaler and it's hard to breathe.",
                "I have asthma and forgot my inhaler.",
            };

            PedQuestion ARquestion1followup = new PedQuestion();
            ARquestion1followup.Question = "Do you take medication for anything?";
            ARquestion1followup.Answers = new System.Collections.Generic.List<string>
            {
                "Nothing additional.",
                "Nothing additional.",
                "Nothing additional.",
                "Nothing additional.",
                "Nothing additional.",
                "No I don't.",
                "No.",
                "Nope.",
                "I take vitamins and suppliments.",
                "My doctor prescribes me blood pressure medication.",
            };

            PedQuestion ARquestion1followup2 = new PedQuestion();
            ARquestion1followup2.Question = "Can you describe the pain in your chest?";
            ARquestion1followup2.Answers = new System.Collections.Generic.List<string>
            {
                "It feels like someone is pressing on my chest.",
                "It feels like pins and needles in my chest.",
                "My lungs feel like they're being pressed.",
                "I feel like someone is squeezing my lungs.",
                "It's like someone is pressing down on my chest.",
            };

            PedQuestion ARquestion2 = new PedQuestion();
            ARquestion2.Question = "Did you fall and/or lose conciousness at any point?";
            ARquestion2.Answers = new System.Collections.Generic.List<string>
            {
                "No.",
                "No.",
                "No.",
                "No.",
                "I never fell down.",
                "No i did not.",
                "I did trip but I'm fine.",
                "I-I'm not sure.",
                "I don't remember.",
                "I fell but I never lost conciousness.",
            };

            PedQuestion ARquestion3 = new PedQuestion();
            ARquestion3.Question = "Was there anyone else with you?";
            ARquestion3.Answers = new System.Collections.Generic.List<string>
            {
                "No I came alone.",
                "I can't remember, I think I was?",
                "I mean, for you, I absolutely came alone, but I don't wanna tonight.",
                "I didn't come with anyone I cared about.",
                "No one else.",
            };

            PedQuestion ARmenu2 = new PedQuestion();
            ARmenu2.Question = "-= Medical Assessments =-";

            PedQuestion ARquestion4 = new PedQuestion();
            ARquestion4.Question = "*Check Blood Pressure and Pulse (B/P,P)*";
            ARquestion4.Answers = new System.Collections.Generic.List<string>
            {
                "*147/121, 105*",
                "*138/103, 120*",
                "*91/59, 53*",
                "*131/101, 72*",
                "*99/62, 68*",
                "*107/88, 73*",
                "*116/79, 65*",
                "*121/76, 83*",
                "*126/83, 61*",
                "*111/75, 52*",
                "*119/72, 54*",
                "*101/64, 50*",
                "*82/44, 46*",
            };

            PedQuestion ARquestionekg = new PedQuestion();
            ARquestionekg.Question = "*Attach ECG/EKG and check electrical signals.*";
            ARquestionekg.Answers = new System.Collections.Generic.List<string>
            {
                "*ECG/EKG results clean.*",
                "*ECG/EKG results clean.*",
                "*ECG/EKG results clean.*",
                "*ECG/EKG results clean.*",
                "*ECG/EKG results clean.*",
                "*ECG/EKG results clean.*",
                "*Atrial fibrillation (Heart Failure/Stroke)*",
                "*Atrial flutter (Stroke)*",
            };

            Random concusyn = new Random();
            int yesconcus = concusyn.Next(1, 100 + 1);
            if (yesconcus <= 50)
            {
                    PedQuestion ARquestionconcussionyes = new PedQuestion();
                    ARquestionconcussionyes.Question = "*Concussion check*";
                    ARquestionconcussionyes.Answers = new System.Collections.Generic.List<string>
                {
                    "*Patient shows signs of concussion.*",
                };
                AddPedQuestion(vicmedwild, ARquestionconcussionyes);                
            }

            if (yesconcus > 50)
            {
                PedQuestion ARquestionconcussionno = new PedQuestion();
                ARquestionconcussionno.Question = "*Concussion check*";
                ARquestionconcussionno.Answers = new System.Collections.Generic.List<string>
                {
                    "*Patient does not show signs of concussion.*",
                };
                AddPedQuestion(vicmedwild, ARquestionconcussionno);
            }

            AddPedQuestion(vicmedwild, ARmenu1);
            AddPedQuestion(vicmedwild, ARquestion1);
            AddPedQuestion(vicmedwild, ARquestion1followup);
            AddPedQuestion(vicmedwild, ARquestion1followup2);
            AddPedQuestion(vicmedwild, ARquestion2);
            AddPedQuestion(vicmedwild, ARquestion3);
            AddPedQuestion(vicmedwild, ARquestion4);
            AddPedQuestion(vicmedwild, ARmenu2);
            AddPedQuestion(vicmedwild, ARquestionekg);   
        }

        public async Task AnimalRelated()
        {
            PedQuestion ANRmenu1 = new PedQuestion();
            ANRmenu1.Question = "-= Medical Questions =-";

            PedQuestion ANRquestion1 = new PedQuestion();
            ANRquestion1.Question = "Can you tell me what happened?";
            ANRquestion1.Answers = new System.Collections.Generic.List<string>
            {
                "I was attacked by some sort of animal.",
                "I was attached by a cougar.",
                "I was attacked by some large cat.",
                "Some dog attacked me.",
                "I was attacked by a dog.",
                "Some mangie mutt attacked me.",
                "I got attacked, I think I still hear it nearby!",
                "I, I think something attacked me, but I don't remember.",
                "I was attacked by a wild animal.",
                "It was a giant deer!  Ten feet tall with webbed antlers!",
                "It must have been bigfoot, I've been waiting for him to come out.",
                "I bet it was the feds, they know I'm on their trail for killing Jay Norris.",
                "It was a cougar brainwashed by the feds to attack me, they're sending a message.",
                "It was a cougar brainwashed by Epsilon to attack me, they're sending a message.",
                "It was a cougar that was tortured by bigfoot, it's really angry."
            };

            PedQuestion ANRquestion1followup = new PedQuestion();
            ANRquestion1followup.Question = "Do you know which way the animal went that attacked you?";
            ANRquestion1followup.Answers = new System.Collections.Generic.List<string>
            {

            };

            PedQuestion ANRquestion2 = new PedQuestion();
            ANRquestion2.Question = "Are you able to walk?";
            ANRquestion2.Answers = new System.Collections.Generic.List<string>
            {
                "I think I can walk.",
                "I should be able to.",
                "I feel a bit faint.",
                "I feel a bit dizzy.",
                "I think a strong hunk like you should carry me.",
                "But you came all this way, the least you could do is carry me.",
                "I'm not sure."
            };

            PedQuestion ANRquestion3 = new PedQuestion();
            ANRquestion3.Question = "Do you have any medical history or taking any medications right now?";

            PedQuestion ANRquestion4 = new PedQuestion();
            ANRquestion4.Question = "Did you take any sort of hallucinogens or other type of substance?";

            PedQuestion ANRmenu2 = new PedQuestion();
            ANRmenu2.Question = "-= Drug Related Followup =-";

            PedQuestion ANRquestion4followupweed = new PedQuestion();
            ANRquestion4followupweed.Question = "How much marijuana did you consume?";

            PedQuestion ANRquestion4followupweed2 = new PedQuestion();
            ANRquestion4followupweed2.Question = "How did you consume it?";

            PedQuestion ANRquestion4followupweed3 = new PedQuestion();
            ANRquestion4followupweed3.Question = "How long ago was that?";

            PedQuestion ANRquestion4followupmeth = new PedQuestion();
            ANRquestion4followupmeth.Question = "How much meth did you use?";

            PedQuestion ANRquestion4followupmeth2 = new PedQuestion();
            ANRquestion4followupmeth2.Question = "How did you consume it?";

            PedQuestion ANRquestion4followupmeth3 = new PedQuestion();
            ANRquestion4followupmeth3.Question = "How long ago was that?";

            PedQuestion ANRquestion4followupcrack = new PedQuestion();
            ANRquestion4followupcrack.Question = "How much crack did you use?";

            PedQuestion ANRquestion4followupcrack2 = new PedQuestion();
            ANRquestion4followupcrack2.Question = "Did you smoke it?";

            PedQuestion ANRquestion4followupcrack3 = new PedQuestion();
            ANRquestion4followupcrack3.Question = "How long ago was that?";

            PedQuestion ANRquestion4followuplsd = new PedQuestion();
            ANRquestion4followuplsd.Question = "How much LSD did you take?";

            PedQuestion ANRquestion4followuplsd2 = new PedQuestion();
            ANRquestion4followuplsd2.Question = "Was it in sheet form or liquid?";

            PedQuestion ANRquestion4followuplsd3 = new PedQuestion();
            ANRquestion4followuplsd3.Question = "How long ago was that?";

            PedQuestion ANRquestion4followupcoke = new PedQuestion();
            ANRquestion4followupcoke.Question = "How much coke did you use?";

            PedQuestion ANRquestion4followupcoke2 = new PedQuestion();
            ANRquestion4followupcoke2.Question = "Did you snort it?";

            PedQuestion ANRquestion4followupcoke3 = new PedQuestion();
            ANRquestion4followupcoke3.Question = "How long ago was that?";

            PedQuestion ANRquestion4followupmushrooms = new PedQuestion();
            ANRquestion4followupmushrooms.Question = "How many mushrooms did you eat?";

            PedQuestion ANRquestion4followupmushrooms2 = new PedQuestion();
            ANRquestion4followupmushrooms2.Question = "How long ago was that?";

            PedQuestion ANRmenu3 = new PedQuestion();
            ANRmenu3.Question = "-= Mental Health Questions =-";

            PedQuestion ANRquestion5 = new PedQuestion();
            ANRquestion5.Question = "Was there anyone else with you at the time you were attacked?";

            AddPedQuestion(vicmedwild, ANRmenu1);
            AddPedQuestion(vicmedwild, ANRquestion1);
            AddPedQuestion(vicmedwild, ANRquestion1followup);
            AddPedQuestion(vicmedwild, ANRquestion2);
            AddPedQuestion(vicmedwild, ANRquestion3);
            AddPedQuestion(vicmedwild, ANRquestion4);
            AddPedQuestion(vicmedwild, ANRquestion4followupcoke);
            AddPedQuestion(vicmedwild, ANRquestion4followupcoke2);
            AddPedQuestion(vicmedwild, ANRquestion4followupcoke3);
            AddPedQuestion(vicmedwild, ANRquestion4followupcrack);
            AddPedQuestion(vicmedwild, ANRquestion4followupcrack2);
            AddPedQuestion(vicmedwild, ANRquestion4followupcrack3);
            AddPedQuestion(vicmedwild, ANRquestion4followuplsd);
            AddPedQuestion(vicmedwild, ANRquestion4followuplsd2);
            AddPedQuestion(vicmedwild, ANRquestion4followuplsd3);
            AddPedQuestion(vicmedwild, ANRquestion4followupmeth);
            AddPedQuestion(vicmedwild, ANRquestion4followupmeth2);
            AddPedQuestion(vicmedwild, ANRquestion4followupmeth3);
            AddPedQuestion(vicmedwild, ANRquestion4followupmushrooms);
            AddPedQuestion(vicmedwild, ANRquestion4followupmushrooms2);
            AddPedQuestion(vicmedwild, ANRquestion4followupweed);
            AddPedQuestion(vicmedwild, ANRquestion4followupweed2);
            AddPedQuestion(vicmedwild, ANRquestion4followupweed3);
            AddPedQuestion(vicmedwild, ANRquestion5);
        }

        public async Task CardiacRelated()
        {

        }

        public async Task HeatRelated()
        {
            Random heattype = new Random();
            int heattypedecider = heattype.Next(1, 100 + 1);
            if (heattypedecider < 50)
            {
                Tick += HRExhausted;
            }
            if (heattypedecider >= 50)
            {
                Tick += HRStroke;
            }
        }

        public async Task GSWRelated()
        {

        }


        public async Task MRHeart()
        { 
            PedQuestion MRHmenu1 = new PedQuestion();
            MRHmenu1.Question = "-= Medical Questions =-";

            PedQuestion MRHquestion1 = new PedQuestion();
            MRHquestion1.Question = "What's going on today?";
            MRHquestion1.Answers = new System.Collections.Generic.List<string>
            {
                "I forgot to take my medication this morning.",
                "I didn't bring my medication with me today.",
                "I lost my medication on the hike.",
                "I don't know, I just felt weak and lost my balance.",
            };

            PedQuestion MRHquestion1followup = new PedQuestion();
            MRHquestion1followup.Question = "Do you take medication for anything?";
            MRHquestion1followup.Answers = new System.Collections.Generic.List<string>
            {
                "I have a heart condition.",
                "My spouse makes me take medication for my heart.",
                "My doctor prescribes me heart medication.",
            };

            PedQuestion MRHquestion1followup2 = new PedQuestion();
            MRHquestion1followup2.Question = "What do you take medication for?";
            MRHquestion1followup2.Answers = new System.Collections.Generic.List<string>
            {
                "I have a heart condition.",
                "My spouse makes me take medication for my heart.",
                "My doctor prescribes me heart medication.",
                "History of cardiac issues.",
                "Defective hearts run is genetic in my family.",
                "Enjoying good food and having a weak heart.",
            };

            PedQuestion MRHquestion1followup3 = new PedQuestion();
            MRHquestion1followup3.Question = "Are you feeling any chest pain?";
            MRHquestion1followup3.Answers = new System.Collections.Generic.List<string>
            {
                "A little.",
                "I'm feeling tightness in my chest.",
                "I have a little tinglging in my chest... and pants.",
                "I have a little tinglging in my chest.",
                "I have sharp pain in the left side.",
                "I have sharp pain in the right side.",
                "No pain.",
                "No pain.",
                "No pain.",
                "No pain.",
                "No pain.",
                "No pain.",
            };

            PedQuestion MRHquestion1followup4 = new PedQuestion();
            MRHquestion1followup4.Question = "Do you believe your heart condiiton caused this?";
            MRHquestion1followup4.Answers = new System.Collections.Generic.List<string>
            {
                "Yes.",
                "I don't know.",
                "I don't believe so but it's possible.",
                "Not likely, I have no chest pain.",
            };

            PedQuestion MRHquestion2 = new PedQuestion();
            MRHquestion2.Question = "Did you fall and/or lose conciousness at any point?";
            MRHquestion2.Answers = new System.Collections.Generic.List<string>
            {
                "I-I'm not sure.",
                "I don't remember.",
                "I fell but I never lost conciousness.",
                "I did lose conciousness.",
            };

            PedQuestion MRHquestion3 = new PedQuestion();
            MRHquestion3.Question = "Do you remember how you got here?";
            MRHquestion3.Answers = new System.Collections.Generic.List<string>
            {
                "I remember walking up the trail.",
                "I sorta remember coming to the trail to walk.",
                "I just remmeber going to bed yesterday.",
                "What do you mean?  I'm at home.",
                "Why are you people in my home?",
            };

            PedQuestion MRHquestion4 = new PedQuestion();
            MRHquestion4.Question = "Was there anyone else with you?";
            MRHquestion4.Answers = new System.Collections.Generic.List<string>
            {
                "No I came alone.",
                "I can't remember, I think I was?",
                "I mean, for you, I absolutely came alone, but I don't wanna tonight.",
                "I didn't come with anyone I cared about.",
                "No one else.",
            };

            PedQuestion MRHmenu2 = new PedQuestion();
            MRHmenu2.Question = "-= Medical Assessments =-";

            PedQuestion MRHquestion5 = new PedQuestion();
            MRHquestion5.Question = "*Check Blood Pressure and Pulse (B/P,P)*";
            MRHquestion5.Answers = new System.Collections.Generic.List<string>
            {
                "*147/121, 105*",
                "*138/103, 120*",
                "*91/59, 53*",
                "*131/101, 72*",
                "*99/62, 68*",
                "*107/88, 73*",
                "*116/79, 65*",
                "*121/76, 83*",
                "*126/83, 61*",
                "*111/75, 52*",
                "*119/72, 54*",
                "*101/64, 50*",
                "*82/44, 46*",
            };

            PedQuestion MRHquestion6 = new PedQuestion();
            MRHquestion6.Question = "*Physical Observation(s)*";
            MRHquestion6.Answers = new System.Collections.Generic.List<string>
            {
                "*Patient has no visible physical abnormalities.*",
                "*Patient has no visible physical abnormalities.*",
                "*Patient has no visible physical abnormalities.*",
                "*Patient has no visible physical abnormalities.*",
                "*Patient has no visible physical abnormalities.*",
                "*Patient has no visible physical abnormalities.*",
                "*Patient has no visible physical abnormalities.*",
                "*Patient has no visible physical abnormalities.*",
                "*Patient has a few scratches, no active bleeding.*",
                "*Patient has a few scratches, actively bleeding.*",
                "*Patient has a lump on their forehead.*",
                "*Patient has a laceration across their forehead.*",
                "*Patient is bleeding from their ear(s).*",
                "*Patient is bleeding from their nose.*",
                "*Patient is bleeding from their left eye.*",
                "*Patient is bleeding from their right eye.*",
            };

            PedQuestion MRHquestionekg = new PedQuestion();
            MRHquestionekg.Question = "*Attach ECG/EKG and check electrical signals.*";
            MRHquestionekg.Answers = new System.Collections.Generic.List<string>
            {
                "*ECG/EKG results clean.*",
                "*ECG/EKG results clean.*",
                "*ECG/EKG results clean.*",
                "*ECG/EKG results clean.*",
                "*ECG/EKG results clean.*",
                "*ECG/EKG results clean.*",
                "*Atrial fibrillation (Heart Failure/Stroke)*",
                "*Atrial flutter (Stroke)*",
            };

            PedQuestion MRHquestionconcussion = new PedQuestion();
            MRHquestionconcussion.Question = "*Concussion check*";
            MRHquestionconcussion.Answers = new System.Collections.Generic.List<string>
            {
                "*Patient shows no sign of concussion.*",
                "*Patient shows no sign of concussion.*",
                "*Patient shows no sign of concussion.*",
                "*Patient shows signs of concussion.*",
            };
            AddPedQuestion(vicmedwild, MRHmenu1);
            AddPedQuestion(vicmedwild, MRHquestion1);
            AddPedQuestion(vicmedwild, MRHquestion1followup);
            AddPedQuestion(vicmedwild, MRHquestion1followup2);
            AddPedQuestion(vicmedwild, MRHquestion1followup3);
            AddPedQuestion(vicmedwild, MRHquestion1followup4);
            AddPedQuestion(vicmedwild, MRHquestion2);
            AddPedQuestion(vicmedwild, MRHquestion3);
            AddPedQuestion(vicmedwild, MRHquestion4);
            AddPedQuestion(vicmedwild, MRHmenu2);
            AddPedQuestion(vicmedwild, MRHquestion5);
            AddPedQuestion(vicmedwild, MRHquestion6);
            AddPedQuestion(vicmedwild, MRHquestionekg);
            AddPedQuestion(vicmedwild, MRHquestionconcussion);
        }

        public async Task MROrgan()
        {

        }

        public async Task MRSeizure()
        {

        }

        public async Task HRExhausted()
        {

        }

        public async Task HRStroke()
        {

        }

        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }
    }
}