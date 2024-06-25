﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

[Serializable]
public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }

    public string ImagePath { get; set; }
    public string TimePeriod { get; set; }
    public string PriceDOO { get; set; }
    public string LaborCategory { get; set; }
    public string FullName => $"{FirstName} {LastName}";

    [Required(ErrorMessage = "EGN is required")]
    public int EGN { get; set; }

    [Required(ErrorMessage = "Employee Name is required.")]
    public string EmployeeName => $"{FirstName} {LastName}";

    [Required(ErrorMessage = "Remaining Leave Days is required")]
    public int RemainingLeaveDays { get; set; }

    [Required(ErrorMessage = "Job Title is required")]
    public string JobTitle { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Internship { get; set; }
    public string InternshipPeriod { get; set; }
    public string InsuranceInternship { get; set; }
    public string ProfessionalInternship { get; set; }
    public string TypeOfInsurance { get; set; }
    public string Salary { get; set; }
    public string Currency { get; set; }
    public string CurrencyDOO { get; set; }
    public string CurrencyZO { get; set; }
    public string IBAN { get; set; }
    public string BIC { get; set; }
    public string Bank { get; set; }


    [Required(ErrorMessage = "Department is required")]
    public string Department { get; set; }

    public string FromDate { get; set; }
    public string ToDate { get; set; }
    public string NumberAndSeries { get; set; }
    public string Reason { get; set; }
    public bool IsArchived { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    //[Timestamp]
    //public byte[] NewRowVersion { get; set; }

    public bool PaidLeave { get; set; }
    public bool UnpaidLeave { get; set; }
    public bool OtherLeave { get; set; }
    public List<DateTime> PaidLeaveDates { get; set; } = new List<DateTime>();
    public List<DateTime> UnpaidLeaveDates { get; set; } = new List<DateTime>();
    public List<DateTime> OtherLeaveDates { get; set; } = new List<DateTime>();
    public int RemainingPaidLeaveDays { get; set; }
    public int RemainingUnpaidLeaveDays { get; set; }
    public int RemainingOtherLeaveDays { get; set; }

    public int GetTotalPaidLeaveDays()
    {
        return PaidLeaveDates.Count;
    }

    public int GetTotalUnpaidLeaveDays()
    {
        return UnpaidLeaveDates.Count;
    }

    public int GetTotalOtherLeaveDays()
    {
        return OtherLeaveDates.Count;
    }
    public List<Employee> Employees { get; set; }

}
