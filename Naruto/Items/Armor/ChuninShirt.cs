using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Naruto.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ChuninShirt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chunin Shirt");
            Tooltip.SetDefault("24% Increased Chakra Damage"
                + "\n16% Increased Chakra knockback" +
                               "\n+300 Max Chakra");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.value = 12000;
            item.rare = 10;
            item.defense = 12;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == mod.ItemType("ChuninBand") && legs.type == mod.ItemType("ChuninPants");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increased speed, increased chakra, and chakra damage";
            MyPlayer.ModPlayer(player).chuninBonus = true;
            MyPlayer.ModPlayer(player).ChakraDamage += 0.15f;
            MyPlayer.ModPlayer(player).ChakraMax2 += 300;
            player.moveSpeed *= 1.5f;
        }
        public override void UpdateEquip(Player player)
        {
            MyPlayer.ModPlayer(player).ChakraDamage += 0.24f;
            MyPlayer.ModPlayer(player).ChakraKbAddition += 16;
            MyPlayer.ModPlayer(player).ChakraMax2 += 300;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.DemoniteBar, 18);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}