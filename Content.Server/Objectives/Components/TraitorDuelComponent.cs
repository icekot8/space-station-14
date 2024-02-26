using Content.Server.Objectives.Systems;
using Content.Shared.Objectives;
using Robust.Shared.Prototypes;

namespace Content.Server.Objectives.Components;

/// <summary>
/// Requires that you steal a certain item (or several)
/// </summary>

[RegisterComponent, Access(typeof(TraitorDuelConditionSystem))]
public sealed partial class TraitorDuelKillComponent : Component
{
}
[RegisterComponent, Access(typeof(TraitorDuelConditionSystem))]
public sealed partial class TraitorDuelSaveComponent : Component
{
}

