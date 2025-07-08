using UIFramwork.Layers;

namespace UIFramework.Core;

using ImGuiSupport;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;


public class AppRunner
{
    private readonly IWindow _window;
    private IInputContext _inputContext;
    private GL _gl;
    private ImGuiController _imgui;
    
    private readonly DockLayer _dockLayer;
    private AppLayer _sampleLayer;

    public AppRunner(IWindow window)
    {
        _window = window;
        _window.Load += OnLoad;
        _window.Resize += OnResize;
        _window.Update += OnUpdate;
        _window.Render += OnRender;
        _window.Closing += OnClose;

        _dockLayer = new DockLayer(new DockModel(true));
    }

    public void Run(AppLayer app)
    {
        _sampleLayer = app;
        _window.Run();
    }

    private void OnLoad()
    {
        _inputContext = _window.CreateInput();
        _gl = _window.CreateOpenGL();
        _imgui = new ImGuiController(_gl, _window, _inputContext);
    }
    private void OnResize(Vector2D<int> newSize)
    {
        _gl.Viewport(newSize);
    }

    private void OnUpdate(double deltaTime)
    {
        _imgui.Update((float)deltaTime);

        _dockLayer.Update((float)deltaTime);
        _sampleLayer.Update((float)deltaTime); 
    }

    private void OnRender(double deltaTime)
    {
        _gl.Clear((uint)ClearBufferMask.ColorBufferBit);
        _gl.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);

        _dockLayer.Render((float)deltaTime);
        _sampleLayer.Render((float)deltaTime);

        _imgui.Render();
    }


    private void OnClose()
    {
        _imgui?.Dispose();
        _inputContext?.Dispose();
        _gl?.Dispose();
    }
}