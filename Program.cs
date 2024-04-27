using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
    public string Major { get; set; }
    public double Tuition { get; set; }
}

public class StudentClubs
{
    public int StudentID { get; set; }
    public string ClubName { get; set; }
}

public class StudentGPA
{
    public int StudentID { get; set; }
    public double GPA { get; set; }
}

class Program
{
    static void Main()
    {
        IList<Student> studentList = new List<Student>() {
            new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
            new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
            new Student() { StudentID = 3, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
            new Student() { StudentID = 4, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
            new Student() { StudentID = 5, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
            new Student() { StudentID = 6, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
            new Student() { StudentID = 7, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
        };

        IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
            new StudentGPA() { StudentID = 1,  GPA=4.0} ,
            new StudentGPA() { StudentID = 2,  GPA=3.5} ,
            new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
            new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
            new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
            new StudentGPA() { StudentID = 6,  GPA=2.5} ,
            new StudentGPA() { StudentID = 7,  GPA=1.0 }
        };

        IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
        };

        var gpaGroup = studentGPAList.GroupBy(g => g.GPA)
            .Select(group => new { GPA = group.Key, Students = group.Select(x => x.StudentID) });

        Console.WriteLine("Group by GPA:");
        foreach (var group in gpaGroup)
        {
            Console.WriteLine($"GPA: {group.GPA}, Student IDs: {string.Join(", ", group.Students)}");
        }

        var clubGroup = studentClubList.OrderBy(c => c.ClubName)
            .GroupBy(c => c.ClubName)
            .Select(group => new { Club = group.Key, Students = group.Select(x => x.StudentID) });

        Console.WriteLine("\nSorted and grouped by Club:");
        foreach (var group in clubGroup)
        {
            Console.WriteLine($"Club: {group.Club}, Student IDs: {string.Join(", ", group.Students)}");
        }

        var countGPA = studentGPAList.Count(g => g.GPA >= 2.5 && g.GPA <= 4.0);
        Console.WriteLine($"\nNumber of students with GPA between 2.5 and 4.0: {countGPA}");

        var averageTuition = studentList.Average(s => s.Tuition);
        Console.WriteLine($"\nAverage tuition: {averageTuition:C}");

        var highestTuition = studentList.Max(s => s.Tuition);
        var highestPayingStudents = studentList.Where(s => s.Tuition == highestTuition);

        Console.WriteLine("\nStudent(s) paying the most tuition:");
        foreach (var student in highestPayingStudents)
        {
            Console.WriteLine($"{student.StudentName}, Major: {student.Major}, Tuition: {student.Tuition:C}");
        }

        var studentsWithGPA = studentList.Join(studentGPAList,
                                               s => s.StudentID,
                                               g => g.StudentID,
                                               (s, g) => new { s.StudentName, s.Major, g.GPA });

        Console.WriteLine("\nStudents with their GPA:");
        foreach (var student in studentsWithGPA)
        {
            Console.WriteLine($"Name: {student.StudentName}, Major: {student.Major}, GPA: {student.GPA}");
        }
        var gameClubStudents = studentList.Join(studentClubList.Where(c => c.ClubName == "Game"),
                                                s => s.StudentID,
                                                c => c.StudentID,
                                                (s, c) => s.StudentName)
                                          .Distinct();

        Console.WriteLine("\nStudents in the Game club:");
        foreach (var name in gameClubStudents)
        {
            Console.WriteLine(name);
        }
    }
}