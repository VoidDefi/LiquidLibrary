using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace LiquidLibrary
{
    public class LiquidConfig: ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Mods.LiquidLibrary.Config.Liquid")]
        public bool automaticDrying;
    }
}
