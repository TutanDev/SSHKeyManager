using ImGuiSupport;
using UIFramwork.Layers;

namespace UIFramwork.Core;

public class Application
{
    private readonly List<IView> _views = new();
    private readonly AppDocking _appDocking;

    public Application()
    {
        _appDocking = new AppDocking(new DockModel(true));
    }

    public void Update(float deltaTime)
    {
        _appDocking.Update(deltaTime);
        _views.ForEach(v => v.Update(deltaTime));
    }

    public void Render(float deltaTime)
    {
        _appDocking.Render(deltaTime);
        _views.ForEach(v => v.Render(deltaTime));
    }

    public void AddView(IView view)
    {
        _views.Add(view);
    }
    
    public void RemoveView(IView view)
    {
        _views.Remove(view);
    }
}