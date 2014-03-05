﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AiS.Models
{
    public class Teacher : IEquatable<Teacher>
    {
        private IList<Subject> subjects;

        public string ID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string TitleSuffix { get; set; }
        public IList<Subject> Teaches
        {
            get
            {
                if (subjects == null)
                    subjects = new List<Subject>();
                return subjects;
            }
            set { subjects = value; }
        }

        public bool Equals(Teacher other)
        {
            return other != null && other.ID == ID;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(obj as Teacher);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public Teacher Clone()
        {
            return new Teacher { ID = ID, Title = Title, Name = Name, Lastname = Lastname, TitleSuffix = TitleSuffix, Teaches = Teaches.Select(x => x.Clone()).ToList() };
        }
    }
}
