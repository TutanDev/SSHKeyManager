using UIFramwork.UI;

namespace UIFramework.Core;

public delegate  (TModel, IEnumerable<object>) UpdateFnc<TModel>(TModel model, object message, float deltaTime);
public delegate IWidget RenderFnc<TModel>(TModel model, float deltaTime);


public abstract class AppLayer
{
    public abstract void Update(float deltaTime);
    public abstract void Render(float deltaTime);
}

public abstract class AppLayer<TModel> : AppLayer
{
    public abstract UpdateFnc<TModel> UpdateFunc { get; }
    public abstract RenderFnc<TModel> RendeFunc{ get; }
    
    private readonly Queue<object> _messageQueue = new();
    private TModel _model;

    public AppLayer(TModel model)
    {
        _model = model;
    }
    
    public override void Update(float deltaTime)
    {
        while (_messageQueue.TryDequeue(out var msg))
        {
            var (newModel, cmds) = UpdateFunc(_model, msg, deltaTime);
            _model = newModel;

            EnqueueMessages(cmds);
        }
    }

    public override void Render(float deltaTime)
    {
        var messages = RendeFunc(_model, deltaTime).Render();
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

