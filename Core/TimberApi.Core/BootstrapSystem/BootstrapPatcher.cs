﻿using System;
using System.IO;
using Bindito.Core;
using HarmonyLib;
using TimberApi.Common;
using Timberborn.Bootstrapper;

namespace TimberApi.Core.BootstrapSystem
{
    internal static class BootstrapPatcher
    {
        public static void Patch()
        {
            try
            {
                var harmony = new Harmony("TimberApi.bootstrapper");

                harmony.Patch(AccessTools.Method(typeof(BootstrapperConfigurator), "Configure"),
                    new HarmonyMethod(AccessTools.Method(typeof(BootstrapPatcher), nameof(BootstrapperConfiguratorPatch))));
            }
            catch (Exception e)
            {
                File.WriteAllText(Path.Combine(Paths.Logs, $"HarmonyException-{DateTime.Now:yyyy-MM-dd-HH\\hmm\\mss\\s}.log"), e.ToString());
                throw;
            }
        }

        /// <summary>
        ///     Prefixes bootstrapper to install itself into bindito
        /// </summary>
        /// <param name="containerDefinition"></param>
        private static void BootstrapperConfiguratorPatch(IContainerDefinition containerDefinition)
        {
            containerDefinition.Install(TimberApiBootstrapSystemConfigurator.Instance);
        }
    }
}