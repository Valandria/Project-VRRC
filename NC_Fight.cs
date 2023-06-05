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
    [CalloutProperties("Northern Command Fight in Progress", "Valandria", "0.0.2")]
    public class NC_Fight : Callout
    {
        Ped suspect, suspect2, suspect3, suspect4, suspect5, suspect6, suspect7, suspect8, suspect9, suspect10;
        
        public NC_Fight()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if(x <= 40)
            { 
                InitInfo(new Vector3(1994.89f, 3059.96f, 47.05f)); // Yellow Jack
            }
            else if(x > 40 && x <= 65)
            {
                InitInfo(new Vector3(56.64f, 3715.29f, 39.74f)); // Stab City
            }
            else
            {
                InitInfo(new Vector3(-73.29f, 6555.29f, 31.49f)); // 3019 Procopio Dr
            }
            ShortName = "NC - Fight in Progress";
            CalloutDescription = "Caller reports a large group of individuals fighting amongst themselves.";
            ResponseCode = 3;
            StartDistance = 200f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            suspect2 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 1);
            suspect3 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            suspect4 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 2);
            suspect5 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            suspect6 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 1);
            suspect7 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 3);
            suspect8 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 3);
            suspect9 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            suspect10 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 1);
            
            //Suspect 1
            PedData data = new PedData();
            data.BloodAlcoholLevel = 0.08;
            Utilities.SetPedData(suspect.NetworkId,data);
            
            //Suspect 2
            PedData data2 = new PedData();
            data2.BloodAlcoholLevel = 0.05;
            Utilities.SetPedData(suspect2.NetworkId,data2);
            
            //Suspect 3
            PedData data3 = new PedData();
            data3.BloodAlcoholLevel = 0.02;
            Utilities.SetPedData(suspect3.NetworkId,data3);
            
            //Suspect 4
            PedData data4 = new PedData();
            data4.BloodAlcoholLevel = 0.00;
            List<Item> items = new List<Item>();
            Item Cash = new Item {
                Name = "$500 Cash",
                IsIllegal = false
            };
            items.Add(Cash);
            data4.Items = items;
            Utilities.SetPedData(suspect4.NetworkId,data4);
            
            //Suspect 5
            PedData data5 = new PedData();
            data5.BloodAlcoholLevel = 0.00;
            Utilities.SetPedData(suspect5.NetworkId,data5);
            
            //Suspect 6
            PedData data6 = new PedData();
            data6.BloodAlcoholLevel = 0.20;
            Utilities.SetPedData(suspect6.NetworkId,data6);
            
            //Suspect 7
            PedData data7 = new PedData();
            data7.BloodAlcoholLevel = 0.01;
            Utilities.SetPedData(suspect7.NetworkId,data7);
            
            //Suspect 8
            PedData data8 = new PedData();
            data8.BloodAlcoholLevel = 0.08;
            Utilities.SetPedData(suspect8.NetworkId,data8);
            
            //Suspect 9
            PedData data9 = new PedData();
            data9.BloodAlcoholLevel = 0.00;
            Utilities.SetPedData(suspect9.NetworkId,data9);
            
            //Suspect 10
            PedData data10 = new PedData();
            data10.BloodAlcoholLevel = 0.00;
            items.Add(Cash);
            data10.Items = items;
            Utilities.SetPedData(suspect10.NetworkId,data10);

            //TASKS
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            suspect2.AlwaysKeepTask = true;
            suspect2.BlockPermanentEvents = true;
            suspect3.AlwaysKeepTask = true;
            suspect3.BlockPermanentEvents = true;
            suspect4.AlwaysKeepTask = true;
            suspect4.BlockPermanentEvents = true;
            suspect5.AlwaysKeepTask = true;
            suspect5.BlockPermanentEvents = true;
            suspect6.AlwaysKeepTask = true;
            suspect6.BlockPermanentEvents = true;
            suspect7.AlwaysKeepTask = true;
            suspect7.BlockPermanentEvents = true;
            suspect8.AlwaysKeepTask = true;
            suspect8.BlockPermanentEvents = true;
            suspect9.AlwaysKeepTask = true;
            suspect9.BlockPermanentEvents = true;
            suspect10.AlwaysKeepTask = true;
            suspect10.BlockPermanentEvents = true;
            suspect.AttachBlip();
            suspect2.AttachBlip();
            suspect3.AttachBlip();
            suspect4.AttachBlip();
            suspect5.AttachBlip();
            suspect6.AttachBlip();
            suspect7.AttachBlip();
            suspect8.AttachBlip();
            suspect9.AttachBlip();
            suspect10.AttachBlip();
            suspect.Task.FightAgainst(suspect2);
            suspect2.Task.FightAgainst(suspect3);
            suspect3.Task.FightAgainst(suspect4);
            suspect4.Task.FightAgainst(suspect5);
            suspect5.Task.FightAgainst(suspect6);
            suspect6.Task.FightAgainst(suspect7);
            suspect7.Task.FightAgainst(suspect8);
            suspect8.Task.FightAgainst(suspect9);
            suspect9.Task.FightAgainst(suspect10);
            suspect10.Task.FightAgainst(suspect);
            string firstname = data.FirstName;
            string firstname2 = data2.FirstName;
            string firstname3 = data3.FirstName;
            string firstname4 = data4.FirstName;
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname + "] ~s~Why does my day at the beach always turn into this?", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname2 + "] ~s~You are not my friend anymore!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname3 + "] ~s~YOUR MEAN!", 5000);
            API.Wait(6000);
            DrawSubtitle("~r~[" + firstname4 + "] ~s~DIE!", 5000);
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