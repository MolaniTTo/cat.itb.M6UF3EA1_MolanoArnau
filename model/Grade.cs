using System;

namespace UF3_test.model
{

    public class Grade
    {
        public Date date { get; set; }
        public string grade { get; set; }
        public int score { get; set; }

        public override string ToString()
        {
            return
                "Grade{" +
                "date = '" + date + '\'' +
                ",grade = '" + grade + '\'' +
                ",score = '" + score + '\'' +
                "}";
        }

    }
}
