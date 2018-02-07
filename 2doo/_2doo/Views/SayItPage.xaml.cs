using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.AudioRecorder;
using Xamarin.Cognitive.BingSpeech;

namespace _2doo
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SayItPage : ContentPage
    {
        AudioRecorderService recorder;
        BingSpeechApiClient bingSpeechClient;

        public SayItPage()
        {
            InitializeComponent();
            recorder = new AudioRecorderService
            {
                StopRecordingOnSilence = true, //will stop recording after 2 seconds of silence
                StopRecordingAfterTimeout = true,  //stop recording after a max timeout 
                TotalAudioTimeout = TimeSpan.FromSeconds(10) //audio will stop recording after 15 seconds
            };

            bingSpeechClient = new BingSpeechApiClient(""); //insert bingspeech api key here

            //go fetch an auth token up front - this should decrease latecy on the first call. 
            //	Otherwise, this would be called automatically the first time I use the speech client
            Task.Run(() => bingSpeechClient.Authenticate());
        }

        async void RecordButton_Click(object sender, EventArgs e)
        {
            await RecordAudio();
        }


        async Task DoCommands(string[] splitText, string Text)
        {
            if (splitText[0].Equals("Add") && splitText[1].Equals("task"))
            {
                var _Item = new Item
                {
                    Text = Text.Replace("Add task ", ""),
                    Description = "",
                    Deadline = DateTime.Today,
                    isDone = false,
                    isValid = "❌"
            };

                _Item.DaysNumber = _Item.isValid + " " + _Item.Text + " (Finished in " + (_Item.Deadline - DateTime.Today).TotalDays.ToString() + " days)";
                MessagingCenter.Send(this, "AddItem", _Item);
                await Navigation.PopToRootAsync();
            }
            //delete task
            //valid task
            //unvalid task
        }

        async Task RecordAudio()
        {
            try
            {
                if (!recorder.IsRecording) //Record button clicked
                {
                    //start recording audio
                    var audioRecordTask = await recorder.StartRecording();

                    var audioFile = await audioRecordTask;

                    //if we're not streaming the audio as we're recording, we'll use the file-based STT API here
                    if (audioFile != null)
                    {
                        
                        var resultText = await bingSpeechClient.SpeechToTextSimple(audioFile);
                        if ((ResultsLabel.Text = resultText.DisplayText) != null)
                        {
                            var splitText = resultText.DisplayText.Split(new[] { ' ' }, 3);
                            if (splitText.Length == 3)
                                await DoCommands(splitText, resultText.DisplayText);
                        }
                    }
                }
                else //Stop button clicked
                {
                    //stop the recording...
                    await recorder.StopRecording();
                }
            }
            catch (Exception ex)
            {
                //blow up the app!
                throw ex;
            }
        }
    }
}