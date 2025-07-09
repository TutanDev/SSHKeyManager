using UIFramework.Core;
using UIFramwork.UI;

namespace UIFramwork.Core;

public interface IView
{
    void Update(float deltaTime);
    void Render(float deltaTime);
}


public delegate (TModel, IEnumerable<object>) UpdateFnc<TModel>(TModel model, object msg, float deltaTime);
public delegate IWidget RenderFnc<in TModel>(TModel model, float deltaTime);

public abstract class View<TModel> : IView
{
    protected abstract UpdateFnc<TModel> UpdateFunc { get; }
    protected abstract RenderFnc<TModel> RenderFunc{ get; }
    
    private readonly Queue<object> _messageQueue = new();
    private TModel _model;

    public View(TModel model) => _model = model;
    
    public void Update(float deltaTime)
    {
        while (_messageQueue.TryDequeue(out var msg))
        {
            var (newModel, cmds) = UpdateFunc(_model, msg, deltaTime);
            _model = newModel;

            EnqueueMessages(cmds);
        }
    }

    public void Render(float deltaTime)
    {
        var messages = RenderFunc(_model, deltaTime).Render();
        EnqueueMessages(messages);
    }
    
    private void EnqueueMessages(IEnumerable<object> messages)
    {
        foreach (var msg in messages ?? Enumerable.Empty<object>())
        {
            _messageQueue.Enqueue(msg);
        }
    }
}