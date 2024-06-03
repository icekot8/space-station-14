using Robust.Shared.GameStates;

namespace Content.Shared.Weapons.Misc;

[RegisterComponent, NetworkedComponent]
public sealed partial class GrapplingProjectileComponent : Component
{
    [ViewVariables(VVAccess.ReadWrite), DataField("autoDeleteOnMinLength")]
    public bool AutoDeleteOnMinLength = false;
}
