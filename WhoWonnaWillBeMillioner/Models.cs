using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WhoWonnaWillBeMillioner
{
    [Serializable]
    public class GameModel
    {

        public GameModel()
        {
            Lvls = new List<int>();
            Questions = new ListCategory();
        }
        public List<int> Lvls {
            get;
            set;
        }
        public ListCategory Questions { get; set; }

        public List<Question> GenerateRandlist()
        {
            var qList = new List<Question>();

            foreach (var item in Lvls)
            {
                qList.Add(Questions.GetRandQuestion(item));
            }


            return qList;
        }


        static public GameModel DeSerealize()
        {
            FileStream fs = new FileStream("DataFile.dat", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                return (GameModel)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                MessageBox.Show("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public void Serealize()
        {
            FileStream fs = new FileStream("DataFile.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, this);
            }
            catch (SerializationException e)
            {
                MessageBox.Show("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public void AddLvl(int lvl)
        {
            Lvls.Add(lvl);
            Questions.listQuests.Add(lvl, new List<Question>());
        }

    }

    [Serializable]
    public class ListCategory
    {
        public ListCategory()
        {
            listQuests = new Dictionary<int, List<Question>>();
        }

        public Dictionary<int, List<Question>> listQuests { get; set; }
        public List<Question> this[int category]{
            get
            {
                return listQuests[category];
            }

            set
            {

            }
            
        }

        public Question GetRandQuestion(int category)
        {
            var count = this[category].Count;
            if (count == 1)
                return this[category][0];

            Random random = new Random();

            return this[category][random.Next(0, count)];
        }
    }

    [Serializable]
    public class Question
    {
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }

        public string Quest { get; set; }

        public ResponceType ValidResponce { get; set; }
    }

    [Serializable]
    public enum ResponceType
    {
        A, B, C, D
    }
}
