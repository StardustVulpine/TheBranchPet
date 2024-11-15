using TheBranchPet.Content.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheBranchPet.Content.Pets.TheBranchPet
{
    
    public class TheBranchPetItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.shoot = ModContent.ProjectileType<TheBranchPetProjectile>();
            Item.buffType = ModContent.BuffType<TheBranchPetBuff>();
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                player.AddBuff(Item.buffType, 3600);
            }
            return true;
        }
        

        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.DirtBlock)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}

