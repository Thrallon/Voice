using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace J.A.R.V.I.S
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Interact : Page
    {

        public String Name;
        public String Message;
        public int msg;
        Contact contactt;
        Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint con;

        public Interact()
        {
            this.InitializeComponent();

            mediaElement.MediaEnded += MediaElement_MediaEnded;

            Start2();
        }

        public async void jarvis()
        {
            textBox1.Text = Message;
            if (Message.Equals("Hello"))
            {//hello 

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Welcome");
                textBox.Text = "Welcome";
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
                textBox.Text = "The time is" + thisDay;
                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
                msg = 0;
                Start3();
            }
            else if (Message.Equals("Where are you from"))
            {//Where are you from//Who are your parents //Do you want to send email//bye
                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("I was created at Hackathon");
                textBox.Text = "I was created at Hackathon";
                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
                msg = 0;
                Start3();
            }
            else if (Message.Equals("Who are your Creators"))
            {
                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("My Creators are Jason and Antony");
                textBox.Text = "My Creators are Jason and Antony";
                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
                msg = 0;
                Start3();
            }
            else if (Message.Equals("Jarvis call my girlfriend"))
            {
                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Which one");
                textBox.Text = "Which one";
                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
                msg = 0;
                Start3();
            }
            else if (Message.Equals("Bye"))
            {
                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("My Creators are Jason and Antony");
                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
                msg = 3;
                Frame.Navigate(typeof(MainPage));
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
                    contactt = contact;
                    Start4();
                    // ComposeEmail(contact, Message);
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
                responses = new string[]{ "Hello","What time is it", "I was created at Hackathon"
                    ,"Jarvis call my girlfriend","Who are your Creators","Bye" };
                con = new Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint(responses, "yesOrNo");
            }
            else if (msg == 2)
            {
                responses = new string[] { "A message" };
                con = new Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint(responses, "yesOrNo");
            }



            speechRecognizer.UIOptions.AudiblePrompt = "Say what you want to search for...";
            speechRecognizer.UIOptions.ExampleText = @"George";
            if (msg == 0)
            {
                speechRecognizer.Constraints.Add(con);
            }
            else if (msg == 1)
            {
                speechRecognizer.Constraints.Add(con);
            }
            else if (msg == 2)
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
                if (speechRecognitionResult.Text != "")
                {
                    Name = speechRecognitionResult.Text;
                    //start2();
                    findContact();

                }
                else
                {
                    await Task.Delay(2000);

                    Start();
                }
            }
            else if (msg == 1)
            {
                if (speechRecognitionResult.Text != "")
                {
                    Message = speechRecognitionResult.Text;

                    jarvis();
                }
                else
                {
                    await Task.Delay(2000);

                    Start2();
                }
            }
            else if (msg == 2)
            {
                if (speechRecognitionResult.Text != "")
                {
                    Message = speechRecognitionResult.Text;
                    ComposeEmail(contactt, Message);
                }
                else
                {
                    await Task.Delay(2000);

                    Start4();
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
            textBox.Text = "Hello";
            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
            msg = 1;
        }

        public async void Start4()
        {
            // The media object for controlling and playing audio.

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Please say meassage to be sent");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
            msg = 2;
        }



        public async void Start3()
        {
            // The media object for controlling and playing audio.

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Tell me whats wrong");
            textBox.Text = "Tell me whats wrong";
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
