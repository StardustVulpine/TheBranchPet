using TheBranchPet.Content.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheBranchPet.Content.Pets.TheBranchPet
{
    
    public class TheBranchPetItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.UnluckyYarn);
            
            Item.shoot = ModContent.ProjectileType<TheBranchPetProjectile>();
            Item.buffType = ModContent.BuffType<TheBranchPetBuff>();
        }
        
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.DirtBlock, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override void UseStyle(Player player, Rectangle heldItem)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }
    }
}

