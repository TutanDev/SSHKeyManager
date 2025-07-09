
using UIFramwork.Core;
using UIFramwork.UI;

namespace SampleApp;

public record IncrementMsg();
public record DecrementMsg();

public record CounterModel(int Count);

public class CounterView : View<CounterModel>
{
    public CounterView(CounterModel model) : base(model) { }

    protected override UpdateFnc<CounterModel> UpdateFunc => (model, msg, dt) =>
    {
        return msg switch
        {
            IncrementMsg => (model with { Count = model.Count + 1 }, Enumerable.Empty<object>()),
            DecrementMsg => (model with { Count = model.Count - 1 }, Enumerable.Empty<object>()),
            _ => (model, Enumerable.Empty<object>())
        };
    };
    
    protected override RenderFnc<CounterModel> RenderFunc => (model, dt) => 
            UI.Window("Counter Sample App",
            UI.Vertical(
                UI.Label($"Count: {model.Count}"),
                UI.Spacer(0, 100),
                UI.Horizontal(
                    UI.Button("+", new IncrementMsg()),
                    UI.Button("-", new DecrementMsg())
                )));
}
