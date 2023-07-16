using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;

namespace RangersoftheWildernessCallouts
{

    [CalloutProperties("Northern Command Medical Animal MVA", "Valandria", "0.0.4")]
    public class NCAnimalMVA : Callout
    {
        private Ped ncamvadriver, ncamvapassenger, ncamvaanimal;
        private Vehicle ncamvavehicle;
        private string[] vehicleList = { "adder", "carbonrs", "oracle", "oracle2", "phoenix", "vigero", "zentorno", "youga", "youga2", "sultan", "sultanrs", "ruiner", "ruiner2", "ruiner3", "burrito", "burrito2", "burrito3", "gburrito", "bagger", "buffalo", "buffalo2", "comet2", "comet3", "felon" };
        private Vector3[] coordinates =
        {
        new Vector3(1663.35f, 2866.89f, 41.72f),
        new Vector3(1558.74f, 2796.8f, 38.22f),
        new Vector3(1463.9f, 2737.08f, 37.61f),
        new Vector3(1335.68f, 2691.68f, 37.6f),
        new Vector3(1816.68f, 2943.92f, 45.74f),
        new Vector3(1686.75f, 3138.51f, 43.31f),
        new Vector3(1337.34f, 2966.53f, 40.91f),
        new Vector3(1104.85f, 2806.89f, 37.77f),
        new Vector3(809.03f, 2843.7f, 58.01f),
        new Vector3(138.55f, 2872.97f, 49.6f),
        new Vector3(28.17f, 2846.81f, 55.63f),
        new Vector3(-146.99f, 2749.34f, 56.51f),
        new Vector3(-171.62f, 2826.9f, 36.15f),
        new Vector3(-13.87f, 3007.09f, 40.53f),
        new Vector3(-319.29f, 2938.51f, 29.13f),
        new Vector3(-713.84f, 2965.98f, 25.4f),
        new Vector3(-731.84f, 2965.98f, 25.4f),
        new Vector3(-770.26f, 2911.88f, 25.15f),
        new Vector3(-824.12f, 2867.29f, 24.49f),
        new Vector3(-859.73f, 2866.03f, 24.05f),
        new Vector3(-896.73f, 2864f, 23.5f),
        new Vector3(-918.88f, 2858.64f, 22.83f),
        new Vector3(-886.68f, 2844.02f, 22.12f),
        new Vector3(-997.22f, 2889.45f, 12.72f),
        new Vector3(-1003.74f, 2901.76f, 12.03f),
        new Vector3(-1073.11f, 2873.42f, 12.26f),
        new Vector3(-1102.97f, 2872.86f, 13.61f),
        new Vector3(-1173.3f, 2817.46f, 14.92f),
        new Vector3(-1247.36f, 2773.6f, 14.25f),
        new Vector3(-1285.2f, 2724.72f, 9.6f),
        new Vector3(-1367.73f, 2710.62f, 5.51f),
        new Vector3(-1393.33f, 2688.64f, 4.73f),
        new Vector3(-1429.88f, 2693.57f, 5.01f),
        new Vector3(-1444.13f, 2695.69f, 4.37f),
        new Vector3(-1477.39f, 2677.51f, 3.72f),
        new Vector3(-1506.51f, 2689.57f, 3.77f),
        new Vector3(-1515.91f, 2678.92f, 3.84f),
        new Vector3(-1568.12f, 2725.69f, 5.39f),
        new Vector3(-1592.28f, 2726.3f, 5.86f),
        new Vector3(-1618.4f, 2720.8f, 5.8f),
        new Vector3(-1639.58f, 2700.1f, 5.79f),
        new Vector3(-1671.92f, 2745.58f, 5.61f),
        new Vector3(-1711.94f, 2740.71f, 4.86f),
        new Vector3(-1744.44f, 2751.88f, 5.81f),
        new Vector3(-1754f, 2730.22f, 5.73f),
        new Vector3(-1784.24f, 2704.48f, 4.88f),
        new Vector3(-1800.46f, 2676.77f, 3.68f),
        new Vector3(-1828.1f, 2698.56f, 4.16f),
        new Vector3(-1858.54f, 2679.27f, 3.86f),
        new Vector3(-1891.1f, 2696.35f, 4.38f),
        new Vector3(-1926.98f, 2698.78f, 4.22f),
        new Vector3(-1939.33f, 2719.45f, 4.12f),
        new Vector3(-1995.48f, 2703.11f, 3.49f),
        new Vector3(-2028.6f, 2714.6f, 3.48f),
        new Vector3(-2087.32f, 2711.43f, 4.12f),
        new Vector3(-2131.47f, 2709.66f, 3.78f),
        new Vector3(-2159.66f, 2723.31f, 4.6f),
        new Vector3(-2159.38f, 2737.82f, 4.67f),
        new Vector3(-2184.68f, 2753.94f, 5.36f),
        new Vector3(-2204.99f, 2788.26f, 4.68f),
        new Vector3(-2235.3f, 2825.51f, 3.6f),
        new Vector3(-2270.27f, 2829.61f, 3.55f),
        new Vector3(-2285.67f, 2854.27f, 2.91f),
        new Vector3(-2326.18f, 2854.07f, 3.7f),
        new Vector3(-2352.69f, 2833.6f, 4.19f),
        new Vector3(-2398.11f, 2824.55f, 3.25f),
        new Vector3(-2406.66f, 2837.7f, 3.45f),
        new Vector3(-2444.09f, 2832.45f, 3.57f),
        new Vector3(-2515.47f, 2849.88f, 3.76f),
        new Vector3(-2552.54f, 2855.58f, 3.32f),
        new Vector3(-2606.73f, 2868.09f, 2.85f),
        new Vector3(-2616.97f, 2904.59f, 5.7f),
        new Vector3(-2654.22f, 2940.48f, 8.49f),
        new Vector3(-2650f, 2990.18f, 9.23f),
        new Vector3(-2693.36f, 3019.95f, 8.9f),
        new Vector3(-2727.87f, 3072f, 9.06f),
        new Vector3(-2774.21f, 3085.31f, 8.96f),
        new Vector3(-2823.33f, 3119.91f, 9.68f),
        new Vector3(-2878.69f, 3184.17f, 11.06f),
        new Vector3(-2916.28f, 3231.19f, 10.57f),
        new Vector3(-2988.15f, 3278.58f, 10.17f),
        new Vector3(-3007.48f, 3354.24f, 10.65f),
        new Vector3(-3030.01f, 3383.67f, 9.94f),
        new Vector3(-2998.32f, 3420.46f, 9.99f),
        new Vector3(-2972.31f, 3473.52f, 9.5f),
        new Vector3(-2947.61f, 3518.26f, 8.33f),
        new Vector3(-2884.91f, 3518.52f, 8.08f),
        new Vector3(-2838.16f, 3525.75f, 8.48f),
        new Vector3(-2769.05f, 3494.6f, 10.31f),
        new Vector3(-2737.2f, 3483.34f, 11.64f),
        new Vector3(-2702.22f, 3481.82f, 12.93f),
        new Vector3(-2651.67f, 3468.62f, 14.49f),
        new Vector3(-2585.77f, 3485.37f, 13.76f),
        new Vector3(-2575.22f, 3501.58f, 12.6f),
        new Vector3(-2558.86f, 3560.55f, 11.42f),
        new Vector3(-2539.43f, 3595.74f, 11.92f),
        new Vector3(-2517.54f, 3660.68f, 13.1f),
        new Vector3(-2490f, 3673.92f, 13.91f),
        };
        public NCAnimalMVA()
        {
            InitInfo(coordinates[RandomUtils.Random.Next(coordinates.Length)]);
            ShortName = "NC Med - Animal MVA";
            CalloutDescription = "A vehicle collision involving wildlife has occurred. Get the location and assess.";
            ResponseCode = 2;
            StartDistance = 200f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);

