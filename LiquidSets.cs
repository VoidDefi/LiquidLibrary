using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace LiquidLibrary
{
    public static class LiquidSets
    {
        public static bool[] CanEvaporate { get; set; } = new bool[LiquidID.Count];

        public static int[] EvaporateSpeed { get; set; } = new int[LiquidID.Count];

        public static int[] Viscosity { get; set; } = new int[LiquidID.Count];

        public static int[] WaterfallLength { get; set; } = new int[LiquidID.Count];

        public static float[] DefaultOpacity { get; set; } = new float[LiquidID.Count];

        internal static void SetVanillaValues()
        {
            CanEvaporate[LiquidID.Water] = true;

            Viscosity[LiquidID.Lava] = 5;
            Viscosity[LiquidID.Honey] = 10;

            WaterfallLength[LiquidID.Water] = 10;
            WaterfallLength[LiquidID.Lava] = 3;
            WaterfallLength[LiquidID.Honey] = 2;
            WaterfallLength[LiquidID.Shimmer] = 10;

            DefaultOpacity[LiquidID.Water] = 0.6f;
            DefaultOpacity[LiquidID.Lava] = 0.95f;
            DefaultOpacity[LiquidID.Honey] = 0.95f;
            DefaultOpacity[LiquidID.Shimmer] = 0.75f;
        }
    }
}
