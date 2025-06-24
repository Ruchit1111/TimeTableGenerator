using System.Collections.Generic;

public class Step2ViewModel
{
    public int WorkingDays { get; set; }
    public int SubjectsPerDay { get; set; }
    public int TotalSubjects { get; set; }
    public int TotalHours { get; set; }

    public List<SubjectHour> Subjects { get; set; } = new List<SubjectHour>();
    public List<TimeTableCell> Cells { get; set; } = new List<TimeTableCell>();
}