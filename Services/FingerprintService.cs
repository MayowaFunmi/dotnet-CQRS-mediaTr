using CQRSMediaTr.Data;
using CQRSMediaTr.Models;
using SourceAFIS;

namespace CQRSMediaTr.Services
{
    public class FingerprintService
    {
        private readonly DataContext _context;

        public FingerprintService(DataContext context)
        {
            _context = context;
        }

        public FingerprintTemplateModel Identify(FingerprintTemplate probe)
        {
            var candidates = _context.FingerprintTemplateModels;
            var matcher = new FingerprintMatcher(probe);
            FingerprintTemplateModel match = null;
            double max = Double.NegativeInfinity;
            foreach (var candidate in candidates)
            {
                double similarity = matcher.Match(candidate.TemplateData);
                if (similarity > max)
                {
                    max = similarity;
                    match = candidate;
                }
            }
            double threshold = 30;
            return max >= threshold ? match : null;
        }
    }
}