using System.Drawing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SVLib;
    
    
namespace TheBranchPet.Content.Pets.TheBranchPet
{
    public class TheBranchPetProjectile : ModProjectile
    {
        
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            Main.projPet[Projectile.type] = true;
            
            ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(
                0, Main.projFrames[Projectile.type], // Frames range for movement animation
                6, true)
                .WithOffset(-10f, 0f)
                .WithSpriteDirection(-1)
                .WhenNotSelected(0, 0); // Frames range for idle animation

            ProjectileID.Sets.SimpleLoop(
                0, Main.projFrames[Projectile.type], // Frames range for movement animation
                6, true)
                .WhenNotSelected(0, 0)
                .WhenSelected(0, 4);
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

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.dead && player.HasBuff(ModContent.BuffType<TheBranchPetBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            Visuals();
        }

        private void Visuals()
        {
            Player player = Main.player[Projectile.owner];

            bool isOnGround = PlayerUtils.IsPlayerOnGround(player);
            
            Main.NewText(isOnGround ? "Is On Ground!" : "Is Not On Ground!");
            
            int frameSpeed = 5;
            
            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }


        }
        
        /*/// <summary>
        /// Method to check if player is standing on solid ground or not
        /// </summary>
        /// <param name="player"></param>
        /// <returns>True if is on ground or false if not</returns>
        private bool IsPlayerOnGround(Player player)
        {
            // Get the tile directly below the player's feet
            int tileX = (int)(player.position.X + player.width / 2) / 16; // Horizontal center of player
            int tileY = (int)(player.position.Y + player.height + 1) / 16; // Bottom edge of player

            // Get the tile at the calculated position
            Tile tile = Main.tile[tileX, tileY];

            // Check if the tile is active and solid (indicates ground)
            return tile != null && tile.HasUnactuatedTile && Main.tileSolid[tile.TileType];
        }*/
        
    }
}

