using Terraria.ID;
using Terraria.ModLoader;

namespace Naruto.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class GeninBand : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Genin Headband");
			Tooltip.SetDefault("This was used by Genin in Naruto.");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 2;
            item.defense = 5;
		}
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawAltHair = true;
            drawHair=true;
        }

        public override void   AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 10);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
