﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	internal abstract class AndromedonShader : AndromedonDrawLayer
	{
		public const int ShaderNumSegments = 8;
		public const int ShaderDrawOffset = 2;

		public override void Draw(ref PlayerDrawSet drawInfo) {
			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis) {
				return;
			}

			DrawDataInfo drawDataInfo = GetData(drawInfo);
			Player drawPlayer = drawInfo.drawPlayer;
			var devPlayer = drawPlayer.GetModPlayer<DeveloperPlayer>();
			SpriteEffects effects = SpriteEffects.None;

			if (drawPlayer.direction == -1) {
				effects |= SpriteEffects.FlipHorizontally;
			}

			if (drawPlayer.gravDir == -1) {
				effects |= SpriteEffects.FlipVertically;
			}

			var data = new DrawData(
				drawDataInfo.Texture,
				drawDataInfo.Position,
				drawDataInfo.Frame,
				Color.White * Main.essScale * devPlayer.AndromedonEffect.LayerStrength * devPlayer.AndromedonEffect.ShaderStrength,
				drawDataInfo.Rotation,
				drawDataInfo.Origin,
				1f,
				effects,
				0);

			BeginShaderBatch(Main.spriteBatch);

			ShaderId ??= GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingRainbowDye);
			
			GameShaders.Armor.Apply(ShaderId.Value, drawPlayer, data);
			
			var centerPos = data.position;

			for (int i = 0; i < ShaderNumSegments; i++) {
				data.position = centerPos + GetDrawOffset(i);
				data.Draw(Main.spriteBatch);
			}

			data.position = centerPos;
		}

		protected static Vector2 GetDrawOffset(int i) => new Vector2(0, ShaderDrawOffset).RotatedBy((float)i / ShaderNumSegments * MathHelper.TwoPi);

		private static void BeginShaderBatch(SpriteBatch batch) {
			batch.End();
			RasterizerState rasterizerState = Main.LocalPlayer.gravDir == 1f ? RasterizerState.CullCounterClockwise : RasterizerState.CullClockwise;
			batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, rasterizerState, null, Main.GameViewMatrix.TransformationMatrix);
		}
	}
}
