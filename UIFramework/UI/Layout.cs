namespace UIFramwork.UI;

using ImGuiNET;


public class Vertical : IWidget
{
    private readonly IWidget[] _children;
    
    public Vertical(params IWidget[] Children) => _children = Children;
        
    public IEnumerable<object> Render()
    {
        ImGui.BeginGroup();
            
        foreach (var child in _children)
        foreach (var msg in child.Render())
            yield return msg;
            
        ImGui.EndGroup();
    }
}

public class Horizontal : IWidget
{
    private readonly IWidget[] _children;
    
    public Horizontal(params IWidget[] Children) => _children = Children;
        
    public IEnumerable<object> Render()
    {
        ImGui.BeginGroup();

        foreach (var child in _children)
        {
            var childMsgs = child.Render();
            ImGui.SameLine();
            foreach (var msg in childMsgs)
            {
                yield return msg;
            }
        }
            
        ImGui.EndGroup();
    }
}

public record Spacer(float width = 0, float height = 0) : IWidget
{
    public IEnumerable<object> Render()
    {
        ImGui.Dummy(new System.Numerics.Vector2(width, height));
        return Enumerable.Empty<object>();
    }
}