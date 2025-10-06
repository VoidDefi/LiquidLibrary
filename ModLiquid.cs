using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace LiquidLibrary
{
    public abstract class ModLiquid : ModBlockType
    {
        internal static PropertyInfo TypeInfo { get; private set; }

        public override string LocalizationCategory => "Liquids";

        /// <summary>
        /// Determines whether this liquid can evaporate in the underworld.
        /// </summary>
        public bool canEvaporate = false;

        /// <summary>
        /// Determines the speed of liquid flow. The higher the value, the longer it flows.
        /// <code>
        /// Vanilla liquids viscosity:
        /// 
        /// * water   - 0
        /// * lava    - 5
        /// * honey   - 10
        /// * shimmer - 0
        /// </code>
        /// </summary>
        public int viscosity = 0;

        /// <summary>
        /// Determines the length of liquid waterfalls
        /// <code>
        /// Vanilla waterfalls Length:
        /// 
        /// * water   - 10
        /// * lava    - 3
        /// * honey   - 2
        /// * shimmer - 10
        /// </code>
        /// </summary>
        public int waterfallLength = 10;

        /// <summary>
        /// Opacity of liquid
        /// <code>
        /// Vanilla opacity Length:
        /// 
        /// * water   - 0.6
        /// * lava    - 0.95
        /// * honey   - 0.95
        /// * shimmer - 0.75
        /// </code>
        /// </summary>
        public float defaultOpacity = 1f;

        protected sealed override void Register()
        {
            if (TypeInfo == null) throw new Exception("TypeInfo cannot be null!");

            ModTypeLookup<ModLiquid>.Register(this);
            TypeInfo.SetMethod.Invoke(this, new object[] { (ushort)LiquidLoader.ReserveLiquidID() });
            LiquidLoader.liquids.Add(this);

            Load();
        }

        internal void BypassRegister()
        {
            Register();
        }

        internal static void Init()
        {
            Type type = typeof(ModBlockType);
            TypeInfo = type.GetProperty("Type");
        }
    }
}
