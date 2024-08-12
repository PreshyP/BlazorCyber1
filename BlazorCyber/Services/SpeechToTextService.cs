using BlazorCyber;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCyber.Services
{
    public class SpeechToTextService
    {
        private readonly ISpeechToText _speechToText;

        public SpeechToTextService(ISpeechToText speechToText)
        {
            _speechToText = speechToText;
        }

        public Task<string> Listen(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken)
        {
            return _speechToText.Listen(culture, recognitionResult, cancellationToken);
        }

        public Task<bool> RequestPermissions()
        {
            return _speechToText.RequestPermissions();
        }
    }
}