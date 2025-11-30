using MedicalImageProcessor.Core.Entities;
using MedicalImageProcessor.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace MedicalImageProcessor.Application.Services
{
    public class ImageDetectionService
    {
        private readonly IImageProcessor _processor;
        private readonly IDetectionService _detector;
        private readonly ILogger<ImageDetectionService> _logger;

        public ImageDetectionService(IImageProcessor processor, IDetectionService detector, ILogger<ImageDetectionService> logger)
        {
            _processor = processor;
            _detector = detector;
            _logger = logger;
        }

        public async Task<DetectionResult> ProcessAndDetectAsync(ImageInput input, CancellationToken ct = default)
        {
            _logger.LogInformation("Starting preprocessing for image {ImageId}", input.ImageId);
            var preprocessed = await _processor.PreprocessAsync(input, ct);
            _logger.LogInformation("Preprocessing complete, starting detection");
            return await _detector.DetectAsync(preprocessed, ct);
        }
    }
}