using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.Media.SpeechSynthesis;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace J.A.R.V.I.S
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {

        public String Name;
        public String Message;
        public int msg;
        Contact contactt;
        Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint conword;
        Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint con;

        public MainPage()
        {
            this.InitializeComponent();

            mediaElement.MediaEnded += MediaElement_MediaEnded;
            
            Start2();
        }

        public async void jarvis()
        {
            
            if (Message.Equals("Hello"))
            {//hello 

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Welcome");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
                msg = 0;
                Start3();
            }
            else if (Message.Equals("What time is it"))
            {
                DateTime thisDay = DateTime.Today;
                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("The time is" + thisDay);

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
                msg = 0;
            }

            
        }

        public async void findContact()
        {
            var contactmanager = await ContactManager.RequestStoreAsync();
            var contactlist = await contactmanager.FindContactsAsync();

            foreach (var contact in contactlist)
            {
                if (contact.DisplayName.Equals(Name))
                {
                    if(msg == 0)
                    {
                        contactt = contact;
                        Start2();
                    }
                    else if(msg == 1)
                    {
                        
                        //ComposeEmail(contact, Message,);
                    }
                }
            }        
        }

        private async void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();
            

            string[] responses = { "George", "John", "Tony", "Jason", "Antony", "Gabriel" };



            if (msg == 0)
            {
                con = new Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint(responses, "yesOrNo");
            }
            else if (msg == 1)
            {
                responses = new string[]{ "Hello","What time is it"};
                con = new Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint(responses, "yesOrNo");
            }



                speechRecognizer.UIOptions.AudiblePrompt = "Say what you want to search for...";
            speechRecognizer.UIOptions.ExampleText = @"George";
            if(msg == 0)
            {
                speechRecognizer.Constraints.Add(con);
            }else
            {
                speechRecognizer.Constraints.Add(con);          
            }
            // Compile the dictation grammar by default.
            await speechRecognizer.CompileConstraintsAsync();

            // Start recognition.
            Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeWithUIAsync();

            //psaxneis epafes
            if (msg == 0)
            {
                if (speechRecognitionResult.Text!="")
                {
                    Name = speechRecognitionResult.Text;
                    //start2();
                    findContact();

                }
                else
                {
                    await Task.Delay(1000);

                    Start();

                }
            }else if(msg == 1)
            {
                if (speechRecognitionResult.Text != "")
                {
                    Message = speechRecognitionResult.Text;
                    Debug.WriteLine(Name);
                    jarvis();
                    //start2();
                   // ComposeEmail(contactt, Message);
                }
                else
                {
                    await Task.Delay(1000);

                    Start2();

                }
            }
        }

        public async void Start()
        {

            // The media object for controlling and playing audio.

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Please Say contact Name");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
            msg = 0;
            //Check(mediaElement);
           /* 
            // Do something with the recognition result.
            var messageDialog = new Windows.UI.Popups.MessageDialog(, "Text spoken");
            await messageDialog.ShowAsync();*/
        }

        public async void Start2()
        {
            // The media object for controlling and playing audio.

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Hello");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
            msg = 1;
        }

        public async void Start3()
        {
            // The media object for controlling and playing audio.

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Tell me whats wrong");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
            msg = 1;
        }


        private async void ComposeEmail(Windows.ApplicationModel.Contacts.Contact recipient,
        string messageBody)
        {
            var emailMessage = new Windows.ApplicationModel.Email.EmailMessage();
            emailMessage.Body = messageBody;      

            var email = recipient.Emails.FirstOrDefault<Windows.ApplicationModel.Contacts.ContactEmail>();
            if (email != null)
            {
                var emailRecipient = new Windows.ApplicationModel.Email.EmailRecipient(email.Address);
                emailMessage.To.Add(emailRecipient);
            }

            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(emailMessage);

        }
    }
}
