using Content.Server.Objectives.Components;
using Content.Server.Shuttles.Systems;
using Content.Shared.Mind;
using Content.Server.GameTicking.Rules;
using Content.Shared.Objectives.Components;
using Content.Shared.Roles.Jobs;
using Robust.Shared.Random;
using Robust.Shared.Configuration;
using System.Linq;
using Content.Shared.Cuffs.Components;

namespace Content.Server.Objectives.Systems
{
    public sealed class TraitorDuelConditionSystem : EntitySystem
    {
        [Dependency] private readonly EmergencyShuttleSystem _emergencyShuttle = default!;
        [Dependency] private readonly IConfigurationManager _config = default!;
        [Dependency] private readonly IRobustRandom _random = default!;
        [Dependency] private readonly SharedJobSystem _job = default!;
        [Dependency] private readonly SharedMindSystem _mind = default!;
        [Dependency] private readonly TargetObjectiveSystem _target = default!;
        [Dependency] private readonly TraitorRuleSystem _traitorRule = default!;
        [Dependency] private readonly KillPersonConditionSystem _killPersonConditionSystem = default!;
        [Dependency] private readonly ObjectivesSystem _objectivesSystem = default!;


        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<TraitorDuelKillComponent, ObjectiveGetProgressEvent>(OnGetProgressOne);
            SubscribeLocalEvent<TraitorDuelSaveComponent, ObjectiveGetProgressEvent>(OnGetProgressTwo);
        }

        private void OnGetProgressOne(EntityUid uid, TraitorDuelKillComponent comp, ref ObjectiveGetProgressEvent args)
        {
            args.Progress = GetProgressOne(uid, args.Mind);
        }

        private void OnGetProgressTwo(EntityUid uid, TraitorDuelSaveComponent comp, ref ObjectiveGetProgressEvent args)
        {
            args.Progress = GetProgressTwo(uid, args.Mind);
        }

        private void OnAssignedOne(Entity<TraitorDuelKillComponent> condition, ref ObjectiveAssignedEvent args)
        {
            var traitorMinds = _traitorRule.GetOtherTraitorMindsAliveAndConnected(args.Mind);
            
            var traitorsWithOwnedEntities = traitorMinds.Where(traitor => traitor.Mind.OwnedEntity != null).ToList();
            
            if (traitorsWithOwnedEntities.Count < 2)
            {
                args.Cancelled = true;
                return;
            }

            var duelist1 = traitorsWithOwnedEntities[_random.Next(0, traitorsWithOwnedEntities.Count)];
            
            // Создаем компоненты целей для каждого дуэлянта
            var killObjective1 = new TraitorDuelKillComponent();

            // Назначаем цели для каждого дуэлянта
            _objectivesSystem.GetDuelKillObjective(duelist1.uid, killObjective1);
        }

        private void OnAssignedTwo(Entity<TraitorDuelSaveComponent> condition, ref ObjectiveAssignedEvent args)
        {
            var traitorMinds = _traitorRule.GetOtherTraitorMindsAliveAndConnected(args.Mind);
            
            var traitorsWithOwnedEntities = traitorMinds.Where(traitor => traitor.Mind.OwnedEntity != null).ToList();
            
            if (traitorsWithOwnedEntities.Count < 2)
            {
                args.Cancelled = true;
                return;
            }

            var duelist2 = traitorsWithOwnedEntities[_random.Next(0, traitorsWithOwnedEntities.Count)];
            var saveObjective2 = new TraitorDuelSaveComponent();

            _objectivesSystem.GetDuelSaveObjective(duelist2.uid, saveObjective2);
        }

        private void OnPersonAssigned(EntityUid uid, PickRandomPersonComponent comp, ref ObjectiveAssignedEvent args)
        {
            _killPersonConditionSystem.OnPersonAssigned(uid, comp, ref args);
        }
    
        private float GetProgressOne(EntityUid? uid, MindComponent mind)
        {
            if (uid == null || mind.OwnedEntity == null || _mind.IsCharacterDeadIc(mind))
                return 0f;
            return 0f; // You need to return a value in all code paths
        }

        private float GetProgressTwo(EntityUid? uid, MindComponent mind)
        {
            if (uid == null || mind.OwnedEntity == null || _mind.IsCharacterDeadIc(mind))
                return 1f;
            return 0f; // You need to return a value in all code paths
        }
    }
}

//    private float GetProgress(EntityUid mindId, MindComponent mind)
//    {
        // not escaping alive if you're deleted/dead
//        if (mind.OwnedEntity == null || _mind.IsCharacterDeadIc(mind))
//            return 0f;

        // You're not escaping if you're restrained!
//        if (TryComp<CuffableComponent>(mind.OwnedEntity, out var cuffed) && cuffed.CuffedHandCount > 0)
//            return 0f;

        // Any emergency shuttle counts for this objective, but not pods.
//        return _emergencyShuttle.IsTargetEscaping(mind.OwnedEntity.Value) ? 1f : 0f;
