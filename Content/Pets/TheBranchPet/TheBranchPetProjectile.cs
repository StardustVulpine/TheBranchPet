using System.Diagnostics.CodeAnalysis;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SVLib;

namespace TheBranchPet.Content.Pets.TheBranchPet
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class TheBranchPetProjectile : ModProjectile
    {
        private float AirTime {get => Projectile.ai[0]; set => Projectile.ai[0] = value; }
        
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
            Main.projPet[Projectile.type] = true;
            
            ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(
                0, 4, // Frames range for movement animation
                6, true)
                .WithOffset(-10f, 0f)
                .WithSpriteDirection(-1)
                .WhenNotSelected(0, 0); // Frames range for idle animation
            
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.DynamiteKitten);
            AIType = ProjectileID.BlackCat;
            DrawOriginOffsetY = -9;
        }
        
        
        
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.blackCat = false;
            return true;
        }
    
        // This method updates once per frame, 60 times per second
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.dead && player.HasBuff(ModContent.BuffType<TheBranchPetBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            Visuals();
            
            // Main.NewText($"Frame: {Projectile.frame}, Counter: {Projectile.frameCounter}");
        }
 
        private void Visuals()
        {
            Player player = Main.player[Projectile.owner];

            bool isOnGround = PlayerUtils.IsPlayerOnGround(player);
            bool isFlying = false;

            // Main.NewText(isOnGround ? "Is On Ground!" : "Is Not On Ground!");
            
            
            if (!isOnGround && player.velocity.Y != 0f)
            {
                AirTime++;
                if (AirTime > 10f)
                {
                    isFlying = true;
                }
            }
            else
            {
                AirTime = 0;
                isFlying = false;
            }
            
            // Main.NewText(isFlying ? "Player is flying!" : "Not Flying!");
            
            int frameSpeed = 10;
            int frameStart;
            int frameEnd;

            if (isOnGround && !isFlying)
            {
                frameStart = 0;
                frameEnd = 3;
            }
            else if (!isOnGround && !isFlying)
            {
                frameStart = 0;
                frameEnd = 3;
            }
            else
            {
                frameStart = 4;
                frameEnd = 4;
            }
            
            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame > frameEnd || Projectile.frame < frameStart)
                {
                    Projectile.frame = frameStart;
                }
            }
            
            if (Projectile.frame < frameStart || Projectile.frame > frameEnd)
            {
                Projectile.frame = frameStart;
            }
            
        }
        
        
    }
}

