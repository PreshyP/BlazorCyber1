using OpenQA.Selenium;

namespace BlazorCyber.Models
{
    public class SpeechToTextModel
    {
       
            public string RecognitionText { get; set; }

        public Command ListenCommand { get; set; }
        public Command ListenCancelCommand { get; set; }
    }
    }


