using System;
using System.Collections.Generic;
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

namespace WhoWonnaWillBeMillioner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public enum ResponceType
    {
        A, B, C, D
    }

    public class Question
    {
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }

        public string Quest { get; set; }

        public ResponceType ValidResponce { get; set; }
    }

    class Game : IMilionerPresenter
    {
        List<Question> qs = new List<Question>()
        {
            new Question(){
                Quest = "Первый вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Второй вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Третий вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Четвертый вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Пятый вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Шестой вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Седьмой вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Восьмой вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Девятый вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Десятый вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Одинадцатый вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Двенадцатый вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Тринадцатый вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            },
            new Question(){
                Quest = "Четырнадцатый вопрос?",
                A = "Да, определенно!",
                B = "Нет, сто проц!",
                C = "Мейби!",
                D = "Хз",
                ValidResponce = ResponceType.A
            }

        };
        int currentQuest = 0;
        List<int> moneyLvl = new List<int>(){
            100,200,500,1000,2000,5000,10000,25000,50000,100000,200000,500000,1000000
        };
        int StonePrice = 0;
        MainWindow _view;
        public Game(MainWindow view)
        {
            _view = view;
            view.A.Click += A_Click;
            view.B.Click += B_Click;
            view.C.Click += C_Click;
            view.D.Click += D_Click;
            view.CallFriend.Click += CallFriend_Click;
            view.GetPrice.Click += GetPrice_Click;
            view.Fifty.Click += Fifty_Click;
            view.HollHelp.Click += HollHelp_Click;
            view.SetQuest(qs[currentQuest]);
        }

        private bool IsValid(ResponceType responceType)
        {
            if (qs[currentQuest].ValidResponce != responceType)
            {
                MessageBox.Show("Вы проиграли!");
                _view.Close();
                return false;
            }
            else
            {
                if (moneyLvl[currentQuest] == 1000000)
                {
                    MessageBox.Show("Поздравляем!!!!!! Вы выйграли миллион!! Теперь вы миллионер!!!");
                    _view.Close();
                }
                else
                {
                    MessageBox.Show("Это правильный ответ! Поздравляю, вы выйграли " + moneyLvl[currentQuest] + " денег!");

                    if (moneyLvl[currentQuest] == 2000)
                    {
                        StonePrice = 2000;
                        MessageBox.Show("Вы достигли первой несгораемой суммы - " + StonePrice.ToString());
                    }

                    if (moneyLvl[currentQuest] == 50000)
                    {
                        StonePrice = 50000;
                        MessageBox.Show("Вы достигли второй несгораемой суммы - " + StonePrice.ToString());
                    }

                        currentQuest++;
                    _view.SetQuest(qs[currentQuest]);
                    if (!_view.A.IsEnabled) _view.A.IsEnabled = true;
                    if (!_view.B.IsEnabled) _view.B.IsEnabled = true;
                    if (!_view.C.IsEnabled) _view.C.IsEnabled = true;
                    if (!_view.D.IsEnabled) _view.D.IsEnabled = true;

                }
                return true;
            }
        }

        public void A_Click(object sender, RoutedEventArgs e)
        {
            IsValid(ResponceType.A);
        }

        public void B_Click(object sender, RoutedEventArgs e)
        {
            IsValid(ResponceType.B);
        }

        public void C_Click(object sender, RoutedEventArgs e)
        {
            IsValid(ResponceType.C);
        }

        public void D_Click(object sender, RoutedEventArgs e)
        {
            IsValid(ResponceType.D);
        }

        public void Fifty_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            Random rand = new Random();
            ResponceType rnd = (ResponceType)rand.Next(0,4);
            while(rnd == qs[currentQuest].ValidResponce)
            {
                rnd = (ResponceType)rand.Next(0, 4);
            }

            _view.A.IsEnabled = false;
            _view.B.IsEnabled = false;
            _view.C.IsEnabled = false;
            _view.D.IsEnabled = false;
            DisableAllButtonNot(qs[currentQuest].ValidResponce);
            DisableAllButtonNot(rnd);



        }

        void DisableAllButtonNot(ResponceType type)
        {
            switch (type)
            {
                case ResponceType.A:
                    _view.A.IsEnabled = true;
                    break;
                case ResponceType.B:
                    _view.B.IsEnabled = true;
                    break;
                case ResponceType.C:
                    _view.C.IsEnabled = true;
                    break;
                case ResponceType.D:
                    _view.D.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }

        public void HollHelp_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            Random rand = new Random();
            int result = rand.Next(0, 100);
            if (result < 80)
            {
                MessageBox.Show("Зрители думают, что правильный ответ - " + qs[currentQuest].ValidResponce.ToString(), "Ответ зала");
            }
            else
            {
                ResponceType rnd = (ResponceType)rand.Next(0, 4);
                while (rnd == qs[currentQuest].ValidResponce)
                {
                    rnd = (ResponceType)rand.Next(0, 4);
                }
                MessageBox.Show("Зрители думают, что правильный ответ - " + rnd.ToString(), "Ответ зала");

            }
        }

        public void CallFriend_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            Random rand = new Random();
            int result = rand.Next(0, 100);
            if (result < 80)
            {
                MessageBox.Show("Я думаю, что правильный ответ - " + qs[currentQuest].ValidResponce.ToString(), "Ответ Коляна" + result.ToString());
            }
            else
            {
                ResponceType rnd = (ResponceType)rand.Next(0, 4);
                while (rnd == qs[currentQuest].ValidResponce)
                {
                    rnd = (ResponceType)rand.Next(0, 4);
                }
                MessageBox.Show("Я думаю, что правильный ответ - " + rnd.ToString(), "Ответ Коляна" + result.ToString());

            }
        }

        public void GetPrice_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы уверены, что хотите забрать незгораемую сумму, в размере " + StonePrice.ToString() + " ?", "Забрать деньги", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.OK)
            {
                MessageBox.Show("Поздравляем вас с вашим выйграшем!", "Поздравляем");
                _view.Close();
            }
        }
    }

    public interface IMilionerPresenter
    {
        void A_Click(object sender, RoutedEventArgs e);

        void C_Click(object sender, RoutedEventArgs e);

        void B_Click(object sender, RoutedEventArgs e);

        void D_Click(object sender, RoutedEventArgs e);

        void Fifty_Click(object sender, RoutedEventArgs e);

        void HollHelp_Click(object sender, RoutedEventArgs e);

        void CallFriend_Click(object sender, RoutedEventArgs e);
        void GetPrice_Click(object sender, RoutedEventArgs e);

    }


    public partial class MainWindow : Window
    {
        IMilionerPresenter pres;
        Question currentQuestion = null;
        public MainWindow()
        {
            InitializeComponent();
            pres = new Game(this);
        }

        public void SetQuest(Question question)
        {
            currentQuestion = question;
            A.Content = question.A;
            B.Content = question.B;
            C.Content = question.C;
            D.Content = question.D;
            Question.Content = question.Quest;
        }
    }
}
