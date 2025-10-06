using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace LiquidLibrary
{
    internal static class LegacyTile
    {
        internal static bool lava(this Tile tile)
        {
            return tile.LiquidType == 1;
        }

        internal static void lava(this Tile tile, bool lava)
        {
            SetIsLiquidType(tile, 1, lava);
        }

        internal static bool honey(this Tile tile)
        {
            return tile.LiquidType == 2;
        }

        internal static void honey(this Tile tile, bool honey)
        {
            SetIsLiquidType(tile, 1, honey);
        }

        internal static bool shimmer(this Tile tile)
        {
            return tile.LiquidType == 3;
        }

        internal static void shimmer(this Tile tile, bool shimmer)
        {
            SetIsLiquidType(tile, 1, shimmer);
        }

        private static void SetIsLiquidType(Tile tile, int liquidId, bool value)
        {
            if (value)
            {
                tile.LiquidType = liquidId;
            }
            else if (tile.LiquidType == liquidId)
            {
                tile.LiquidType = 0;
            }
        }
    }
}
