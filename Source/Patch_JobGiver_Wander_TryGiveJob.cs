using HarmonyLib;
using Verse.AI;
using static CleanPathfinding.ModSettings_CleanPathfinding;
using static CleanPathfinding.Mod_CleanPathfinding;
 
namespace CleanPathfinding
{
    /// <summary>
    /// Applies modifications to the JobGiver_Wander class to adjust the wandering behavior of pawns.
    /// </summary>
    [HarmonyPatch(typeof(JobGiver_Wander), nameof(JobGiver_Wander.TryGiveJob))]
    static class Patch_JobGiver_Wander_TryGiveJob
    {
        /// <summary>
        /// Prepares the patch operation by registering this patch with the internal ledger and setting the configuration.
        /// </summary>
        /// <returns>True if the wander tuning settings allow the patch, otherwise false.</returns>
        static bool Prepare()
        {
            patchLedger.TryAdd(nameof(Patch_JobGiver_Wander_TryGiveJob), wanderTuning);
            return wanderTuning;
        }

        /// <summary>
        /// Postfix method that adjusts the expiry interval of the wandering job based on the mod settings.
        /// </summary>
        /// <param name="__result">The job result from the TryGiveJob method, modified by this patch.</param>
        /// <remarks>
        /// This method checks if the job type is Wait_Wander and modifies its expiry interval by adding the configured delay.
        /// </remarks>
        public static void Postfix(Job __result)
        {
            if (__result?.def.shortHash == RimWorld.JobDefOf.Wait_Wander.shortHash)
                __result.expiryInterval += wanderDelay;
        }
    }
}