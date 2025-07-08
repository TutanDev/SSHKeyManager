using System.Numerics;
using ImGuiNET;
using UIFramework.Core;

namespace UIFramwork.Layers;

public record DockModel(bool DockingEnabled);

public class DockLayer : AppLayer<DockModel>
{
    public DockLayer(DockModel model) : base(model)
    {
    }

    public override UpdateFnc<DockModel> UpdateFunc => (model, msg, dt) => (model, Enumerable.Empty<object>());

    public override RenderFnc<DockModel> RendeFunc => (model, dt) =>
    {
        DockSpace();
        return UI.UI.Spacer();
    };


    private void DockSpace()
    {
        ImGuiDockNodeFlags dockspaceFlags = ImGuiDockNodeFlags.PassthruCentralNode;

        // We are using the ImGuiWindowFlags_NoDocking flag to make the parent
        // window not dockable into, because it would be confusing to have two
        // docking targets within each others.
        ImGuiWindowFlags window_flags = ImGuiWindowFlags.MenuBar | ImGuiWindowFlags.NoDocking;
        // When using ImGuiDockNodeFlags_PassthruCentralNode, DockSpace() will
        // render our background and handle the pass-thru hole, so we ask Begin() to
        // not render a background.
        window_flags |= ImGuiWindowFlags.NoBackground;
        ImGuiViewportPtr viewport = ImGui.GetMainViewport();

        ImGui.SetNextWindowPos(viewport.WorkPos, 0, Vector2.Zero);
        ImGui.SetNextWindowSize(viewport.WorkSize, 0);
        ImGui.SetNextWindowViewport(viewport.ID);

        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);

        window_flags |= ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse |
                        ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove;
        window_flags |= ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus;



        // Important: note that we proceed even if Begin() returns false (aka window
        // is collapsed). This is because we want to keep our DockSpace() active. If
        // a DockSpace() is inactive, all active windows docked into it will lose
        // their parent and become undocked. We cannot preserve the docking
        // relationship between an active window and an inactive docking, otherwise
        // any change of dockspace/settings would lead to windows being stuck in
        // limbo and never being visible.
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);

        ImGui.Begin("DockSpace Demo", window_flags);
        ImGui.PopStyleVar(1);
        ImGui.PopStyleVar(2);

        // Submit the DockSpace
        ImGuiIOPtr io = ImGui.GetIO();
        if ((io.ConfigFlags & ImGuiConfigFlags.DockingEnable) != 0)
        {
            uint dockspaceId = ImGui.GetID("MyDockSpace");
            ImGui.DockSpace(dockspaceId, Vector2.Zero, dockspaceFlags);
        }

        // TODO: Menu bar here
        if (ImGui.BeginMenuBar())
        {
            if (ImGui.BeginMenu("File"))
            {
                if (ImGui.MenuItem("New"))
                {
                    /* handle New */
                }

                if (ImGui.MenuItem("Open..."))
                {
                    /* handle Open */
                }

                if (ImGui.MenuItem("Save"))
                {
                    /* handle Save */
                }

                ImGui.Separator();
                if (ImGui.MenuItem("Exit"))
                {
                    /* handle Exit */
                }

                ImGui.EndMenu();
            }
        }

        ImGui.EndMenuBar();

        ImGui.End();
    }
}