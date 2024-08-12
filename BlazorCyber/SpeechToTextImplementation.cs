using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Media.SpeechRecognition;

namespace BlazorCyber.Platforms.Windows
{
    public class SpeechToTextImplementation : ISpeechToText
    {
        private SpeechRecognizer speechRecognizer;

        public async Task<string> Listen(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken)
        {
            string recognitionText = string.Empty;
            var taskResult = new TaskCompletionSource<string>();

            try
            {
                var language = new Language(culture.Name);

                // Check if the language is supported
                if (!SpeechRecognizer.SupportedTopicLanguages.Contains(language))
                {
                    throw new NotSupportedException($"The language '{culture.Name}' is not supported for speech recognition.");
                }

                speechRecognizer = new SpeechRecognizer(language);
                speechRecognizer.Constraints.Add(new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.Dictation, "dictation"));
                await speechRecognizer.CompileConstraintsAsync();

                speechRecognizer.ContinuousRecognitionSession.ResultGenerated += (s, e) =>
                {
                    recognitionResult?.Report(e.Result.Text);
                    if (e.Result.Confidence == SpeechRecognitionConfidence.High || e.Result.Confidence == SpeechRecognitionConfidence.Medium)
                    {
                        taskResult.TrySetResult(e.Result.Text);
                    }
                };

                await speechRecognizer.ContinuousRecognitionSession.StartAsync();

                cancellationToken.Register(() =>
                {
                    StopRecording();
                    taskResult.TrySetCanceled();
                });
            }
            catch (NotSupportedException ex)
            {
                taskResult.TrySetException(ex); // Handle unsupported language exception
            }
            catch (Exception ex)
            {
                taskResult.TrySetException(ex);
            }

            return await taskResult.Task;
        }

        private void StopRecording()
        {
            speechRecognizer?.ContinuousRecognitionSession?.StopAsync();
            speechRecognizer = null;
        }

        public Task<bool> RequestPermissions()
        {
            // Handle permissions if necessary (e.g., user consent)
            return Task.FromResult(true);
        }
    }
}
