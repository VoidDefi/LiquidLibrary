using LiquidLibrary.ILHooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace LiquidLibrary
{
	public class LiquidLibrary : Mod
	{
        public override void Load()
        {
            ModLiquid.Init();
            LiquidObject.LoadFieldsInfo();
            LiquidHooks.CreateHooks();
            LiquidRendererHooks.CreateHooks();
        }

        public override void Unload()
        {
            
        }

        public override void PostSetupContent()
        {
            LiquidLoader.LoadTypes();
        }
    }
}
