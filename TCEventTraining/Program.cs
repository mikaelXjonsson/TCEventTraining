using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCEventTraining
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CollegeClassModel history = new CollegeClassModel("History 101", 3);
            CollegeClassModel math = new CollegeClassModel("Caculus 201", 2);

            // Ett event består av tre delar. Först själva deklarationen av eventhandlerna
            // Sedan har vi lyssnaren som står standby och agerar om den hör att eventet ropar.
            // Slutligen har vi invokationen av eventet, det som triggar det.

            // Genom att här använda oss av eventet vi skapade i andra klassen
            // så lyssnar vi efter det. TC beskriver det som att den andra klassen,
            // CollegeClassModel i det här fallet, skriker och här så lyssnar vi efter
            // vad som har hänt.
            // När vi då skriver += så kommer ett förslag på att implementera en metod
            // genom att trycka tab. Då skapas en metod för eventet
            history.EnrollmentFull += History_EnrollmentFull;

            history.SignUpStudent("Mike").PrintToConsole();
            history.SignUpStudent("Tim").PrintToConsole();
            history.SignUpStudent("Sue").PrintToConsole();
            history.SignUpStudent("Mary").PrintToConsole();
            history.SignUpStudent("John").PrintToConsole();

            math.SignUpStudent("Tim").PrintToConsole();
            math.SignUpStudent("Sue").PrintToConsole();
            math.SignUpStudent("Mary").PrintToConsole();

            Console.ReadLine();
        }

        // Metoden som skapas vid +=. Den är private men kan ändå kallas på från en 
        // annan klass så det är lite trippy men så ska det vara.
        // Den tar in ett objekt, vilket gör att den kan ta in vad som helst bara vi castar om det. 
        // string e kommer från att det var av typen string som vi skapade eventet utifrån
        // Detta är själva lyssnaren för eventet (som wires up med hjälp av det på rad 22
        private static void History_EnrollmentFull(object sender, string e)
        {
            // Ett exempel på ett meddelande man ska låta skrivas ut. Vi vet att det är
            // från history det kommer eftersom det är på instansieringen av history
            // som vi fäste det här eventet.
            Console.WriteLine();
            Console.WriteLine(e);
            Console.WriteLine();
        }
    }

    public static class ConsolHelpers
    {
        public static void PrintToConsole(this string message)
        {
            Console.WriteLine(message);
        }
    }

    public class CollegeClassModel
    {
        // Skapar här ett event som andra klasser kan lyssna på.
        // Detta är själva deklarationen av EventHandler "the glue in the middle"
        public event EventHandler<string> EnrollmentFull;

        private List<string> enrolledStudents = new List<string>();
        private List<string> waitingList = new List<string>();

        public string CourseTitle { get; private set; }
        public int  MaximumStudents { get; private set; }

        public CollegeClassModel(string title, int maximumStudents)
        {
            CourseTitle = title;
            MaximumStudents = maximumStudents;
        }

        public string SignUpStudent(string studentName)
        {
            string output = "";
            if (enrolledStudents.Count < MaximumStudents)
            {
                enrolledStudents.Add(studentName);
                output = $"{studentName} was enrolled to {CourseTitle}";
                //check to see if we are maxed out

                if (enrolledStudents.Count == MaximumStudents)
                {
                    // Här kommer nu nästa del av eventet och det är vad som utlöser det.
                    // Om det inte är nån som lyssnar så vill vi inte att det ska kastas ett
                    // exception så därför sätts det ett frågetecken efter eventet. Frågetecknet
                    // gör "if its not null, do the next thing". Är den null, ignorera det här
                    // Invoke vill ha en inparameter och det är i det här fallet sändaren. Är man inne i en 
                    // instans av en klass och vill skicka med den så räcker det att man skriver "this"

                    // Vad som ska in i Invoke() styrs av när vi skapade eventet. 
                    EnrollmentFull?.Invoke(this, "The enrollment is full for history. Det här kommer från invokationet");
                }
                
            }
            else
            {
                waitingList.Add(studentName);
                output = $"{studentName} was added to waitinglist in {CourseTitle}";
                               
            }
            return output;
        }
    }
}
