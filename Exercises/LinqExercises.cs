using LinqConsoleLab.EN.Data;

namespace LinqConsoleLab.EN.Exercises;

public sealed class LinqExercises
{
    /// <summary>
    /// Task:
    /// Find all students who live in Warsaw.
    /// Return the index number, full name, and city.
    ///
    /// SQL:
    /// SELECT IndexNumber, FirstName, LastName, City
    /// FROM Students
    /// WHERE City = 'Warsaw';
    /// </summary>
    public IEnumerable<string> Task01_StudentsFromWarsaw()
    {
        return UniversityData.Students
            .Where(s => s.City == "Warsaw") // filtrele
            .Select(s => $"{s.IndexNumber} {s.FirstName} {s.LastName} {s.City}"); // yazdir
    }

    /// <summary>
    /// Task:
    /// Build a list of all student email addresses.
    /// Use projection so that you do not return whole objects.
    ///
    /// SQL:
    /// SELECT Email
    /// FROM Students;
    /// </summary>
    public IEnumerable<string> Task02_StudentEmailAddresses()
    {
        return UniversityData.Students
            .Select(s => s.Email); // emailler
    }

    /// <summary>
    /// Task:
    /// Sort students alphabetically by last name and then by first name.
    /// Return the index number and full name.
    ///
    /// SQL:
    /// SELECT IndexNumber, FirstName, LastName
    /// FROM Students
    /// ORDER BY LastName, FirstName;
    /// </summary>
    public IEnumerable<string> Task03_StudentsSortedAlphabetically()
    {
        return UniversityData.Students
            .OrderBy(s => s.LastName) // soyisme gore
            .ThenBy(s => s.FirstName) // sonra isme gore
            .Select(s => $"{s.IndexNumber} {s.FirstName} {s.LastName}");
    }

    /// <summary>
    /// Task:
    /// Find the first course from the Analytics category.
    /// If such a course does not exist, return a text message.
    ///
    /// SQL:
    /// SELECT TOP 1 Title, StartDate
    /// FROM Courses
    /// WHERE Category = 'Analytics';
    /// </summary>
    public IEnumerable<string> Task04_FirstAnalyticsCourse()
    {
        var course = UniversityData.Courses
            .FirstOrDefault(c => c.Category == "Analytics"); // ilk analytics

        if (course == null)
            return new[] { "No analytics course found" }; // bulunamadi

        return new[] { $"{course.Title} {course.StartDate}" };
    }

    /// <summary>
    /// Task:
    /// Check whether there is at least one inactive enrollment in the data set.
    /// Return one line with a True/False or Yes/No answer.
    ///
    /// SQL:
    /// SELECT CASE WHEN EXISTS (
    ///     SELECT 1
    ///     FROM Enrollments
    ///     WHERE IsActive = 0
    /// ) THEN 1 ELSE 0 END;
    /// </summary>
    public IEnumerable<string> Task05_IsThereAnyInactiveEnrollment()
    {
        bool result = UniversityData.Enrollments
            .Any(e => !e.IsActive); // inaktif var mi

        return new[] { $"Is there inactive enrollment: {result}" };
    }

    /// <summary>
    /// Task:
    /// Check whether every lecturer has a department assigned.
    /// Use a method that validates the condition for the whole collection.
    ///
    /// SQL:
    /// SELECT CASE WHEN COUNT(*) = COUNT(Department)
    /// THEN 1 ELSE 0 END
    /// FROM Lecturers;
    /// </summary>
    public IEnumerable<string> Task06_DoAllLecturersHaveDepartment()
    {
        bool result = UniversityData.Lecturers
            .All(l => l.Department != null); // hepsinde var mi

        return new[] { $"All lecturers have department: {result}" };
    }

    /// <summary>
    /// Task:
    /// Count how many active enrollments exist in the system.
    ///
    /// SQL:
    /// SELECT COUNT(*)
    /// FROM Enrollments
    /// WHERE IsActive = 1;
    /// </summary>
    public IEnumerable<string> Task07_CountActiveEnrollments()
    {
        int count = UniversityData.Enrollments
            .Count(e => e.IsActive); // say

        return new[] { $"Active enrollments: {count}" };
    }

    /// <summary>
    /// Task:
    /// Return a sorted list of distinct student cities.
    ///
    /// SQL:
    /// SELECT DISTINCT City
    /// FROM Students
    /// ORDER BY City;
    /// </summary>
    public IEnumerable<string> Task08_DistinctStudentCities()
    {
        return UniversityData.Students
            .Select(s => s.City) // sehirler
            .Distinct() // tekrarsiz
            .OrderBy(c => c); // sirala
    }

    /// <summary>
    /// Task:
    /// Return the three newest enrollments.
    /// Show the enrollment date, student identifier, and course identifier.
    ///
    /// SQL:
    /// SELECT TOP 3 EnrollmentDate, StudentId, CourseId
    /// FROM Enrollments
    /// ORDER BY EnrollmentDate DESC;
    /// </summary>
    public IEnumerable<string> Task09_ThreeNewestEnrollments()
    {
        return UniversityData.Enrollments
            .OrderByDescending(e => e.EnrollmentDate) // yeniden eskiye
            .Take(3) // ilk 3
            .Select(e => $"{e.EnrollmentDate} {e.StudentId} {e.CourseId}");
    }