            Random ncmamvavehdecider = new Random();
            string ncmamvavehicle = vehicleList[ncmamvavehdecider.Next(vehicleList.Length)];
            VehicleHash ncmvamvavehicleHash = (VehicleHash)API.GetHashKey(ncmamvavehicle);
            ncamvavehicle = await SpawnVehicle(ncmvamvavehicleHash, Location, 0);
            ncamvavehicle.Deform(Location, 10000, 100);
            ncamvavehicle.EngineHealth = 5;
            ncamvavehicle.BodyHealth = 1;
            ncamvavehicle.IsPersistent = true;

            API.Wait(1000);

            ncamvadriver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 5);

            Random ncmmvaanimal = new Random();
            int ncmsuspedanimal = ncmmvaanimal.Next(1, 100 + 1);
            if (ncmsuspedanimal <= 10)
            {
                ncamvaanimal = await SpawnPed(PedHash.MountainLion, Location);
            }
            else if (ncmsuspedanimal > 10 && ncmsuspedanimal <= 20)
            {
                ncamvaanimal = await SpawnPed(PedHash.Boar, Location);
            }
            else if (ncmsuspedanimal > 20 && ncmsuspedanimal <= 30)
            {
                ncamvaanimal = await SpawnPed(PedHash.Coyote, Location);
            }
            else if (ncmsuspedanimal > 30 && ncmsuspedanimal <= 95)
            {
                ncamvaanimal = await SpawnPed(PedHash.Deer, Location);
            }
            else if (ncmsuspedanimal > 95)
            {
                ncamvaanimal = await SpawnPed(PedHash.Cow, Location);
            }

