using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiS.Models
{
    public static class Comparers
    {
        public static bool Equals(StudyProgramme a, StudyProgramme b)
        {
            if (a != null && b != null)
            {
                return a.Equals(b);
            }
            return a == b;
        }
        public static bool Equals(Subject a, Subject b)
        {
            if (a != null && b != null)
            {
                return a.Equals(b);
            }
            return a == b;
        }
        public static bool Equals(Teacher a, Teacher b)
        {
            if (a != null && b != null)
            {
                return a.Equals(b);
            }
            return a == b;
        }
        public static bool Equals(Student a, Student b)
        {
            if (a != null && b != null)
            {
                return a.Equals(b);
            }
            return a == b;
        }
        public static bool Equals(Exam a, Exam b)
        {
            if (a != null && b != null)
            {
                return a.Equals(b);
            }
            return a == b;
        }
    }
}
