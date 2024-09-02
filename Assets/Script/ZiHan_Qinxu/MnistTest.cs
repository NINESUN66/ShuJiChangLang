using System.Linq;
using Unity.Barracuda;
using UnityEngine;


public class MnistTest : MonoBehaviour
{
    public NNModel model;
    public Texture2D image;
    public PreviewManager previewManager;
    public string result = "-1";

    private Model runtimeModel;
    private IWorker engine;

    [SerializeField] private float[] predicted;
    private bool isProcessing;

    private void Start()
    {
        runtimeModel = ModelLoader.Load(model);
        engine = WorkerFactory.CreateWorker(runtimeModel);
        
        Tensor input = new Tensor(image, 1);
        Tensor output = engine.Execute(input).PeekOutput();
        input.Dispose();
        predicted = output.AsFloats().SoftMax().ToArray();
    }
    
    public void OnDrawTexture(Texture texture)
    {
        if (!isProcessing)
        {
            DrawInference(texture);
        }
    }

    private void DrawInference(Texture texture)
    {
        isProcessing = true;
        int channel = 1;
        Tensor input = new Tensor(texture, channel);
        Tensor output = engine.Execute(input).PeekOutput();
        input.Dispose();
        predicted = output.AsFloats().SoftMax().ToArray();
        isProcessing = false;
    }

    public void DrawInferentFromPreview()
    {
        if (!previewManager.ScaledTexture) return;
        
        isProcessing = true;
        int channel = 1;
        Tensor input = new Tensor(previewManager.ScaledTexture, channel);
        Tensor output = engine.Execute(input).PeekOutput();
        input.Dispose();
        predicted = output.AsFloats().SoftMax().ToArray();
        isProcessing = false;

        float res = predicted[0];
        int index = 0;
        for (int i = 0; i < predicted.Length; i++)
        {
            if (predicted[i] >= res)
            {
                res = predicted[i];
                index = i;
            }
        }

        result = index.ToString();
    }   

    private void OnDestroy()
    {
        engine?.Dispose();
    }
}
