using System;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;

namespace LiquidLibrary.ILHooks
{
    internal class LiquidHooks
    {
        public static void CreateHooks()
        {
            IL_Liquid.AddWater += IL_AddWaterHook;
            IL_Liquid.DelWater += IL_DelWaterHook;
            IL_Liquid.UpdateLiquid += IL_UpdateLiquidHook;
            IL_Liquid.QuickWater += IL_QuickWaterHook;
        }

        private static void IL_AddWaterHook(ILContext context)
        {
            try
            {
                var c = new ILCursor(context);

                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldarg_1);
                c.Emit(OpCodes.Call, typeof(LiquidObject).GetMethod(nameof(LiquidObject.AddWater_Hook)));
                c.Emit(OpCodes.Ret);
            }

            catch (Exception ex)
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<LiquidLibrary>(), context);
            }
        }

        private static void IL_UpdateLiquidHook(ILContext context)
        {
            try
            {
                var c = new ILCursor(context);

                c.Emit(OpCodes.Call, typeof(LiquidObject).GetMethod(nameof(LiquidObject.UpdateLiquid_Hook)));
                c.Emit(OpCodes.Ret);
            }

            catch (Exception ex)
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<LiquidLibrary>(), context);
            }
        }

        private static void IL_DelWaterHook(ILContext context)
        {
            try
            {
                var c = new ILCursor(context);

                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Call, typeof(LiquidObject).GetMethod(nameof(LiquidObject.DelWater_Hook)));
                c.Emit(OpCodes.Ret);
            }

            catch (Exception ex)
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<LiquidLibrary>(), context);
            }
        }

        private static void IL_QuickWaterHook(ILContext context)
        {
            try
            {
                var c = new ILCursor(context);

                c.Emit(OpCodes.Ldarg_0);
                c.Emit(OpCodes.Ldarg_1);
                c.Emit(OpCodes.Ldarg_2);
                c.Emit(OpCodes.Call, typeof(LiquidObject).GetMethod(nameof(LiquidObject.QuickWater_Hook)));
                c.Emit(OpCodes.Ret);
            }

            catch (Exception ex)
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<LiquidLibrary>(), context);
            }
        }
    }
}
