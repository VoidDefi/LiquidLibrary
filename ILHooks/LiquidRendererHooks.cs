using MonoMod.Cil;
using Mono.Cecil.Cil;
using System;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.GameContent.Liquid;

namespace LiquidLibrary.ILHooks
{
    public class LiquidRendererHooks
    {
        public static void CreateHooks()
        {
            IL_LiquidRenderer.DrawNormalLiquids += IL_DrawNormalLiquidsHook;
            IL_LiquidRenderer.InternalPrepareDraw += IL_InternalPrepareDrawHook;
        }

        private static void IL_DrawNormalLiquidsHook(ILContext context)
        {
            BindingFlags privateStatic = BindingFlags.NonPublic | BindingFlags.Static;
            Type liquidSets = typeof(LiquidSets);
            FieldInfo DefaultOpacityInfo = liquidSets.GetField("<DefaultOpacity>k__BackingField", privateStatic);

            BindingFlags privateStaticReadonly = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
            Type liquidRenderer = typeof(LiquidRenderer);
            FieldInfo DEFAULT_OPACITYInfo = liquidRenderer.GetField("DEFAULT_OPACITY", privateStaticReadonly);

            try
            {
                var c = new ILCursor(context);

                c.GotoNext(i => i.MatchLdsfld(DEFAULT_OPACITYInfo));
                c.Remove(); 
                c.Emit(OpCodes.Ldsfld, DefaultOpacityInfo);
                c.Index = 0;
            }

            catch (Exception ex)
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<LiquidLibrary>(), context);
            }
        }

        private static void IL_InternalPrepareDrawHook(ILContext context)
        {
            BindingFlags privateStatic = BindingFlags.NonPublic | BindingFlags.Static;
            Type liquidSets = typeof(LiquidSets);
            FieldInfo WaterfallLengthInfo = liquidSets.GetField("<WaterfallLength>k__BackingField", privateStatic);

            BindingFlags privateStaticReadonly = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
            Type liquidRenderer = typeof(LiquidRenderer);
            FieldInfo WATERFALL_LENGTHInfo = liquidRenderer.GetField("WATERFALL_LENGTH", privateStaticReadonly);

            try
            {
                var c = new ILCursor(context);

                for (int i = 0; i < 2; i++)
                {
                    c.GotoNext(i => i.MatchLdsfld(WATERFALL_LENGTHInfo));
                    c.Remove();
                    c.Emit(OpCodes.Ldsfld, WaterfallLengthInfo);
                    c.Index = 0;
                }
            }

            catch (Exception ex)
            {
                MonoModHooks.DumpIL(ModContent.GetInstance<LiquidLibrary>(), context);
            }
        }
    }
}
