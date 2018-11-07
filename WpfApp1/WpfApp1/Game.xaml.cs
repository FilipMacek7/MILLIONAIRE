using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interakční logika pro Page1.xaml
    /// </summary>
    public partial class Game : Page
    {
        Sound question100 = new Sound();
        Sound question2000 = new Sound();
        Sound question4000 = new Sound();
        Sound question8000 = new Sound();
        Sound question16000 = new Sound();
        Sound question32000 = new Sound();
        Sound question64000 = new Sound();
        Sound question125000 = new Sound();
        Sound question250000 = new Sound();
        Sound question500000 = new Sound();
        Sound question1000000 = new Sound();
        Sound wrong = new Sound();
        Sound correct = new Sound();
        Sound letsplay2000 = new Sound();
        Sound letsplay4000 = new Sound();
        Sound letsplay8000 = new Sound();
        Sound letsplay16000 = new Sound();
        Sound letsplay100 = new Sound();
        Sound askFriend = new Sound();
        public Game()
        {
            InitializeComponent();
            createQuestions();
            generateQuestion();
        }
        List<Question> easyQuestions = new List<Question>();
        List<Question> normalQuestions = new List<Question>();
        List<Question> hardQuestions = new List<Question>();
        List<int> easyRandom = new List<int>();
        List<int> normalRandom = new List<int>();
        List<int> hardRandom = new List<int>();
        int level = 1;
        int cash;
        string rightAnswer;
        string currentQuestion;
        string currentAnswer1;
        string currentAnswer2;
        string currentAnswer3;
        string currentAnswer4;
        Random rnd = new Random();
        private void createQuestions()
        {
            //loading questions
            using (var package = new ExcelPackage(new FileInfo("questions.xlsx")))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets["Questions"];
                for (int i = 0; i < ws.Dimension.End.Row - 1; i++)
                {
                    if (ws.Cells[i + 2, 2].Value.ToString().Trim().Equals("1"))
                    {
                        Question question = new Question();
                        easyQuestions.Add(question);
                        easyQuestions[easyQuestions.Count() - 1].Content = ws.Cells[i + 2, 1].Value.ToString().Trim();
                        string diff = ws.Cells[i + 2, 2].Value.ToString().Trim();
                        easyQuestions[easyQuestions.Count() - 1].Difficulty = int.Parse(diff);
                        easyQuestions[easyQuestions.Count() - 1].correctAnswer = ws.Cells[i + 2, 3].Value.ToString().Trim();
                        easyQuestions[easyQuestions.Count() - 1].Answer2 = ws.Cells[i + 2, 4].Value.ToString().Trim();
                        easyQuestions[easyQuestions.Count() - 1].Answer3 = ws.Cells[i + 2, 5].Value.ToString().Trim();
                        easyQuestions[easyQuestions.Count() - 1].Answer4 = ws.Cells[i + 2, 6].Value.ToString().Trim();
                    }
                }
                for (int i = 0; i < ws.Dimension.End.Row - 1; i++)
                {
                    if (ws.Cells[i + 2, 2].Value.ToString().Trim().Equals("2"))
                    {
                        Question question = new Question();
                        normalQuestions.Add(question);
                        normalQuestions[normalQuestions.Count() - 1].Content = ws.Cells[i + 2, 1].Value.ToString().Trim();
                        string diff = ws.Cells[i + 2, 2].Value.ToString().Trim();
                        normalQuestions[normalQuestions.Count() - 1].Difficulty = int.Parse(diff);
                        normalQuestions[normalQuestions.Count() - 1].correctAnswer = ws.Cells[i + 2, 3].Value.ToString().Trim();
                        normalQuestions[normalQuestions.Count() - 1].Answer2 = ws.Cells[i + 2, 4].Value.ToString().Trim();
                        normalQuestions[normalQuestions.Count() - 1].Answer3 = ws.Cells[i + 2, 5].Value.ToString().Trim();
                        normalQuestions[normalQuestions.Count() - 1].Answer4 = ws.Cells[i + 2, 6].Value.ToString().Trim();
                    }
                }
                for (int i = 0; i < ws.Dimension.End.Row - 1; i++)
                {
                    if (ws.Cells[i + 2, 2].Value.ToString().Trim().Equals("3"))
                    {
                        Question question = new Question();
                        hardQuestions.Add(question);
                        hardQuestions[hardQuestions.Count() - 1].Content = ws.Cells[i + 2, 1].Value.ToString().Trim();
                        string diff = ws.Cells[i + 2, 2].Value.ToString().Trim();
                        hardQuestions[hardQuestions.Count() - 1].Difficulty = int.Parse(diff);
                        hardQuestions[hardQuestions.Count() - 1].correctAnswer = ws.Cells[i + 2, 3].Value.ToString().Trim();
                        hardQuestions[hardQuestions.Count() - 1].Answer2 = ws.Cells[i + 2, 4].Value.ToString().Trim();
                        hardQuestions[hardQuestions.Count() - 1].Answer3 = ws.Cells[i + 2, 5].Value.ToString().Trim();
                        hardQuestions[hardQuestions.Count() - 1].Answer4 = ws.Cells[i + 2, 6].Value.ToString().Trim();
                    }
                }

            }
        }
        List<Question> questions = new List<Question>();
        private List<Question> allQuestions()
        {
            if (level <= 5)
            {
                questions.Clear();
                questions.AddRange(easyQuestions);
            }
            else if (level <= 10 & level > 5)
            {
                questions.Clear();
                questions.AddRange(normalQuestions);
            }
            else if (level > 10)
            {
                questions.Clear();
                questions.AddRange(hardQuestions);
            }
            return questions;
        }
        List<int> randomList = new List<int>();
        private void generateQuestion()
        {
            int rndquest = 0;
            switch (level)
            {
                case 0:
                    cash = 0;
                    break;
                case 1:
                    cash = 100;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level1.png", UriKind.Relative));
                    rndquest = rnd.Next(0, easyQuestions.Count() - 1);
                    letsplay100.Play("100letsplay.mp3");
                    letsplay100.SetVolume(10);
                    question100.Play("100question.mp3");
                    question100.SetVolume(10);
                    break;
                case 2:
                    cash = 200;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level2.png", UriKind.Relative));
                    letsplay100.Play("100letsplay.mp3");
                    letsplay100.SetVolume(10);
                    question100.Stop();
                    question100.Play("100question.mp3");
                    question100.SetVolume(10);
                    break;
                case 3:
                    cash = 300;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level3.png", UriKind.Relative));
                    letsplay100.Play("100letsplay.mp3");
                    letsplay100.SetVolume(10);
                    question100.Stop();
                    question100.Play("100question.mp3");
                    question100.SetVolume(10);
                    break;
                case 4:
                    cash = 500;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level4.png", UriKind.Relative));
                    letsplay100.Play("100letsplay.mp3");
                    letsplay100.SetVolume(10);
                    question100.Stop();
                    question100.Play("100question.mp3");
                    question100.SetVolume(10);
                  
                    break;
                case 5:
                    cash = 1000;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level5.png", UriKind.Relative));
                    rndquest = rnd.Next(0, normalQuestions.Count() - 1);
                    letsplay100.Play("100letsplay.mp3");
                    letsplay100.SetVolume(10);
                    question100.Stop();
                    question100.Play("100question.mp3");
                    question100.SetVolume(10);
                    break;
                case 6:
                    cash = 2000;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level6.png", UriKind.Relative));
                    randomList.Clear();
                    letsplay2000.Play("2000letsplay.mp3");
                    letsplay2000.SetVolume(10);
                    question100.Stop();
                    question2000.Play("2000question.mp3");
                    question2000.SetVolume(10);
                    break;
                case 7:
                    cash = 4000;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level7.png", UriKind.Relative));
                    letsplay4000.Play("4000letsplay.mp3");
                    letsplay4000.SetVolume(10);
                    question2000.Stop();
                    question4000.Play("4000question.mp3");
                    question4000.SetVolume(10);
                    break;
                case 8:
                    cash = 8000;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level8.png", UriKind.Relative));
                    letsplay8000.Play("8000letsplay.mp3");
                    letsplay8000.SetVolume(10);
                    question4000.Stop();
                    question8000.Play("8000question.mp3");
                    question8000.SetVolume(10);
                    break;
                case 9:
                    cash = 16000;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level9.png", UriKind.Relative));
                    letsplay16000.Play("16000letsplay.mp3");
                    letsplay16000.SetVolume(10);
                    question8000.Stop();
                    question16000.Play("16000question.mp3");
                    question16000.SetVolume(10);
                    break;
                case 10:
                    cash = 32000;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level10.png", UriKind.Relative));
                    letsplay16000.Play("16000letsplay.mp3");
                    letsplay16000.SetVolume(10);
                    question16000.Stop();
                    question32000.Play("32000question");
                    question32000.SetVolume(10);
                    break;
                case 11:
                    cash = 64000;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level11.png", UriKind.Relative));
                    rndquest = rnd.Next(0, hardQuestions.Count() - 1);
                    randomList.Clear();
                    letsplay100.Play("100letsplay.mp3");
                    question32000.Stop();
                    question64000.Play("64000question.mp3");
                    question64000.SetVolume(10);
                    break;
                case 12:
                    cash = 125000;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level12.png", UriKind.Relative));
                    letsplay16000.Play("16000letsplay.mp3");
                    letsplay16000.SetVolume(10);
                    question64000.Stop();
                    question125000.Play("125000question.mp3");
                    question125000.SetVolume(10);
                    break;
                case 13:
                    cash = 250000;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level13.png", UriKind.Relative));
                    letsplay16000.Play("16000letsplay.mp3");
                    letsplay16000.SetVolume(10);
                    question125000.Stop();
                    question250000.Play("250000question.mp3");
                    question250000.SetVolume(10);
                    break;
                case 14:
                    cash = 500000;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level14.png", UriKind.Relative));
                    letsplay16000.Play("16000letsplay.mp3");
                    letsplay16000.SetVolume(10);
                    question250000.Stop();
                    question500000.Play("500000question.mp3");
                    question500000.SetVolume(10);
                    break;
                case 15:
                    cash = 1000000;
                    ImageLevel.Source = new BitmapImage(new Uri(@"/img/level15.png", UriKind.Relative));
                    letsplay16000.Play("16000letsplay.mp3");
                    letsplay16000.SetVolume(10);
                    question500000.Stop();
                    question1000000.Play("1000000question.mp3");
                    question1000000.SetVolume(10);
                    break;
                case 16:
                    cash = 1000000;
                    question1000000.Stop();
                    break;
            }
            if (level == 16)
            {
                NavigationService.Navigate(new WonGame(cash));
            }
            //generate               
            allQuestions();
            if (!randomList.Contains(rndquest))
            {
                randomList.Add(rndquest);
                Question.Content = questions[rndquest].Content;
            }
            else
            {
                while (randomList.Contains(rndquest))
                {
                    rndquest = rnd.Next(0, questions.Count() - 1);
                    if (!randomList.Contains(rndquest))
                    {
                        randomList.Add(rndquest);
                        Question.Content = questions[rndquest].Content;
                        break;
                    }
                }
            }
            int answerpos = rnd.Next(1, 5);
            switch (answerpos)
            {
                case 1:
                    Button1.Content = questions[rndquest].correctAnswer;
                    Button2.Content = questions[rndquest].Answer2;
                    Button3.Content = questions[rndquest].Answer3;
                    Button4.Content = questions[rndquest].Answer4;
                    break;
                case 2:
                    Button1.Content = questions[rndquest].Answer4;
                    Button2.Content = questions[rndquest].correctAnswer;
                    Button3.Content = questions[rndquest].Answer2;
                    Button4.Content = questions[rndquest].Answer3;
                    break;
                case 3:
                    Button1.Content = questions[rndquest].Answer3;
                    Button2.Content = questions[rndquest].Answer4;
                    Button3.Content = questions[rndquest].correctAnswer;
                    Button4.Content = questions[rndquest].Answer2;
                    break;
                case 4:
                    Button1.Content = questions[rndquest].Answer2;
                    Button2.Content = questions[rndquest].Answer3;
                    Button3.Content = questions[rndquest].Answer4;
                    Button4.Content = questions[rndquest].correctAnswer;
                    break;
            }
            rightAnswer = questions[rndquest].correctAnswer;
            currentQuestion = questions[rndquest].Content;
            currentAnswer1 = Button1.Content.ToString();
            currentAnswer2 = Button2.Content.ToString();
            currentAnswer3 = Button3.Content.ToString();
            currentAnswer4 = Button4.Content.ToString();
        }
        private void showCorrectAnswer()
        {
            if (Button1.Content.Equals(rightAnswer))
            {
                Button1.Background = Brushes.LightGreen;
            }
            else if (Button2.Content.Equals(rightAnswer))
            {
                Button2.Background = Brushes.LightGreen;
            }
            else if (Button3.Content.Equals(rightAnswer))
            {
                Button3.Background = Brushes.LightGreen;
            }
            else if (Button4.Content.Equals(rightAnswer))
            {
                Button4.Background = Brushes.LightGreen;
            }
        }
        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            Button1.IsEnabled = false;
            Button2.IsEnabled = false;
            Button3.IsEnabled = false;
            Button4.IsEnabled = false;
            Button1.Background = Brushes.Orange;
            canvas.Visibility = Visibility.Hidden;
            askFriendText.Visibility = Visibility.Hidden;
            SkipButton.Visibility = Visibility.Hidden;
            PrbarA.Visibility = Visibility.Hidden;
            PrbarB.Visibility = Visibility.Hidden;
            PrbarC.Visibility = Visibility.Hidden;
            PrbarD.Visibility = Visibility.Hidden;
            askAudienceLabel.Visibility = Visibility.Hidden;
            Leave.IsEnabled = false;
            await Task.Delay(2000);
            if (Button1.Content.Equals(rightAnswer))
            {
                Button1.IsEnabled = true;
                Button2.IsEnabled = true;
                Button3.IsEnabled = true;
                Button4.IsEnabled = true;
                Leave.IsEnabled = true;
                Button1.Background = Brushes.LightGreen;
                level++;
                wrong.Play("correct.mp3");
                wrong.SetVolume(20);
                MessageBox.Show("Congratulations! That's a right answer.", "You have won: " + cash.ToString() + " $");
                generateQuestion();
            }
            else
            {
                Button1.IsEnabled = true;
                Button2.IsEnabled = true;
                Button3.IsEnabled = true;
                Button4.IsEnabled = true;
                showCorrectAnswer();
                Button1.Background = Brushes.Red;
                wrong.Play("wrong.mp3");
                wrong.SetVolume(50);
                MessageBox.Show("Wrong answer, right answer is " + rightAnswer);
                if (level <= 5)
                {
                    cash = 0;
                }
                else if (level <= 10 & level > 5)
                {
                    cash = 1000;
                }
                else if (level > 10)
                {
                    cash = 32000;
                }
                NavigationService.Navigate(new WonGame(cash));
            }
            Button1.ClearValue(Button.BackgroundProperty);
        }

        private async void Button2_Click(object sender, RoutedEventArgs e)
        {
            Button1.IsEnabled = false;
            Button2.IsEnabled = false;
            Button3.IsEnabled = false;
            Button4.IsEnabled = false;
            Leave.IsEnabled = false;
            Button2.Background = Brushes.Orange;
            canvas.Visibility = Visibility.Hidden;
            askFriendText.Visibility = Visibility.Hidden;
            SkipButton.Visibility = Visibility.Hidden;
            PrbarA.Visibility = Visibility.Hidden;
            PrbarB.Visibility = Visibility.Hidden;
            PrbarC.Visibility = Visibility.Hidden;
            PrbarD.Visibility = Visibility.Hidden;
            askAudienceLabel.Visibility = Visibility.Hidden;
            await Task.Delay(2000);
            if (Button2.Content.Equals(rightAnswer))
            {
                Button2.IsEnabled = true;
                Button1.IsEnabled = true;
                Button3.IsEnabled = true;
                Button4.IsEnabled = true;
                Leave.IsEnabled = true;
                Button2.Background = Brushes.LightGreen;
                level++;
                wrong.Play("correct.mp3");
                wrong.SetVolume(20);
                MessageBox.Show("Congratulations! That's a right answer.", "You have won: " + cash.ToString() + " $");
                generateQuestion();
            }
            else
            {
                Button1.IsEnabled = true;
                Button2.IsEnabled = true;
                Button3.IsEnabled = true;
                Button4.IsEnabled = true;
                showCorrectAnswer();
                Button2.Background = Brushes.Red;
                wrong.Play("wrong.mp3");
                wrong.SetVolume(50);
                MessageBox.Show("Wrong answer, right answer is " + rightAnswer);
                if (level <= 5)
                {
                    cash = 0;
                }
                else if (level <= 10 & level > 5)
                {
                    cash = 1000;
                }
                else if (level > 10)
                {
                    cash = 32000;
                }
                NavigationService.Navigate(new WonGame(cash));
            }
            Button2.ClearValue(Button.BackgroundProperty);
        }

        private async void Button3_Click(object sender, RoutedEventArgs e)
        {
            Button1.IsEnabled = false;
            Button2.IsEnabled = false;
            Button3.IsEnabled = false;
            Button4.IsEnabled = false;
            Leave.IsEnabled = false;
            Button3.Background = Brushes.Orange;
            canvas.Visibility = Visibility.Hidden;
            askFriendText.Visibility = Visibility.Hidden;
            SkipButton.Visibility = Visibility.Hidden;
            PrbarA.Visibility = Visibility.Hidden;
            PrbarB.Visibility = Visibility.Hidden;
            PrbarC.Visibility = Visibility.Hidden;
            PrbarD.Visibility = Visibility.Hidden;
            askAudienceLabel.Visibility = Visibility.Hidden;
            await Task.Delay(2000);
            if (Button3.Content.Equals(rightAnswer))
            {
                Button1.IsEnabled = true;
                Button2.IsEnabled = true;
                Button3.IsEnabled = true;
                Button4.IsEnabled = true;
                Leave.IsEnabled = true;
                Button3.Background = Brushes.LightGreen;
                level++;
                wrong.Play("correct.mp3");
                wrong.SetVolume(20);
                MessageBox.Show("Congratulations! That's a right answer.", "You have won: " + cash.ToString() + " $");
                generateQuestion();
            }
            else
            {
                Button1.IsEnabled = true;
                Button2.IsEnabled = true;
                Button3.IsEnabled = true;
                Button4.IsEnabled = true;
                showCorrectAnswer();
                Button3.Background = Brushes.Red;
                wrong.Play("wrong.mp3");
                wrong.SetVolume(50);
                MessageBox.Show("Wrong answer, right answer is " + rightAnswer);
                if (level <= 5)
                {
                    cash = 0;
                }
                else if (level <= 10 & level > 5)
                {
                    cash = 1000;
                }
                else if (level > 10)
                {
                    cash = 32000;
                }
                NavigationService.Navigate(new WonGame(cash));
            }
            Button3.ClearValue(Button.BackgroundProperty);
        }

        private async void Button4_Click(object sender, RoutedEventArgs e)
        {
            Button1.IsEnabled = false;
            Button2.IsEnabled = false;
            Button3.IsEnabled = false;
            Button4.IsEnabled = false;
            Leave.IsEnabled = false;
            Button4.Background = Brushes.Orange;
            canvas.Visibility = Visibility.Hidden;
            askFriendText.Visibility = Visibility.Hidden;
            SkipButton.Visibility = Visibility.Hidden;
            PrbarA.Visibility = Visibility.Hidden;
            PrbarB.Visibility = Visibility.Hidden;
            PrbarC.Visibility = Visibility.Hidden;
            PrbarD.Visibility = Visibility.Hidden;
            askAudienceLabel.Visibility = Visibility.Hidden;
            await Task.Delay(2000);
            if (Button4.Content.Equals(rightAnswer))
            {
                Button1.IsEnabled = true;
                Button2.IsEnabled = true;
                Button3.IsEnabled = true;
                Button4.IsEnabled = true;
                Leave.IsEnabled = true;
                Button4.Background = Brushes.LightGreen;
                level++;
                wrong.Play("correct.mp3");
                wrong.SetVolume(20);
                MessageBox.Show("Congratulations! That's a right answer.", "You have won: " + cash.ToString() + " $");
                generateQuestion();
            }
            else
            {
                Button1.IsEnabled = true;
                Button2.IsEnabled = true;
                Button3.IsEnabled = true;
                Button4.IsEnabled = true;
                showCorrectAnswer();
                Button4.Background = Brushes.Red;
                wrong.Play("wrong.mp3");
                wrong.SetVolume(50);
                MessageBox.Show("Wrong answer, right answer is " + rightAnswer);
                if (level <= 5)
                {
                    cash = 0;
                }
                else if (level <= 10 & level > 5)
                {
                    cash = 1000;
                }
                else if (level > 10)
                {
                    cash = 32000;
                }
                NavigationService.Navigate(new WonGame(cash));
            }
            Button4.ClearValue(Button.BackgroundProperty);
        }
        private void turnOffSounds()
        {
            if (question100.HasAudio())
            {
                question100.Stop();
            }
            else if (question2000.HasAudio())
            {
                question2000.Stop();
            }
            else if (question4000.HasAudio())
            {
                question4000.Stop();
            }
            else if (question8000.HasAudio())
            {
                question8000.Stop();
            }
            else if (question16000.HasAudio())
            {
                question16000.Stop();
            }
            else if (question32000.HasAudio())
            {
                question32000.Stop();
            }
            else if (question64000.HasAudio())
            {
                question64000.Stop();
            }
            else if (question125000.HasAudio())
            {
                question125000.Stop();
            }
            else if (question250000.HasAudio())
            {
                question250000.Stop();
            }
            else if (question500000.HasAudio())
            {
                question500000.Stop();
            }
            else if (question1000000.HasAudio())
            {
                question1000000.Stop();
            }
        }   
        private void LeaveGame(object sender, RoutedEventArgs e)
        {
            level--;
            generateQuestion();
            NavigationService.Navigate(new WonGame(cash));         
        }
        private string correctButton()
        {
            string value = " ";
            if (Button1.Content.Equals(rightAnswer))
            {
                value = "A";
            }
            else if (Button2.Content.Equals(rightAnswer))
            {
                value = "B";
            }
            else if (Button3.Content.Equals(rightAnswer))
            {
                value = "C";
            }
            else if (Button4.Content.Equals(rightAnswer))
            {
                value = "D";
            }
            return value;
        }
        private void Fifty_fifty(object sender, RoutedEventArgs e)
        {
            fiftyImage.Source = new BitmapImage(new Uri(@"/img/5050used.png", UriKind.Relative));
            fiftyButton.IsEnabled = false;
            string correct = correctButton();
            int rndblock = rnd.Next(1, 4);
            switch (correct)
            {
                case "A":
                    if (rndblock == 1)
                    {
                        Button2.IsEnabled = false;
                        Button3.IsEnabled = false;
                    }
                    else if (rndblock == 2)
                    {
                        Button2.IsEnabled = false;
                        Button4.IsEnabled = false;
                    }
                    else if (rndblock == 3)
                    {
                        Button3.IsEnabled = false;
                        Button4.IsEnabled = false;
                    }
                    break;
                case "B":
                    if (rndblock == 1)
                    {
                        Button1.IsEnabled = false;
                        Button3.IsEnabled = false;
                    }
                    else if (rndblock == 2)
                    {
                        Button1.IsEnabled = false;
                        Button4.IsEnabled = false;
                    }
                    else if (rndblock == 3)
                    {
                        Button3.IsEnabled = false;
                        Button4.IsEnabled = false;
                    }
                    break;
                case "C":
                    if (rndblock == 1)
                    {
                        Button1.IsEnabled = false;
                        Button2.IsEnabled = false;
                    }
                    else if (rndblock == 2)
                    {
                        Button1.IsEnabled = false;
                        Button4.IsEnabled = false;
                    }
                    else if (rndblock == 3)
                    {
                        Button2.IsEnabled = false;
                        Button4.IsEnabled = false;
                    }
                    break;
                case "D":
                    if (rndblock == 1)
                    {
                        Button1.IsEnabled = false;
                        Button2.IsEnabled = false;
                    }
                    else if (rndblock == 2)
                    {
                        Button1.IsEnabled = false;
                        Button3.IsEnabled = false;
                    }
                    else if (rndblock == 3)
                    {
                        Button2.IsEnabled = false;
                        Button3.IsEnabled = false;
                    }
                    break;
            }
        }

        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        private void callFriend(object sender, RoutedEventArgs e)
        {
            PrbarA.Visibility = Visibility.Hidden;
            PrbarB.Visibility = Visibility.Hidden;
            PrbarC.Visibility = Visibility.Hidden;
            PrbarD.Visibility = Visibility.Hidden;
            askAudienceLabel.Visibility = Visibility.Hidden;
            canvas.Visibility = Visibility.Visible;
            askFriendText.Visibility = Visibility.Visible;
            SkipButton.Visibility = Visibility.Visible;
            askFriendImage.Source = new BitmapImage(new Uri(@"/img/askFriendused.png", UriKind.Relative));
            askFriendButton.IsEnabled = false;
            askFriend.Play("askFriend.mp3");
            askFriend.SetVolume(10);
            timer.Interval = TimeSpan.FromSeconds(0.05);
            timer.Tick += timer_tick;
            timer.Start();
            timer2.Interval = TimeSpan.FromSeconds(1);
            timer2.Tick += timer_tick2;
            timer2.Start();
        }
        int increments = 0;
        int start = 0;
        private void timer_tick2(object sender, EventArgs e)
        {
            start++;
            if (start <= 10)
            {
                askFriendText.Content = "Dialing the call...";
            }
        }
        string txt;
        private void timer_tick(object sender, EventArgs e)
        {
            string answer ="";
            if(level <= 5)
            {
                answer = "I am pretty sure it's " + rightAnswer;
            }
            else if (level > 5 & level <= 10)
            {
                int rndAns = rnd.Next(0, 2);
                if(rndAns == 0)
                {
                    answer = rightAnswer;
                }
                else
                {
                    int rndWrongAns = rnd.Next(0,4);
                    switch (rndWrongAns)
                    {
                        case 0:
                            if(currentAnswer1 != rightAnswer)
                            {
                                answer = "I think it's " + currentAnswer1;
                            }
                            else
                            {
                                rndWrongAns++;
                            }
                            break;
                        case 1:
                            if (currentAnswer2 != rightAnswer)
                            {
                                answer = "I think it's " + currentAnswer2;
                            }
                            else
                            {
                                rndWrongAns++;
                            }
                            break;
                        case 2:
                            if (currentAnswer3 != rightAnswer)
                            {
                                answer = "I think it's " + currentAnswer3;
                            }
                            else
                            {
                                rndWrongAns++;
                            }
                            break;
                        case 3:
                            if (currentAnswer1 != rightAnswer)
                            {
                                answer = "I think it's " + currentAnswer4;
                            }
                            else
                            {
                                rndWrongAns = 0;
                            }
                            break;
                    }
                }
            }
            else if (level > 10)
            {
            int rndAns = rnd.Next(0, 3);
                if(rndAns == 0)
                {
                    answer = rightAnswer;
                }
                else
                {
                    int rndWrongAns = rnd.Next(0,4);
                    switch (rndWrongAns)
                    {
                        case 0:
                            if(currentAnswer1 != rightAnswer)
                            {
                                answer = "I think it's " + currentAnswer1 + "but I am not sure.";
                            }
                            else
                            {
                                rndWrongAns++;
                            }
                            break;
                        case 1:
                            if (currentAnswer2 != rightAnswer)
                            {
                                answer = "I think it's " + currentAnswer2 + "but I am not sure.";
                            }
                            else
                            {
                                rndWrongAns++;
                            }
                            break;
                        case 2:
                            if (currentAnswer3 != rightAnswer)
                            {
                                answer = "I think it's " + currentAnswer3 + "but I am not sure.";
                            }
                            else
                            {
                                rndWrongAns++;
                            }
                            break;
                        case 3:
                            if (currentAnswer1 != rightAnswer)
                            {
                                answer = "I think it's " + currentAnswer4 + "but I am not sure.";
                            }
                            else
                            {
                                rndWrongAns = 0;
                            }
                            break;
                    }
                }              
            }
            txt = "You: Hi friend. I'm in Who wants to be millionaire TV show and I need your help. @You: The question is " + currentQuestion + "@You: And the possible answers are " + currentAnswer1 + ", " + currentAnswer2 + ", " + currentAnswer3 + " and " + currentAnswer4 + ".@You: What is ur guess. @Friend: " + answer + "@You: OK thank you. Goodbye. @Friend: Good luck. Bye.";
            txt = txt.Replace("@", "\n");
            if (start >= 10)
            {               
                increments++;
                if (increments >= txt.Length)
                {
                    timer.Stop();
                    timer2.Stop();
                    askFriendText.Content = txt.Substring(0, txt.Length);
                    askFriend.Stop();
                    question100.Play("100question.mp3");
                    question100.SetVolume(10);
                }
                else
                {
                    askFriendText.Content = txt.Substring(0, increments);
                }
            }     
        }

        private void Skip(object sender, RoutedEventArgs e)
        {
            start = 45;
            increments = txt.Length;      
        }
        private void askAudience(object sender, RoutedEventArgs e)
        {
            PrbarA.Visibility = Visibility.Visible;
            PrbarB.Visibility = Visibility.Visible;
            PrbarC.Visibility = Visibility.Visible;
            PrbarD.Visibility = Visibility.Visible;
            askAudienceLabel.Visibility = Visibility.Visible;
            canvas.Visibility = Visibility.Visible;
            askFriendText.Visibility = Visibility.Hidden;
            SkipButton.Visibility = Visibility.Hidden;
            askAudienceButton.IsEnabled = false;
            askAudienceImage.Source = new BitmapImage(new Uri(@"/img/askAudienceused.png", UriKind.Relative));

            string correct = correctButton();

            if (level <= 5)
            {
                switch (correct)
                {
                    case "A":
                        PrbarA.Value = 85;
                        PrbarB.Value = 5;
                        PrbarC.Value = 5;
                        PrbarD.Value = 5;
                        break;
                    case "B":
                        PrbarA.Value = 5;
                        PrbarB.Value = 85;
                        PrbarC.Value = 5;
                        PrbarD.Value = 5;
                        break;
                    case "C":
                        PrbarA.Value = 5;
                        PrbarB.Value = 5;
                        PrbarC.Value = 85;
                        PrbarD.Value = 5;
                        break;
                    case "D":
                        PrbarA.Value = 5;
                        PrbarB.Value = 5;
                        PrbarC.Value = 5;
                        PrbarD.Value = 85;
                        break;
                }
                   
            }
            else if (level > 5 & level <= 10)
            {
                switch (correct)
                {
                    case "A":
                        PrbarA.Value = 60;
                        PrbarB.Value = 20;
                        PrbarC.Value = 10;
                        PrbarD.Value = 10;
                        break;
                    case "B":
                        PrbarA.Value = 10;
                        PrbarB.Value = 60;
                        PrbarC.Value = 20;
                        PrbarD.Value = 10;
                        break;
                    case "C":
                        PrbarA.Value = 10;
                        PrbarB.Value = 10;
                        PrbarC.Value = 60;
                        PrbarD.Value = 20;
                        break;
                    case "D":
                        PrbarA.Value = 20;
                        PrbarB.Value = 10;
                        PrbarC.Value = 10;
                        PrbarD.Value = 60;
                        break;
                }
            }
            else if (level > 10)
            {
                switch (correct)
                {
                    case "A":
                        PrbarA.Value = 50;
                        PrbarB.Value = 50;
                        PrbarC.Value = 0;
                        PrbarD.Value = 0;
                        break;
                    case "B":
                        PrbarA.Value = 0;
                        PrbarB.Value = 50;
                        PrbarC.Value = 50;
                        PrbarD.Value = 0;
                        break;
                    case "C":
                        PrbarA.Value = 0;
                        PrbarB.Value = 0;
                        PrbarC.Value = 50;
                        PrbarD.Value = 50;
                        break;
                    case "D":
                        PrbarA.Value = 50;
                        PrbarB.Value = 0;
                        PrbarC.Value = 0;
                        PrbarD.Value = 50;
                        break;
                }
            }
        }
    }
}
