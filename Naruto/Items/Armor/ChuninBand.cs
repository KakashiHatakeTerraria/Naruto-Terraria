using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Naruto.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class ChuninBand : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chunin Headband");
			Tooltip.SetDefault("10 % Increased Chakra Damage"
                + "\n15% Increased Chakra crit" +
                               "\n+200 Max Chakra" +
                               "\nIncreased speed");
        }
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 2;
            item.defense = 10;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ChuninShirt") && legs.type == mod.ItemType("ChuninPants");
        }

        public override void UpdateEquip(Player player)
        {
            MyPlayer.ModPlayer(player).ChakraDamage += 0.1f;
            MyPlayer.ModPlayer(player).ChakraCrit += 15;
            MyPlayer.ModPlayer(player).ChakraMax2 += 200;
            player.moveSpeed += 1.2f;

        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawAltHair = true;
            drawHair=false;
        }

        public override void   AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 10);
            recipe.AddIngredient(ItemID.DemoniteBar, 15);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
