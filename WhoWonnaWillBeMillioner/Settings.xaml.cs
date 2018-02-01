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
using System.Windows.Shapes;

namespace WhoWonnaWillBeMillioner
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    /// 


    public class FormQuestion
    {
        public Button Save { get; set; }
        public TextBox QuestionInput { get; set; }
        public TextBox AInput { get; set; }
        public TextBox BInput { get; set; }
        public TextBox CInput { get; set; }
        public TextBox DInput { get; set; }
        public ComboBox ComboBox { get; set; }
        public int Category { get; set;}
        public int Index { get; set; }
        public bool IsChanged { get; set; }

    }

    public interface ISettingsWindow
    {
        void Start();

        void CreateNewQuestion(int category);

        void DeleteQuestion(int category, int index);

        void SaveQuestion(int category, int index);
    }


    public partial class Settings : Window, ISettingsWindow
    {
        GameModel _gameModel;
        ISettingController _controller;

        Dictionary<int, Dictionary<int, FormQuestion>> TreeView;
        FormQuestion newFormQuestion;
        public Settings(GameModel gameModel, ISettingController controller)
        {
            InitializeComponent();
            _gameModel = gameModel;
            _controller = controller;
            TreeView = new Dictionary<int, Dictionary<int, FormQuestion>>();

            foreach (var lvl in _gameModel.Lvls)
            {
                var exp = new Expander();
                exp.Header = lvl.ToString();
                var qStack = new StackPanel();
                TreeView.Add(lvl, new Dictionary<int, FormQuestion>());
                int index = 0;
                foreach (var q in _gameModel.Questions[lvl])
                {
                    qStack.Children.Add(CreateQuestForm(q));
                    newFormQuestion.Category = lvl;
                    newFormQuestion.Index = index;
                    newFormQuestion.IsChanged = false;
                    TreeView[lvl].Add(index++, newFormQuestion);
                }

                var stack = new StackPanel();
                exp.Content = stack;
                stack.Children.Add(qStack);
                var btn = new Button();
                btn.Content = "Создать новый вопрос";
                btn.Margin = new Thickness(0, 5, 0, 5);
                btn.MaxWidth = 200;
                btn.Click += CreateNewQuestion_handle;
                stack.Children.Add(btn);
                MainStack.Children.Add(exp);

            }

        }

        private Border CreateQuestForm(Question q)
        {
            newFormQuestion = new FormQuestion();
            var border = new Border();
            border.Padding = new Thickness(10, 10, 10, 10);
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = Brushes.Black;
            
            var grid = new Grid();
            border.Child = grid;

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            var dockPanel = new DockPanel();
            Grid.SetColumn(dockPanel, 0);
            Grid.SetRow(dockPanel, 0);
            Grid.SetColumnSpan(dockPanel, 2);
            var qText = new TextBlock();
            qText.Text = "Вопрос:";
            dockPanel.Children.Add(qText);
            var qTextBox = new TextBox();
            qTextBox.Margin = new Thickness(10, 0, 0, 0);
            qTextBox.Text = q.Quest;
            dockPanel.Children.Add(qTextBox);
            grid.Children.Add(dockPanel);
            dockPanel.Margin = new Thickness(0, 0, 0, 10);
            newFormQuestion.QuestionInput = qTextBox;
            qTextBox.TextChanged += TextBox_TextChanged;

            dockPanel = new DockPanel();
            Grid.SetColumn(dockPanel, 0);
            Grid.SetRow(dockPanel, 1);
            qText = new TextBlock();
            qText.Text = "A:";
            
            dockPanel.Children.Add(qText);
            qTextBox = new TextBox();
            qTextBox.Margin = new Thickness(10, 0, 0, 10);
            qTextBox.Text = q.A;
            dockPanel.Children.Add(qTextBox);
            grid.Children.Add(dockPanel);
            newFormQuestion.AInput = qTextBox;
            qTextBox.TextChanged += TextBox_TextChanged;

            dockPanel = new DockPanel();
            Grid.SetColumn(dockPanel, 1);
            Grid.SetRow(dockPanel, 1);
            qText = new TextBlock();
            qText.Text = "B:";
            qText.Margin = new Thickness(10, 0, 0, 0);
            dockPanel.Children.Add(qText);
            qTextBox = new TextBox();
            qTextBox.Margin = new Thickness(10, 0, 0, 10);
            qTextBox.Text = q.B;
            dockPanel.Children.Add(qTextBox);
            grid.Children.Add(dockPanel);
            newFormQuestion.BInput = qTextBox;
            qTextBox.TextChanged += TextBox_TextChanged;

            dockPanel = new DockPanel();
            Grid.SetColumn(dockPanel, 0);
            Grid.SetRow(dockPanel, 2);
            qText = new TextBlock();
            qText.Text = "C:";
            dockPanel.Children.Add(qText);
            qTextBox = new TextBox();
            qTextBox.Margin = new Thickness(10, 0, 0, 0);
            qTextBox.Text = q.C;
            dockPanel.Children.Add(qTextBox);
            grid.Children.Add(dockPanel);
            newFormQuestion.CInput = qTextBox;
            qTextBox.TextChanged += TextBox_TextChanged;

            dockPanel = new DockPanel();
            Grid.SetColumn(dockPanel, 1);
            Grid.SetRow(dockPanel, 2);
            qText = new TextBlock();
            qText.Text = "D:";
            dockPanel.Children.Add(qText);
            qTextBox = new TextBox();
            qTextBox.Margin = new Thickness(10, 0, 0, 0);
            qTextBox.Text = q.D;
            qText.Margin = new Thickness(10, 0, 0, 0);
            dockPanel.Children.Add(qTextBox);
            grid.Children.Add(dockPanel);
            newFormQuestion.DInput = qTextBox;
            qTextBox.TextChanged += TextBox_TextChanged;

            dockPanel = new DockPanel();
            Grid.SetColumn(dockPanel, 0);
            Grid.SetRow(dockPanel, 3);
            Grid.SetColumnSpan(dockPanel, 2);
            qText = new TextBlock();
            qText.Text = "Ответ:";
            dockPanel.Children.Add(qText);
            qText.Margin = new Thickness(10, 0, 0, 0);
            var qComboBox = new ComboBox();
            qComboBox.Margin = new Thickness(10, 0, 0, 0);
            foreach (var item in Enum.GetNames(new ResponceType().GetType()))
            {
                qComboBox.Items.Add(item);
            }
            qComboBox.SelectedIndex = Enum.GetNames(new ResponceType().GetType()).ToList().IndexOf(q.ValidResponce.ToString());
            dockPanel.Margin = new Thickness(0, 10, 0, 0);
            dockPanel.Children.Add(qComboBox);
            grid.Children.Add(dockPanel);
            newFormQuestion.ComboBox = qComboBox;
            qComboBox.SelectionChanged += ComboBox_SelectionChanged;

            var buttonStack = new StackPanel();
            buttonStack.Orientation = Orientation.Horizontal;
            buttonStack.HorizontalAlignment = HorizontalAlignment.Right;
            grid.Children.Add(buttonStack);
            Grid.SetColumn(buttonStack, 1);
            Grid.SetRow(buttonStack, 4);

            var button = new Button();
            button.Content = "Удалить";
            button.Margin = new Thickness(0, 10, 3, 0);
            button.Width = 100;
            button.Click += DeleteQuestion_handle;
            buttonStack.Children.Add(button);
            

            button = new Button();
            button.Content = "Сохранить";
            newFormQuestion.Save = button;
            button.Margin = new Thickness(3, 10, 0, 0);
            button.Width = 100;
            button.Click += SaveQuestion_handle;
            button.Visibility = Visibility.Hidden;
            buttonStack.Children.Add(button);


            border.Margin = new Thickness(2, 5, 2, 5);

            return border;
        }

        private FormQuestion GetFormQuestionBySaveButton(Button sender)
        {
            foreach (var pare in TreeView)
            {
                foreach (var item in pare.Value)
                {
                    if (item.Value.Save == sender)
                    {
                        return item.Value;
                    }
                }
            }
            throw new Exception("FormQuestion is not found"); 
        }

        private void SaveQuestion_handle(object sender, RoutedEventArgs e)
        {
            FormQuestion form = GetFormQuestionBySaveButton((Button)sender);


            _controller.SaveQuestion(form.Category, form.Index, new Question()
            {
                A = form.AInput.Text,
                B = form.BInput.Text,
                C = form.CInput.Text,
                D = form.DInput.Text,
                Quest = form.QuestionInput.Text,
                ValidResponce = (ResponceType)Enum.Parse(new ResponceType().GetType(), form.ComboBox.Text)
            });



        }

        private void DeleteQuestion_handle(object sender, RoutedEventArgs e)
        {
            Button sendr = (Button)sender;
            Grid g = (Grid)((StackPanel)sendr.Parent).Parent;
            var stackPanel = ((StackPanel)((StackPanel)((Border)g.Parent).Parent).Parent);
            var xpander = (Expander)stackPanel.Parent;
            stackPanel = (StackPanel)stackPanel.Children[0];
            _controller.DeleteQuestion(int.Parse((string)xpander.Header), stackPanel.Children.IndexOf((Border)g.Parent));
        }

        private void CreateNewQuestion_handle(object sender, RoutedEventArgs e)
        {
            Button sendr = (Button)sender;
            StackPanel g = (StackPanel)sendr.Parent;
            var xpander = (Expander)g.Parent;
            _controller.CreateNewQuestion(int.Parse((string)xpander.Header));

        }

        public void Start()
        {
            this.ShowDialog();
        }

        private Expander GetExpanderByCategory(int category)
        {
            int counter = 0;
            foreach (var item in MainStack.Children)
            {
                if (item is Expander && (string)((Expander)item).Header == category.ToString())
                {
                    
                    return (Expander)item;
                }
                counter++;
            }
            throw new Exception("Expander not found");
        }

        private Border GetQuestForm(int category, int index)
        {
            return (Border)((StackPanel)((StackPanel)(GetExpanderByCategory(category)).Content).Children[0]).Children[index];
        }

        public void CreateNewQuestion(int category)
        {
            var newForm = CreateQuestForm(new Question());
            newFormQuestion.Category = category;
            newFormQuestion.Index = TreeView[category].Count - 1;
            TreeView[category].Add(TreeView[category].Count, newFormQuestion);
            newFormQuestion.Save.Visibility = Visibility.Visible;
            ((StackPanel)((StackPanel)(GetExpanderByCategory(category)).Content).Children[0]).Children.Add(newForm);
            newFormQuestion.IsChanged = false;
        }

        public void DeleteQuestion(int category, int index)
        {
            ((StackPanel)((StackPanel)(GetExpanderByCategory(category))
                .Content)
                .Children[0])
                .Children
                .Remove(GetQuestForm(category,index));
        }

        public void SaveQuestion(int category, int index)
        {
            TreeView[category][index].Save.Visibility = Visibility.Hidden;
            MessageBox.Show(TreeView[category][index].QuestionInput.Text);
            MessageBox.Show("Вопрос успешно сохранен!");
            TreeView[category][index].IsChanged = false;
        }

        private FormQuestion GetFormQuestionByTextBox(TextBox sender)
        {
            foreach (var pare in TreeView)
            {
                foreach (var item in pare.Value)
                {
                    if (item.Value.AInput == sender ||
                        item.Value.BInput == sender ||
                        item.Value.CInput == sender ||
                        item.Value.DInput == sender ||
                        item.Value.QuestionInput == sender)
                    {
                        return item.Value;
                    }
                }
            }
            throw new Exception("FormQuestion is not found");
        }

        private FormQuestion GetFormQuestionByComboBox(ComboBox sender)
        {
            foreach (var pare in TreeView)
            {
                foreach (var item in pare.Value)
                {
                    if (item.Value.ComboBox == sender)
                    {
                        return item.Value;
                    }
                }
            }
            throw new Exception("FormQuestion is not found");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var form = GetFormQuestionByTextBox(textBox);
            if (form.IsChanged == false)
            {
                form.IsChanged = true;
                form.Save.Visibility = Visibility.Visible;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var textBox = (ComboBox)sender;
            var form = GetFormQuestionByComboBox(textBox);
            if (form.IsChanged == false)
            {
                form.IsChanged = true;
                form.Save.Visibility = Visibility.Visible;
            }
        }
    }
}

