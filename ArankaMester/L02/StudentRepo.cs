using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace web_api
{
    public static class StudentRepo{
        public static List<Student> Students= new List<Student>(){
         new Student() {id=1, Nume="Popescu", Prenume="Ion", Facultate="AC", An=2},
         new Student() {id=2, Nume="Ioneescu", Prenume="Mara", Facultate="ETC", An=4},
         new Student() {id=3, Nume="Marinescu", Prenume="Ana", Facultate="AC", An=3},
        };
    }
}


    
   