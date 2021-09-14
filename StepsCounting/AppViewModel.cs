using Newtonsoft.Json;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StepsCounting
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private User selectedUser;
        public static ObservableCollection<UserData> UsersData { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<User> MonthlyData { get; set; }
        public Dictionary<string, List<int>> KeyValuePairs {get; set;}
        public List<Graph> Graphs { get; set; }
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
        private Graph selectedUserGraphs;
        public Graph SelectedUserGraphs
        {
            get { return selectedUserGraphs; }
            set
            {
                selectedUserGraphs = value;
                OnPropertyChanged("SelectedUserGraphs");
            }
        }
        public AppViewModel()
        {

            string[] files = new string[]
            {
                @"json\day1.json", @"json\day2.json",@"json\day3.json", @"json\day4.json", @"json\day5.json",@"json\day6.json", @"json\day7.json", @"json\day8.json", @"json\day9.json", @"json\day10.json", @"json\day11.json",@"json\day12.json", @"json\day13.json", @"json\day14.json",@"json\day15.json", @"json\day16.json", @"json\day17.json", @"json\day18.json", @"json\day19.json", @"json\day20.json",@"json\day21.json",@"json\day22.json",@"json\day23.json",@"json\day24.json",@"json\day25.json", @"json\day26.json",@"json\day27.json", @"json\day28.json", @"json\day29.json", @"json\day30.json",
            };

            ObservableCollection<ObservableCollection<UserData>> data = new ObservableCollection<ObservableCollection<UserData>>();

            foreach (string file in files)
            {
                data.Add(JsonConvert.DeserializeObject<ObservableCollection<UserData>>(File.ReadAllText(file)));
            }

            int i = 0;

            UsersData = new ObservableCollection<UserData>();

            while (i <= files.Length - 1)
            {
                for (int j = 0; j < data[i].Count; j++)
                {
                    UserData user = new UserData() { User = data[i][j].User, Rank = data[i][j].Rank, Status = data[i][j].Status, Steps = data[i][j].Steps, Day = i };
                    UsersData.Add(user);
                }

                i++;
            }

            Users = new ObservableCollection<User>();
            Point point = new Point();

            for (int x = 0; x < UsersData.Count; x++)
            {
                var user1 = UsersData.Where(m => m.User == UsersData[x].User).ToList();
                var UserName = user1[0].User;
                var averageSteps = Convert.ToInt32(user1.Average(m => m.Steps));
                var maxSteps = user1.Max(m => m.Steps);
                var minSteps = user1.Min(m => m.Steps);
                point.X = UsersData[x].Day;
                point.Y = UsersData[x].Steps;

                Users.Add(new User { Name = UserName, AverageSteps = averageSteps, MaxSteps = maxSteps, MinSteps = minSteps, Points = point });
            }


            var res = Users.GroupBy(m => m.Name).Select(x => x.First()).ToList();
            MonthlyData = new ObservableCollection<User>();

            for (int b = 0; b < res.Count; b++)
            {
                MonthlyData.Add(new User { Name = res[b].Name, AverageSteps = res[b].AverageSteps, MaxSteps = res[b].MaxSteps, MinSteps = res[b].MinSteps });
            }


            Graphs = new List<Graph>();

            for (int f = 0; f < Users.Count; f++)
            {
                Graphs.Add(new Graph { Day = Users[f].Points.X, Steps = Users[f].Points.Y, User = Users[f].Name });
            }


            KeyValuePairs = new Dictionary<string, List<int>>(); 
            
            var graphsList = Graphs.OrderBy(m => m.User).ToList();

            for (int r = 0; r < MonthlyData.Count; r++)
            {
                string userName = MonthlyData[r].Name;
                var userData = graphsList.FindAll(m => m.User == userName);
                List<int> pointList = new List<int>();
                pointList.Clear();

                for (int s = 0; s < userData.Count; s++)
                {
                    int stepsDay = userData[s].Steps;
                    string name = userData[s].User;
                    pointList.Add(stepsDay);
                }

                KeyValuePairs.Add(userName, pointList); //данная библиотека была создана для отрисовки графика. Руководствовалась следующей логикой - ключ (имя пользователя) хотелось бы привязать к выбранному пользователю на стакпанели, а из значений (список шагов за месяц) создать график. К сожалению, реализовать это не удалось.
            }
        }

        public static void SaveUserData(User user)
        {
            ObservableCollection<UserDataSave> UsersDataSave = new ObservableCollection<UserDataSave>();
            var userData = UsersData.Where(m => m.User == user.Name).ToList();

            UserDataSave data = new UserDataSave { User = user.Name, AverageSteps = user.AverageSteps, MaxSteps = user.MaxSteps, MinSteps = user.MinSteps, Rank = userData[userData.Count - 1].Rank, Status = userData[userData.Count - 1].Status };

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            string path = @"C:\";
            string subpath = @"jsonFiles";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(subpath);


            File.WriteAllText($@"C:\jsonFiles\{user.Name}.json", json);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
