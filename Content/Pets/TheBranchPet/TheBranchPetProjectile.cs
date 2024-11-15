using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheBranchPet.Content.Pets.TheBranchPet
{
    public class TheBranchPetProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            Main.projPet[Projectile.type] = true;
            
            ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(
                0,Main.projFrames[Projectile.type], // Frames range for movement animation
                4,true)
                .WithOffset(-10f, -10f)
                .WithSpriteDirection(-1)
                .WithCode(DelegateMethods.CharacterPreview.Float)
                .WhenNotSelected(0, 0); // Frames range for idle animation
            
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bunny);
            AIType = ProjectileID.Bunny;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.bunny = false;
            
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.dead && player.HasBuff(ModContent.BuffType<TheBranchPetBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}

