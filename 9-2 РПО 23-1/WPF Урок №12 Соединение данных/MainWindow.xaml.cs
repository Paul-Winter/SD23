using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Урок__12_Соединение_данных
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Student(textBoxInfo.Text);
            listBox.Items.Add(((Student)DataContext).ToString());
        }
    }

    internal sealed class TodayExtension : MarkupExtension
    {
        private readonly int shift;
        public bool UpperCase { get; set; }

        public TodayExtension() : this(0) {}

        public TodayExtension(int shift)
        {
            this.shift = shift;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            string dayOfWeek = DateTime.Now.AddDays(shift).DayOfWeek.ToString();

            if (UpperCase)
            {
                dayOfWeek = dayOfWeek.ToUpper();
            }

            return dayOfWeek;
        }
    }
    internal sealed class KopilcaExtension : MarkupExtension
    {
        int a ;
        int b ;
        int i ;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {

           if( i == 0 )
            {
                return "а упала, а б - пропала ";

            }


           return ((a * b) / i).ToString(); 


                
        }

        public KopilcaExtension(int a , int b, int i )
        {

            this.a = a;
            this.b = b;
            this.i = i;

        }
    }
    internal sealed class DateTimeExtension : MarkupExtension
    { 
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            string date = DateTime.Now.ToLongDateString();
            string time = DateTime.Now.ToLongTimeString();

            return date + " " + time;
        }
    }
    internal sealed class StudentExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Student student = new Student("Чавычалов Владислав Сергеевич",17,2,"IT TOP COLLEGE",1231231);
            return student.ToString();
        }
    }
    internal sealed class TimeToDateExtension : MarkupExtension
    {
        private DateTime dateTime;

        public DateTime DateTime { get => dateTime; set => dateTime = value; }

        public TimeToDateExtension(DateTime dateTime) 
        {
            this.DateTime = dateTime;
        }

        public TimeToDateExtension() : this(new DateTime(2025,06,27)) { }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            TimeSpan timeSpan = DateTime - DateTime.Now;
            if (timeSpan.TotalHours > 0)
            {
                return $"Осталось часов: {timeSpan.TotalHours}";
            }
            else
            {
                return $"Дата уже прошла";
            }            
        }
    }

    internal sealed class College
    {
        private Student[] group = new Student[]
        {
            new Student("Alexandrova"),
            new Student("Antonov"),
            new Student("Dukhina"),
            new Student("Zemlyanskiy"),
            new Student("Zolin"),
            new Student("Krasitskiy"),
            new Student("Kubanov"),
            new Student("Lushnikov"),
            new Student("Mamontova"),
            new Student("Metelitcin"),
            new Student("Chavyvhalov"),
            new Student("Yunusov")
        };

        public Student[] Group { get => group; set => group = value; }
    }
    internal sealed class Student
    {
        private string name;
        private int years;
        private int kurs;
        private string mestoUch;
        private int stud;

        public string Name { get => name; set => this.name = value; }
        public int Years { get => years; set => years = value; }
        public int Kurs { get => kurs; set => kurs = value; }
        public string MestoUch { get => mestoUch; set => mestoUch = value; }
        public int Stud { get => stud; set => stud = value; }

        public Student() : this("unnamed", 0, 0, "unknown", 0) {}
        public Student(string name) : this(name, 0, 0, "unknown", 0) {}
        public Student(string name, int years, int kurs, string mestoUch, int stud)
        {
            this.name = name;
            this.years = years; 
            this.kurs = kurs;   
            this.mestoUch = mestoUch;
            this.stud = stud;
        }

        public override string ToString()
        {
            return $"{name}, {years}, {kurs}, {mestoUch}, {stud}";
        }
    }
}
