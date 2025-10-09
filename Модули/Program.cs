using System;

namespace Модули
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student();
            Lesson lesson = new Lesson();

            student.InsertStudent();
            lesson.InsertLesson();
            student.ShowStudent();
            lesson.ShowLesson();
        }
    }
}
