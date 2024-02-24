using Content.Shared.CrewManifest;
using Content.Shared.Eui;
using JetBrains.Annotations;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.GameObjects;
using Content.Client.Eui;
using Content.Client.CrewManifest.UI;

namespace Content.Client.CrewManifest
{
    public sealed class CrewManifestViewUserInterface : BoundUserInterface
    {
        public sealed class CrewManifestEui : BaseEui
        {
            private readonly CrewManifestUi _window = new();

            public CrewManifestEui() => _window.OnClose += () => SendMessage(new CloseEuiMessage());
            public override void Opened() => _window.OpenCentered();
            public override void Closed() => _window.Close();
            public override void HandleState(EuiStateBase state)
            {
                if (state is CrewManifestEuiState cast)
                {
                    _window.Populate(cast.StationName, cast.Entries);
                }
            }
        }

        public CrewManifestViewUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey) { }
    }
}