using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StepsCounting
{
   public class User : INotifyPropertyChanged
    {
        private string name;
        private int averageSteps;
        private int maxSteps;
        private int minSteps;
        private Point points;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int AverageSteps
        {
            get { return averageSteps; }
            set
            {
                if(averageSteps != value)
                {
                    averageSteps = value;
                    OnPropertyChanged(nameof(AverageSteps));
                    OnPropertyChanged(nameof(NameBrush));

                }
            }
        }
        public int MaxSteps
        {
            get { return maxSteps; }
            set
            {
                maxSteps = value;
                OnPropertyChanged("MaxSteps");
            }
        }
        public int MinSteps
        {
            get { return minSteps; }
            set
            {
                minSteps = value;
                OnPropertyChanged("MinSteps");
            }
        }
        public Point Points
        {
            get { return points; }
            set
            {
                points = value;
                OnPropertyChanged("Points");
            }
        }

        public Brush NameBrush
        {
            get
            {
                bool input = averageSteps >= minSteps;
                switch (input)
                {
                    case true:
                        return Brushes.LightGreen;
                    default:
                        break;
                }

                return Brushes.Transparent;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
       
    }
}
