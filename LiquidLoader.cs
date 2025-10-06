using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace LiquidLibrary
{
    public static class LiquidLoader
    {
        private static int nextLiquid = LiquidID.Count;

        internal static readonly List<ModLiquid> liquids = new List<ModLiquid>();

        internal static readonly List<GlobalLiquid> globalLiquids = new List<GlobalLiquid>();

        public static int LiquidCount => nextLiquid;

        internal static int ReserveLiquidID()
        {
            int reserveID = nextLiquid;
            nextLiquid++;
            return reserveID;
        }

        public static ModLiquid GetLiquid(int type)
        {
            if (type < LiquidID.Count || type >= LiquidCount)
                return null;

            return liquids[type - LiquidID.Count];
        }

        public static int LiquidType<T>() where T : ModLiquid
        {
            return ModContent.GetInstance<T>()?.Type ?? 0;
        }

        internal static void LoadTypes()
        {
            Mod[] mods = ModLoader.Mods;
            Type modLiquidType = typeof(ModLiquid);
            Type globalLiquidType = typeof(GlobalLiquid);

            foreach (var mod in mods)
            {
                if (mod.Name == "ModLoader" || mod.Name == "LiquidLibrary")
                    continue;

                Assembly assembly = mod.Code;

                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsAbstract) continue;

                    if (type.IsSubclassOf(modLiquidType))
                        ((ModLiquid)Activator.CreateInstance(type)).BypassRegister();

                    //if (globalLiquidType.IsAssignableFrom(type))
                    //    globalLiquids.Add((GlobalLiquid)Activator.CreateInstance(type));
                }
            }

            ResizeArrays(LiquidCount);
        }

        private static void ResizeArrays(int liquidCount)
        {
            FieldInfo liquidCountsInfo = typeof(SceneMetrics).GetField("_liquidCounts", BindingFlags.NonPublic | BindingFlags.Instance);

            if (liquidCountsInfo == null)
                throw new Exception("Info is not exist!");

            liquidCountsInfo.SetValue(Main.SceneMetrics, new int[liquidCount]);

            //Resize Sets

            LiquidSets.CanEvaporate = new bool[liquidCount];
            LiquidSets.Viscosity = new int[liquidCount];
            LiquidSets.WaterfallsLength = new int[liquidCount];
            LiquidSets.DefaultOpacity = new float[liquidCount];

            LiquidSets.SetVanillaValues();

            for (int i = 0; i < liquids.Count; i++) 
            {
                ModLiquid liquid = liquids[i];

                if (liquid == null) continue;
                liquid.SetStaticDefaults();

                int liquidIndex = (LiquidID.Count - 1) + i;

                LiquidSets.CanEvaporate[liquidIndex] = liquid.canEvaporate;
                LiquidSets.Viscosity[liquidIndex] = liquid.viscosity;
                LiquidSets.WaterfallsLength[liquidIndex] = liquid.waterfallLength;
                LiquidSets.DefaultOpacity[liquidIndex] = liquid.defaultOpacity;
            }
        }
    }
}
