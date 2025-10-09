using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Project
{
    internal class Pet
    {
        private const int MAX_AGE = 8;
        private Emotion emo;
        private int level;
        private DateTime birth;

        Pet()
        {
            this.emo = 0;
            this.level = 1;
            this.birth = DateTime.Now;
        }

        private enum Emotion
        {
            simple,
            sick,
            tired,
            sleepy,
            hungry,
            happy,
            dead
        }
        private void Age()
        {
        }
        
        public int Emotions(Pet pet)
        {
            int sick_time;
            int tired_time;
            int hungry_time;
            int sleepy_time;


        }
    }
}