            ncamvadriver.AlwaysKeepTask = true;
            ncamvadriver.BlockPermanentEvents = true;
            ncamvadriver.IsPersistent = true;

            //ncamvapassenger.AlwaysKeepTask = true;
            //ncamvapassenger.BlockPermanentEvents = true;
            //ncamvapassenger.IsPersistent = true;

            ncamvaanimal.AlwaysKeepTask = true;
            ncamvaanimal.BlockPermanentEvents = true;
            ncamvaanimal.IsPersistent = true;

            ncamvaanimal.Kill();

            Random ncmmvadriverposition = new Random();
            int ncmdriverseat = ncmmvadriverposition.Next(1, 100 + 1);
            if (ncmdriverseat <= 20)
            {
                ncamvadriver.SetIntoVehicle(ncamvavehicle, VehicleSeat.Driver);
            }
            if (ncmdriverseat > 60 && ncmdriverseat <= 75)
            {
                ncamvadriver.SetIntoVehicle(ncamvavehicle, VehicleSeat.Driver);
                ncamvadriver.Kill();
            }
            if (ncmdriverseat > 90)
            {
                ncamvadriver.Kill();
                Tick += ncmamvanoseatbelt;
            }

            ncamvavehicle.Deform(Location, 10000, 100);
            ncamvadriver.AttachBlip();
            ncamvapassenger.AttachBlip();
            ncamvavehicle.AttachBlip();

            // ncamvadriver Data
            PedData ncamvadriverdata = new PedData();
            VehicleData ncamvavehicleData = new VehicleData();

            //PedQuestion ncamvadq1 = new PedQuestion();
            //ncamvadq1.Question = "";
            //ncamvadq1.Answers = new List<string>
            //{
            //    "",
            //    "",
            //};

            //PedQuestion ncamvadq2 = new PedQuestion();
            //ncamvadq2.Question = "";
            //ncamvadq2.Answers = new List<string>
            //{
            //    "",
            //    "",
            //};

            //PedQuestion ncamvadq3 = new PedQuestion();
            //ncamvadq3.Question = "";
            //ncamvadq3.Answers = new List<string>
            //{
            //    "",
            //    "",
            //};

            //PedQuestion ncamvadq4 = new PedQuestion();
            //ncamvadq4.Question = "";
            //ncamvadq4.Answers = new List<string>
            //{
            //    "",
            //    "",
            //};

            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if (x <= 5)
            {
                ncamvadriverdata.BloodAlcoholLevel = 0.09;

                var items = new List<Item>();
                Item sixpack = new Item
                {
                    Name = "Open six pack of beer",
                    IsIllegal = true
                };

                items.Add(sixpack);
                ncamvavehicleData.Items = items;
                //Tick += dwincamva;
            }
            if (x > 26 && x <= 28)
            {
                ncamvadriverdata.UsedDrugs[0] = PedData.Drugs.Meth;
                Item item = new Item();
                item.Name = "Meth pipe";
                item.IsIllegal = true;

                ncamvadriverdata.Items = new List<Item>();
                ncamvadriverdata.Items.Add(item);

                var items = new List<Item>();
                Item methbag = new Item
                {
                    Name = "Bag of Meth",
                    IsIllegal = true
                };

                items.Add(methbag);
                ncamvavehicleData.Items = items;
                //Tick += methncamva;
            }
            if (x > 68 && x <= 69)
            {
                ncamvadriverdata.Warrant = "Bench Warrant";
                //Tick += warrantncamva;
            }
        }
        //public async Task dwincamva()
        //{
        //}
        //public async Task methncamva()
        //{
        //}
        //public async Task warrantncamva()
        //{
        //}
        public async Task ncmamvanoseatbelt()
        {
            float noseatbeltnotification = Game.PlayerPed.Position.DistanceTo(ncamvadriver.Position);
            if (noseatbeltnotification < 15f)
            {
                ShowDialog("The driver was ejected from their vehicle.", 10000, 15f);
            }

            float noseatbeltvehiclenotification = Game.PlayerPed.Position.DistanceTo(ncamvavehicle.Position);
            if (noseatbeltvehiclenotification < 10f)
            {
                ShowDialog("There seems to be a seatbelt avoidance device installed on the driver seatbelt.", 10000, 10f);
            }
        }
        public override async Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }
    }
}