    /// <summary>
    /// Task:
    /// Implement simple pagination for the course list.
    /// Assume a page size of 2 and return the second page of data.
    ///
    /// SQL:
    /// SELECT Title, Category
    /// FROM Courses
    /// ORDER BY Title
    /// OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY;
    /// </summary>
    public IEnumerable<string> Task10_SecondPageOfCourses()
    {
        return UniversityData.Courses
            .OrderBy(c => c.Title) // sirala
            .Skip(2) // ilk 2 atla
            .Take(2) // sonraki 2 al
            .Select(c => $"{c.Title} {c.Category}");
    }

    /// <summary>
    /// Task:
    /// Join students with enrollments by StudentId.
    /// Return the full student name and the enrollment date.
    ///
    /// SQL:
    /// SELECT s.FirstName, s.LastName, e.EnrollmentDate
    /// FROM Students s
    /// JOIN Enrollments e ON s.Id = e.StudentId;
    /// </summary>
    public IEnumerable<string> Task11_JoinStudentsWithEnrollments()
    {
        return UniversityData.Students
            .Join(UniversityData.Enrollments, // birlestir
                s => s.Id,
                e => e.StudentId,
                (s, e) => $"{s.FirstName} {s.LastName} {e.EnrollmentDate}");
    }

    /// <summary>
    /// Task:
    /// Prepare all student-course pairs based on enrollments.
    /// Use an approach that flattens the data into a single result sequence.
    ///
    /// SQL:
    /// SELECT s.FirstName, s.LastName, c.Title
    /// FROM Enrollments e
    /// JOIN Students s ON s.Id = e.StudentId
    /// JOIN Courses c ON c.Id = e.CourseId;
    /// </summary>
    public IEnumerable<string> Task12_StudentCoursePairs()
    {
        return UniversityData.Enrollments
            .SelectMany(e => // duzlestir
                UniversityData.Students
                    .Where(s => s.Id == e.StudentId)
                    .SelectMany(s =>
                        UniversityData.Courses
                            .Where(c => c.Id == e.CourseId)
                            .Select(c => $"{s.FirstName} {s.LastName} {c.Title}")));
    }

    /// <summary>
    /// Task:
    /// Group enrollments by course and return the course title together with the number of enrollments.
    ///
    /// SQL:
    /// SELECT c.Title, COUNT(*)
    /// FROM Enrollments e
    /// JOIN Courses c ON c.Id = e.CourseId
    /// GROUP BY c.Title;
    /// </summary>
    public IEnumerable<string> Task13_GroupEnrollmentsByCourse()
    {
        return UniversityData.Enrollments
            .Join(UniversityData.Courses,
                e => e.CourseId,
                c => c.Id,
                (e, c) => c.Title) // kurs adi al
            .GroupBy(title => title) // grupla
            .Select(g => $"{g.Key} {g.Count()}");
    }

    /// <summary>
    /// Task:
    /// Calculate the average final grade for each course.
    /// Ignore records where the final grade is null.
    ///
    /// SQL:
    /// SELECT c.Title, AVG(e.FinalGrade)
    /// FROM Enrollments e
    /// JOIN Courses c ON c.Id = e.CourseId
    /// WHERE e.FinalGrade IS NOT NULL
    /// GROUP BY c.Title;
    /// </summary>
    public IEnumerable<string> Task14_AverageGradePerCourse()
    {
        return UniversityData.Enrollments
            .Where(e => e.FinalGrade != null) // null atla
            .Join(UniversityData.Courses,
                e => e.CourseId,
                c => c.Id,
                (e, c) => new { c.Title, e.FinalGrade })
            .GroupBy(x => x.Title)
            .Select(g => $"{g.Key} {g.Average(x => x.FinalGrade):F2}");
    }

    /// <summary>
    /// Task:
    /// For each lecturer, count how many courses are assigned to that lecturer.
    /// Return the full lecturer name and the course count.
    ///
    /// SQL:
    /// SELECT l.FirstName, l.LastName, COUNT(c.Id)
    /// FROM Lecturers l
    /// LEFT JOIN Courses c ON c.LecturerId = l.Id
    /// GROUP BY l.FirstName, l.LastName;
    /// </summary>
    public IEnumerable<string> Task15_LecturersAndCourseCounts()
    {
        return UniversityData.Lecturers
            .Select(l => new
            {
                Name = $"{l.FirstName} {l.LastName}",
                Count = UniversityData.Courses.Count(c => c.LecturerId == l.Id) // say
            })
            .Select(x => $"{x.Name} {x.Count}");
    }

