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
    
    [CalloutProperties("Northern Command Individuals using Fireworks", "Valandria", "0.0.1")]
    public class NC_Fireworks : Callout
    {

        Ped suspect1, suspect2, suspect3, suspect4, suspect5, suspect6, suspect7, suspect8, suspect9, suspect10;
        private string[] badItemList = { "Beer Bottle", "Open Beer Can", "Wine Bottle", "Random Pills", "Needles", "Firework Launcher", "Fireworks", "Flares"};
        public NC_Fireworks()
        {
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if(x <= 40)
            { 
                InitInfo(new Vector3(-312.59f, 6507.62f, 3.25f)); // Beach 3014
            }
            else if(x > 40 && x <= 65)
            {
                InitInfo(new Vector3(1316.86f, 4339.04f, 38.81f)); // Boat area 2008
            }
            else
            {
                InitInfo(new Vector3(2467.24f, 3772.29f, 41.33f)); // Hippie area 1040
            }
            ShortName = "NC - Individuals with Fireworks";
            CalloutDescription = "Caller reports a group of individuals are launching fireworks at the beach.";
            ResponseCode = 2;
            StartDistance = 400f;
        }
        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            suspect1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
            suspect2 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            suspect3 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 3);
            suspect4 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 1);
            suspect5 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 5);
            suspect6 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 6);
            suspect7 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 7);
            suspect8 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 4);
            suspect9 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 2);
            suspect10 = await SpawnPed(RandomUtils.GetRandomPed(), Location - 5);
            suspect1.Weapons.Give(WeaponHash.Firework, 130, true, true);
            suspect2.Weapons.Give(WeaponHash.Firework, 130, true, true);
            suspect3.Weapons.Give(WeaponHash.Firework, 130, true, true);
            suspect4.Weapons.Give(WeaponHash.Firework, 130, true, true);
            suspect5.Weapons.Give(WeaponHash.Firework, 130, true, true);
            suspect7.Weapons.Give(WeaponHash.Firework, 130, true, true);
            suspect7.Weapons.Give(WeaponHash.Firework, 130, true, true);
            suspect8.Weapons.Give(WeaponHash.Firework, 130, true, true);
            suspect9.Weapons.Give(WeaponHash.Firework, 130, true, true);
            suspect10.Weapons.Give(WeaponHash.Firework, 130, true, true);
            suspect1.AlwaysKeepTask = true;
            suspect1.BlockPermanentEvents = true;
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
            
            
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspects are firing fireworks into the air!");
            
            
            
            PedData data = new PedData();
            Random random2 = new Random();
            string name = badItemList[random2.Next(badItemList.Length)];
            List<Item> items = new List<Item>();
            Item badItem = new Item {
                Name = name,
                IsIllegal = false
            };
            items.Add(badItem);
            data.Items = items;
            Utilities.SetPedData(suspect1.NetworkId,data);
            Utilities.SetPedData(suspect2.NetworkId,data);
            Utilities.SetPedData(suspect3.NetworkId,data);
            Utilities.SetPedData(suspect4.NetworkId,data);
            Utilities.SetPedData(suspect5.NetworkId,data);
            Utilities.SetPedData(suspect6.NetworkId,data);
            Utilities.SetPedData(suspect7.NetworkId,data);
            Utilities.SetPedData(suspect8.NetworkId,data);
            Utilities.SetPedData(suspect9.NetworkId,data);
            Utilities.SetPedData(suspect10.NetworkId,data);
            suspect1.AttachBlip();
            suspect2.AttachBlip();
            suspect3.AttachBlip();
            suspect4.AttachBlip();
            suspect5.AttachBlip();
            suspect6.AttachBlip();
            suspect7.AttachBlip();
            suspect8.AttachBlip();
            suspect9.AttachBlip();
            suspect10.AttachBlip();
            PedData data1 = await Utilities.GetPedData(suspect1.NetworkId);
            string firstname = data1.FirstName;
            API.Wait(30000);
            Random random = new Random();
            int x = random.Next(1, 100 + 1);
            if(x <= 10)
            { 
                DrawSubtitle("~r~[" + firstname + "] ~s~Come join us!", 5000);
                suspect1.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect2.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect3.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect4.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect5.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect6.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect7.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect8.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect9.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect10.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect1.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect2.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect3.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect4.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect5.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect6.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect7.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect8.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect9.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect10.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect1.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect2.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect3.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect4.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect5.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect6.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect7.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect8.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect9.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect10.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect1.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect2.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect3.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect4.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect5.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect6.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect7.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect8.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect9.Task.ShootAt(new Vector3(100f, 1000f, 50f));
                suspect10.Task.ShootAt(new Vector3(100f, 1000f, 50f));
            }
            else if(x > 10 && x <= 65)
            {
                suspect1.Task.ReactAndFlee(player); //run
                suspect2.Task.ReactAndFlee(player); //run
                suspect3.Task.ReactAndFlee(player); //run
                suspect4.Task.ReactAndFlee(player); //run
                suspect5.Task.ReactAndFlee(player); //run
                suspect6.Task.ReactAndFlee(player); //run
                suspect7.Task.ReactAndFlee(player); //run
                suspect8.Task.ReactAndFlee(player); //run
                suspect9.Task.ReactAndFlee(player); //run
                suspect10.Task.ReactAndFlee(player); //run
                DrawSubtitle("~r~[" + firstname + "] ~s~BAIL! RUN!", 5000);
            }
            else
            {
                DrawSubtitle("~r~[" + firstname + "] ~s~You five go that way we will hold them off!", 5000);
                suspect1.Task.ReactAndFlee(player); //run
                suspect2.Task.ReactAndFlee(player); //run
                suspect3.Task.ReactAndFlee(player); //run
                suspect4.Task.ReactAndFlee(player); //run
                suspect5.Task.ReactAndFlee(player); //run
                suspect6.Task.FightAgainst(player); //run
                suspect7.Task.FightAgainst(player); //run
                suspect8.Task.FightAgainst(player); //run
                suspect9.Task.FightAgainst(player); //run
                suspect10.Task.FightAgainst(player); //run
            }
            suspect1.AlwaysKeepTask = true;
            suspect1.BlockPermanentEvents = true;
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