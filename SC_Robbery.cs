using System;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;

namespace BeachCallouts
{
    
    [CalloutProperties("Southern Command Robbery", "Valandria", "0.0.1")]
    public class SC_Robbery : Callout
    {
        private Ped vic, suspect;

        public SC_Robbery()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if(x <= 40)
            { 
                InitInfo(new Vector3(-1291.22f, -1613.01f, 4.10214f));
            }
            else if(x > 40 && x <= 65)
            {
                InitInfo(new Vector3(-1553.91f, -913.503f, 9.15462f));
            }
            else
            {
                InitInfo(new Vector3(-2020.81f, -469.617f, 11.4716f));
            }
            ShortName = "SC - Robbery";
            CalloutDescription = "Callers report a robbery in progress.";
            ResponseCode = 3;
            StartDistance = 200f;
        }
        
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            vic = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            
            
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.02;
            Utilities.SetPedData(vic.NetworkId,data);
            
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            suspect.Accuracy = 60;
            vic.AlwaysKeepTask = true;
            vic.BlockPermanentEvents = true;
            
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspect is armed and dangerous!");
            vic.AttachBlip();
            suspect.AttachBlip();
            
            PedData data1 = await Utilities.GetPedData(vic.NetworkId);
            string firstname = data1.FirstName;
            PedData data2 = await Utilities.GetPedData(suspect.NetworkId);
            string firstname2 = data2.FirstName;
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if (x <= 40)
            {
                vic.Task.ReactAndFlee(suspect);
                DrawSubtitle("~r~[" + firstname + "] ~s~Please help me!", 5000);
                suspect.Task.FightAgainst(vic);
                suspect.Weapons.Give(WeaponHash.Hammer, 1000, true, true);
                DrawSubtitle("~r~[" + firstname2 + "] ~s~Let me get your money!", 5000);
            }
            else if (x > 40 && x <= 65)
            {
                vic.Task.ReactAndFlee(suspect);
                DrawSubtitle("~r~[" + firstname + "] ~s~Leave me alone!", 5000);
                suspect.Weapons.Give(WeaponHash.Pistol, 1000, true, true);
                API.Wait(5000);
                suspect.Task.FightAgainst(player);
                DrawSubtitle("~r~[" + firstname2 + "] ~s~Cops... I will not go back to jail!", 5000);
            }
            else
            {
                vic.Task.ReactAndFlee(suspect);
                DrawSubtitle("~r~[" + firstname + "] ~s~Please don't kill me!", 5000);
                suspect.Weapons.Give(WeaponHash.Knife, 1000, true, true);
                suspect.Task.FightAgainst(vic);
                DrawSubtitle("~r~[" + firstname2 + "] ~s~Give me your wallet!", 5000);
                API.Wait(20000);
                suspect.Task.FightAgainst(player);
                DrawSubtitle("~r~[" + firstname2 + "] ~s~Cops... I will not go back to jail!", 5000);
            }
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