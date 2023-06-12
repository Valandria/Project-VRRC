using System;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace RangersoftheWildernessCallouts
{
    [CalloutProperties("NC Hiker Attack", "Valandria", "0.1.0")]
    public class NCHikerAttack : Callout
    {
        private Ped vic, suspect;
        private Vector3[] coordinates =
        {
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
            new Vector3(169.86f, 4410.23f, 75.4f),
            new Vector3(374.93f, 4395.47f, 63.32f),
            new Vector3(771.51f, 4221.78f, 50.64f),
            new Vector3(976.24f, 4454.6f, 51.82f),
            new Vector3(1443.5f, 4512.78f, 54.7f),
            new Vector3(-506.53f, 4350.54f, 67.34f),
            new Vector3(-880.92f, 4396.09f, 20.52f),
            new Vector3(-1018.03f, 4402.19f, 14.63f),
            new Vector3(-1362.16f, 4461f, 23.97f),
            new Vector3(-1557.78f, 4695.01f, 49.77f),
            new Vector3(-1619.74f, 4723.76f, 51.66f),
            new Vector3(-1844.36f, 4396.56f, 50.74f),
            new Vector3(-431.74f, 1581.86f, 357.22f),
            new Vector3(-341.61f, 1367.04f, 341.16f),
            new Vector3(-1442.66f, 2704.05f, 5.23f),
            new Vector3(-187f, 3783.66f, 40.76f),
            new Vector3(-241.76f, 3822.44f, 40.78f),
            new Vector3(-305.25f, 4206.35f, 47.3f),
            new Vector3(-390f, 4324.15f, 54.8f),
            new Vector3(-506.81f, 4352.7f, 67.6f),
            new Vector3(-971.75f, 4340.9f, 13.59f),
            new Vector3(-979.24f, 4385.83f, 12.64f),
            new Vector3(-1081.15f, 4349.67f, 14.63f),
            new Vector3(-1167.57f, 4353.03f, 11.49f),
            new Vector3(-1317.05f, 4453.69f, 21.23f),
            new Vector3(-1416.69f, 4466.03f, 22.39f),
            new Vector3(-1571.4f, 4496.59f, 21.54f),
            new Vector3(-1478.02f, 4631.2f, 49.16f),
            new Vector3(-1592f, 4741.64f, 52.75f),
            new Vector3(-1692.23f, 4671.87f, 23.86f),
            new Vector3(-935.52f, 4612.42f, 237.75f),
            new Vector3(-492.97f, 2667.62f, 56.15f),
            new Vector3(-856.34f, 2874.12f, 25.02f),
            new Vector3(50f, 3330.84f, 37.15f),
            new Vector3(-1225.78f, 4300.43f, 73.93f),
            new Vector3(-1214.29f, 4285.95f, 76.87f),
            new Vector3(-1016.44f, 4169.76f, 123.82f),
            new Vector3(-613.51f, 4000.42f, 121.47f),
            new Vector3(-446.77f, 4009.83f, 80.04f),
            new Vector3(-346.83f, 3990.1f, 52.67f),
            new Vector3(-210.89f, 3173.5f, 43.64f),
            new Vector3(-509.52f, 3045.55f, 36.97f),
            new Vector3(-726.21f, 2942.87f, 25.28f),
            new Vector3(-1129.79f, 2878.14f, 18.27f),
            new Vector3(-1493.95f, 2918.93f, 32.67f),
            new Vector3(-2168.01f, 4241.06f, 50.03f),
            new Vector3(-2326.06f, 4290.23f, 29.38f),
        };

        public NCHikerAttack()
        {
            InitInfo(coordinates[RandomUtils.Random.Next(coordinates.Length)]);
            ShortName = "NC - Hiker Attacked";
            CalloutDescription = "A hiker is being attacked by an unknown suspect.";
            ResponseCode = 3;
            StartDistance = 200f;
        }
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            Random random = new Random();
            int susped = random.Next(1, 100 + 1);
            if (susped <= 10)
            {
                suspect = await SpawnPed(PedHash.MountainLion, Location);
            }
            else if (susped > 10 && susped <= 20)
            {
                suspect = await SpawnPed(PedHash.Boar, Location);
            }
            else if (susped > 20 && susped <= 30)
            {
                suspect = await SpawnPed(PedHash.Coyote, Location);
            }
            else if (susped > 30 && susped <= 40)
            {
                suspect = await SpawnPed(PedHash.Deer, Location);
            }
            else
            {
                suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location);
                suspect.Weapons.Give(WeaponHash.Pistol, 1000, true, true);
            }
            vic = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            PedData data = new PedData();
            Utilities.SetPedData(vic.NetworkId, data);
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            vic.AlwaysKeepTask = true;
            vic.BlockPermanentEvents = true;
            vic.AttachBlip();
            suspect.AttachBlip();
            suspect.RelationshipGroup = 0xCE133D78;
            if (vic.IsDead == false)
            {
                suspect.Task.FightAgainst(vic);
            }
            if (vic.IsDead == true)
            {
                suspect.Task.FightAgainstHatedTargets(this.StartDistance);
            }
            PedData firstnamedata1 = await Utilities.GetPedData(vic.NetworkId);
            string firstname = firstnamedata1.FirstName;
            PedData firstnamedata2 = await Utilities.GetPedData(suspect.NetworkId);
            string firstname2 = firstnamedata2.FirstName;
            Random speechpatternrandom2 = new Random();
            int x = speechpatternrandom2.Next(1, 100 + 1);
            if (x <= 40)
            {
                vic.Task.ReactAndFlee(suspect);
                DrawSubtitle("~r~[" + firstname + "] ~s~Please help me!", 5000);
            }
            else if (x > 40 && x <= 65)
            {
                vic.Task.ReactAndFlee(suspect);
                DrawSubtitle("~r~[" + firstname + "] ~s~Leave me alone!", 5000);
            }
            else
            {
                vic.Task.ReactAndFlee(suspect);
                DrawSubtitle("~r~[" + firstname + "] ~s~Please don't kill me!", 5000);
            }
        }
        public async override Task OnAccept()
        {
            InitBlip();
            UpdateData();
        }
        private void DrawSubtitle(string message, int duration)
        {
            API.BeginTextCommandPrint("STRING");
            API.AddTextComponentSubstringPlayerName(message);
            API.EndTextCommandPrint(duration, false);
        }
    }
}