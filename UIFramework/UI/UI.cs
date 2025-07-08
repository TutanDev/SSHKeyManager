using ImGuiNET;

namespace UIFramwork.UI;

using UIFramework.Core;


public interface IWidget
{
    IEnumerable<object> Render();
}

public static class UI
{
    #region WIDGETS
    public static IWidget Window(string title, params IWidget[] children)
        => new Window(title, children);

    public static IWidget Label(string text)
        => new Label(text);

    public static IWidget Button(string label, object message)
        => new Button(label, message);

    #endregion WIDGETS
    

    #region LAYOUT
    public static IWidget Horizontal(params IWidget[] Children)
        => new Horizontal(Children);
    
    public static IWidget Vertical(params IWidget[] Children)
        => new Vertical(Children);
    
    public static IWidget Spacer(float width = 0, float height = 0) 
        => new Spacer(width, height);
    
    #endregion LAYOUT
}


