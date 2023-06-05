using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;



[CalloutProperties("Local Med Vehicle Collision", "Valandria", "0.1.0")]
public class CarCrash : FivePD.API.Callout
{
    private Ped driver1, driver2;
    private Vehicle car1, car2;
    public CarCrash()
    {
        InitInfo(World.GetNextPositionOnStreet(Vector3Extension.Around(Game.PlayerPed.Position, 400)));

        ShortName = "L - Vehicle Collision";
        CalloutDescription = "A vehicle collision has occured. Get the location and assess.";
        ResponseCode = 1;
        StartDistance = 150f;
    }

    public override async Task OnAccept()
    {
        InitBlip(25);

        var cars = new[]
          {
               VehicleHash.Adder,
               VehicleHash.CarbonRS,
               VehicleHash.Oracle,
               VehicleHash.Oracle2,
               VehicleHash.Phoenix,
               VehicleHash.Vigero,
               VehicleHash.Zentorno,
               VehicleHash.Youga2,
               VehicleHash.Youga,
               VehicleHash.Sultan,
               VehicleHash.SultanRS,
               VehicleHash.Sentinel,
               VehicleHash.Sentinel2,
               VehicleHash.Ruiner,
               VehicleHash.Ruiner2,
               VehicleHash.Ruiner3,
               VehicleHash.Burrito,
               VehicleHash.Burrito2,
               VehicleHash.Burrito3,
               VehicleHash.GBurrito,
               VehicleHash.Bagger,
               VehicleHash.Buffalo,
               VehicleHash.Buffalo2,
               VehicleHash.Comet2,
               VehicleHash.Comet3,
               VehicleHash.Felon,
           };

        car1 = await SpawnVehicle(cars[RandomUtils.Random.Next(cars.Length)], Location, 180);
        car2 = await SpawnVehicle(cars[RandomUtils.Random.Next(cars.Length)], Location);
        World.ShootBullet(Location, car1.Position, Game.PlayerPed, WeaponHash.RayPistol, 0);
        World.ShootBullet(Location, car1.Position, Game.PlayerPed, WeaponHash.RayPistol, 0);
        car1.Deform(Location, 10000, 100);
        car2.Deform(Location, 10000, 100);
        car1.EngineHealth = 5;
        car2.EngineHealth = 5;
        car1.BodyHealth = 1;
        car2.BodyHealth = 2;
        
        API.Wait(2);

        driver1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 5);
        driver2 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 6, 180);

        driver1.AlwaysKeepTask = true;
        driver1.BlockPermanentEvents = true;

        driver2.AlwaysKeepTask = true;
        driver2.BlockPermanentEvents = true;

    }

    public override void OnStart(Ped player)
    {
        base.OnStart(player);

        car1.Deform(Location, 10000, 100);
        car2.Deform(Location, 10000, 100);
        World.ShootBullet(Location, car1.Position, Game.PlayerPed, WeaponHash.RayPistol, 0);
        driver1.AttachBlip();
        driver2.AttachBlip();
        car1.AttachBlip();
        car2.AttachBlip();

        PedQuestion medq = new PedQuestion(); 
        medq.Question = "Are you okay?";
        medq.Answers = new List<string>
        {
            "I hit my head pretty hard.",
            "I hit my head and my chest hurts.",
            "I hit my head and my arm hurts a lot.",
            "I hit my head and my leg hurts a lot.",
            "My arm and leg are hurting a lot.",
            "My arm hurts a lot.",
            "My leg is hurting a lot.",
            "I feel dizzy and nauseous.",
            "*Unintelligible*",
            "I'm fine, just a little shaken.",
            "I'm slightly bruised but I should be okay.",
            "My whole body hurts"
        };
        AddPedQuestion(driver1, medq);
        AddPedQuestion(driver2, medq);
        PedQuestion medq2 = new PedQuestion();
        medq2.Question = "Do you want me to call an ambulance?";
        medq2.Answers = new List<string>
        {
            "I'd like for them to just check me out.",
            "Yes please.",
            "I should be okay.",
            "They're not already on the way?",
            "Yes, please, this hurts!",
            "Yes, my whole body hurts."
        };
        AddPedQuestion(driver1, medq2);
        AddPedQuestion(driver2, medq2);
        PedQuestion medq3 = new PedQuestion();
        medq3.Question = "I have medical on the way.";
        medq3.Answers = new List<string>
        {
            "*Sobs in pain*",
            "*Groans in pain*",
            "*Cries in pain*",
            "*Acknowledges your statement in pain*",
            "*Lays in pain in Spanish*",
            "My back hurts so much."
        };
        AddPedQuestion(driver1, medq3);
        AddPedQuestion(driver2, medq3);
        PedQuestion medplaceholder = new PedQuestion();
        medplaceholder.Question = "-= Medical Section =-";
        medplaceholder.Answers = new List<string>
        {
            "This is a placeholder dummy!"
        };
        AddPedQuestion(driver1, medplaceholder);
        AddPedQuestion(driver2, medplaceholder);
        PedQuestion medhead = new PedQuestion();
        medhead.Question = "Where at on your head hurts?";
        medhead.Answers = new List<string>
        {
            "Around my nose.",
            "Around my forehead.",
            "All over.",
            "Behind my eyes and around my eyes.",
            "Just around the front."
        };
        AddPedQuestion(driver1, medhead);
        AddPedQuestion(driver2, medhead);
        PedQuestion medchest = new PedQuestion();
        medchest.Question = "Where at on your chest hurts?";
        medchest.Answers = new List<string>
        {
            "Where the seatbelt was.",
            "Across my chest.",
            "On the left side.",
            "On the right side."
        };
        AddPedQuestion(driver1, medchest);
        AddPedQuestion(driver2, medchest);
        PedQuestion medarm = new PedQuestion();
        medarm.Question = "Where at on your arm hurts?";
        medarm.Answers = new List<string>
        {
            "All over.",
            "Around the elbow mostly.",
            "From my wrist to my elbow",
            "Mostly around my shoulder but it's shooting pain though the whole thing.",
            "It's around my wrist and above it."
        };
        AddPedQuestion(driver1, medarm);
        AddPedQuestion(driver2, medarm);
        PedQuestion medleg = new PedQuestion();
        medleg.Question = "Where at on your leg hurts?";
        medleg.Answers = new List<string>
        {
            "All over.",
            "Around the knee mostly.",
            "From my ankle to my knee.",
            "Mostly around my hip but it's shooting pain through the whole thing.",
            "Mostly around my tailbone but it's shooting pain through the whole thing.",
            "It's all around my ankle and above it."
        };
        AddPedQuestion(driver1, medleg);
        AddPedQuestion(driver2, medleg);
        PedQuestion medBP = new PedQuestion();
        medBP.Question = "- Blood Pressure -";
        medBP.Answers = new List<string>
        {
            "Blood Pressure 83/50",
            "Blood Pressure 132/101",
            "Blood Pressure 119/72"
        };
        AddPedQuestion(driver1, medBP);
        AddPedQuestion(driver2, medBP);
        PedQuestion medpch = new PedQuestion();
        medpch.Question = "- Physical Check; Head -";
        medpch.Answers = new List<string>
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
        AddPedQuestion(driver1, medpch);
        AddPedQuestion(driver2, medpch);
        PedQuestion medpca = new PedQuestion();
        medpca.Question = "- Physical Check; Arm -";
        medpca.Answers = new List<string>
        {
            "Bruising on arm, no visual broken bone(s).",
            "Bruising on arm, visually broken bone(s).",
            "No bruising on arm, no visual broken bone(s).",
            "No bruising on arm, visual broken bone(s).",
            "Bone visible through laceration in arm."
        };
        AddPedQuestion(driver1, medpca);
        AddPedQuestion(driver2, medpca);
        PedQuestion medpcl = new PedQuestion();
        medpcl.Question = "- Physical Check; Leg -";
        medpcl.Answers = new List<string>
        {
            "Bruising on leg, no visual broken bone(s).",
            "Bruising on leg, visually broken bone(s).",
            "No bruising on leg, no visual broken bone(s).",
            "No bruising on leg, visually broken bone(s).",
            "Bone visible through laceration in leg."
        };
        AddPedQuestion(driver1, medpcl);
        AddPedQuestion(driver2, medpcl);
        PedQuestion medbs = new PedQuestion();
        medbs.Question = "- Blood Sugar -";
        medbs.Answers = new List<string>
        {
            "Blood sugar; low",
            "Blood sugar; normal",
            "Blood sugar; high"
        };
        AddPedQuestion(driver1, medbs);
        AddPedQuestion(driver2, medbs);
        PedQuestion meda = new PedQuestion();
        meda.Question = "- Other Findings -";
        meda.Answers = new List<string>
        {
            "Nothing",
            "Nothing",
            "Nothing",
            "Nothing",
            "Nothing",
            "Nothing",
            "Nothing",
            "Nothing",
            "Medical bracelet.",
            "Needle marks on forearm."
        };
        AddPedQuestion(driver1, meda);
        AddPedQuestion(driver2, meda);


        // Driver1 Data
        PedData data = new PedData();
        VehicleData vehicleData = new VehicleData();
        Random random = new Random();
        int x = random.Next(1, 100 + 1);
        if (x <= 5)
        {
            data.BloodAlcoholLevel = 0.09;
            Utilities.SetPedData(driver1.NetworkId, data);

            var items = new List<Item>();
            Item sixpack = new Item
            {
                Name = "Six pack of beer",
                IsIllegal = false
            };

            items.Add(sixpack);
            vehicleData.Items = items;
            Utilities.SetVehicleData(car1.NetworkId, vehicleData);
        }
        if (x > 26 && x <= 28)
        {
            data.UsedDrugs[0] = PedData.Drugs.Meth;
            Item item = new Item();
            item.Name = "Meth pipe";
            item.IsIllegal = true;

            data.Items = new List<Item>();
            data.Items.Add(item);

            Utilities.SetPedData(driver1.NetworkId, data);

            var items = new List<Item>();
            Item methbag = new Item
            {
                Name = "Bag of Meth",
                IsIllegal = true
            };

            items.Add(methbag);
            vehicleData.Items = items;
            Utilities.SetVehicleData(car1.NetworkId, vehicleData);
        }
        if (x > 68 && x <= 69)
        {
            data.Warrant = "Bench Warrant";

            Utilities.SetPedData(driver1.NetworkId, data);
        }

        // Driver2 Data
        PedData data2 = new PedData();
        VehicleData vehicleData2 = new VehicleData();
        Random random2 = new Random();
        int y = random2.Next(1, 100 + 1);
        if (y <= 5)
        {
            data2.BloodAlcoholLevel = 0.09;
            Utilities.SetPedData(driver2.NetworkId, data2);

            var items2 = new List<Item>();
            Item sixpack = new Item
            {
                Name = "Six pack of beer",
                IsIllegal = false
            };

            items2.Add(sixpack);
            vehicleData2.Items = items2;
            Utilities.SetVehicleData(car2.NetworkId, vehicleData2);
        }
        if (y > 26 && y <= 28)
        {
            data2.UsedDrugs[0] = PedData.Drugs.Meth;
            Item item2 = new Item();
            item2.Name = "Meth pipe";
            item2.IsIllegal = true;

            data2.Items = new List<Item>();
            data2.Items.Add(item2);

            Utilities.SetPedData(driver2.NetworkId, data2);

            var items2 = new List<Item>();
            Item methbag = new Item
            {
                Name = "Bag of Meth",
                IsIllegal = true
            };

            items2.Add(methbag);
            vehicleData2.Items = items2;
            Utilities.SetVehicleData(car2.NetworkId, vehicleData2);
        }
        if (y > 68 && y <= 69)
        {
            data2.Warrant = "Bench Warrant";

            Utilities.SetPedData(driver2.NetworkId, data2);
        }

    }
}