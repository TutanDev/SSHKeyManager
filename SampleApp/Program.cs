using SampleApp;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using UIFramework.Core;
using UIFramwork.Core;

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
        
        var app = new Application();
        var counterView = new CounterView(new CounterModel(0));
        app.AddView(counterView);
        
        runner.Run(app);

        Environment.Exit(0);
    }
}