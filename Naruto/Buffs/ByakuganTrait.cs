using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Naruto.Buffs
{
    public class ByakuganTrait : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Byakugan");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = false;
            Description.SetDefault("You are a byakugan holder from the Hyuga Clan.");
        }
    }
}

