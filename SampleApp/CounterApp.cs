
using UIFramwork.UI;

namespace SampleApp;

using ImGuiNET;
using UIFramework.Core;

public record IncrementMsg();
public record DecrementMsg();

public record CounterModel(int Count);

public class CounterApp : AppLayer<CounterModel>
{
    public CounterApp(CounterModel model) : base(model)
    {
    }

    public override UpdateFnc<CounterModel> UpdateFunc => (model, msg, dt) =>
    {
        return msg switch
        {
            IncrementMsg => (model with { Count = model.Count + 1 }, Enumerable.Empty<object>()),
            DecrementMsg => (model with { Count = model.Count - 1 }, Enumerable.Empty<object>()),
            _ => (model, Enumerable.Empty<object>())
        };
    };
    
    public override RenderFnc<CounterModel> RendeFunc => (model, dt) => 
            UI.Window("Counter Sample App",
            UI.Vertical(
                UI.Label($"Count: {model.Count}"),
                UI.Spacer(0, 100),
                UI.Horizontal(
                    UI.Button("+", new IncrementMsg()),
                    UI.Button("-", new DecrementMsg())
                )));
}
