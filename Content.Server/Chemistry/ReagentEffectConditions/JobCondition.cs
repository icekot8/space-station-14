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
using Content.Server.Roles.Jobs;
using Content.Server.Administration;
using Content.Server.GameTicking.Components;
using Content.Server.GameTicking.Rules.Components;
using Content.Shared.Administration;
using Content.Shared.Database;
using Content.Shared.Prototypes;
using JetBrains.Annotations;
using Robust.Shared.Console;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;
using Content.Server.GameTicking;

namespace Content.Server.Chemistry.ReagentEffectConditions
{    
    public sealed partial class JobCondition : ReagentEffectCondition
    {
        [DataField]
        public string Job = "Passenger";
        
        public override bool Condition(ReagentEffectArgs args)
        {   
            var job = new JobSystem();
            args.EntityManager.TryGetComponent<MindContainerComponent>(args.SolutionEntity, out var mindContainer);
            if (mindContainer != null && mindContainer.Mind != null)
            {
                if (job.MindTryGetJob(mindContainer?.Mind, out _, out var prototype) && prototype != null)
                {
                    if (prototype.LocalizedName == Job)
                        return true;
                }
            }
            
            return false;
        }

        public override string GuidebookExplanation(IPrototypeManager prototype)
        {
            return Loc.GetString("reagent-effect-condition-guidebook-job-condition", ("job", Job));
        }
    }
}

