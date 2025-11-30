using MedicalImageProcessor.Core.Entities;

namespace MedicalImageProcessor.Core.Interfaces
{
    public interface IDetectionService
    {
        Task<DetectionResult> DetectAsync(byte[] preprocessedImage, CancellationToken cancellationToken = default);
    }
}