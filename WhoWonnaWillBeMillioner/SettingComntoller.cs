using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WhoWonnaWillBeMillioner
{
    public interface ISettingController
    {
        void DeleteQuestion(int category, int index);
        void CreateNewQuestion(int category);
        void SaveQuestion(int category, int index, Question q);
    }

    public class SettingContoller: ISettingController
    {
        GameModel gameModel;
        ISettingsWindow view;
        IList<int> changeCategody;
        public SettingContoller(GameModel gm)
        {
            gameModel = gm;
            view = new Settings(gm,this);
            changeCategody = new List<int>();
        }

        public void Start()
        {
            view.Start();
        }

        public void DeleteQuestion(int category, int index)
        {
            if (gameModel.Questions[category].Count == 1)
            {
                MessageBox.Show("Вы не можете удалить этот вопрос!\n В категории должен быть хотя бы один вопрос");
            }
            else
            {
                if (index == gameModel.Questions[category].Count - 1 && changeCategody.IndexOf(category) != -1)
                {
                    changeCategody.Remove(category);
                }
                gameModel.Questions[category].RemoveAt(index);
                view.DeleteQuestion(category, index);
            }
        }

        public void CreateNewQuestion(int category)
        {
            if(changeCategody.IndexOf(category) == -1)
            {

                changeCategody.Add(category);
                view.CreateNewQuestion(category);
                gameModel.Questions[category].Add(new Question());
            }
            else
            {
                MessageBox.Show("Сначала завершите работу над новым элементом!");
            }
        }

        public void SaveQuestion(int category, int index, Question q)
        {
            if (q.Quest == "")
            {
                MessageBox.Show("Введите текст вопроса!");
                return;
            }
            if (q.A == "") { 
                MessageBox.Show("Введите текст ответа А!");
                return;
            }
            if (q.B == "")
            {
                MessageBox.Show("Введите текст вопроса B!");
                return;
            }
            if (q.C == "")
            {    
                MessageBox.Show("Введите текст вопроса C!");
                return;
            }
            if (q.D == "")
            {
                MessageBox.Show("Введите текст вопроса D!");
                return;
            }

            gameModel.Questions[category][index] = q;
            gameModel.Serealize();
            view.SaveQuestion(category, gameModel.Questions[category].IndexOf(q));
            

        }

    }
}
