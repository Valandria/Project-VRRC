using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FivePD.API;
using FivePD.API.Utils;


namespace LocalAutoUnion404
{
    
    [CalloutProperties("Local Bus Pursuit", "Valandria", "0.0.1")]
    public class BusPursuit : Callout
    {
        private Vehicle lbpstolenbus;
        Ped lbpdriver;
        public BusPursuit()
        {

            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "L - Bus Pursuit";
            CalloutDescription = "A bus has been stolen!";
            ResponseCode = 3;
            StartDistance = 250f;
        }

        public async override void OnStart(Ped player)
        {
            base.OnStart(player);
            Random random = new Random();
            lbpstolenbus = await SpawnVehicle(VehicleHash.Bus, Location,12);
            lbpdriver = await SpawnPed(RandomUtils.GetRandomPed(), Location + 2);
            lbpdriver.SetIntoVehicle(lbpstolenbus, VehicleSeat.Driver);
            
            lbpdriver.AlwaysKeepTask = true;
            lbpdriver.BlockPermanentEvents = true;
            
            PlayerData playerData = Utilities.GetPlayerData();
            string displayName = playerData.DisplayName;
            VehicleData datalbpstolenbus = await Utilities.GetVehicleData(lbpstolenbus.NetworkId);
            string vehicleName = datalbpstolenbus.Name;
            datalbpstolenbus.Flag = "Stolen";
            Notify("~y~Officer ~b~" + displayName + ",~y~ the suspects are driving a " + vehicleName + "!");
                
            lbpdriver.Task.CruiseWithVehicle(lbpstolenbus, 2f, 387);
            lbpstolenbus.AttachBlip();
            lbpdriver.AttachBlip();
            API.AddBlipForEntity(lbpstolenbus.GetHashCode());
            API.AddBlipForEntity(lbpdriver.GetHashCode());

            Random lbpstolenbusscenario = new Random();
            int buspursuitending = lbpstolenbusscenario.Next(1, 100 + 1);
            if (buspursuitending < 25)
            {
                Tick += Methmademedoitagainofficer;
            }
            else if (buspursuitending >= 25 && buspursuitending < 50)
            {
                Tick += Gangrelatedinitiation;
            }
            else if (buspursuitending >= 50 && buspursuitending < 75)
            {
                Tick += Neededtogethome;
            }
            else if (buspursuitending >= 75)
            {
                Tick += Chopshopdestination;
            }
        }
        
        public async Task Methmademedoitagainofficer()
        {
            Tick -= Methmademedoitagainofficer;

            VehicleData methbus = new VehicleData();
            List<Item> busstuff = new List<Item>();
            Item buspmeth = new Item
            {
                Name = "Meth",
                IsIllegal = true
            };
            Item buspmethpipe = new Item
            {
                Name = "Meth pipe",
                IsIllegal = true
            };
            Item buspstolenpolicebadge = new Item
            {
                Name = "(Stolen) police badge",
                IsIllegal = true
            };
            Item buspbackpack = new Item
            {
                Name = "Backpack with drug paraphernalia",
                IsIllegal = true
            };
            Item buspblackbag = new Item
            {
                Name = "Black bag with drug paraphernalia",
                IsIllegal = true
            };
            Item buspcolouringbook = new Item
            {
                Name = "Colouring book for children",
                IsIllegal = false
            };
            Item busphacksaw = new Item
            {
                Name = "Hacksaw",
                IsIllegal = false
            };
            Item buspbloodyhacksaw = new Item
            {
                Name = "Bloody Hacksaw",
                IsIllegal = true
            };
            Item buspflashlight = new Item
            {
                Name = "Flashlight",
                IsIllegal = false
            };
            Item buspusedflare = new Item
            {
                Name = "Used flare",
                IsIllegal = false
            };
            Item buspemptybagoffamilychips = new Item
            {
                Name = "Empty family sized bag of chips",
                IsIllegal = false
            };
            Item buspsnackbagwithmeth = new Item
            {
                Name = "Bag of chips with meth inside",
                IsIllegal = true
            };
            Item buspcalculator = new Item
            {
                Name = "Calculator",
                IsIllegal = false
            };
        }

        public async Task Gangrelatedinitiation()
        {
            Tick -= Gangrelatedinitiation;
        }

        public async Task Neededtogethome()
        {
            Tick -= Neededtogethome;
        }

        public async Task Chopshopdestination()
        {
            Tick -= Chopshopdestination;
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