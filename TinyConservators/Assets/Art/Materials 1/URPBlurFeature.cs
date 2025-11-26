using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.Universal.Internal;

public class URPBlurFeature : ScriptableRendererFeature
{
    class BlurPass : ScriptableRenderPass
    {
        private Material blurMat;
        private RTHandle tempTexture;
        private RTHandle cameraColorTargetHandle;
        private string profilerTag = "URP Blur Pass";

        public BlurPass(Material mat)
        {
            blurMat = mat;
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        }

        public void Setup(RTHandle cameraColorTargetHandle)
        {
            this.cameraColorTargetHandle = cameraColorTargetHandle;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (blurMat == null) return;

            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);

            // Allocate temporary RTHandle if null or different size
            if (tempTexture == null || tempTexture.rt.width != renderingData.cameraData.cameraTargetDescriptor.width)
            {
                tempTexture?.Release();
                tempTexture = RTHandles.Alloc(renderingData.cameraData.cameraTargetDescriptor);
            }

            // Blit original → temp with blur
            cmd.Blit(cameraColorTargetHandle, tempTexture, blurMat);
            // Blit back temp → camera
            cmd.Blit(tempTexture, cameraColorTargetHandle);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            // Optional: keep tempTexture for reuse; no need to release every frame
        }
    }

    [System.Serializable]
    public class BlurSettings
    {
        public Material blurMaterial = null;
    }

    public BlurSettings settings = new BlurSettings();
    private BlurPass blurPass;

    public override void Create()
    {
        blurPass = new BlurPass(settings.blurMaterial);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        blurPass.Setup(renderer.cameraColorTargetHandle);
        renderer.EnqueuePass(blurPass);
    }
}