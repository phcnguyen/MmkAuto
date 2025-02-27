﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auto.Common.Entities.Employees;

/// <summary>
/// Lớp đại diện cho nhân viên.
/// </summary>
[Table(nameof(Employee))]
public class Employee
{
    private DateTime? _endDate;
    private DateTime _startDate = DateTime.UtcNow;

    /// <summary>
    /// Mã nhân viên.
    /// </summary>
    [Key]
    public int EmployeeId { get; set; }

    /// <summary>
    /// Tên nhân viên.
    /// </summary>
    [Required(ErrorMessage = "Employee name is required.")]
    [MaxLength(50)]
    public string Name { get; set; }

    /// <summary>
    /// Giới tính.
    /// </summary>
    public Gender Gender { get; set; } = Gender.None;

    /// <summary>
    /// Ngày sinh.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Địa chỉ nhân viên.
    /// </summary>
    [MaxLength(200)]
    public string Address { get; set; }

    /// <summary>
    /// Số điện thoại nhân viên.
    /// </summary>
    [MaxLength(14)]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Email nhân viên.
    /// </summary>
    [MaxLength(50)]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Chức vụ.
    /// </summary>
    public Position Position { get; set; } = Position.None;

    /// <summary>
    /// Ngày bắt đầu làm việc.
    /// </summary>
    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (EndDate.HasValue && value > EndDate.Value)
                throw new ArgumentException("Start date cannot be later than end date.");
            _startDate = value;
        }
    }

    /// <summary>
    /// Ngày kết thúc hợp đồng.
    /// </summary>
    public DateTime? EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            UpdateStatus();
        }
    }

    /// <summary>
    /// Trạng thái công việc.
    /// </summary>
    public EmploymentStatus Status { get; set; } = EmploymentStatus.None;

    /// <summary>
    /// Cập nhật trạng thái công việc.
    /// </summary>
    public void UpdateStatus()
    {
        if (EndDate.HasValue && EndDate.Value < DateTime.Now)
        {
            Status = EmploymentStatus.Inactive;
        }
    }
}