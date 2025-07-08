namespace UIFramwork.UI;

using ImGuiNET;
using UIFramework.Core;

public class Window : IWidget
{
    private readonly string _title;
    private readonly IWidget[] _children;

    public Window(string title, params IWidget[] children)
    {
        _title = title;
        _children = children;
    }

    public IEnumerable<object> Render()
    {
        if (ImGui.Begin(_title))
        {
            foreach (var child in _children)
            foreach (var msg in child.Render())
                yield return msg;

            ImGui.End();
        }
    }
}
    
public class Label : IWidget
{
    private readonly string _text;

    public Label(string text) => _text = text;

    public IEnumerable<object> Render()
    {
        ImGui.Text(_text);
        return Enumerable.Empty<object>();
    }
}
    
public class Button : IWidget
{
    private readonly string _label;
    private readonly object _onClick;

    public Button(string label, object onClick)
    {
        _label = label;
        _onClick = onClick;
    }

    public IEnumerable<object> Render()
    {
        if (ImGui.Button(_label))
            yield return _onClick;
    }
}