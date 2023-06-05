using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;



namespace Sniper
{
    [CalloutProperties("Sniper", "Valandria", "0.0.3")]
    public class Sniper : Callout
    {
        Ped suspect;
        private Vector3[] coordinates = {

        new Vector3(-63.78735f, -809.3801f, 322.323f),
        new Vector3(-72.87148f, -791.8232f, 219.3506f),
        new Vector3(-28.58344f, -573.8484f, 107.3254f),
        new Vector3(27.29232f, -380.3864f, 73.94032f),
        new Vector3(-215.0798f, -379.5858f, 66.88085f),
        new Vector3(-706.9851f, -419.2187f, 67.11575f),
        new Vector3(-439.1875f, -909.3343f, 47.57659f),
        new Vector3(-291.7555f, -941.7549f, 131.5332f),
        new Vector3(-257.1837f, -715.4226f, 110.6872f),
        new Vector3(-331.377f, -66.94482f, 91.77756f),
        new Vector3(-454.9214f, -689.7439f, 81.18629f),
        new Vector3(458.0213f, -1077.517f, 45.34976f),
        new Vector3(529.9276f, -1049.96f, 56.78746f),
        new Vector3(644.3027f, -1004.046f, 56.78407f),
        new Vector3(-286.7011f, -636.9671f, 50.67485f),
        new Vector3(-522.8298f, -683.8063f, 46.73934f),
        new Vector3(-549.7852f, -626.4073f, 56.49168f),
        new Vector3(-75.981f, -990.4119f, 103.5957f),
        new Vector3(-143.8756f, -939.3723f, 268.924f),
        new Vector3(-309.7954f, -827.897f, 96.41442f),
        new Vector3(415.1362f, 5.536114f, 161.2257f),
        new Vector3(-201.9443f, -574.4083f, 176.7132f),
        new Vector3(-269.7795f, -750.269f, 125.2136f),
        new Vector3(-786.3641f, -2248.831f, 81.68291f),
        new Vector3(-813.4422f, -2182.879f, 96.65736f),
        new Vector3(122.9571f, -892.1083f, 135.782f),


        };

        public Sniper()
        {
            Vector3 location = coordinates.OrderBy(x => World.GetDistance(x, Game.PlayerPed.Position)).Skip(2).First();

            InitInfo(location);
            ShortName = "SC - Sniper on Roof";
            CalloutDescription = "Someone is standing on a roof shooting at people with a sniper rifle. Respond Code 3.";
            ResponseCode = 2;
            StartDistance = 200f;
        }

        public async override Task OnAccept()
        {
            InitBlip(200f, BlipColor.Red, BlipSprite.Waypoint, 90);
            UpdateData();
            var weapons = new[]
            {
                WeaponHash.HeavySniper,
                WeaponHash.HeavySniperMk2,
                WeaponHash.MarksmanRifle,
                WeaponHash.MarksmanRifleMk2,

            };
            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect.Weapons.Give(weapons[RandomUtils.Random.Next(weapons.Length)], 9999, true, true);
            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            



        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);
            suspect.AttachBlip();
            suspect.Accuracy = 95;
            suspect.Armor = 50;
            suspect.RelationshipGroup = 0xCE133D78;
            suspect.Task.FightAgainstHatedTargets(this.StartDistance);

        }
    }


}
