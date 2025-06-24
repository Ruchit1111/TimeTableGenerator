using System.ComponentModel.DataAnnotations;

public class Step1ViewModel
{
    [Required]
    [Range(1, 7)]
    public int WorkingDays { get; set; }

    [Required]
    [Range(1, 8)]
    public int SubjectsPerDay { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int TotalSubjects { get; set; }

    public int TotalHours => WorkingDays * SubjectsPerDay;
}