    /// <summary>
    /// Task:
    /// For each student, find the highest final grade.
    /// Skip students who do not have any graded enrollment yet.
    ///
    /// SQL:
    /// SELECT s.FirstName, s.LastName, MAX(e.FinalGrade)
    /// FROM Students s
    /// JOIN Enrollments e ON s.Id = e.StudentId
    /// WHERE e.FinalGrade IS NOT NULL
    /// GROUP BY s.FirstName, s.LastName;
    /// </summary>
    public IEnumerable<string> Task16_HighestGradePerStudent()
    {
        return UniversityData.Students
            .Select(s => new
            {
                Name = $"{s.FirstName} {s.LastName}",
                Grades = UniversityData.Enrollments
                    .Where(e => e.StudentId == s.Id && e.FinalGrade != null) // notlu
                    .Select(e => e.FinalGrade)
            })
            .Where(x => x.Grades.Any()) // notu olanlari al
            .Select(x => $"{x.Name} {x.Grades.Max()}");
    }

    /// <summary>
    /// Challenge:
    /// Find students who have more than one active enrollment.
    /// Return the full name and the number of active courses.
    ///
    /// SQL:
    /// SELECT s.FirstName, s.LastName, COUNT(*)
    /// FROM Students s
    /// JOIN Enrollments e ON s.Id = e.StudentId
    /// WHERE e.IsActive = 1
    /// GROUP BY s.FirstName, s.LastName
    /// HAVING COUNT(*) > 1;
    /// </summary>
    public IEnumerable<string> Challenge01_StudentsWithMoreThanOneActiveCourse()
    {
        return UniversityData.Students
            .Select(s => new
            {
                Name = $"{s.FirstName} {s.LastName}",
                Count = UniversityData.Enrollments
                    .Count(e => e.StudentId == s.Id && e.IsActive) // aktif say
            })
            .Where(x => x.Count > 1) // birden fazla
            .Select(x => $"{x.Name} {x.Count}");
    }

    /// <summary>
    /// Challenge:
    /// List the courses that start in April 2026 and do not have any final grades assigned yet.
    ///
    /// SQL:
    /// SELECT c.Title
    /// FROM Courses c
    /// JOIN Enrollments e ON c.Id = e.CourseId
    /// WHERE MONTH(c.StartDate) = 4 AND YEAR(c.StartDate) = 2026
    /// GROUP BY c.Title
    /// HAVING SUM(CASE WHEN e.FinalGrade IS NOT NULL THEN 1 ELSE 0 END) = 0;
    /// </summary>
    public IEnumerable<string> Challenge02_AprilCoursesWithoutFinalGrades()
    {
        return UniversityData.Courses
            .Where(c => c.StartDate.Month == 4 && c.StartDate.Year == 2026) // nisan 2026
            .Where(c => !UniversityData.Enrollments
                .Any(e => e.CourseId == c.Id && e.FinalGrade != null)) // notu yok
            .Select(c => c.Title);
    }

    /// <summary>
    /// Challenge:
    /// Calculate the average final grade for every lecturer across all of their courses.
    /// Ignore missing grades but still keep the lecturers in mind as the reporting dimension.
    ///
    /// SQL:
    /// SELECT l.FirstName, l.LastName, AVG(e.FinalGrade)
    /// FROM Lecturers l
    /// LEFT JOIN Courses c ON c.LecturerId = l.Id
    /// LEFT JOIN Enrollments e ON e.CourseId = c.Id
    /// WHERE e.FinalGrade IS NOT NULL
    /// GROUP BY l.FirstName, l.LastName;
    /// </summary>
    public IEnumerable<string> Challenge03_LecturersAndAverageGradeAcrossTheirCourses()
    {
        return UniversityData.Lecturers
            .Select(l => new
            {
                Name = $"{l.FirstName} {l.LastName}",
                Grades = UniversityData.Courses
                    .Where(c => c.LecturerId == l.Id)
                    .SelectMany(c => UniversityData.Enrollments
                        .Where(e => e.CourseId == c.Id && e.FinalGrade != null)
                        .Select(e => e.FinalGrade)) // notlari topla
            })
            .Where(x => x.Grades.Any()) // notu olanlari al
            .Select(x => $"{x.Name} {x.Grades.Average():F2}");
    }

    /// <summary>
    /// Challenge:
    /// Show student cities and the number of active enrollments created by students from each city.
    /// Sort the result by the active enrollment count in descending order.
    ///
    /// SQL:
    /// SELECT s.City, COUNT(*)
    /// FROM Students s
    /// JOIN Enrollments e ON s.Id = e.StudentId
    /// WHERE e.IsActive = 1
    /// GROUP BY s.City
    /// ORDER BY COUNT(*) DESC;
    /// </summary>
    public IEnumerable<string> Challenge04_CitiesAndActiveEnrollmentCounts()
    {
        return UniversityData.Students
            .GroupBy(s => s.City) // sehire gore grupla
            .Select(g => new
            {
                City = g.Key,
                Count = g.SelectMany(s => UniversityData.Enrollments
                    .Where(e => e.StudentId == s.Id && e.IsActive)) // aktif say
                    .Count()
            })
            .OrderByDescending(x => x.Count) // azalan
            .Select(x => $"{x.City} {x.Count}");
    }

    private static NotImplementedException NotImplemented(string methodName)
    {
        return new NotImplementedException(
            $"Complete method {methodName} in Exercises/LinqExercises.cs and run the command again.");
    }
}