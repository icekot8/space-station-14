using Content.Shared.Chemistry.Reagent;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Components;
using Robust.Shared.Prototypes;
using Content.Shared.Mind;
using Content.Shared.Mind.Components;
using Content.Shared.Roles;
using Content.Shared.Roles.Jobs;
using Content.Server.Roles.Jobs;
using Content.Shared.Station;
using Content.Server.Database;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;
using Content.Server.Administration.Logs;
using Content.Shared.Administration.Logs;
using Content.Shared.Construction;
using Content.Shared.Database;
using Content.Server.Construction.Completions;
using Robust.Shared.Analyzers;
using Robust.Shared.Exceptions;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Log;
using Robust.Shared.Map;
using Robust.Shared.Reflection;
using System.Text;
using Robust.Shared.Utility;
using Robust.Shared.Analyzers;
using Robust.Shared.Exceptions;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Log;
using Robust.Shared.Map;
using Robust.Shared.Reflection;

namespace Content.Server.Chemistry.ReagentEffectConditions
{    
    public sealed partial class JobCondition : ReagentEffectCondition
    {
        private SharedMindSystem? _minds;
        private IAdminLogManager _adminLog = default!;
        
        [DataField]
        public string Job = "Passenger";

        public override bool Condition(ReagentEffectArgs args)
        {   
            if (args.EntityManager.TryGetComponent(args.SolutionEntity, out MindContainerComponent? mind))
            {
                var jobTitle = "No Profession";
                var jobs = args.EntityManager.System<SharedJobSystem>();
                jobs.MindTryGetJobName(args.SolutionEntity, out string? prototype);
                if (prototype != null)
                    jobTitle = prototype;

                if (Job == jobTitle) 
                    _adminLog.Add(LogType.Action, LogImpact.Medium, $"jobtitle = job");
                    return true;
            }
            
            return false;
        }

        public override string GuidebookExplanation(IPrototypeManager prototype)
        {
            return Loc.GetString("reagent-effect-condition-guidebook-job-condition", ("job", Job));
        }
    }
}

