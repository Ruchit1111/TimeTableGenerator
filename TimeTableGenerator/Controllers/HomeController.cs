using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Step1()
    {
        return View(new Step1ViewModel());
    }

    [HttpPost]
    public IActionResult Step1(Step1ViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        return RedirectToAction("Step2", new
        {
            workingDays = model.WorkingDays,
            subjectsPerDay = model.SubjectsPerDay,
            totalSubjects = model.TotalSubjects,
            totalHours = model.TotalHours
        });
    }

    [HttpGet]
    public IActionResult Step2(int workingDays, int subjectsPerDay, int totalSubjects, int totalHours)
    {
        var vm = new Step2ViewModel
        {
            WorkingDays = workingDays,
            SubjectsPerDay = subjectsPerDay,
            TotalSubjects = totalSubjects,
            TotalHours = totalHours
        };

        for (int i = 0; i < totalSubjects; i++)
        {
            vm.Subjects.Add(new SubjectHour());
        }

        return View(vm);
    }

    [HttpPost]
    public IActionResult Step2(Step2ViewModel vm)
    {
        int enteredTotal = vm.Subjects.Sum(s => s.Hours);
        if (enteredTotal != vm.TotalHours)
        {
            ViewData["Error"] = $"Total subject hours ({enteredTotal}) must equal total weekly hours ({vm.TotalHours}).";
            return View(vm);
        }

        var subjectQueue = new Queue<string>();
        foreach (var subject in vm.Subjects)
        {
            for (int i = 0; i < subject.Hours; i++)
            {
                subjectQueue.Enqueue(subject.Name);
            }
        }

        var cells = new List<TimeTableCell>();
        for (int row = 0; row < vm.SubjectsPerDay; row++)
        {
            for (int col = 0; col < vm.WorkingDays; col++)
            {
                if (subjectQueue.Count > 0)
                {
                    cells.Add(new TimeTableCell
                    {
                        Row = row,
                        Col = col,
                        Subject = subjectQueue.Dequeue()
                    });
                }
            }
        }

        vm.Cells = cells;

        return View("Result", vm);
    }
}