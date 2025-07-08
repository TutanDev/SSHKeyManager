using ImGuiNET;
using SampleApp;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using UIFramework.Core;

public class Program 
{
    public static void Main(string[] args)
    {
        var options = WindowOptions.Default with
        {
            Title = "SilkUI",
            Size = new Vector2D<int>(1280, 720),
            API = GraphicsAPI.Default with
            {
                API = ContextAPI.OpenGL,
                Profile = ContextProfile.Core,
                Flags = ContextFlags.Debug
            }
        };

        var window = Window.Create(options);
        var runner = new AppRunner(window);

        var CounterLayer = new CounterApp(new CounterModel(0));
        runner.Run(CounterLayer);

        Environment.Exit(0);
    }
}