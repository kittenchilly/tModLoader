﻿using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	internal class AndromedonHeadGlow : AndromedonGlow
	{
		private static Asset<Texture2D> _glowTexture;

		public override bool IsHeadLayer => true;

		public override DrawDataInfo GetData(PlayerDrawSet info) {
			_glowTexture ??= ModContent.GetTexture("ModLoader/Developer.Jofairden.PowerRanger_Head_Head_Glow");

			return GetHeadDrawDataInfo(info, _glowTexture.Value);
		}

		public override void GetDefaults(Player drawPlayer, out bool visible, out float depth) {
			base.GetDefaults(drawPlayer, out visible, out _);

			depth = Head.depth + 0.5f;
		}
	}
}
