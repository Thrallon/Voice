using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Windows.Media.SpeechRecognition.SpeechRecognizer recon;

        public Interact()
        {
            this.InitializeComponent();


            recon = new Windows.Media.SpeechRecognition.SpeechRecognizer();
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
                msg = 0;
                mediaElement.Play();
               // jarvis();
                
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
                mediaElement.SetSource(stream, stream.ContentType);msg = 0;
                mediaElement.Play();
                
                //Start3();
            }
            else if (Message.Equals("Where are you from"))
            {//Where are you from//Who are your parents //Do you want to send email//bye
                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("I come from the small vilage of Alkmini");
                textBox.Text = "I come from the small vilage of Alkmini";
                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);msg = 0;
                mediaElement.Play();
                
                //Start3();
            }
            else if (Message.Equals("Who are your Creators"))
            {
                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("My Creators are Jason and Antony");
                textBox.Text = "My Creators are Jason and Antony";
                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);msg = 0;
                mediaElement.Play();
                
               // Start3();
            }
            else if (Message.Equals("What is your form"))
            {
                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("My form is binary");
                textBox.Text = "My Creators are Jason and Antony";
                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType); msg = 0;
                mediaElement.Play();

                // Start3();
            }
            else if (Message.Equals("Jarvis call my girlfriend"))
            {
                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Which one");
                textBox.Text = "Which one";
                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType); msg = 0;
                mediaElement.Play();
               
                //Start3();
            }
            else if (Message.Equals("Bye"))
            {
                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("GoodBye");
                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
                msg = 3;             
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
            var speechRecognizer = recon;


            string[] responses = { "George", "John", "Tony", "Jason", "Antony", "Gabriel" };



            if (msg == 0)
            {


                Start3();
                return;

                //responses = new string[]{ "Hello","What time is it", "Where are you from"
                  //  ,"Jarvis call my girlfriend","Who are your Creators","Bye","What is your form","bye" };
                //con = new Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint(responses, "yesOrNo");
            }
            else if (msg == 1)
            {
                responses = new string[]{ "Hello","What time is it", "Where are you from"
                    ,"Jarvis call my girlfriend","Who are your Creators","Bye","What is your form","bye" };
                con = new Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint(responses, "yesOrNo");
            }
            else if (msg == 2)
            {
                responses = new string[] { "A message" };
                con = new Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint(responses, "yesOrNo");
            }else if(msg == 3)
            {
                Frame.Navigate(typeof(MainPage));
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

            await Task.Delay(1000);

            // Start recognition.
            Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeAsync();

            if (speechRecognitionResult.Status == Windows.Media.SpeechRecognition.SpeechRecognitionResultStatus.Success)
            {
                Debug.WriteLine("bfgffgnf");
            }

            Debug.WriteLine(speechRecognitionResult);

            //psaxneis epafes
            if (msg == 0)
            {
                if (speechRecognitionResult.Text !="")
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
                if (speechRecognitionResult.Text !="")
                {
                    Message = speechRecognitionResult.Text;

                    jarvis();
                    return;
                }
               else
                {
                    await Task.Delay(2000);
                   // jarvis();
                   Start3();
                    return;
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
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Hello Master");
            textBox.Text = "Hello Master";
            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType); msg = 1;
            mediaElement.Play(); 

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
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Command me");
            textBox.Text = "Command me";
            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);msg = 1;
            mediaElement.Play();
            
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
