using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;



[CalloutProperties("NC Med Animal MVA", "Valandria", "0.0.1")]
public class NCAnimalMVA : FivePD.API.Callout
{
    private Ped ncamvadriver, ncamvapassenger, ncamvaanimal;
    private Vehicle ncamvavehicle;
    public NCAnimalMVA()
    {
        //To be changed to static locations.
        InitInfo(World.GetNextPositionOnStreet(Vector3Extension.Around(Game.PlayerPed.Position, 400)));

        ShortName = "NC Med - Animal MVA";
        CalloutDescription = "A vehicle collision involving wildlife has occurred. Get the location and assess.";
        ResponseCode = 2;
        StartDistance = 200f;
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

        ncamvavehicle = await SpawnVehicle(cars[RandomUtils.Random.Next(cars.Length)], Location, 0);
        ncamvavehicle.Deform(Location, 10000, 100);
        ncamvavehicle.EngineHealth = 5;
        ncamvavehicle.BodyHealth = 1;

        API.Wait(1000);

        ncamvadriver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 5);

        ncamvadriver.AlwaysKeepTask = true;
        ncamvadriver.BlockPermanentEvents = true;

        ncamvapassenger.AlwaysKeepTask = true;
        ncamvapassenger.BlockPermanentEvents = true;

        Random mvaanimal = new Random();
        int susped = mvaanimal.Next(1, 40 + 1);
        if (susped <= 10)
        {
            ncamvaanimal = await SpawnPed(PedHash.MountainLion, Location);
        }
        else if (susped > 10 && susped <= 20)
        {
            ncamvaanimal = await SpawnPed(PedHash.Boar, Location);
        }
        else if (susped > 20 && susped <= 30)
        {
            ncamvaanimal = await SpawnPed(PedHash.Coyote, Location);
        }
        else if (susped > 30)
        {
            ncamvaanimal = await SpawnPed(PedHash.Deer, Location);
        }

        ncamvaanimal.Kill();

        //Random chance instead for death/vehicle seat positon?  Possibly same for passenger.
        ncamvadriver.SetIntoVehicle(ncamvavehicle, VehicleSeat.Driver);
        //ncamvadriver.Kill();
    }

    public override void OnStart(Ped player)
    {
        base.OnStart(player);

        ncamvavehicle.Deform(Location, 10000, 100);
        ncamvadriver.AttachBlip();
        ncamvapassenger.AttachBlip();
        ncamvavehicle.AttachBlip();

        // ncamvadriver Data
        PedData data = new PedData();
        VehicleData vehicleData = new VehicleData();
        Random random = new Random();
        int x = random.Next(1, 100 + 1);
        if (x <= 5)
        {
            data.BloodAlcoholLevel = 0.09;
            Utilities.SetPedData(ncamvadriver.NetworkId, data);

            var items = new List<Item>();
            Item sixpack = new Item
            {
                Name = "Six pack of beer",
                IsIllegal = false
            };

            items.Add(sixpack);
            vehicleData.Items = items;
            Utilities.SetVehicleData(ncamvavehicle.NetworkId, vehicleData);
        }
        if (x > 26 && x <= 28)
        {
            data.UsedDrugs[0] = PedData.Drugs.Meth;
            Item item = new Item();
            item.Name = "Meth pipe";
            item.IsIllegal = true;

            data.Items = new List<Item>();
            data.Items.Add(item);

            Utilities.SetPedData(ncamvadriver.NetworkId, data);

            var items = new List<Item>();
            Item methbag = new Item
            {
                Name = "Bag of Meth",
                IsIllegal = true
            };

            items.Add(methbag);
            vehicleData.Items = items;
            Utilities.SetVehicleData(ncamvavehicle.NetworkId, vehicleData);
        }
        if (x > 68 && x <= 69)
        {
            data.Warrant = "Bench Warrant";

            Utilities.SetPedData(ncamvadriver.NetworkId, data);
        }
    }
}